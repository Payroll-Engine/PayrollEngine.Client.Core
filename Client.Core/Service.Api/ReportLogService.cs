using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll report log service</summary>
public class ReportLogService : Service, IReportLogService
{
    /// <summary>Initializes a new instance of the <see cref="ReportLogService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public ReportLogService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> QueryAsync<T>(TenantServiceContext context, Query query = null) where T : class, IReportLog
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(TenantApiEndpoints.ReportLogsUrl(context.TenantId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc />
    public virtual async Task<long> QueryCountAsync(TenantServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(TenantApiEndpoints.ReportLogsUrl(context.TenantId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc />
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(TenantServiceContext context, Query query = null) where T : class, IReportLog
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(TenantApiEndpoints.ReportLogsUrl(context.TenantId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc />
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, int logId) where T : class, IReportLog
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (logId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(logId));
        }

        return await HttpClient.GetAsync<T>(TenantApiEndpoints.ReportLogUrl(context.TenantId, logId));
    }

    /// <inheritdoc />
    public virtual async Task<T> CreateAsync<T>(TenantServiceContext context, T log) where T : class, IReportLog
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (log == null)
        {
            throw new ArgumentNullException(nameof(log));
        }

        return await HttpClient.PostAsync(TenantApiEndpoints.ReportLogsUrl(context.TenantId), log);
    }

    /// <inheritdoc />
    public virtual Task UpdateAsync<T>(TenantServiceContext context, T log) where T : class, IReportLog =>
        throw new NotSupportedException("Update of report log is not supported");

    /// <inheritdoc />
    public virtual async Task DeleteAsync(TenantServiceContext context, int logId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (logId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(logId));
        }

        await HttpClient.DeleteAsync(TenantApiEndpoints.ReportLogsUrl(context.TenantId), logId);
    }
}