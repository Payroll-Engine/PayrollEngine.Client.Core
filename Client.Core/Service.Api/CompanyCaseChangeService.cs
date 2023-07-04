using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll company case change service</summary>
public class CompanyCaseChangeService : ServiceBase, ICompanyCaseChangeService
{
    /// <summary>Initializes a new instance of the <see cref="CompanyCaseChangeService"/> class</summary>
    /// <param name="httpClient">The HTTP client</param>
    public CompanyCaseChangeService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(TenantServiceContext context, CaseChangeQuery query = null) where T : class, ICaseChange
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        var url = query.BuildQueryString(CompanyCaseApiEndpoints.CompanyCaseChangesUrl(context.TenantId), QueryResultType.Items);
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(TenantServiceContext context, CaseChangeQuery query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        var url = query.BuildQueryString(CompanyCaseApiEndpoints.CompanyCaseChangesUrl(context.TenantId), QueryResultType.Count);
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(TenantServiceContext context, CaseChangeQuery query = null) where T : class, ICaseChange
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        var url = query.BuildQueryString(CompanyCaseApiEndpoints.CompanyCaseChangesUrl(context.TenantId), QueryResultType.ItemsWithCount);
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryValuesAsync<T>(TenantServiceContext context, CaseChangeQuery query = null) where T : class, ICaseChangeCaseValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        var url = query.BuildQueryString(CompanyCaseApiEndpoints.CompanyCaseChangesValuesUrl(context.TenantId), QueryResultType.Items);
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryValuesCountAsync(TenantServiceContext context, CaseChangeQuery query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        var url = query.BuildQueryString(CompanyCaseApiEndpoints.CompanyCaseChangesValuesUrl(context.TenantId), QueryResultType.Count);
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryValuesResultAsync<T>(TenantServiceContext context, CaseChangeQuery query = null) where T : class, ICaseChangeCaseValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        var url = query.BuildQueryString(CompanyCaseApiEndpoints.CompanyCaseChangesValuesUrl(context.TenantId), QueryResultType.ItemsWithCount);
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, int caseChangeId) where T : class, ICaseChange
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (caseChangeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(caseChangeId));
        }
        return await HttpClient.GetAsync<T>(CompanyCaseApiEndpoints.CompanyCaseChangeUrl(context.TenantId, caseChangeId));
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> GetAsync<T>(TenantServiceContext context, CaseChangeQuery query = null)
        where T : class, ICaseChange
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var url = query.BuildQueryString(CompanyCaseApiEndpoints.CompanyCaseChangesUrl(context.TenantId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }
}