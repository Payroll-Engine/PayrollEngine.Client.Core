using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll payrun job service</summary>
public class PayrunJobService : ServiceBase, IPayrunJobService
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
        ArgumentNullException.ThrowIfNull(context);

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(PayrunApiEndpoints.PayrunJobsUrl(context.TenantId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(TenantServiceContext context, Query query = null)
    {
        ArgumentNullException.ThrowIfNull(context);

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(PayrunApiEndpoints.PayrunJobsUrl(context.TenantId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(TenantServiceContext context, Query query = null) where T : class, IPayrunJob
    {
        ArgumentNullException.ThrowIfNull(context);

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(PayrunApiEndpoints.PayrunJobsUrl(context.TenantId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryEmployeePayrunJobsAsync<T>(TenantServiceContext context,
        int employeeId, Query query = null) where T : class, IPayrunJob
    {
        ArgumentNullException.ThrowIfNull(context);
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
        ArgumentNullException.ThrowIfNull(context);
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
        ArgumentNullException.ThrowIfNull(context);
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
        ArgumentNullException.ThrowIfNull(context);
        if (payrunJobId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunJobId));
        }

        return await HttpClient.GetAsync<T>(PayrunApiEndpoints.PayrunJobUrl(context.TenantId, payrunJobId));
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, string name) where T : class, IPayrunJob
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        // query single item
        var query = QueryFactory.NewNameQuery(name);
        var uri = query.AppendQueryString(PayrunApiEndpoints.PayrunJobsUrl(context.TenantId));
        var result = await HttpClient.GetSingleAsync<T>(uri);
        return result;
    }

    /// <inheritdoc/>
    public virtual async Task<string> GetJobStatusAsync(TenantServiceContext context, int payrunJobId)
    {
        ArgumentNullException.ThrowIfNull(context);
        if (payrunJobId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunJobId));
        }

        return await HttpClient.GetAsync<string>(PayrunApiEndpoints.PayrunJobStatusUrl(context.TenantId, payrunJobId));
    }

    /// <inheritdoc/>
    public virtual async Task<T> PreviewJobAsync<T>(TenantServiceContext context, PayrunJobInvocation jobInvocation) where T : class, IPayrollResultSet
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(jobInvocation);

        return await HttpClient.PostAsync<PayrunJobInvocation, T>(PayrunApiEndpoints.PayrunJobPreviewUrl(context.TenantId),
            jobInvocation);
    }

    /// <inheritdoc/>
    public virtual async Task<T> StartJobAsync<T>(TenantServiceContext context, PayrunJobInvocation jobInvocation) where T : class, IPayrunJob
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(jobInvocation);

        return await HttpClient.PostAsync<PayrunJobInvocation, T>(PayrunApiEndpoints.PayrunJobsUrl(context.TenantId),
            jobInvocation);
    }

    /// <inheritdoc/>
    public virtual async Task<int> ImportJobSetsAsync<T>(TenantServiceContext context, IEnumerable<T> jobSets)
        where T : class, IPayrunJob
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(jobSets);

        var url = PayrunApiEndpoints.PayrunJobSetsImportUrl(context.TenantId);
        return await HttpClient.PostAsync<IEnumerable<T>, int>(url, jobSets);
    }

    /// <inheritdoc/>
    public virtual async Task ChangeJobStatusAsync(TenantServiceContext context, int payrunJobId,
        PayrunJobStatus jobStatus, int userId, string reason, bool patchMode)
    {
        ArgumentNullException.ThrowIfNull(context);
        if (payrunJobId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunJobId));
        }
        if (userId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(userId));
        }
        ArgumentException.ThrowIfNullOrWhiteSpace(reason);

        var url = PayrunApiEndpoints.PayrunJobStatusUrl(context.TenantId, payrunJobId)
            .AddQueryString(nameof(userId), userId)
            .AddQueryString(nameof(reason), reason)
            .AddQueryString(nameof(patchMode), patchMode);
        await HttpClient.PostAsync(url, jobStatus);
    }

    /// <inheritdoc/>
    public virtual async Task<T> CreateAsync<T>(TenantServiceContext context, T payrunJob) where T : class, IPayrunJob
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(payrunJob);

        return await HttpClient.PostAsync(PayrunApiEndpoints.PayrunJobsUrl(context.TenantId), payrunJob);
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync<T>(TenantServiceContext context, T payrunJob) where T : class, IPayrunJob
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(payrunJob);

        await HttpClient.PutAsync(PayrunApiEndpoints.PayrunJobsUrl(context.TenantId), payrunJob);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(TenantServiceContext context, int payrunJobId)
    {
        ArgumentNullException.ThrowIfNull(context);
        if (payrunJobId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunJobId));
        }

        await HttpClient.DeleteAsync(PayrunApiEndpoints.PayrunJobsUrl(context.TenantId), payrunJobId);
    }

    /// <inheritdoc/>
    public virtual async Task<string> GetAttributeAsync(TenantServiceContext context, int payrunJobId, string attributeName)
    {
        ArgumentNullException.ThrowIfNull(context);
        if (payrunJobId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunJobId));
        }
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);

        return await HttpClient.GetAttributeAsync(
            PayrunApiEndpoints.PayrunJobAttributeUrl(context.TenantId, payrunJobId, attributeName));
    }

    /// <inheritdoc/>
    public virtual async Task SetAttributeAsync(TenantServiceContext context, int payrunJobId, string attributeName, string attributeValue)
    {
        ArgumentNullException.ThrowIfNull(context);
        if (payrunJobId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunJobId));
        }
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);

        await HttpClient.PostAttributeAsync(
            PayrunApiEndpoints.PayrunJobAttributeUrl(context.TenantId, payrunJobId, attributeName), attributeValue);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAttributeAsync(TenantServiceContext context, int payrunJobId, string attributeName)
    {
        ArgumentNullException.ThrowIfNull(context);
        if (payrunJobId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunJobId));
        }
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);

        await HttpClient.DeleteAttributeAsync(PayrunApiEndpoints.PayrunJobAttributeUrl(context.TenantId, payrunJobId,
            attributeName));
    }
}