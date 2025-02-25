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
    /// <param name="logger">Command logger.</param>
    /// <param name="console">Command console.</param>
    /// <param name="httpClient">Http client.</param>
    /// <param name="displayLevel">Command display level.</param>
    public CommandContext(CommandManager commandManager, ICommandConsole console, ILogger logger = null,
        PayrollHttpClient httpClient = null, DisplayLevel displayLevel = DisplayLevel.Full)
    {
        CommandManager = commandManager ?? throw new ArgumentNullException(nameof(commandManager));
        Console = console ?? throw new ArgumentNullException(nameof(console));
        Logger = logger;
        HttpClient = httpClient;
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