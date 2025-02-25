using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Net.Http;
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

        // script errors
        var scriptErrors = GetScriptErrors(exception);
        if (scriptErrors != null)
        {
            return string.Join("\n", scriptErrors);
        }

        // plain exception message
        // use first line only, remove the stack trace lines
        return GetPlainErrorMessage(exception, maxLines: 1);
    }

    /// <summary>Get the exception message from an API error</summary>
    /// <param name="exception">The exception</param>
    /// <returns>The payroll API error, null on others errors</returns>
    private static ApiError GetApiError(this Exception exception)
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
    private static ApiException GetApiException(Exception exception)
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

    /// <summary>Get script errors</summary>
    /// <param name="exception">The exception</param>
    /// <returns>The payroll API error, null on others errors</returns>
    private static string GetScriptErrors(Exception exception)
    {
        // script errors resulting to a http request exception with status code unprocessable
        if (exception is not HttpRequestException httpException ||
            httpException.StatusCode != HttpStatusCode.UnprocessableContent)
        {
            return null;
        }

        // script errors
        var message = GetPlainErrorMessage(exception);
        return string.IsNullOrWhiteSpace(message) ? null : message;
    }

    /// <summary>Get plain error message</summary>
    /// <param name="exception">The exception</param>
    /// <param name="maxLines">Maximum exception lines</param>
    private static string GetPlainErrorMessage(Exception exception, int maxLines = 0)
    {
        var message = exception.GetBaseMessage();
        message = message.Replace(@"\r\n", "\n");
        var lines = message.Split(['\n'], StringSplitOptions.RemoveEmptyEntries).ToList();

        if (maxLines > 0 && lines.Count > maxLines)
        {
            lines.RemoveRange(maxLines, lines.Count - maxLines);
        }

        // trim quotes and line separators
        for (var i = 0; i < lines.Count; i++)
        {
            lines[i] = lines[i].Trim('\r', '\n', '"');
        }
        return string.Join("\n", lines);
    }

    private static string GetExceptionMessage(Exception exception) =>
        exception.GetBaseMessage().Trim('"')
            .Replace("\\u0027", "'")
            .Replace("\\r", string.Empty)
            .Replace("\\n", string.Empty)
            .Replace("\\", string.Empty);
}