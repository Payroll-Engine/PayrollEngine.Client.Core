using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PayrollEngine.Client.Command;

/// <inheritdoc />
public abstract class CommandBase : ICommand
{
    /// <inheritdoc />
    public virtual string Name => GetType().Name;

    /// <inheritdoc />
    public virtual bool BackendCommand => true;

    /// <inheritdoc />
    public abstract ICommandParameters GetParameters(CommandLineParser parser);

    /// <inheritdoc />
    public async Task<int> ExecuteAsync(CommandContext context, ICommandParameters parameters)
    {
        if (!string.IsNullOrWhiteSpace(parameters.Test()))
        {
            return -1;
        }
        return await OnExecute(context, parameters);
    }

    /// <summary>
    /// Execute command.
    /// </summary>
    /// <param name="context">Command context.</param>
    /// <param name="parameters">Command parameters.</param>
    /// <returns>Error code, zero on valid command execution.</returns>
    protected abstract Task<int> OnExecute(CommandContext context, ICommandParameters parameters);

    #region Display

    /// <inheritdoc />
    public abstract void ShowHelp(ICommandConsole console);

    /// <summary>
    /// Display program title.
    /// </summary>
    /// <param name="console">Command console.</param>
    /// <param name="title">Application title.</param>
    protected void DisplayTitle(ICommandConsole console, string title)
    {
        console.DisplayTitleLine($"=== {title} ===");
    }

    /// <summary>
    /// Process command error.
    /// </summary>
    /// <param name="console">Command console.</param>
    /// <param name="exception">Command error.</param>
    protected void ProcessError(ICommandConsole console, Exception exception)
    {
        if (exception == null)
        {
            return;
        }
        var message = exception.GetBaseException().Message;

        // api error
        var apiError = exception.GetApiErrorMessage();
        if (!string.IsNullOrWhiteSpace(apiError))
        {
            message = apiError;
            console.DisplayErrorLine(apiError);
        }

        // log
        Log.Error(exception, message);
    }

    /// <summary>
    /// Display respondent content.
    /// </summary>
    /// <param name="console"></param>
    /// <param name="response"></param>
    /// <returns></returns>
    protected static async Task DisplayResponseContent(ICommandConsole console, HttpResponseMessage response)
    {
        var json = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(json))
        {
            return;
        }
        var formattedJson = JsonSerializer.Serialize(JsonSerializer.Deserialize<object>(json),
            typeof(object), new JsonSerializerOptions { WriteIndented = true });
        console.DisplayInfoLine(formattedJson);
    }

    #endregion

    /// <inheritdoc />
    public override string ToString() =>
        GetType().Name;
}