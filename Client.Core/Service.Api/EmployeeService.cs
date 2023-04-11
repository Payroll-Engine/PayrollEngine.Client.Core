using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll employee service</summary>
public class EmployeeService : Service, IEmployeeService
{
    /// <summary>Initializes a new instance of the <see cref="EmployeeService"/> class</summary>
    /// <param name="httpClient">The HTTP client</param>
    public EmployeeService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(TenantServiceContext context, DivisionQuery query = null) where T : class, IEmployee
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(EmployeeCaseApiEndpoints.EmployeesUrl(context.TenantId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(TenantServiceContext context, DivisionQuery query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(EmployeeCaseApiEndpoints.EmployeesUrl(context.TenantId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(TenantServiceContext context, DivisionQuery query = null) where T : class, IEmployee
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(EmployeeCaseApiEndpoints.EmployeesUrl(context.TenantId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, int employeeId) where T : class, IEmployee
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (employeeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeId));
        }

        return await HttpClient.GetAsync<T>(EmployeeCaseApiEndpoints.EmployeeUrl(context.TenantId, employeeId));
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, string identifier) where T : class, IEmployee
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
        var uri = query.AppendQueryString(EmployeeCaseApiEndpoints.EmployeesUrl(context.TenantId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<T> CreateAsync<T>(TenantServiceContext context, T employee) where T : class, IEmployee
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee));
        }

        return await HttpClient.PostAsync(EmployeeCaseApiEndpoints.EmployeesUrl(context.TenantId), employee);
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync<T>(TenantServiceContext context, T employee) where T : class, IEmployee
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee));
        }

        await HttpClient.PutAsync(EmployeeCaseApiEndpoints.EmployeesUrl(context.TenantId), employee);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(TenantServiceContext context, int employeeId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (employeeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeId));
        }

        await HttpClient.DeleteAsync(EmployeeCaseApiEndpoints.EmployeesUrl(context.TenantId), employeeId);
    }

    /// <inheritdoc/>
    public virtual async Task<string> GetAttributeAsync(TenantServiceContext context, int employeeId, string attributeName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (employeeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        return await HttpClient.GetAttributeAsync(EmployeeCaseApiEndpoints.EmployeeAttributeUrl(context.TenantId, employeeId,
            attributeName));
    }

    /// <inheritdoc/>
    public virtual async Task SetAttributeAsync(TenantServiceContext context, int employeeId, string attributeName, string attributeValue)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (employeeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        await HttpClient.PostAttributeAsync(EmployeeCaseApiEndpoints.EmployeeAttributeUrl(context.TenantId, employeeId,
            attributeName), attributeValue);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAttributeAsync(TenantServiceContext context, int employeeId, string attributeName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (employeeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        await HttpClient.DeleteAttributeAsync(EmployeeCaseApiEndpoints.EmployeeAttributeUrl(context.TenantId, employeeId, attributeName));
    }
}