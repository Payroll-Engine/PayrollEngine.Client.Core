using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll case field service</summary>
public class CaseFieldService : ServiceBase, ICaseFieldService
{
    /// <summary>Initializes a new instance of the <see cref="CaseFieldService"/> class</summary>
    /// <param name="httpClient">The HTTP client</param>
    public CaseFieldService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(CaseServiceContext context, Query query = null) where T : class, ICaseField
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationCaseFieldsUrl(context.TenantId, context.RegulationId, context.CaseId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(CaseServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationCaseFieldsUrl(context.TenantId, context.RegulationId, context.CaseId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(CaseServiceContext context, Query query = null) where T : class, ICaseField
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationCaseFieldsUrl(context.TenantId, context.RegulationId, context.CaseId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(CaseServiceContext context, int caseFieldId) where T : class, ICaseField
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (caseFieldId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(caseFieldId));
        }

        return await HttpClient.GetAsync<T>(RegulationApiEndpoints.RegulationCaseFieldUrl(context.TenantId,
            context.RegulationId, context.CaseId, caseFieldId));
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(CaseServiceContext context, string name) where T : class, ICaseField
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
        var uri = query.AppendQueryString(RegulationApiEndpoints.RegulationCaseFieldsUrl(context.TenantId, context.RegulationId, context.CaseId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<T> CreateAsync<T>(CaseServiceContext context, T caseField) where T : class, ICaseField
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (caseField == null)
        {
            throw new ArgumentNullException(nameof(caseField));
        }

        return await HttpClient.PostAsync(
            RegulationApiEndpoints.RegulationCaseFieldsUrl(context.TenantId, context.RegulationId, context.CaseId),
            caseField);
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync<T>(CaseServiceContext context, T caseField) where T : class, ICaseField
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (caseField == null)
        {
            throw new ArgumentNullException(nameof(caseField));
        }

        await HttpClient.PutAsync(
            RegulationApiEndpoints.RegulationCaseFieldsUrl(context.TenantId, context.RegulationId, context.CaseId),
            caseField);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(CaseServiceContext context, int caseFieldId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (caseFieldId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(caseFieldId));
        }

        await HttpClient.DeleteAsync(
            RegulationApiEndpoints.RegulationCaseFieldsUrl(context.TenantId, context.RegulationId, context.CaseId),
            caseFieldId);
    }
}