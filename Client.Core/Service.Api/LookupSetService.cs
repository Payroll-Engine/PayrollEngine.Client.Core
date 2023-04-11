using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll lookup service</summary>
public class LookupSetService : Service, ILookupSetService
{
    /// <summary>Initializes a new instance of the <see cref="LookupSetService"/> class</summary>
    /// <param name="httpClient">The HTTP client</param>
    public LookupSetService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(RegulationServiceContext context, Query query = null) where T : class, ILookupSet
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationLookupSetsUrl(context.TenantId, context.RegulationId));
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
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationLookupSetsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(RegulationServiceContext context, Query query = null) where T : class, ILookupSet
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationLookupSetsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(RegulationServiceContext context, int lookupSetId) where T : class, ILookupSet
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (lookupSetId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(lookupSetId));
        }

        return await HttpClient.GetAsync<T>(
            RegulationApiEndpoints.RegulationLookupSetUrl(context.TenantId, context.RegulationId, lookupSetId));
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(RegulationServiceContext context, string name) where T : class, ILookupSet
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
        var uri = query.AppendQueryString(RegulationApiEndpoints.RegulationLookupSetsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<T> CreateAsync<T>(RegulationServiceContext context, T lookup) where T : class, ILookupSet
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (lookup == null)
        {
            throw new ArgumentNullException(nameof(lookup));
        }

        await CreateAsync(context, new List<T> { lookup });
        return lookup;
    }

    /// <inheritdoc/>
    public virtual async Task CreateAsync<T>(RegulationServiceContext context, IEnumerable<T> lookupSets) where T : class, ILookupSet
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (lookupSets == null)
        {
            throw new ArgumentNullException(nameof(lookupSets));
        }

        await HttpClient.PostAsync(RegulationApiEndpoints.RegulationLookupSetsUrl(context.TenantId, context.RegulationId),
            lookupSets);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(RegulationServiceContext context, int lookupSetId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (lookupSetId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(lookupSetId));
        }

        await HttpClient.DeleteAsync(RegulationApiEndpoints.RegulationLookupSetsUrl(context.TenantId, context.RegulationId),
            lookupSetId);
    }
}