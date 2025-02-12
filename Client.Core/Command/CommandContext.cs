using System;

namespace PayrollEngine.Client.Command;

/// <summary>
/// Command context.
/// </summary>
public class CommandContext
{
    /// <summary>
    /// Command context.
    /// </summary>
    /// <param name="commandManager">Command manager.</param>
    /// <param name="console">Command console.</param>
    /// <param name="displayLevel">Command display level.</param>
    public CommandContext(CommandManager commandManager, ICommandConsole console, DisplayLevel displayLevel = DisplayLevel.Full)
    {
        CommandManager = commandManager ?? throw new ArgumentNullException(nameof(commandManager));
        Console = console ?? throw new ArgumentNullException(nameof(console));
        DisplayLevel = displayLevel;
    }

    /// <summary>
    /// Command context.
    /// </summary>
    /// <param name="commandManager">Command manager.</param>
    /// <param name="httpClient">Http client.</param>
    /// <param name="logger">Command logger.</param>
    /// <param name="console">Command console.</param>
    /// <param name="displayLevel">Command display level.</param>
    public CommandContext(CommandManager commandManager, PayrollHttpClient httpClient,
        ILogger logger, ICommandConsole console, DisplayLevel displayLevel = DisplayLevel.Full)
    {
        CommandManager = commandManager ?? throw new ArgumentNullException(nameof(commandManager));
        Console = console ?? throw new ArgumentNullException(nameof(console));
        HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        DisplayLevel = displayLevel;
    }

    /// <summary>
    /// Command manager.
    /// </summary>
    public CommandManager CommandManager { get; }

    /// <summary>
    /// Command console.
    /// </summary>
    public ICommandConsole Console { get; }

    /// <summary>
    /// Http client.
    /// </summary>
    public PayrollHttpClient HttpClient { get; }

    /// <summary>
    /// Command logger.
    /// </summary>
    public ILogger Logger { get; }

    /// <summary>
    /// Command info level.
    /// </summary>
    public DisplayLevel DisplayLevel { get; }
}