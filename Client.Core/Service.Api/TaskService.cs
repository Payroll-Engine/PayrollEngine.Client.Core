using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll task service</summary>
public class TaskService : Service, ITaskService
{
    /// <summary>Initializes a new instance of the <see cref="TaskService"/> class</summary>
    /// <param name="httpClient">The HTTP client</param>
    public TaskService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(TenantServiceContext context, Query query = null) where T : class, ITask
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(TenantApiEndpoints.TasksUrl(context.TenantId));
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
        var url = query.AppendQueryString(TenantApiEndpoints.TasksUrl(context.TenantId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(TenantServiceContext context, Query query = null) where T : class, ITask
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(TenantApiEndpoints.TasksUrl(context.TenantId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, int taskId) where T : class, ITask
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (taskId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(taskId));
        }

        return await HttpClient.GetAsync<T>(TenantApiEndpoints.TaskUrl(context.TenantId, taskId));
    }

    /// <inheritdoc/>
    public virtual async Task<T> CreateAsync<T>(TenantServiceContext context, T task) where T : class, ITask
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (task == null)
        {
            throw new ArgumentNullException(nameof(task));
        }

        return await HttpClient.PostAsync(TenantApiEndpoints.TasksUrl(context.TenantId), task);
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync<T>(TenantServiceContext context, T task) where T : class, ITask
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (task == null)
        {
            throw new ArgumentNullException(nameof(task));
        }

        await HttpClient.PutAsync(TenantApiEndpoints.TasksUrl(context.TenantId), task);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(TenantServiceContext context, int taskId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (taskId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(taskId));
        }

        await HttpClient.DeleteAsync(TenantApiEndpoints.TasksUrl(context.TenantId), taskId);
    }
}