
namespace PayrollEngine.Client;

/// <summary>The payroll API error</summary>
public class ApiException
{
    /// <summary>The error status code</summary>
    public int StatusCode { get; set; }

    /// <summary>The error message</summary>
    public string Message { get; set; }

    /// <summary>The error stack trace</summary>
    public string StackTrace { get; set; }
}
