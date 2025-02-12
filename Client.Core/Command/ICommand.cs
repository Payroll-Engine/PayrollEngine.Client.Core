using System.Threading.Tasks;

namespace PayrollEngine.Client.Command;

/// <summary>
/// Command interface.
/// </summary>
public interface ICommand
{
    /// <summary>
    /// Command name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Get command parameters.
    /// </summary>
    /// <param name="parser">Command line parser.</param>
    /// <returns></returns>
    ICommandParameters GetParameters(CommandLineParser parser);

    /// <summary>
    /// Execute command.
    /// </summary>
    /// <param name="context">Command context.</param>
    /// <param name="parameters">Command parameters.</param>
    /// <returns>Error code, zero on valid command execution.</returns>
    Task<int> ExecuteAsync(CommandContext context, ICommandParameters parameters);

    /// <summary>
    /// Show command help.
    /// </summary>
    /// <param name="console">Display console.</param>
    void ShowHelp(ICommandConsole console);
}