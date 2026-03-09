//#define HTTP_PERFORMANCE
#if HTTP_PERFORMANCE
#define LOG_STOPWATCH
#endif
using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using Task = System.Threading.Tasks.Task;
using PayrollEngine.Serialization;

namespace PayrollEngine.Client;

/// <summary>Http client for the Payroll Engine API</summary>
/// <remarks>This class is not thread-safe. Do not share instances across threads
/// or modify headers (API key, tenant authorization) concurrently.</remarks>
public sealed class PayrollHttpClient : IDisposable
{
    private HttpClient HttpClient { get; }

    #region Constructor

    /// <summary>New instance of the payroll http client with unknown server certificate, timeout is 100 seconds.
    /// See https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.timeout
    /// </summary>
    /// <param name="httpClientHandler">The http client handler</param>
    /// <param name="baseUrl">The Uri the request is sent to</param>
    /// <param name="port">The backend port</param>
    /// <param name="version">The request version</param>
    public PayrollHttpClient(HttpClientHandler httpClientHandler, string baseUrl,
        int port = 0, Version version = null) :
        this(httpClientHandler, baseUrl, TimeSpan.FromSeconds(100), port, version)
    {
    }

    /// <summary>New instance of the payroll http client with unknown server certificate and request timeout</summary>
    /// <param name="httpClientHandler">The http client handler</param>
    /// <param name="baseUrl">The Uri the request is sent to</param>
    /// <param name="port">The backend port</param>
    /// <param name="requestTimeout">The request timeout</param>
    /// <param name="version">The request version</param>
    public PayrollHttpClient(HttpClientHandler httpClientHandler, string baseUrl,
        TimeSpan requestTimeout, int port = 0, Version version = null)
    {
        ArgumentNullException.ThrowIfNull(httpClientHandler);
        ArgumentException.ThrowIfNullOrWhiteSpace(baseUrl);

        // http client
        HttpClient = new(httpClientHandler, false)
        {
            BaseAddress = port > 0 ? new Uri($"{baseUrl}:{port}/") : new Uri($"{baseUrl}/"),
            Timeout = requestTimeout
        };
        OwnsHttpClient = true;

        InitHttpClient(version);
    }

    /// <summary>New instance of the payroll http client with a custom http configuration</summary>
    /// <param name="httpClientHandler">The http client handler</param>
    /// <param name="configuration">The HTTP configuration</param>
    /// <param name="version">The request version</param>
    public PayrollHttpClient(HttpClientHandler httpClientHandler,
        PayrollHttpConfiguration configuration, Version version = null) :
        this(httpClientHandler, configuration.BaseUrl, configuration.Timeout, configuration.Port, version)
    {
    }

