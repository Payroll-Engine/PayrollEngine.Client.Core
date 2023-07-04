using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll payrun parameter service</summary>
public class PayrunParameterService : ServiceBase, IPayrunParameterService
{
    /// <summary>Initializes a new instance of the <see cref="PayrunParameterService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public PayrunParameterService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> QueryAsync<T>(PayrunServiceContext context, Query query = null) where T : class, IPayrunParameter
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(PayrunApiEndpoints.PayrunParametersUrl(context.TenantId, context.PayrunId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc />
    public virtual async Task<long> QueryCountAsync(PayrunServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(PayrunApiEndpoints.PayrunParametersUrl(context.TenantId, context.PayrunId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc />
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(PayrunServiceContext context, Query query = null) where T : class, IPayrunParameter
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(PayrunApiEndpoints.PayrunParametersUrl(context.TenantId, context.PayrunId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc />
    public virtual async Task<T> GetAsync<T>(PayrunServiceContext context, int parameterId) where T : class, IPayrunParameter
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (parameterId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(parameterId));
        }

        return await HttpClient.GetAsync<T>(PayrunApiEndpoints.PayrunParameterUrl(context.TenantId, context.PayrunId,
            parameterId));
    }

    /// <inheritdoc />
    public virtual async Task<T> GetAsync<T>(PayrunServiceContext context, string name) where T : class, IPayrunParameter
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
        var uri = query.AppendQueryString(PayrunApiEndpoints.PayrunParametersUrl(context.TenantId, context.PayrunId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc />
    public virtual async Task<T> CreateAsync<T>(PayrunServiceContext context, T parameter) where T : class, IPayrunParameter
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (parameter == null)
        {
            throw new ArgumentNullException(nameof(parameter));
        }

        return await HttpClient.PostAsync(PayrunApiEndpoints.PayrunParametersUrl(context.TenantId, context.PayrunId),
            parameter);
    }

    /// <inheritdoc />
    public virtual async Task UpdateAsync<T>(PayrunServiceContext context, T parameter) where T : class, IPayrunParameter
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (parameter == null)
        {
            throw new ArgumentNullException(nameof(parameter));
        }

        await HttpClient.PutAsync(PayrunApiEndpoints.PayrunParametersUrl(context.TenantId, context.PayrunId), parameter);
    }

    /// <inheritdoc />
    public virtual async Task DeleteAsync(PayrunServiceContext context, int parameterId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (parameterId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(parameterId));
        }

        await HttpClient.DeleteAsync(PayrunApiEndpoints.PayrunParametersUrl(context.TenantId, context.PayrunId),
            parameterId);
    }
}