using System;
using System.Linq;
using System.Collections.Generic;

namespace PayrollEngine.Client;

/// <summary>The Payroll HTTP configuration</summary>
public class PayrollHttpConfiguration
{
    /// <summary>Default constructor</summary>
    public PayrollHttpConfiguration()
    {
    }

    /// <summary>New instance of the Payroll Engine http client with unknown server certificate, timeout is 100 seconds.
    /// See https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.timeout
    /// </summary>
    /// <param name="baseUrl">The Uri the request is sent to</param>
    /// <param name="port">The backend port</param>
    public PayrollHttpConfiguration(string baseUrl, int port) :
        this(baseUrl, port, TimeSpan.FromSeconds(100), null)
    {
    }

    /// <summary>New instance of Payroll Engine http client with unknown server certificate and request timeout</summary>
    /// <param name="baseUrl">The Uri the request is sent to</param>
    /// <param name="port">The backend port</param>
    /// <param name="requestTimeout">The request timeout</param>
    /// <param name="apiKey">Api key</param>
    public PayrollHttpConfiguration(string baseUrl, int port, TimeSpan requestTimeout, string apiKey)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            throw new ArgumentException(nameof(baseUrl));
        }
        BaseUrl = baseUrl;
        Port = port;
        Timeout = requestTimeout;
        ApiKey = apiKey;
    }

    /// <summary>The default API request timeout</summary>
    public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(100);

    /// <summary>The API base url</summary>
    public string BaseUrl { get; set; }

    /// <summary>The API port</summary>
    public int Port { get; set; }

    /// <summary>The API request timeout</summary>
    public TimeSpan Timeout { get; set; } = DefaultTimeout;

    /// <summary>The API key</summary>
    public string ApiKey { get; set; }

    /// <summary>Test for valid configuration</summary>
    public bool Valid() =>
        !string.IsNullOrWhiteSpace(BaseUrl);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    public override string ToString() =>
        Port > 0 ? $"{BaseUrl}:{Port}" : BaseUrl;

    /// <summary>Payroll http configuration from a connection string</summary>
    /// <param name="connectionString">Configuration connection string</param>
    public static PayrollHttpConfiguration FromConnectionString(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException(nameof(connectionString));
        }

        var tokens = connectionString.Split(';', StringSplitOptions.RemoveEmptyEntries).
            Select(x => x.Trim()).ToList();

        // base url
        var baseUrl = GetConnectionStringValue(tokens, nameof(BaseUrl));

        // port
        var port = 0;
        var portString = GetConnectionStringValue(tokens, nameof(Port));
        if (!string.IsNullOrWhiteSpace(portString))
        {
            port = int.Parse(portString);
        }

        // timeout
        var timeout = DefaultTimeout;
        var timeoutString = GetConnectionStringValue(tokens, nameof(Timeout));
        if (!string.IsNullOrWhiteSpace(timeoutString))
        {
            timeout = TimeSpan.Parse(timeoutString);
        }

        // api key
        var apiKey = GetConnectionStringValue(tokens, nameof(ApiKey));
        return new(baseUrl, port, timeout, apiKey);
    }

    private static string GetConnectionStringValue(IEnumerable<string> tokens, string key)
    {
        foreach (var token in tokens)
        {
            if (token.StartsWith(key + "=", StringComparison.InvariantCultureIgnoreCase))
            {
                return token.Substring(key.Length + 1).Trim();
            }
        }
        return null;
    }
}