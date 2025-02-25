using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using PayrollEngine.IO;

namespace PayrollEngine.Client.Command;

/// <summary>
/// Command manager
/// </summary>
public class CommandManager(ICommandConsole console, ILogger logger = null)
{
    private readonly Dictionary<string, ICommand> commands = new();
    private ICommandConsole Console { get; } = console ?? throw new ArgumentNullException(nameof(console));
    private ILogger Logger { get; } = logger;

    /// <summary>
    /// Name fo the help command (default: Help)
    /// </summary>
    private string HelpCommandName => "Help";

    /// <summary>
    /// Extension for command files (default: <see cref="FileExtensions.PayrollEngineCommand"/>)
    /// </summary>
    private string CommandFileExtension => FileExtensions.PayrollEngineCommand;

    /// <summary>
    /// Register assembly commands
    /// </summary>
    /// <param name="assembly">Source assembly</param>
    public void RegisterAssembly(Assembly assembly)
    {
        if (assembly == null)
        {
            throw new ArgumentNullException(nameof(assembly));
        }

        foreach (var type in assembly.GetTypes())
        {
            if (type.IsAbstract)
            {
                continue;
            }

            // command marker
            var attribute = type.GetCustomAttribute(typeof(CommandAttribute)) as CommandAttribute;
            if (attribute == null)
            {
                continue;
            }

            // command instance
            var cmd = Activator.CreateInstance(type) as ICommand;
            if (cmd == null)
            {
                throw new PayrollException($"Invalid command {type.FullName}.");
            }
            commands.Add(attribute.Name, cmd);
        }
    }

    /// <summary>
    /// Test for any command
    /// </summary>
    public bool HasCommands => commands.Any();

    /// <summary>
    /// List of available commands
    /// </summary>
    public List<ICommand> GetCommands() =>
        commands.Values.ToList();

    /// <summary>
    /// Get command by name
    /// </summary>
    /// <param name="name">Command name</param>
    public ICommand GetCommand(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(nameof(name));
        }

