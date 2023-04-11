using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll collector service</summary>
public class CollectorService : Service, ICollectorService
{
    /// <summary>Initializes a new instance of the <see cref="CollectorService"/> class</summary>
    /// <param name="httpClient">The HTTP client</param>
    public CollectorService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(RegulationServiceContext context, Query query = null) where T : class, ICollector
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationCollectorsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(RegulationServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationCollectorsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(RegulationServiceContext context, Query query = null) where T : class, ICollector
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationCollectorsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(RegulationServiceContext context, int collectorId) where T : class, ICollector
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (collectorId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(collectorId));
        }

        return await HttpClient.GetAsync<T>(
            RegulationApiEndpoints.RegulationCollectorUrl(context.TenantId, context.RegulationId, collectorId));
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(RegulationServiceContext context, string name) where T : class, ICollector
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(nameof(name));
        }

        // query single item
        var query = QueryFactory.NewNameQuery(name);
        var uri = query.AppendQueryString(RegulationApiEndpoints.RegulationCollectorsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<T> CreateAsync<T>(RegulationServiceContext context, T collector) where T : class, ICollector
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (collector == null)
        {
            throw new ArgumentNullException(nameof(collector));
        }

        return await HttpClient.PostAsync(
            RegulationApiEndpoints.RegulationCollectorsUrl(context.TenantId, context.RegulationId), collector);
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync<T>(RegulationServiceContext context, T collector) where T : class, ICollector
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (collector == null)
        {
            throw new ArgumentNullException(nameof(collector));
        }

        await HttpClient.PutAsync(RegulationApiEndpoints.RegulationCollectorsUrl(context.TenantId, context.RegulationId),
            collector);
    }

    /// <inheritdoc/>
    public virtual async Task RebuildAsync(RegulationServiceContext context, int collectorId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (collectorId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(collectorId));
        }

        await HttpClient.PutAsync(RegulationApiEndpoints.RegulationCollectorRebuildUrl(context.TenantId, context.RegulationId, collectorId));
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(RegulationServiceContext context, int collectorId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (collectorId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(collectorId));
        }

        await HttpClient.DeleteAsync(RegulationApiEndpoints.RegulationCollectorsUrl(context.TenantId, context.RegulationId),
            collectorId);
    }
}