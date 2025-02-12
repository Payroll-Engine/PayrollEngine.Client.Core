using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PayrollEngine.Client.Command;

/// <summary>Represents the environment command line arguments</summary>
public class CommandLineParser(string[] commandLineArgs)
{
    private string[] CommandLineArgs { get; } = commandLineArgs ?? throw new ArgumentNullException(nameof(commandLineArgs));

    /// <summary>Get the command line arguments count, excluding the implicit parameter at index 0</summary>
    public int ParameterCount =>
        Count - 1;

    /// <summary>Get the command line arguments count</summary>
    public int Count =>
        CommandLineArgs.Length;

    /// <summary>Determines whether the specified argument exists</summary>
    /// <param name="name">The argument</param>
    /// <returns>True if the specified argument exists</returns>
    private bool Contains(string name) =>
        CommandLineArgs.FirstOrDefault(x => string.Equals(name, x, StringComparison.InvariantCultureIgnoreCase)) != null;

    /// <summary>Determines whether the specified argument exists</summary>
    /// <returns>True if the specified toggle exists</returns>
    public bool IsValidOrder()
    {
        var arguments = CommandLineArgs;
        int? firstToggleIndex = null;
        for (var i = 0; i < arguments.Length; i++)
        {
            var argument = arguments[i];
            if (IsToggleArgument(argument))
            {
                firstToggleIndex ??= i;
            }
            else if (i > firstToggleIndex)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>Determines whether the specified argument exists</summary>
    /// <param name="toggleValue">The toggle value</param>
    /// <returns>True if the specified toggle exists</returns>
    private bool ContainsToggle(string toggleValue)
    {
        if (IsToggleArgument(toggleValue))
        {
            return Contains(toggleValue);
        }
        return Contains($"/{toggleValue}") || Contains($"-{toggleValue}");
    }

    /// <summary>Determines whether the specified argument is a toggle</summary>
    /// <param name="argument">The argument</param>
    /// <returns>True if the specified argument exists</returns>
    private bool IsToggleArgument(string argument)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            return false;
        }
        return argument.StartsWith('/') || argument.StartsWith('-');
    }

    /// <summary>Gets the specified index, starting at 1</summary>
    /// <param name="index">The argument index</param>
    /// <param name="memberName">The caller name</param>
    /// <param name="allowToggle">Argument can be a toggle</param>
    /// <returns>The argument value</returns>
    public string GetMember(int index, [CallerMemberName] string memberName = "", bool allowToggle = false) =>
        Get(index, memberName, allowToggle);

