using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll lookup service</summary>
public class LookupService : ServiceBase, ILookupService
{
    /// <summary>Initializes a new instance of the <see cref="LookupService"/> class</summary>
    /// <param name="httpClient">The HTTP client</param>
    public LookupService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(RegulationServiceContext context, Query query = null) where T : class, ILookup
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationLookupsUrl(context.TenantId, context.RegulationId));
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
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationLookupsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(RegulationServiceContext context, Query query = null) where T : class, ILookup
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationLookupsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(RegulationServiceContext context, int lookupId) where T : class, ILookup
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (lookupId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(lookupId));
        }

        return await HttpClient.GetAsync<T>(RegulationApiEndpoints.RegulationLookupUrl(context.TenantId, context.RegulationId,
            lookupId));
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(RegulationServiceContext context, string name) where T : class, ILookup
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
        var uri = query.AppendQueryString(RegulationApiEndpoints.RegulationLookupsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<T> CreateAsync<T>(RegulationServiceContext context, T lookup) where T : class, ILookup
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (lookup == null)
        {
            throw new ArgumentNullException(nameof(lookup));
        }

        return await HttpClient.PostAsync(RegulationApiEndpoints.RegulationLookupsUrl(context.TenantId, context.RegulationId),
            lookup);
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync<T>(RegulationServiceContext context, T lookup) where T : class, ILookup
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (lookup == null)
        {
            throw new ArgumentNullException(nameof(lookup));
        }

        await HttpClient.PutAsync(RegulationApiEndpoints.RegulationLookupsUrl(context.TenantId, context.RegulationId),
            lookup);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(RegulationServiceContext context, int lookupId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (lookupId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(lookupId));
        }

        await HttpClient.DeleteAsync(RegulationApiEndpoints.RegulationLookupsUrl(context.TenantId, context.RegulationId),
            lookupId);
    }
}