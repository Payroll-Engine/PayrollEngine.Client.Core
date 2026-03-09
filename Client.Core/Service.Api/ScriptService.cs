using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll script service</summary>
public class ScriptService : ServiceBase, IScriptService
{
    /// <summary>Initializes a new instance of the <see cref="ScriptService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public ScriptService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(RegulationServiceContext context, Query query = null) where T : class, IScript
    {
        ArgumentNullException.ThrowIfNull(context);

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationScriptsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(RegulationServiceContext context, Query query = null)
    {
        ArgumentNullException.ThrowIfNull(context);

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationScriptsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(RegulationServiceContext context, Query query = null) where T : class, IScript
    {
        ArgumentNullException.ThrowIfNull(context);

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationScriptsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(RegulationServiceContext context, int scriptId) where T : class, IScript
    {
        ArgumentNullException.ThrowIfNull(context);
        if (scriptId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(scriptId));
        }

        return await HttpClient.GetAsync<T>(RegulationApiEndpoints.RegulationScriptUrl(context.TenantId, context.RegulationId,
            scriptId));
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(RegulationServiceContext context, string name) where T : class, IScript
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        // query single item
        var query = QueryFactory.NewNameQuery(name);
        var uri = query.AppendQueryString(RegulationApiEndpoints.RegulationScriptsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<T> CreateAsync<T>(RegulationServiceContext context, T script) where T : class, IScript
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(script);

        return await HttpClient.PostAsync(RegulationApiEndpoints.RegulationScriptsUrl(context.TenantId, context.RegulationId),
            script);
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync<T>(RegulationServiceContext context, T script) where T : class, IScript
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(script);

        await HttpClient.PutAsync(RegulationApiEndpoints.RegulationScriptsUrl(context.TenantId, context.RegulationId),
            script);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(RegulationServiceContext context, int scriptId)
    {
        ArgumentNullException.ThrowIfNull(context);
        if (scriptId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(scriptId));
        }

        await HttpClient.DeleteAsync(RegulationApiEndpoints.RegulationScriptsUrl(context.TenantId, context.RegulationId),
            scriptId);
    }
}