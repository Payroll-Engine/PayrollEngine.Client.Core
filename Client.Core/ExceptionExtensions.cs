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
    public static string GetApiErrorMessage(this Exception exception)
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

        // api exception
        var apiException = GetApiException(exception);
        if (apiException != null)
        {
            return apiException.Message;
        }

        // maybe a plain exception message
        // use first line only, remove the stack trace lines
        var message = exception.GetBaseMessage();
        message = message.Replace("\\r\\n", "\n");
        var lines = message.Split(['\n']);
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

        var message = GetExceptionMessage(exception);

        // test for api error json
        if (!message.StartsWith('{') || !message.EndsWith('}') ||
            !message.Contains(nameof(ApiError.Status), StringComparison.InvariantCultureIgnoreCase))
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

    /// <summary>Get the exception message from an API exception</summary>
    /// <param name="exception">The exception</param>
    /// <returns>The payroll API error, null on others errors</returns>
    public static ApiException GetApiException(Exception exception)
    {
        if (exception == null)
        {
            return null;
        }

        var message = GetExceptionMessage(exception);

        // test for api exception json
        if (!message.StartsWith('{') || !message.EndsWith('}') ||
            !message.Contains(nameof(ApiException.StatusCode), StringComparison.InvariantCultureIgnoreCase))
        {
            return null;
        }

        try
        {
            var apiException = JsonSerializer.Deserialize<ApiException>(message);
            if (apiException.StatusCode != 0)
            {
                return apiException;
            }
        }
        catch
        {
            return null;
        }
        return null;
    }

    private static string GetExceptionMessage(Exception exception) =>
        exception.GetBaseMessage().Trim('"')
            .Replace("\\u0027", "'")
            .Replace("\\r", string.Empty)
            .Replace("\\n", string.Empty)
            .Replace("\\", string.Empty);
}