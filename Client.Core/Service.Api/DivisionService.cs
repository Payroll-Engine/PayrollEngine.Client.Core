using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll division service</summary>
public class DivisionService : ServiceBase, IDivisionService
{
    /// <summary>Initializes a new instance of the <see cref="DivisionService"/> class</summary>
    /// <param name="httpClient">The HTTP client</param>
    public DivisionService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(TenantServiceContext context, Query query = null) where T : class, IDivision
    {
        ArgumentNullException.ThrowIfNull(context);

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(TenantApiEndpoints.DivisionsUrl(context.TenantId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(TenantServiceContext context, Query query = null)
    {
        ArgumentNullException.ThrowIfNull(context);

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(TenantApiEndpoints.DivisionsUrl(context.TenantId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(TenantServiceContext context, Query query = null) where T : class, IDivision
    {
        ArgumentNullException.ThrowIfNull(context);

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(TenantApiEndpoints.DivisionsUrl(context.TenantId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, int divisionId) where T : class, IDivision
    {
        ArgumentNullException.ThrowIfNull(context);
        if (divisionId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(divisionId));
        }

        return await HttpClient.GetAsync<T>(TenantApiEndpoints.DivisionUrl(context.TenantId, divisionId));
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, string name) where T : class, IDivision
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        // query single item
        var query = QueryFactory.NewNameQuery(name);
        var uri = query.AppendQueryString(TenantApiEndpoints.DivisionsUrl(context.TenantId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<T> CreateAsync<T>(TenantServiceContext context, T division) where T : class, IDivision
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(division);

        return await HttpClient.PostAsync(TenantApiEndpoints.DivisionsUrl(context.TenantId), division);
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync<T>(TenantServiceContext context, T division) where T : class, IDivision
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(division);

        await HttpClient.PutAsync(TenantApiEndpoints.DivisionsUrl(context.TenantId), division);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(TenantServiceContext context, int divisionId)
    {
        ArgumentNullException.ThrowIfNull(context);
        if (divisionId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(divisionId));
        }

        await HttpClient.DeleteAsync(TenantApiEndpoints.DivisionsUrl(context.TenantId), divisionId);
    }
}