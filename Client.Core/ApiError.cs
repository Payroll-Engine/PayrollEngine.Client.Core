using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client;

/// <summary>The payroll API error</summary>
public class ApiError
{
    /// <summary>The error type</summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    /// <summary>The error title</summary>
    [JsonPropertyName("title")]
    public string Title { get; set; }

    /// <summary>The http status code</summary>
    [JsonPropertyName("status")]
    public int Status { get; set; }

    /// <summary>The error trace id</summary>
    [JsonPropertyName("traceId")]
    public string TraceId { get; set; }

    /// <summary>The errors</summary>
    [JsonPropertyName("errors")]
    public Dictionary<string, string[]> Errors { get; set; }
}