using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll employee case value service</summary>
public class EmployeeCaseValueService : ServiceBase, IEmployeeCaseValueService
{
    /// <summary>Initializes a new instance of the <see cref="EmployeeCaseValueService"/> class</summary>
    /// <param name="httpClient">The HTTP client</param>
    public EmployeeCaseValueService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(EmployeeServiceContext context, CaseValueQuery query = null) where T : class, ICaseValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(EmployeeCaseApiEndpoints.EmployeeCasesUrl(context.TenantId, context.EmployeeId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(EmployeeServiceContext context, CaseValueQuery query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(EmployeeCaseApiEndpoints.EmployeeCasesUrl(context.TenantId, context.EmployeeId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(EmployeeServiceContext context, CaseValueQuery query = null)
        where T : class, ICaseValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(EmployeeCaseApiEndpoints.EmployeeCasesUrl(context.TenantId, context.EmployeeId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(EmployeeServiceContext context, int employeeCaseValueId) where T : class, ICaseValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (employeeCaseValueId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeCaseValueId));
        }

        return await HttpClient.GetAsync<T>(EmployeeCaseApiEndpoints.EmployeeCaseUrl(context.TenantId, context.EmployeeId,
            employeeCaseValueId));
    }

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<string>> GetCaseValueSlotsAsync(EmployeeServiceContext context, string caseFieldName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (string.IsNullOrWhiteSpace(caseFieldName))
        {
            throw new ArgumentException(nameof(caseFieldName));
        }

        var requestUri = EmployeeCaseApiEndpoints.EmployeeCaseSlotsUrl(context.TenantId, context.EmployeeId);
        requestUri = requestUri.AddQueryString(nameof(caseFieldName), caseFieldName);
        return await HttpClient.GetAsync<IEnumerable<string>>(requestUri);
    }
}