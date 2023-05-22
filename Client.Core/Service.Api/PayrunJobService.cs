using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll payrun job service</summary>
public class PayrunJobService : Service, IPayrunJobService
{
    /// <summary>Initializes a new instance of the <see cref="PayrunJobService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public PayrunJobService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(TenantServiceContext context, Query query = null) where T : class, IPayrunJob
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(PayrunApiEndpoints.PayrunJobsUrl(context.TenantId));
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
        var url = query.AppendQueryString(PayrunApiEndpoints.PayrunJobsUrl(context.TenantId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(TenantServiceContext context, Query query = null) where T : class, IPayrunJob
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(PayrunApiEndpoints.PayrunJobsUrl(context.TenantId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryEmployeePayrunJobsAsync<T>(TenantServiceContext context,
        int employeeId, Query query = null) where T : class, IPayrunJob
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (employeeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeId));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(PayrunApiEndpoints.PayrunJobEmployeeUrl(context.TenantId, employeeId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryEmployeePayrunJobsCountAsync(TenantServiceContext context,
        int employeeId, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (employeeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeId));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(PayrunApiEndpoints.PayrunJobEmployeeUrl(context.TenantId, employeeId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryEmployeePayrunJobsCountAsync<T>(TenantServiceContext context,
        int employeeId, Query query = null) where T : class, IPayrunJob
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (employeeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeId));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(PayrunApiEndpoints.PayrunJobEmployeeUrl(context.TenantId, employeeId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, int payrunJobId) where T : class, IPayrunJob
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrunJobId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunJobId));
        }

        return await HttpClient.GetAsync<T>(PayrunApiEndpoints.PayrunJobUrl(context.TenantId, payrunJobId));
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, string name) where T : class, IPayrunJob
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
        var uri = query.AppendQueryString(PayrunApiEndpoints.PayrunJobsUrl(context.TenantId));
        var result = await HttpClient.GetSingleAsync<T>(uri);
        return result;
    }

    /// <inheritdoc/>
    public virtual async Task<string> GetJobStatusAsync(TenantServiceContext context, int payrunJobId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrunJobId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunJobId));
        }

        return await HttpClient.GetAsync<string>(PayrunApiEndpoints.PayrunJobStatusUrl(context.TenantId, payrunJobId));
    }

    /// <inheritdoc/>
    public virtual async Task<T> StartJobAsync<T>(TenantServiceContext context, PayrunJobInvocation jobInvocation) where T : class, IPayrunJob
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (jobInvocation == null)
        {
            throw new ArgumentNullException(nameof(jobInvocation));
        }

        return await HttpClient.PostAsync<PayrunJobInvocation, T>(PayrunApiEndpoints.PayrunJobsUrl(context.TenantId),
            jobInvocation);
    }

    /// <inheritdoc/>
    public virtual async Task ChangeJobStatusAsync(TenantServiceContext context, int payrunJobId,
        PayrunJobStatus jobStatus, int userId, string reason, bool patchMode)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrunJobId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunJobId));
        }
        if (userId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(userId));
        }
        if (string.IsNullOrWhiteSpace(reason))
        {
            throw new ArgumentException(nameof(reason));
        }

        var url = PayrunApiEndpoints.PayrunJobStatusUrl(context.TenantId, payrunJobId)
            .AddQueryString(nameof(userId), userId)
            .AddQueryString(nameof(reason), reason)
            .AddQueryString(nameof(patchMode), patchMode);
        await HttpClient.PostAsync(url, jobStatus);
    }

    /// <inheritdoc/>
    public virtual async Task<T> CreateAsync<T>(TenantServiceContext context, T payrunJob) where T : class, IPayrunJob
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrunJob == null)
        {
            throw new ArgumentNullException(nameof(payrunJob));
        }

        return await HttpClient.PostAsync(PayrunApiEndpoints.PayrunJobsUrl(context.TenantId), payrunJob);
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync<T>(TenantServiceContext context, T payrunJob) where T : class, IPayrunJob
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrunJob == null)
        {
            throw new ArgumentNullException(nameof(payrunJob));
        }

        await HttpClient.PutAsync(PayrunApiEndpoints.PayrunJobsUrl(context.TenantId), payrunJob);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(TenantServiceContext context, int payrunJobId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrunJobId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunJobId));
        }

        await HttpClient.DeleteAsync(PayrunApiEndpoints.PayrunJobsUrl(context.TenantId), payrunJobId);
    }

    /// <inheritdoc/>
    public virtual async Task<string> GetAttributeAsync(TenantServiceContext context, int payrunJobId, string attributeName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrunJobId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunJobId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        return await HttpClient.GetAttributeAsync(
            PayrunApiEndpoints.PayrunJobAttributeUrl(context.TenantId, payrunJobId, attributeName));
    }

    /// <inheritdoc/>
    public virtual async Task SetAttributeAsync(TenantServiceContext context, int payrunJobId, string attributeName, string attributeValue)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrunJobId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunJobId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        await HttpClient.PostAttributeAsync(
            PayrunApiEndpoints.PayrunJobAttributeUrl(context.TenantId, payrunJobId, attributeName), attributeValue);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAttributeAsync(TenantServiceContext context, int payrunJobId, string attributeName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrunJobId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunJobId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        await HttpClient.DeleteAttributeAsync(PayrunApiEndpoints.PayrunJobAttributeUrl(context.TenantId, payrunJobId,
            attributeName));
    }
}