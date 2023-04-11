using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll case relation service</summary>
public class CaseRelationService : Service, ICaseRelationService
{
    /// <summary>Initializes a new instance of the <see cref="CaseRelationService"/> class</summary>
    /// <param name="httpClient">The HTTP client</param>
    public CaseRelationService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(RegulationServiceContext context, Query query = null) where T : class, ICaseRelation
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationCaseRelationsUrl(context.TenantId, context.RegulationId));
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
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationCaseRelationsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(RegulationServiceContext context, Query query = null) where T : class, ICaseRelation
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationCaseRelationsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(RegulationServiceContext context, int caseRelationId) where T : class, ICaseRelation
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (caseRelationId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(caseRelationId));
        }

        return await HttpClient.GetAsync<T>(
            RegulationApiEndpoints.RegulationCaseRelationUrl(context.TenantId, context.RegulationId, caseRelationId));
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(RegulationServiceContext context, string sourceCaseName, string targetCaseName,
        string sourceCaseSlot = null, string targetCaseSlot = null) where T : class, ICaseRelation
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (string.IsNullOrWhiteSpace(sourceCaseName))
        {
            throw new ArgumentException(nameof(sourceCaseName));
        }
        if (string.IsNullOrWhiteSpace(targetCaseName))
        {
            throw new ArgumentException(nameof(targetCaseName));
        }

        // query case relation by case names and slots
        var queryValues = new Dictionary<string, object>
        {
            {nameof(sourceCaseName), sourceCaseName},
            {nameof(targetCaseName), targetCaseName}
        };
        if (!string.IsNullOrWhiteSpace(sourceCaseSlot))
        {
            queryValues.Add(nameof(sourceCaseSlot), sourceCaseSlot);
        }
        if (!string.IsNullOrWhiteSpace(targetCaseSlot))
        {
            queryValues.Add(nameof(targetCaseSlot), targetCaseSlot);
        }
        var query = QueryFactory.NewEqualFilterQuery(queryValues);
        var uri = query.AppendQueryString(RegulationApiEndpoints.RegulationCaseRelationsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<T> CreateAsync<T>(RegulationServiceContext context, T caseRelation) where T : class, ICaseRelation
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (caseRelation == null)
        {
            throw new ArgumentNullException(nameof(caseRelation));
        }

        return await HttpClient.PostAsync(
            RegulationApiEndpoints.RegulationCaseRelationsUrl(context.TenantId, context.RegulationId), caseRelation);
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync<T>(RegulationServiceContext context, T caseRelation) where T : class, ICaseRelation
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (caseRelation == null)
        {
            throw new ArgumentNullException(nameof(caseRelation));
        }

        await HttpClient.PutAsync(RegulationApiEndpoints.RegulationCaseRelationsUrl(context.TenantId, context.RegulationId),
            caseRelation);
    }

    /// <inheritdoc/>
    public virtual async Task RebuildAsync(RegulationServiceContext context, int caseRelationId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (caseRelationId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(caseRelationId));
        }

        await HttpClient.PutAsync(RegulationApiEndpoints.RegulationCaseRelationRebuildUrl(context.TenantId, context.RegulationId, caseRelationId));
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(RegulationServiceContext context, int caseRelationId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (caseRelationId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(caseRelationId));
        }

        await HttpClient.DeleteAsync(
            RegulationApiEndpoints.RegulationCaseRelationsUrl(context.TenantId, context.RegulationId), caseRelationId);
    }
}