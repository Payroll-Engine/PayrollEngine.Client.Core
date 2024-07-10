using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll layer service</summary>
public class PayrollLayerService : ServiceBase, IPayrollLayerService
{
    /// <summary>Initializes a new instance of the <see cref="PayrollLayerService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public PayrollLayerService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(PayrollServiceContext context, Query query = null) where T : class, IPayrollLayer
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(PayrollApiEndpoints.PayrollLayersUrl(context.TenantId, context.PayrollId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(PayrollServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(PayrollApiEndpoints.PayrollLayersUrl(context.TenantId, context.PayrollId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(PayrollServiceContext context, Query query = null) where T : class, IPayrollLayer
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(PayrollApiEndpoints.PayrollLayersUrl(context.TenantId, context.PayrollId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(PayrollServiceContext context, int payrollLayerId) where T : class, IPayrollLayer
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrollLayerId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrollLayerId));
        }

        return await HttpClient.GetAsync<T>(PayrollApiEndpoints.PayrollLayerUrl(context.TenantId, context.PayrollId,
            payrollLayerId));
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(PayrollServiceContext context, string identifier) where T : class, IPayrollLayer
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (string.IsNullOrWhiteSpace(identifier))
        {
            throw new ArgumentException(nameof(identifier));
        }

        // query single item
        var query = QueryFactory.NewIdentifierQuery(identifier);
        var uri = query.AppendQueryString(PayrollApiEndpoints.PayrollLayersUrl(context.TenantId, context.PayrollId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<T> CreateAsync<T>(PayrollServiceContext context, T payrollLayer) where T : class, IPayrollLayer
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrollLayer == null)
        {
            throw new ArgumentNullException(nameof(payrollLayer));
        }

        return await HttpClient.PostAsync(PayrollApiEndpoints.PayrollLayersUrl(context.TenantId, context.PayrollId),
            payrollLayer);
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync<T>(PayrollServiceContext context, T payrollLayer) where T : class, IPayrollLayer
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrollLayer == null)
        {
            throw new ArgumentNullException(nameof(payrollLayer));
        }

        await HttpClient.PutAsync(PayrollApiEndpoints.PayrollLayersUrl(context.TenantId, context.PayrollId), payrollLayer);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(PayrollServiceContext context, int payrollLayerId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrollLayerId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrollLayerId));
        }

        await HttpClient.DeleteAsync(PayrollApiEndpoints.PayrollLayersUrl(context.TenantId, context.PayrollId),
            payrollLayerId);
    }

    /// <inheritdoc/>
    public virtual async Task<string> GetAttributeAsync(PayrollServiceContext context, int payrollLayerId, string attributeName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrollLayerId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrollLayerId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        return await HttpClient.GetAttributeAsync(
            PayrollApiEndpoints.PayrollLayerAttributeUrl(context.TenantId, context.PayrollId, payrollLayerId, attributeName));
    }

    /// <inheritdoc/>
    public virtual async Task SetAttributeAsync(PayrollServiceContext context, int payrollLayerId, string attributeName, string attributeValue)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrollLayerId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrollLayerId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        await HttpClient.PostAttributeAsync(
            PayrollApiEndpoints.PayrollLayerAttributeUrl(context.TenantId, context.PayrollId, payrollLayerId, attributeName), attributeValue);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAttributeAsync(PayrollServiceContext context, int payrollLayerId, string attributeName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrollLayerId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrollLayerId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        await HttpClient.DeleteAttributeAsync(
            PayrollApiEndpoints.PayrollLayerAttributeUrl(context.TenantId, context.PayrollId, payrollLayerId, attributeName));
    }
}