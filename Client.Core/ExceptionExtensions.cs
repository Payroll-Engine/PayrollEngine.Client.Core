using System;
using System.Text;
using System.Text.Json;

namespace PayrollEngine.Client;

/// <summary>Extension methods for <see cref="Exception"/></summary>
public static class ExceptionExtensions
{
    /// <summary>Get the exception message from an API error</summary>
    /// <param name="exception">The exception</param>
    /// <returns>The payroll API error, null on others errors</returns>
    public static string GetApiMessage(this Exception exception)
    {
        if (exception == null)
        {
            return null;
        }

        // api error
        ApiError apiError = GetApiError(exception);
        if (apiError != null)
        {
            var buffer = new StringBuilder();
            buffer.AppendLine(apiError.Title);
            foreach (var error in apiError.Errors)
            {
                foreach (var errorValue in error.Value)
                {
                    buffer.AppendLine($"{error.Key}: {errorValue.Trim()}");
                }
            }
            return buffer.ToString().Trim('\r', '\n', '"');
        }

        // extract message in case of exception stacks
        var message = exception.GetBaseMessage();
        var lines = message.Split(new[] { '\n' });
        if (lines.Length > 1)
        {
            message = lines[0];
        }

        // trim quotes and line separators
        return message.Trim('\r', '\n', '"');
    }

    /// <summary>Get the exception message from an API error</summary>
    /// <param name="exception">The exception</param>
    /// <returns>The payroll API error, null on others errors</returns>
    public static ApiError GetApiError(this Exception exception)
    {
        if (exception == null)
        {
            return null;
        }

        var message = exception.GetBaseMessage();

        // api validation error
        if (!message.StartsWith("{") || !message.EndsWith("}"))
        {
            return null;
        }

        try
        {
            var apiError = JsonSerializer.Deserialize<ApiError>(message);
            if (apiError.Status != 0)
            {
                return apiError;
            }
        }
        catch
        {
            return null;
        }
        return null;
    }
}