        foreach (var command in commands)
        {
            if (string.Equals(command.Key, name, StringComparison.InvariantCultureIgnoreCase))
            {
                return command.Value;
            }
        }
        return null;
    }

    /// <summary>
    /// Get command line command
    /// </summary>
    public ICommand GetCommandLineCommand()
    {
        var parser = CommandLineParser.NewFromEnvironment();
        if (parser.Count == 0)
        {
            return null;
        }

        var argCommand = parser.Get(1);
        if (string.IsNullOrWhiteSpace(argCommand))
        {
            return null;
        }

        return GetCommand(argCommand);
    }

    /// <summary>
    /// Execute command using the environment command line arguments
    /// </summary>
    /// <returns>Exit code</returns>
    public async Task<int> ExecuteAsync(PayrollHttpClient httpClient = null)
    {
        var parser = CommandLineParser.NewFromEnvironment();
        if (parser.Count == 0)
        {
            return -1;
        }

        // command
        ICommand command = null;

        // first argument: command name or command file name
        var argCommand = parser.Get(1);
        if (string.IsNullOrWhiteSpace(argCommand))
        {
            // missing command: show app help
            if (!string.IsNullOrWhiteSpace(HelpCommandName))
            {
                command = GetCommand(HelpCommandName);
            }
        }
        else
        {
            // command file
            if (!string.IsNullOrWhiteSpace(argCommand) && IsCommandFile(argCommand))
            {
                return await ExecuteFileAsync(parser, httpClient);
            }

            // single command using the environment command line parser
            command = !string.IsNullOrWhiteSpace(argCommand) ? GetCommand(argCommand) : null;
            // unknown command
            if (command == null)
            {
                throw new PayrollException($"Unknown command or command file {argCommand}");
            }
        }
        if (command == null)
        {
            return -1;
        }
        return await ExecuteAsync(command, parser, httpClient);
    }

    /// <summary>
    /// Execute specific command using the environment command line arguments
    /// </summary>
    /// <returns>Exit code</returns>
    public async Task<int> ExecuteAsync(ICommand command, PayrollHttpClient httpClient = null) =>
        await ExecuteAsync(command, CommandLineParser.NewFromEnvironment(), httpClient);

    /// <summary>
    /// Execute specific command using a custom command line parser
    /// </summary>
    /// <returns>Exit code</returns>
    private async Task<int> ExecuteAsync(ICommand command, CommandLineParser commandLineParser,
        PayrollHttpClient httpClient = null)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }
        if (!commands.Values.Contains(command))
        {
            throw new ArgumentException(nameof(command));
        }
        if (commandLineParser == null)
        {
            throw new ArgumentNullException(nameof(commandLineParser));
        }

        // store toggles
        var displayMode = Console.DisplayLevel;
        var errorMode = Console.ErrorMode;

        try
        {
            // global parameters
            Console.DisplayLevel = commandLineParser.GetEnumToggle(Console.DisplayLevel);
            Console.ErrorMode = commandLineParser.GetEnumToggle(Console.ErrorMode);
            Console.WaitMode = commandLineParser.GetEnumToggle(Console.WaitMode);

            // command parameters
            var parameters = command.GetParameters(commandLineParser);

            // execute
            if (Logger != null && Logger.IsEnabled(LogLevel.Verbose))
            {
                var arguments = commandLineParser.GetArguments();
                Logger.Trace(arguments == null
                    ? $"Executing {command}"
                    : $"Executing {command}: {string.Join(' ', commandLineParser.GetArguments())}");
            }

            var context = new CommandContext(
                commandManager: this,
                console: Console,
                logger: Logger,
                httpClient: httpClient,
                displayLevel: Console.DisplayLevel);
            var result = await command.ExecuteAsync(context, parameters);
            Logger?.Trace($"{command} result: {result}");
            return result;
        }
        catch (Exception exception)
        {
            Logger?.Error(exception, exception.GetBaseException().Message);
        }
        finally
        {
            // restore global parameters
            Console.DisplayLevel = displayMode;
            Console.ErrorMode = errorMode;
        }

        return -1;
    }

    #region Command File

    private class FileItem(string text, ICommand command, CommandLineParser parser)
    {
        public FileItem(string text, CommandLineParser parser, List<FileItem> children) :
            this(text, null, parser)
        {
            Parser = parser;
            Children = children;
        }

        internal string Text { get; } = text;
        public ICommand Command { get; } = command;
        public CommandLineParser Parser { get; } = parser;
        internal List<FileItem> Children { get; } = [];
    }

    private async Task<int> ExecuteFileAsync(CommandLineParser parser, PayrollHttpClient httpClient = null)
    {
        if (parser == null)
        {
            throw new ArgumentNullException(nameof(parser));
        }

        var items = ReadCommandFile(parser);
        if (items == null)
        {
            return -2;
        }

        // command file default toggle values
        Console.DisplayLevel = parser.GetEnumToggle(Console.DisplayLevel);
        Console.ErrorMode = parser.GetEnumToggle(Console.ErrorMode);

        foreach (var item in items)
        {
            var exitCode = await ExecuteFileItemAsync(item, httpClient);
            if (exitCode != 0)
            {
                return exitCode;
            }
        }

        // command file final wait overwrite
        Console.WaitMode = parser.GetEnumToggle(Console.WaitMode);

        return 0;
    }

    private async Task<int> ExecuteFileItemAsync(FileItem item, PayrollHttpClient httpClient = null)
    {
        // command
        if (item.Command != null)
        {
            return await ExecuteAsync(item.Command, item.Parser, httpClient);
        }

        // command file
        // working dir
        var currentDirectory = Directory.GetCurrentDirectory();
        if (item.Parser.GetEnumToggle(PathChangeMode.ChangePath) == PathChangeMode.ChangePath)
        {
            var directory = new FileInfo(item.Text).DirectoryName;
            EnsureCurrentDirectory(directory);
        }

        // display
        var displayMode = Console.DisplayLevel;
        var errorMode = Console.ErrorMode;
        if (item.Parser != null)
        {
            Console.DisplayLevel = item.Parser.GetEnumToggle(displayMode);
            Console.ErrorMode = item.Parser.GetEnumToggle(errorMode);
        }

        // process children
        foreach (var children in item.Children)
        {
            var result = await ExecuteFileItemAsync(children, httpClient);
            if (result != 0)
            {
                return result;
            }
        }

        // restore values
        EnsureCurrentDirectory(currentDirectory);
        Console.DisplayLevel = displayMode;
        Console.ErrorMode = errorMode;

        return 0;
    }

    private List<FileItem> ReadCommandFile(CommandLineParser fileParser)
    {
        // test file
        var fileName = fileParser.Get(1);
        if (!IsCommandFile(fileName))
        {
            return null;
        }

        var info = new FileInfo(fileName);
        var fileBaseName = info.Name;

        // ensure working dir
        var startDirectory = Directory.GetCurrentDirectory();
        EnsureCurrentDirectory(info.DirectoryName);
        if (!File.Exists(fileBaseName))
        {
            Logger?.Warning($"Missing command file {info.FullName}");
            return null;
        }

        // read commands
        var fileItems = new List<FileItem>();
        var lines = File.ReadAllLines(fileBaseName).Select(x => x.Trim()).ToList();
        Logger?.Trace($"Parsing command file {fileBaseName} ({lines.Count} lines)");

        var error = false;
        foreach (var line in lines)
        {
            // empty or comment
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
            {
                continue;
            }

            var parsedLine = ParseParameters(fileParser, line);

            var lineParser = CommandLineParser.NewFromCommand(parsedLine);
            var name = lineParser.Get(1);

            // command file
            if (IsCommandFile(name))
            {
                Logger?.Trace($"Command file {fileName} command file: {parsedLine}");
                var commandFileItems = ReadCommandFile(lineParser);

                // restore working dir
                EnsureCurrentDirectory(info.DirectoryName);

                // add file item
                if (commandFileItems != null)
                {
                    var fileItem = new FileItem(parsedLine, lineParser, commandFileItems);
                    fileItems.Add(fileItem);
                }
                continue;
            }

            // command
            // first argument: command name or command file name
            if (string.IsNullOrWhiteSpace(name))
            {
                Logger?.Warning($"Command file {fileName} error: invalid line {parsedLine}");
                Console.DisplayErrorLine($"Command file {fileName} error: invalid line {parsedLine}");
                error = true;
                break;
            }
            var command = GetCommand(name);
            if (command == null)
            {
                Logger?.Warning($"Command file {fileName} error: unknown command {name}");
                Console.DisplayErrorLine($"Command file {fileName} error: unknown command {name}");
                error = true;
                break;
            }

            // test parameters
            var parameters = command.GetParameters(lineParser);
            var test = parameters.Test();
            if (!string.IsNullOrWhiteSpace(test))
            {
                Logger?.Warning($"Command file {fileName} error: invalid parameters for command {name}: {parsedLine}");
                Console.DisplayErrorLine($"Command file {fileName} error: invalid parameters for command {name}: {parsedLine}");
                error = true;
                break;
            }

            // command
            Logger?.Trace($"Command file {fileName} command: {parsedLine}");
            fileItems.Add(new FileItem(parsedLine, command, lineParser));
        }

        // restore directory
        EnsureCurrentDirectory(startDirectory);

        return !error && fileItems.Any() ? fileItems : null;
    }

    private static string ParseParameters(CommandLineParser parser, string line)
    {
        // variables
        char indicator = '$';
        var regex = new Regex($"[{indicator}][\\S]+{indicator}");
        var variables = regex.Matches(line).Select(x => x.Value.Trim(indicator)).ToList();
        foreach (var variable in variables)
        {
            var marker = $"{indicator}{variable}{indicator}";
            var value = parser.GetByName(variable);
            line = line.Replace(marker, value);
        }

        return line;
    }

    private bool IsCommandFile(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName) || !File.Exists(fileName))
        {
            return false;
        }

        var info = new FileInfo(fileName);
        return CommandFileExtension.Equals(info.Extension, StringComparison.InvariantCultureIgnoreCase);
    }

    private void EnsureCurrentDirectory(string directory)
    {
        if (string.IsNullOrWhiteSpace(directory))
        {
            return;
        }

        // unknown directory
        if (!Directory.Exists(directory))
        {
            throw new PayrollException($"Invalid command file directory {directory}.");
        }

        // no dir change
        var currentDirectory = Directory.GetCurrentDirectory();
        if (string.Equals(currentDirectory, directory))
        {
            return;
        }

        Logger?.Trace($"New working directory {directory}");
        Directory.SetCurrentDirectory(directory);
    }

    #endregion

}