    /// <summary>New instance of the payroll http client with a custom http client</summary>
    /// <param name="httpClient">The HTTP client</param>
    /// <param name="version">The request version</param>
    public PayrollHttpClient(HttpClient httpClient, Version version = null)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        this.HttpClient = httpClient;
        OwnsHttpClient = false; // externally provided, caller is responsible for disposal
        InitHttpClient(version);
    }

    private void InitHttpClient(Version version)
    {
        HttpClient.DefaultRequestHeaders.Accept.Clear();
        HttpClient.DefaultRequestHeaders.Accept.Add(
            new(ContentType.Json));

        // version
        version ??= PayrollApiSpecification.CurrentApiVersion;
        HttpClient.DefaultRequestHeaders.Add("X-Version", version.ToString(2));
    }

    #endregion

    #region Authentication

    /// <summary>
    /// Test for api key
    /// </summary>
    public bool HasApiKey() =>
        HttpClient.DefaultRequestHeaders.Contains(PayrollApiSpecification.ApiKeyHeader);

    /// <summary>
    /// Set the api key
    /// </summary>
    /// <param name="apiKey">The api key</param>
    public void SetApiKey(string apiKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);

        // remove existing api
        RemoveApiKey();
        // set api key header
        HttpClient.DefaultRequestHeaders.Add(PayrollApiSpecification.ApiKeyHeader, apiKey);
    }

    /// <summary>
    /// Remove the api key
    /// </summary>
    public void RemoveApiKey()
    {
        // remove existing api key header
        if (HasApiKey())
        {
            HttpClient.DefaultRequestHeaders.Remove(PayrollApiSpecification.ApiKeyHeader);
        }
    }

    #endregion

    #region Authorization

    /// <summary>
    /// Test for tenant authorization
    /// </summary>
    public bool HasTenantAuthorization() =>
        HttpClient.DefaultRequestHeaders.Contains(PayrollApiSpecification.TenantAuthorizationHeader);

    /// <summary>
    /// Get the tenant authorization
    /// </summary>
    public string GetTenantAuthorization()
    {
        if (!HasTenantAuthorization())
        {
            return null;
        }
        var values = HttpClient.DefaultRequestHeaders.GetValues(PayrollApiSpecification.TenantAuthorizationHeader);
        return values.FirstOrDefault();
    }

    /// <summary>
    /// Set the tenant authorization
    /// </summary>
    /// <param name="tenantIdentifier">The tenant identifier</param>
    public void SetTenantAuthorization(string tenantIdentifier)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(tenantIdentifier);

        // remove existing authorization
        RemoveTenantAuthorization();
        // set new authorization
        HttpClient.DefaultRequestHeaders.Add(PayrollApiSpecification.TenantAuthorizationHeader, tenantIdentifier);
    }

    /// <summary>
    /// Remove the tenant authorization
    /// </summary>
    public void RemoveTenantAuthorization()
    {
        // remove existing authorization
        if (HasTenantAuthorization())
        {
            HttpClient.DefaultRequestHeaders.Remove(PayrollApiSpecification.TenantAuthorizationHeader);
        }
    }

    #endregion

    #region Connection

    /// <summary>
    /// The http address
    /// </summary>
    public string Address =>
        HttpClient.BaseAddress != null ? HttpClient.BaseAddress.AbsoluteUri : null;

    /// <summary>Test for the client internet connection</summary>
    /// <param name="address">The address to test</param>
    /// <returns>True if internet connection is available</returns>
    public async Task<bool> IsConnectionAvailableAsync(string address)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(address);

        try
        {
            // get test
            var httpResponse = await HttpClient.GetAsync(address);
            return httpResponse.IsSuccessStatusCode;
        }
        catch (HttpRequestException exception)
        {
            // connection refused or unavailable - expected when backend is not running
            Log.Warning($"Backend connection {address} is not available: {exception.GetBaseException().Message}");
        }
        catch (TaskCanceledException)
        {
            // timeout - expected when backend is not reachable
            Log.Warning($"Backend connection {address} timed out.");
        }
        catch (Exception exception)
        {
            // unexpected error
            Log.Error(exception, exception.GetBaseException().Message);
        }
        return false;
    }

    #endregion

    #region Get Requests

    /// <summary>Get record id from the response (last uri segment)</summary>
    /// <param name="response">The http response message</param>
    /// <returns>The new record id</returns>
    public static async Task<int> GetRecordIdAsync(HttpResponseMessage response)
    {
        ArgumentNullException.ThrowIfNull(response);

        // location header
        if (response.Headers.Location != null && 
            response.Headers.Location.TryGetLastSegmentId(out var id))
        {
            return id;
        }

        // created object
        var json = await response.Content.ReadAsStringAsync();
        if (!string.IsNullOrWhiteSpace(json))
        {
            var obj = DefaultJsonSerializer.Deserialize<Dictionary<string, object>>(json);
            if (obj.TryGetValue("id", out var objId) && objId is JsonElement jsonValue)
            {
                return jsonValue.GetInt32();
            }
        }

        return 0;
    }

    /// <summary>Get backend resource response</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <returns>The http response message</returns>
    public async Task<HttpResponseMessage> GetAsync(string requestUri)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);

        LogStopwatch.Start(GetLogName(nameof(GetAsync), requestUri));
        var response = await HttpClient.GetAsync(requestUri);
        LogStopwatch.Stop(GetLogName(nameof(GetAsync), requestUri));

        await EnsureSuccessResponse(response);
        return response;
    }

    /// <summary>Get backend resource response with string content</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <param name="content">The request content</param>
    /// <returns>The http response message</returns>
    public async Task<HttpResponseMessage> GetAsync(string requestUri, StringContent content)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);
        ArgumentNullException.ThrowIfNull(content);

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new(requestUri, UriKind.RelativeOrAbsolute),
            Content = content
        };

        LogStopwatch.Start(GetLogName(nameof(GetAsync), requestUri));
        var response = await HttpClient.SendAsync(request).ConfigureAwait(false);
        LogStopwatch.Stop(GetLogName(nameof(GetAsync), requestUri));

        await EnsureSuccessResponse(response);
        return response;
    }

    /// <summary>Get backend resource</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <typeparam name="T">The object type</typeparam>
    /// <returns>The backend resource</returns>
    public async Task<T> GetAsync<T>(string requestUri)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);

        var response = await GetAsync(requestUri);
        var json = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(json))
        {
            return default;
        }
        return DefaultJsonSerializer.Deserialize<T>(json);
    }

    /// <summary>Get backend resource with request content</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <param name="content">The request content</param>
    /// <typeparam name="T">The object type</typeparam>
    /// <returns>The backend resource</returns>
    public async Task<T> GetAsync<T>(string requestUri, object content)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);
        ArgumentNullException.ThrowIfNull(content);

        var response = await GetAsync(requestUri, DefaultJsonSerializer.SerializeJson(content));
        var json = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(json))
        {
            return default;
        }
        return DefaultJsonSerializer.Deserialize<T>(json);
    }

    /// <summary>Get single resource from a collection</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <typeparam name="T">The object type</typeparam>
    /// <returns>First collection resource, null on empty collections</returns>
    public async Task<T> GetSingleAsync<T>(string requestUri) where T : class =>
        (await GetCollectionAsync<T>(requestUri)).FirstOrDefault();

    #endregion

    #region Get Collection Requests

    /// <summary>Get collection of backend resource</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <typeparam name="T">The object type</typeparam>
    /// <returns>A collection of backend resources</returns>
    public async Task<List<T>> GetCollectionAsync<T>(string requestUri) where T : class
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);

        LogStopwatch.Start(GetLogName(nameof(GetCollectionAsync), requestUri));
        using var response = await HttpClient.GetAsync(requestUri);
        LogStopwatch.Stop(GetLogName(nameof(GetCollectionAsync), requestUri));

        await EnsureSuccessResponse(response);
        var json = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(json))
        {
            return [];
        }

        return DefaultJsonSerializer.Deserialize<List<T>>(json);
    }

    /// <summary>Get collection of backend resource with request content</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <param name="content">The request content</param>
    /// <typeparam name="T">The object type</typeparam>
    /// <returns>A collection of backend resources</returns>
    public async Task<List<T>> GetCollectionAsync<T>(string requestUri, object content) where T : class
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);
        ArgumentNullException.ThrowIfNull(content);

        var response = await GetAsync(requestUri, DefaultJsonSerializer.SerializeJson(content));
        var json = await response.Content.ReadAsStringAsync();
        return string.IsNullOrWhiteSpace(json) ? [] : DefaultJsonSerializer.Deserialize<List<T>>(json);
    }

    #endregion

    #region Post Requests

    /// <summary>Post resource to backend</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <returns>The backend resource</returns>
    public async Task<T> PostAsync<T>(string requestUri)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);

        LogStopwatch.Start(GetLogName(nameof(PostAsync), requestUri));
        using var response = await HttpClient.PostAsync(requestUri, null);
        LogStopwatch.Stop(GetLogName(nameof(PostAsync), requestUri));

        await EnsureSuccessResponse(response);

        var json = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(json))
        {
            return default;
        }
        var responseObj = DefaultJsonSerializer.Deserialize<T>(json);
        if (responseObj == null)
        {
            throw new HttpRequestException($"Empty POST response in request {requestUri}");
        }

        if (responseObj is IModel model && model.Id == 0)
        {
            model.Id = await GetRecordIdAsync(response);
        }
        LogPost(requestUri, responseObj);
        return responseObj;
    }

    /// <summary>Post resource to backend</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <param name="content">The resource content</param>
    /// <returns>The backend resource</returns>
    public async Task<T> PostAsync<T>(string requestUri, T content) where T : class
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);
        ArgumentNullException.ThrowIfNull(content);

        LogStopwatch.Start(GetLogName(nameof(PostAsync), requestUri));
        using var response = await HttpClient.PostAsync(requestUri, DefaultJsonSerializer.SerializeJson(content));
        LogStopwatch.Stop(GetLogName(nameof(PostAsync), requestUri));

        await EnsureSuccessResponse(response);

        var json = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(json))
        {
            return null;
        }
        var responseObj = DefaultJsonSerializer.Deserialize<T>(json);
        if (responseObj == null)
        {
            throw new HttpRequestException($"Empty POST response in request {requestUri}");
        }

        if (responseObj is IModel model && model.Id == 0)
        {
            model.Id = await GetRecordIdAsync(response);
        }
        LogPost(requestUri, responseObj);
        return responseObj;
    }

    /// <summary>Post resource to backend</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <param name="content">The resource content</param>
    /// <returns>The backend resource</returns>
    public async Task<TOut> PostAsync<TIn, TOut>(string requestUri, TIn content)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);
        ArgumentNullException.ThrowIfNull(content);

        LogStopwatch.Start(GetLogName(nameof(PostAsync), requestUri));
        using var response = await HttpClient.PostAsync(requestUri, DefaultJsonSerializer.SerializeJson(content));
        LogStopwatch.Stop(GetLogName(nameof(PostAsync), requestUri));

        await EnsureSuccessResponse(response);

        var json = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(json))
        {
            return default;
        }
        var responseObj = DefaultJsonSerializer.Deserialize<TOut>(json);
        if (responseObj == null)
        {
            throw new HttpRequestException($"Empty POST response in request {requestUri}");
        }

        if (responseObj is IModel model && model.Id == 0)
        {
            model.Id = await GetRecordIdAsync(response);
        }
        LogPost(requestUri, responseObj);
        return responseObj;
    }


    /// <summary>Post resource to backend</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <param name="content">The resource content</param>
    public async Task PostAsync(string requestUri, object content)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);
        ArgumentNullException.ThrowIfNull(content);

        using var response = await PostAsync(requestUri, DefaultJsonSerializer.SerializeJson(content));
    }

    /// <summary>Post resource to backend</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <param name="content">The resource content</param>
    /// <returns>The http response message</returns>
    public async Task<HttpResponseMessage> PostAsync(string requestUri, StringContent content)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);
        ArgumentNullException.ThrowIfNull(content);

        LogStopwatch.Start(GetLogName(nameof(PostAsync), requestUri));
        var response = await HttpClient.PostAsync(requestUri, content);
        LogStopwatch.Stop(GetLogName(nameof(PostAsync), requestUri));

        await EnsureSuccessResponse(response);
        return response;
    }

    #endregion

    #region Put Requests

    /// <summary>Put resource to backend</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <param name="content">The resource content</param>
    public async Task PutAsync(string requestUri, string content = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);

        if (string.IsNullOrWhiteSpace(content))
        {
            content = "{}";
        }
        await PutAsync(requestUri, DefaultJsonSerializer.SerializeJson(content));
    }

    /// <summary>Put resource to backend</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <param name="content">The resource content</param>
    public async Task PutAsync(string requestUri, StringContent content)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);
        ArgumentNullException.ThrowIfNull(content);

        LogStopwatch.Start(GetLogName(nameof(PutAsync), requestUri));
        using var response = await HttpClient.PutAsync(requestUri, content);
        LogStopwatch.Stop(GetLogName(nameof(PutAsync), requestUri));

        await EnsureSuccessResponse(response);
    }

    /// <summary>Put resource to backend</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <param name="content">The resource content</param>
    public async Task PutAsync<T>(string requestUri, T content) where T : IModel
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);
        ArgumentNullException.ThrowIfNull(content);

        // extend url with the object id
        requestUri = $"{requestUri}/{content.Id}";

        LogStopwatch.Start(GetLogName(nameof(PutAsync), requestUri));
        using var response = await HttpClient.PutAsync(requestUri, DefaultJsonSerializer.SerializeJson(content));
        LogStopwatch.Stop(GetLogName(nameof(PutAsync), requestUri));

        await EnsureSuccessResponse(response);
        LogPut(requestUri, content);
    }

    #endregion

    #region Upsert Requests

    /// <summary>Insert or update backend resource</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <param name="newObject">Content of new resource</param>
    /// <param name="existingObject">Content of existing resource (optional)</param>
    /// <param name="createdDate">The created date for new objects</param>
    /// <typeparam name="T">The object type</typeparam>
    public async Task UpsertObjectAsync<T>(string requestUri, T newObject, T existingObject,
        DateTime? createdDate = null)
        where T : IModel
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);
        ArgumentNullException.ThrowIfNull(newObject);

        if (existingObject != null)
        {
            // update
            newObject.Id = existingObject.Id;
            if (newObject.UpdateMode is UpdateMode.Update)
            {
                requestUri = $"{requestUri}/{newObject.Id}";
                using var response = await HttpClient.PutAsync(requestUri, DefaultJsonSerializer.SerializeJson(newObject));
                await EnsureSuccessResponse(response);
                LogPut(requestUri, newObject);
            }
        }
        else
        {
            // create
            if (!newObject.Created.IsDefined() && createdDate.HasValue)
            {
                newObject.Created = createdDate.Value;
            }
            // reset the id
            newObject.Id = 0;

            using var response = await HttpClient.PostAsync(requestUri, DefaultJsonSerializer.SerializeJson(newObject));
            await EnsureSuccessResponse(response);
            newObject.Id = await GetRecordIdAsync(response);
            if (newObject.Id <= 0)
            {
                throw new PayrollException($"error while creating new record of object {newObject}.");
            }
            LogPost(requestUri, newObject);
        }
    }

    #endregion

    #region Delete Requests

    /// <summary>Delete backend resource</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <param name="id">The object id</param>
    public async Task DeleteAsync(string requestUri, int id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }

        requestUri = requestUri.EnsureEnd("/");
        LogStopwatch.Start(GetLogName(nameof(DeleteAsync), requestUri));
        using var response = await HttpClient.DeleteAsync($"{requestUri}{id}");
        LogStopwatch.Stop(GetLogName(nameof(DeleteAsync), requestUri));

        await EnsureSuccessResponse(response);
    }

    /// <summary>Delete backend collection</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    public async Task DeleteAsync(string requestUri)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);

        requestUri = requestUri.EnsureEnd("/");
        LogStopwatch.Start(GetLogName(nameof(DeleteAsync), requestUri));
        using var response = await HttpClient.DeleteAsync(requestUri);
        LogStopwatch.Stop(GetLogName(nameof(DeleteAsync), requestUri));

        await EnsureSuccessResponse(response);
    }

    #endregion

    #region Attribute Requests

    /// <summary>Get resource attribute</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <returns>The attribute value</returns>
    public async Task<string> GetAttributeAsync(string requestUri)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);

        LogStopwatch.Start(GetLogName(nameof(GetAttributeAsync), requestUri));
        using var response = await HttpClient.GetAsync(requestUri);
        LogStopwatch.Stop(GetLogName(nameof(GetAttributeAsync), requestUri));

        // not found on missing attribute
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        await EnsureSuccessResponse(response);
        return await response.Content.ReadAsStringAsync();
    }

    /// <summary>Post resource attribute value</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <param name="attributeValue">Value of the attribute</param>
    public async Task<string> PostAttributeAsync(string requestUri, string attributeValue)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeValue);

        LogStopwatch.Start(GetLogName(nameof(PostAttributeAsync), requestUri));
        using var response = await HttpClient.PostAsync(requestUri, DefaultJsonSerializer.SerializeJson(attributeValue));
        LogStopwatch.Stop(GetLogName(nameof(PostAttributeAsync), requestUri));

        await EnsureSuccessResponse(response);

        return await response.Content.ReadAsStringAsync();
    }

    /// <summary>Delete resource attribute</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    public async Task DeleteAttributeAsync(string requestUri)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);

        LogStopwatch.Start(GetLogName(nameof(DeleteAttributeAsync), requestUri));
        using var response = await HttpClient.DeleteAsync(requestUri);
        LogStopwatch.Stop(GetLogName(nameof(DeleteAttributeAsync), requestUri));

        await EnsureSuccessResponse(response);
    }

    #endregion

    #region Logging

    /// <summary>Log a backend post</summary>
    /// <param name="url">The Uri the request is sent to</param>
    /// <param name="newObject">The posted object</param>
    private static void LogPost(string url, object newObject)
    {
        if (Log.IsEnabled(LogLevel.Verbose))
        {
            Log.Trace($"Created resource {url}: {DefaultJsonSerializer.Serialize(newObject)}");
        }
        else if (Log.IsEnabled(LogLevel.Debug))
        {
            Log.Debug($"Created resource {url}: {newObject}");
        }
    }
    /// <summary>Log a backend put</summary>
    /// <param name="requestUri">The Uri the request is sent to</param>
    /// <param name="newObject">The posted object</param>
    private static void LogPut(string requestUri, object newObject)
    {
        if (Log.IsEnabled(LogLevel.Verbose))
        {
            Log.Trace($"Updated resource {requestUri}: {DefaultJsonSerializer.Serialize(newObject)}");
        }
        else if (Log.IsEnabled(LogLevel.Debug))
        {
            Log.Debug($"Updated resource {requestUri}: {newObject}");
        }
    }

    /// <summary>
    /// Get the log name
    /// </summary>
    /// <param name="methodName">The method name</param>
    /// <param name="requestUri">The request uri</param>
    /// <returns>The log name</returns>
    public static string GetLogName(string methodName, string requestUri) =>
        $"{nameof(PayrollHttpClient)}.{methodName}: {requestUri}";

    #endregion

    /// <summary>Ensure valid http response</summary>
    /// <param name="response">The http response message</param>
    /// <exception cref="HttpRequestException">Throws an exception if response has invalid status code</exception>
    private static async Task EnsureSuccessResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        // error message
        var content = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(content))
        {
            content = response.ReasonPhrase;
        }
        throw new HttpRequestException(content, null, response.StatusCode);
    }

    /// <summary>The string representation</summary>
    public override string ToString() => Address;

    /// <summary>The ownership flag indicates whether this instance owns the HttpClient</summary>
    private bool OwnsHttpClient { get; }

    /// <summary>Dispose http client</summary>
    public void Dispose()
    {
        if (OwnsHttpClient)
        {
            HttpClient?.Dispose();
        }
    }
}
