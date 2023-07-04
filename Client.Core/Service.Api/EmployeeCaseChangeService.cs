using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll employee case change service</summary>
public class EmployeeCaseChangeService : ServiceBase, IEmployeeCaseChangeService
{
    /// <summary>Initializes a new instance of the <see cref="EmployeeCaseChangeService"/> class</summary>
    /// <param name="httpClient">The HTTP client</param>
    public EmployeeCaseChangeService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(EmployeeServiceContext context, CaseChangeQuery query = null) where T : class, ICaseChange
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        var url = query.BuildQueryString(EmployeeCaseApiEndpoints.EmployeeCaseChangesUrl(context.TenantId, context.EmployeeId), QueryResultType.Items);
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(EmployeeServiceContext context, CaseChangeQuery query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        var url = query.BuildQueryString(EmployeeCaseApiEndpoints.EmployeeCaseChangesUrl(context.TenantId, context.EmployeeId), QueryResultType.Count);
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(EmployeeServiceContext context, CaseChangeQuery query = null) where T : class, ICaseChange
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        var url = query.BuildQueryString(EmployeeCaseApiEndpoints.EmployeeCaseChangesUrl(context.TenantId, context.EmployeeId), QueryResultType.ItemsWithCount);
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryValuesAsync<T>(EmployeeServiceContext context, CaseChangeQuery query = null) where T : class, ICaseChangeCaseValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        var url = query.BuildQueryString(EmployeeCaseApiEndpoints.EmployeeCaseChangesValuesUrl(context.TenantId, context.EmployeeId), QueryResultType.Items);
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryValuesCountAsync(EmployeeServiceContext context, CaseChangeQuery query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        var url = query.BuildQueryString(EmployeeCaseApiEndpoints.EmployeeCaseChangesValuesUrl(context.TenantId, context.EmployeeId), QueryResultType.Count);
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryValuesResultAsync<T>(EmployeeServiceContext context, CaseChangeQuery query = null) where T : class, ICaseChangeCaseValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        var url = query.BuildQueryString(EmployeeCaseApiEndpoints.EmployeeCaseChangesValuesUrl(context.TenantId, context.EmployeeId), QueryResultType.ItemsWithCount);
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(EmployeeServiceContext context, int caseChangeId) where T : class, ICaseChange
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (caseChangeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(caseChangeId));
        }

        return await HttpClient.GetAsync<T>(
            EmployeeCaseApiEndpoints.EmployeeCaseChangeUrl(context.TenantId, context.EmployeeId, caseChangeId));
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> GetAsync<T>(EmployeeServiceContext context, CaseChangeQuery query = null)
        where T : class, ICaseChange
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var url = query.BuildQueryString(EmployeeCaseApiEndpoints.EmployeeCaseChangesUrl(context.TenantId, context.EmployeeId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }
}