using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll lookup value service</summary>
public class LookupValueService : Service, ILookupValueService
{
    /// <summary>Initializes a new instance of the <see cref="LookupValueService"/> class</summary>
    /// <param name="httpClient">The HTTP client</param>
    public LookupValueService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(LookupServiceContext context, Query query = null) where T : class, ILookupValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationLookupValuesUrl(context.TenantId, context.RegulationId, context.LookupId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(LookupServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationLookupValuesUrl(context.TenantId, context.RegulationId, context.LookupId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(LookupServiceContext context, Query query = null) where T : class, ILookupValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationLookupValuesUrl(context.TenantId, context.RegulationId, context.LookupId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(LookupServiceContext context, int lookupValueId) where T : class, ILookupValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (lookupValueId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(lookupValueId));
        }

        return await HttpClient.GetAsync<T>(RegulationApiEndpoints.RegulationLookupValueUrl(context.TenantId,
            context.RegulationId, context.LookupId, lookupValueId));
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> GetLookupValuesDataAsync<T>(LookupServiceContext context,
        Language? language = null) where T : LookupValueData
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        var uri = RegulationApiEndpoints.RegulationLookupsValuesDataUrl(context.TenantId, 
                context.RegulationId, context.LookupId)
            .AddQueryString(nameof(language), language);
        return await HttpClient.GetCollectionAsync<T>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<T> CreateAsync<T>(LookupServiceContext context, T lookupValue) where T : class, ILookupValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (lookupValue == null)
        {
            throw new ArgumentNullException(nameof(lookupValue));
        }

        return await HttpClient.PostAsync(
            RegulationApiEndpoints.RegulationLookupValuesUrl(context.TenantId, context.RegulationId, context.LookupId),
            lookupValue);
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync<T>(LookupServiceContext context, T lookupValue) where T : class, ILookupValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (lookupValue == null)
        {
            throw new ArgumentNullException(nameof(lookupValue));
        }

        await HttpClient.PutAsync(
            RegulationApiEndpoints.RegulationLookupValuesUrl(context.TenantId, context.RegulationId, context.LookupId),
            lookupValue);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(LookupServiceContext context, int lookupValueId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (lookupValueId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(lookupValueId));
        }

        await HttpClient.DeleteAsync(
            RegulationApiEndpoints.RegulationLookupValuesUrl(context.TenantId, context.RegulationId, context.LookupId),
            lookupValueId);
    }
}