using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll employee case document service</summary>
public class EmployeeCaseDocumentService : ServiceBase, IEmployeeCaseDocumentService
{
    /// <summary>Initializes a new instance of the <see cref="EmployeeCaseDocumentService"/> class</summary>
    /// <param name="httpClient">The HTTP client</param>
    public EmployeeCaseDocumentService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(EmployeeCaseValueServiceContext context, Query query = null) where T : class, ICaseDocument
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new CaseValueQuery();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(EmployeeCaseApiEndpoints.EmployeeCaseDocumentsUrl(context.TenantId, context.EmployeeId, context.CaseValueId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(EmployeeCaseValueServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new CaseValueQuery();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(EmployeeCaseApiEndpoints.EmployeeCaseDocumentsUrl(context.TenantId, context.EmployeeId, context.CaseValueId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(EmployeeCaseValueServiceContext context, Query query = null)
        where T : class, ICaseDocument
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new CaseValueQuery();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(EmployeeCaseApiEndpoints.EmployeeCaseDocumentsUrl(context.TenantId, context.EmployeeId, context.CaseValueId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(EmployeeCaseValueServiceContext context, int documentId) where T : class, ICaseDocument
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (documentId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(documentId));
        }

        return await HttpClient.GetAsync<T>(EmployeeCaseApiEndpoints.EmployeeCaseDocumentUrl(context.TenantId, context.EmployeeId, context.CaseValueId,
            documentId));
    }
}