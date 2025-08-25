using System;
using System.Threading.Tasks;

namespace PayrollEngine.Client.Command;

/// <inheritdoc />
public abstract class CommandBase<TArgs> : CommandBase where TArgs : ICommandParameters, new()
{
    /// <summary>
    /// Execute command.
    /// </summary>
    /// <param name="context">Command context.</param>
    /// <param name="parameters">Command parameters.</param>
    /// <returns>Error code, zero on valid command execution.</returns>
    protected abstract Task<int> Execute(CommandContext context, TArgs parameters);

    /// <summary>
    /// Execute command.
    /// </summary>
    /// <param name="context">Command context.</param>
    /// <param name="parameters">Command parameters.</param>
    /// <returns>Error code, zero on valid command execution.</returns>
    protected override async Task<int> OnExecute(CommandContext context, ICommandParameters parameters)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (parameters == null)
        {
            throw new ArgumentNullException(nameof(parameters));
        }
        return await Execute(context, (TArgs)parameters);
    }
}