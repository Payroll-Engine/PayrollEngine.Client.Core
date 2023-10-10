using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll wage type service</summary>
public class WageTypeService : ServiceBase, IWageTypeService
{
    /// <summary>Initializes a new instance of the <see cref="WageTypeService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public WageTypeService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> QueryAsync<T>(RegulationServiceContext context, Query query = null) where T : class, IWageType
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationWageTypesUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc />
    public virtual async Task<long> QueryCountAsync(RegulationServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationWageTypesUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc />
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(RegulationServiceContext context, Query query = null) where T : class, IWageType
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationWageTypesUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc />
    public virtual async Task<T> GetAsync<T>(RegulationServiceContext context, int wageTypeId) where T : class, IWageType
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (wageTypeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(wageTypeId));
        }

        return await HttpClient.GetAsync<T>( 
            RegulationApiEndpoints.RegulationWageTypeUrl(context.TenantId, context.RegulationId, wageTypeId));
    }

    /// <inheritdoc />
    public virtual async Task<T> GetAsync<T>(RegulationServiceContext context, decimal wageTypeNumber,
        CultureInfo culture) where T : class, IWageType
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // query single item
        var query = QueryFactory.NewEqualFilterQuery(nameof(wageTypeNumber), wageTypeNumber.ToString(culture));
        var uri = query.AppendQueryString(RegulationApiEndpoints.RegulationWageTypesUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc />
    public virtual async Task<T> GetAsync<T>(RegulationServiceContext context, string name) where T : class, IWageType
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
        var uri = query.AppendQueryString(RegulationApiEndpoints.RegulationWageTypesUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc />
    public virtual async Task<T> CreateAsync<T>(RegulationServiceContext context, T wageType) where T : class, IWageType
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (wageType == null)
        {
            throw new ArgumentNullException(nameof(wageType));
        }

        return await HttpClient.PostAsync(
            RegulationApiEndpoints.RegulationWageTypesUrl(context.TenantId, context.RegulationId), wageType);
    }

    /// <inheritdoc />
    public virtual async Task UpdateAsync<T>(RegulationServiceContext context, T wageType) where T : class, IWageType
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (wageType == null)
        {
            throw new ArgumentNullException(nameof(wageType));
        }

        await HttpClient.PutAsync(RegulationApiEndpoints.RegulationWageTypesUrl(context.TenantId, context.RegulationId),
            wageType);
    }

    /// <inheritdoc/>
    public virtual async Task RebuildAsync(RegulationServiceContext context, int wageTypeId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (wageTypeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(wageTypeId));
        }

        await HttpClient.PutAsync(RegulationApiEndpoints.RegulationWageTypeRebuildUrl(context.TenantId, context.RegulationId, wageTypeId));
    }

    /// <inheritdoc />
    public virtual async Task DeleteAsync(RegulationServiceContext context, int wageTypeId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (wageTypeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(wageTypeId));
        }

        await HttpClient.DeleteAsync(RegulationApiEndpoints.RegulationWageTypesUrl(context.TenantId, context.RegulationId),
            wageTypeId);
    }
}