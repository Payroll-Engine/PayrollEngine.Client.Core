using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll case service</summary>
public class CaseService : Service, ICaseService
{
    /// <summary>Initializes a new instance of the <see cref="CaseService"/> class</summary>
    /// <param name="httpClient">The HTTP client</param>
    public CaseService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(RegulationServiceContext context, Query query = null) where T : class, ICase
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationCasesUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(RegulationServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationCasesUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(RegulationServiceContext context, Query query = null) where T : class, ICase
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationCasesUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(RegulationServiceContext context, int caseId) where T : class, ICase
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (caseId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(caseId));
        }

        return await HttpClient.GetAsync<T>(RegulationApiEndpoints.RegulationCaseUrl(context.TenantId, context.RegulationId,
            caseId));
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(RegulationServiceContext context, string name) where T : class, ICase
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
        var uri = query.AppendQueryString(RegulationApiEndpoints.RegulationCasesUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<T> CreateAsync<T>(RegulationServiceContext context, T @case) where T : class, ICase
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (@case == null)
        {
            throw new ArgumentNullException(nameof(@case));
        }

        return await HttpClient.PostAsync(RegulationApiEndpoints.RegulationCasesUrl(context.TenantId, context.RegulationId),
            @case);
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync<T>(RegulationServiceContext context, T @case) where T : class, ICase
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (@case == null)
        {
            throw new ArgumentNullException(nameof(@case));
        }

        await HttpClient.PutAsync(RegulationApiEndpoints.RegulationCasesUrl(context.TenantId, context.RegulationId), @case);
    }
        
    /// <inheritdoc/>
    public virtual async Task RebuildAsync(RegulationServiceContext context, int caseId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (caseId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(caseId));
        }

        await HttpClient.PutAsync(RegulationApiEndpoints.RegulationCaseRebuildUrl(context.TenantId, context.RegulationId, caseId));
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(RegulationServiceContext context, int caseId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (caseId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(caseId));
        }

        await HttpClient.DeleteAsync(RegulationApiEndpoints.RegulationCasesUrl(context.TenantId, context.RegulationId),
            caseId);
    }
}