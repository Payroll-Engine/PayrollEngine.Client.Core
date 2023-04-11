using System;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace PayrollEngine.Client;

/// <summary>Extension methods for <see cref="Exception"/></summary>
public static class ExceptionExtensions
{
    /// <summary>Convert an exception to an payroll API error</summary>
    /// <param name="exception">The exception</param>
    /// <returns>The payroll API error, null on others errors</returns>
    public static ApiError ToApiError(this Exception exception)
    {
        if (exception == null)
        {
            return null;
        }

        var message = exception.Message;
        if (string.IsNullOrWhiteSpace(exception.Message))
        {
            return null;
        }

        try
        {
            ApiError apiError = null;
            var statusCode = GetHttpStatusCode(message);
            if (statusCode.HasValue)
            {
                var httpMessage = GetHttpMessage(message, statusCode.Value);
                // internal server error
                if (!string.IsNullOrWhiteSpace(httpMessage) &&
                    statusCode >= (int)HttpStatusCode.InternalServerError)
                {
                    apiError = JsonSerializer.Deserialize<ApiError>(httpMessage);
                    if (apiError != null)
                    {
                        // message
                        apiError.Message = apiError.Message.Replace("\\r\\n", Environment.NewLine);
                        // stack trace format
                        apiError.StackTrace = apiError.StackTrace.Replace("\\r\\n", Environment.NewLine);
                        apiError.StackTrace = apiError.StackTrace.Replace("   at ", "at ");
                        apiError.StackTrace = apiError.StackTrace.Replace(" in ", $"{Environment.NewLine}   in ");
                    }
                }
                else
                {
                    apiError = new() { Message = httpMessage, StatusCode = statusCode.Value};
                }
            }

            return apiError ?? new() { Message = message };
        }
        catch
        {
            return null;
        }
    }

    private static int? GetHttpStatusCode(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return null;
        }
        // http status code is the first message token, separated with a space
        var code = message.Split(' ').FirstOrDefault();
        if (string.IsNullOrWhiteSpace(code))
        {
            return null;
        }

        if (int.TryParse(code, out var statusCode))
        {
            return statusCode;
        }
        return null;
    }

    private static string GetHttpMessage(string message, int statusCode)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return null;
        }
        var prefix = $"{statusCode} (\"";
        var postfix = "\")";
        if (message.StartsWith(prefix) && message.EndsWith(postfix))
        {
            message = message.Substring(prefix.Length);
            message = message.Substring(0, message.Length - postfix.Length);
        }

        message = message.Replace("\\r\\n", Environment.NewLine);
        message = message.Replace("\\\"", "\"");
        message = message.Replace("\\\\\\\\", "\\\\");
        return message;
    }
}