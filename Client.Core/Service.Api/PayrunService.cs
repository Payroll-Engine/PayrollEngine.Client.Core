using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll payrun service</summary>
public class PayrunService : ServiceBase, IPayrunService
{
    /// <summary>Initializes a new instance of the <see cref="PayrunService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public PayrunService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(TenantServiceContext context, Query query = null) where T : class, IPayrun
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(PayrunApiEndpoints.PayrunsUrl(context.TenantId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(TenantServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(PayrunApiEndpoints.PayrunsUrl(context.TenantId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(TenantServiceContext context, Query query = null) where T : class, IPayrun
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(PayrunApiEndpoints.PayrunsUrl(context.TenantId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, int payrunId) where T : class, IPayrun
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrunId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunId));
        }

        return await HttpClient.GetAsync<T>(PayrunApiEndpoints.PayrunUrl(context.TenantId, payrunId));
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, string name) where T : class, IPayrun
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
        var uri = query.AppendQueryString(PayrunApiEndpoints.PayrunsUrl(context.TenantId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<T> CreateAsync<T>(TenantServiceContext context, T payrun) where T : class, IPayrun
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrun == null)
        {
            throw new ArgumentNullException(nameof(payrun));
        }

        return await HttpClient.PostAsync(PayrunApiEndpoints.PayrunsUrl(context.TenantId), payrun);
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync<T>(TenantServiceContext context, T payrun) where T : class, IPayrun
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrun == null)
        {
            throw new ArgumentNullException(nameof(payrun));
        }

        await HttpClient.PutAsync(PayrunApiEndpoints.PayrunsUrl(context.TenantId), payrun);
    }

    /// <inheritdoc/>
    public virtual async Task RebuildAsync(TenantServiceContext context, int payrunId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrunId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunId));
        }

        await HttpClient.PutAsync(PayrunApiEndpoints.PayrunRebuildUrl(context.TenantId, payrunId));
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(TenantServiceContext context, int payrunId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrunId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunId));
        }

        await HttpClient.DeleteAsync(PayrunApiEndpoints.PayrunsUrl(context.TenantId), payrunId);
    }
}