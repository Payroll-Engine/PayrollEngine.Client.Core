using System;
using System.Globalization;
using Microsoft.AspNetCore.WebUtilities;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Base client service</summary>
public abstract class ServiceBase
{
    /// <summary>The Payroll http client</summary>
    public PayrollHttpClient HttpClient { get; }

    /// <summary>Initializes a new instance of the <see cref="ServiceBase"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    protected ServiceBase(PayrollHttpClient httpClient)
    {
        HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    #region Query

    /// <summary>Append the given query key and value to the URI</summary>
    /// <param name="uri">The base URI</param>
    /// <param name="name">The name of the query key</param>
    /// <param name="value">The query value</param>
    /// <returns>The combined result</returns>
    protected string AddQueryValue(string uri, string name, object value) =>
        value != null ? QueryHelpers.AddQueryString(uri, name, value.ToString()) : uri;

    /// <summary>Append the given query key and date value to the URI</summary>
    /// <param name="uri">The base URI</param>
    /// <param name="name">The name of the query key</param>
    /// <param name="value">The query value</param>
    /// <returns>The combined result</returns>
    protected string AddQueryValue(string uri, string name, DateTime value) =>
        QueryHelpers.AddQueryString(uri, name, value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));

    #endregion

}