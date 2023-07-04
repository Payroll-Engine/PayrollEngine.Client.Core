using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll global case value service</summary>
public class GlobalCaseValueService : ServiceBase, IGlobalCaseValueService
{
    /// <summary>Initializes a new instance of the <see cref="GlobalCaseValueService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public GlobalCaseValueService(PayrollHttpClient httpClient) :
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
        var url = query.AppendQueryString(GlobalCaseApiEndpoints.GlobalCasesUrl(context.TenantId));
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
        var url = query.AppendQueryString(GlobalCaseApiEndpoints.GlobalCasesUrl(context.TenantId));
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
        var url = query.AppendQueryString(GlobalCaseApiEndpoints.GlobalCasesUrl(context.TenantId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, int globalCaseValueId) where T : class, ICaseValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (globalCaseValueId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(globalCaseValueId));
        }

        return await HttpClient.GetAsync<T>(GlobalCaseApiEndpoints.GlobalCaseUrl(context.TenantId, globalCaseValueId));
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

        var requestUri = GlobalCaseApiEndpoints.GlobalCaseSlotsUrl(context.TenantId);
        requestUri = requestUri.AddQueryString(nameof(caseFieldName), caseFieldName);
        return await HttpClient.GetAsync<IEnumerable<string>>(requestUri);
    }
}