    /// <summary>Gets the specified index, starting at 1</summary>
    /// <param name="index">The argument index</param>
    /// <param name="name">The argument name (optional)</param>
    /// <param name="allowToggle">Argument can be a toggle (optional)</param>
    /// <returns>The argument value</returns>
    public string Get(int index, string name = null, bool allowToggle = false)
    {
        if (index <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if (CommandLineArgs.Length <= index || index >= CommandLineArgs.Length)
        {
            return null;
        }

        // named argument
        if (!string.IsNullOrWhiteSpace(name))
        {
            var value = GetByName(name);
            if (value != null)
            {
                return value;
            }
        }

        var arg = CommandLineArgs[index];

        // name
        if (name != null)
        {
            var namedParameter = arg.IndexOf(':') > 0;
            // ignore other named parameter
            if (namedParameter || !allowToggle && IsToggleArgument(arg))
            {
                arg = null;
            }
        }

        return arg;
    }

    /// <summary>Get argument by name</summary>
    /// <param name="name">The argument name (optional)</param>
    /// <returns>The argument value</returns>
    public string GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(nameof(name));
        }
        var marker = $"{name}:";
        foreach (var cmdLineArg in CommandLineArgs)
        {
            if (cmdLineArg.StartsWith(marker, StringComparison.InvariantCultureIgnoreCase))
            {
                return cmdLineArg.Substring(marker.Length);
            }
        }
        return null;
    }

    /// <summary>Gets an integer argument</summary>
    /// <param name="index">The argument index</param>
    /// <param name="name">The argument name (optional)</param>
    /// <returns>The argument value</returns>
    public int? GetInt(int index, string name = null)
    {
        if (index <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        var arg = Get(index, name);
        if (!string.IsNullOrWhiteSpace(arg) &&
            int.TryParse(arg, out var value))
        {
            return value;
        }
        return null;
    }

    /// <summary>Gets an integer argument</summary>
    /// <param name="index">The argument index</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="name">The argument name (optional)</param>
    /// <returns>The argument value</returns>
    public int GetInt(int index, int defaultValue, string name = null)
    {
        var arg = Get(index, name);
        if (!string.IsNullOrWhiteSpace(arg) &&
            int.TryParse(arg, out var value))
        {
            return value;
        }
        return defaultValue;
    }

    /// <summary>Gets a mandatory enum argument</summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="index">The argument index</param>
    /// <param name="name">The argument name (optional)</param>
    /// <returns>The argument value</returns>
    public T GetEnum<T>(int index, string name = null)
    {
        var enumType = typeof(T);
        if (!enumType.IsEnum)
        {
            throw new ArgumentException("Argument must an enum.");
        }
        var arg = Get(index, name);
        if (string.IsNullOrWhiteSpace(arg))
        {
            throw new ArgumentException($"Missing argument at position {index}.", nameof(index));

        }
        if (string.IsNullOrWhiteSpace(arg))
        {
            return default;
        }
        if (!Enum.TryParse(typeof(T), arg, true, out var value))
        {
            throw new ArgumentOutOfRangeException($"Unknown argument {arg} at position {index}.", nameof(index));
        }
        return (T)value;
    }

    /// <summary>Gets an enum argument</summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="index">The argument index</param>
    /// <param name="name">The argument name (optional)</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The argument value</returns>
    public T GetEnum<T>(int index, T defaultValue, string name = null)
    {
        var enumType = typeof(T);
        if (!enumType.IsEnum)
        {
            throw new ArgumentException("Argument must an enum.");
        }
        var arg = Get(index, name);
        if (!string.IsNullOrWhiteSpace(arg) &&
            Enum.TryParse(typeof(T), arg, true, out var value))
        {
            return (T)value;
        }
        return defaultValue;
    }

    /// <summary>Gets an enum toggle is present: /toggle or -toggle</summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <returns>The argument value</returns>
    private T? GetEnumToggle<T>() where T : struct, Enum
    {
        var enumType = typeof(T);
        if (!enumType.IsEnum)
        {
            throw new ArgumentException("Argument must an enum.");
        }
        foreach (var enumValue in Enum.GetValues<T>().ToList())
        {
            if (ContainsToggle(Enum.GetName(enumValue)))
            {
                return enumValue;
            }
        }
        return null;
    }

    /// <summary>Gets all toggle with fallback value</summary>
    /// <returns>The argument value</returns>
    public IEnumerable<string> GetToggles() =>
        CommandLineArgs.Where(IsToggleArgument);

    /// <summary>Gets a toggle with fallback value</summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The argument value</returns>
    public T GetEnumToggle<T>(T defaultValue) where T : struct, Enum =>
        GetEnumToggle<T>() ?? defaultValue;

    /// <summary>Test for unknown toggle arguments</summary>
    /// <param name="enumTypes">The supported enum types</param>
    /// <returns>The unknown argument</returns>
    public string TestUnknownToggles(IEnumerable<Type> enumTypes)
    {
        var enumTypesArray = enumTypes.ToArray();
        // test for unknown toggles
        for (var i = 1; i < CommandLineArgs.Length; i++)
        {
            // command line argument
            var arg = CommandLineArgs[i];
            if (!IsToggleArgument(arg))
            {
                continue;
            }

            var knownToggle = false;
            var toggleName = arg.Substring(1);
            foreach (var enumType in enumTypesArray)
            {
                var enumNames = Enum.GetNames(enumType).ToList();
                if (!enumNames.Any(x => string.Equals(x, toggleName, StringComparison.InvariantCultureIgnoreCase)))
                {
                    // unknown toggle
                    continue;
                }
                knownToggle = true;
                break;
            }
            if (!knownToggle)
            {
                return arg;
            }
        }
        return null;
    }

    /// <summary>Test for multiple toggles</summary>
    /// <param name="enumTypes">The supported enum types</param>
    /// <returns>The failed type</returns>
    public Type TestMultipleToggles(IEnumerable<Type> enumTypes)
    {
        foreach (var enumType in enumTypes)
        {
            if (Enum.GetNames(enumType).Count(ContainsToggle) > 1)
            {
                return enumType;
            }
        }
        return null;
    }

    /// <summary>
    /// Get all arguments.
    /// </summary>
    public string[] GetArguments()
    {
        if (commandLineArgs.Length <= 2)
        {
            return null;
        }
        return CommandLineArgs.Skip(2).ToArray();
    }

    #region Static

    /// <summary>New command line parser from environment command line arguments</summary>
    public static CommandLineParser NewFromEnvironment() =>
        new(Environment.GetCommandLineArgs());

    /// <summary>New command line parser from command string</summary>
    public static CommandLineParser NewFromCommand(string command) =>
        new(SplitCommandArguments(command));

    /// <summary>Split command string into parameters</summary>
    /// <remarks>see https://stackoverflow.com/a/66450199</remarks>
    private static string[] SplitCommandArguments(string command)
    {
        if (string.IsNullOrWhiteSpace(command))
        {
            throw new ArgumentException(nameof(command));
        }

        var result = new List<string> {
            // copy first parameter from the environment to keep the same array indexes
            Environment.GetCommandLineArgs()[0]
        };
        if (string.IsNullOrWhiteSpace(command))
        {
            return result.ToArray();
        }

        var text = string.Empty;
        var doubleQuote = false;
        var singleQuote = false;

        var index = 0;
        while (index < command.Length)
        {
            if (command[index] == ' ' && !doubleQuote && !singleQuote)
            {
                if (!string.IsNullOrWhiteSpace(text.Trim()))
                {
                    result.Add(text.Trim());
                }
                text = string.Empty;
            }

            if (command[index] == '"')
            {
                doubleQuote = !doubleQuote;
            }

            if (command[index] == '\'')
            {
                singleQuote = !singleQuote;
            }
            text += command[index];
            index++;
        }

        if (!string.IsNullOrWhiteSpace(text.Trim()))
        {
            result.Add(text.Trim());
        }
        return result.ToArray();
    }

    #endregion

}