using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll regulation service</summary>
public class RegulationService : ServiceBase, IRegulationService
{
    /// <summary>Initializes a new instance of the <see cref="RegulationService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public RegulationService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(TenantServiceContext context, Query query = null) where T : class, IRegulation
    {
        ArgumentNullException.ThrowIfNull(context);

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationsUrl(context.TenantId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(TenantServiceContext context, Query query = null)
    {
        ArgumentNullException.ThrowIfNull(context);

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationsUrl(context.TenantId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(TenantServiceContext context, Query query = null) where T : class, IRegulation
    {
        ArgumentNullException.ThrowIfNull(context);

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationsUrl(context.TenantId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, int regulationId) where T : class, IRegulation
    {
        ArgumentNullException.ThrowIfNull(context);
        if (regulationId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(regulationId));
        }

        return await HttpClient.GetAsync<T>(RegulationApiEndpoints.RegulationUrl(context.TenantId, regulationId));
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, string name) where T : class, IRegulation
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        // query single item
        var query = QueryFactory.NewNameQuery(name);
        var uri = query.AppendQueryString(RegulationApiEndpoints.RegulationsUrl(context.TenantId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<string> GetCaseOfCaseFieldAsync(TenantServiceContext context, string caseFieldName)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentException.ThrowIfNullOrWhiteSpace(caseFieldName);

        return await HttpClient.GetAsync<string>(
            RegulationApiEndpoints.RegulationsCasesCaseFieldUrl(context.TenantId, caseFieldName));
    }

    /// <inheritdoc/>
    public virtual async Task<T> CreateAsync<T>(TenantServiceContext context, T regulation) where T : class, IRegulation
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(regulation);

        return await HttpClient.PostAsync(RegulationApiEndpoints.RegulationsUrl(context.TenantId), regulation);
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync<T>(TenantServiceContext context, T regulation) where T : class, IRegulation
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(regulation);

        await HttpClient.PutAsync(RegulationApiEndpoints.RegulationsUrl(context.TenantId), regulation);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(TenantServiceContext context, int regulationId)
    {
        ArgumentNullException.ThrowIfNull(context);
        if (regulationId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(regulationId));
        }

        await HttpClient.DeleteAsync(RegulationApiEndpoints.RegulationsUrl(context.TenantId), regulationId);
    }

    /// <inheritdoc/>
    public virtual async Task<string> GetAttributeAsync(TenantServiceContext context, int regulationId, string attributeName)
    {
        ArgumentNullException.ThrowIfNull(context);
        if (regulationId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(regulationId));
        }
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);

        return await HttpClient.GetAttributeAsync(RegulationApiEndpoints.RegulationAttributeUrl(context.TenantId, regulationId, attributeName));
    }

    /// <inheritdoc/>
    public virtual async Task SetAttributeAsync(TenantServiceContext context, int regulationId, string attributeName, string attributeValue)
    {
        ArgumentNullException.ThrowIfNull(context);
        if (regulationId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(regulationId));
        }
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);

        await HttpClient.PostAttributeAsync(RegulationApiEndpoints.RegulationAttributeUrl(context.TenantId, regulationId, attributeName), attributeValue);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAttributeAsync(TenantServiceContext context, int regulationId, string attributeName)
    {
        ArgumentNullException.ThrowIfNull(context);
        if (regulationId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(regulationId));
        }
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);

        await HttpClient.DeleteAttributeAsync(RegulationApiEndpoints.RegulationAttributeUrl(context.TenantId, regulationId, attributeName));
    }
}