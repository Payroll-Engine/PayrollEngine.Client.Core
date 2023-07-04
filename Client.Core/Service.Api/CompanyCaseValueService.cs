using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll company case value service</summary>
public class CompanyCaseValueService : ServiceBase, ICompanyCaseValueService
{
    /// <summary>Initializes a new instance of the <see cref="CompanyCaseValueService"/> class</summary>
    /// <param name="httpClient">The HTTP client</param>
    public CompanyCaseValueService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(TenantServiceContext context, CaseValueQuery query = null) where T : class, ICaseValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(CompanyCaseApiEndpoints.CompanyCasesUrl(context.TenantId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(TenantServiceContext context, CaseValueQuery query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(CompanyCaseApiEndpoints.CompanyCasesUrl(context.TenantId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(TenantServiceContext context, CaseValueQuery query = null) where T : class, ICaseValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(CompanyCaseApiEndpoints.CompanyCasesUrl(context.TenantId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, int companyCaseValueId) where T : class, ICaseValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (companyCaseValueId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(companyCaseValueId));
        }

        return await HttpClient.GetAsync<T>(CompanyCaseApiEndpoints.CompanyCaseUrl(context.TenantId, companyCaseValueId));
    }

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<string>> GetCaseValueSlotsAsync(TenantServiceContext context, string caseFieldName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (string.IsNullOrWhiteSpace(caseFieldName))
        {
            throw new ArgumentException(nameof(caseFieldName));
        }

        var requestUri = CompanyCaseApiEndpoints.CompanyCaseSlotsUrl(context.TenantId);
        requestUri = requestUri.AddQueryString(nameof(caseFieldName), caseFieldName);
        return await HttpClient.GetAsync<IEnumerable<string>>(requestUri);
    }
}