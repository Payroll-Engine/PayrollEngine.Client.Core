using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using PayrollEngine.Data;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll tenant service</summary>
public class TenantService : Service, ITenantService
{
    /// <summary>Initializes a new instance of the <see cref="TenantService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public TenantService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> QueryAsync<T>(RootServiceContext context, Query query = null) where T : class, ITenant
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(TenantApiEndpoints.TenantsUrl());
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc />
    public virtual async Task<long> QueryCountAsync(RootServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(TenantApiEndpoints.TenantsUrl());
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc />
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(RootServiceContext context, Query query = null) where T : class, ITenant
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(TenantApiEndpoints.TenantsUrl());
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc />
    public virtual async Task<T> GetAsync<T>(RootServiceContext context, int tenantId) where T : class, ITenant
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (tenantId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(tenantId));
        }

        return await HttpClient.GetAsync<T>(TenantApiEndpoints.TenantUrl(tenantId));
    }

    /// <inheritdoc />
    public virtual async Task<T> GetAsync<T>(RootServiceContext context, string identifier) where T : class, ITenant
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (string.IsNullOrWhiteSpace(identifier))
        {
            throw new ArgumentException(nameof(identifier));
        }

        // query single item
        var query = QueryFactory.NewIdentifierQuery(identifier);
        var uri = query.AppendQueryString(TenantApiEndpoints.TenantsUrl());
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc />
    public virtual async Task<T> CreateAsync<T>(RootServiceContext context, T tenant) where T : class, ITenant
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (tenant == null)
        {
            throw new ArgumentNullException(nameof(tenant));
        }

        return await HttpClient.PostAsync(TenantApiEndpoints.TenantsUrl(), tenant);
    }

    /// <inheritdoc />
    public virtual async Task UpdateAsync<T>(RootServiceContext context, T tenant) where T : class, ITenant
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (tenant == null)
        {
            throw new ArgumentNullException(nameof(tenant));
        }

        await HttpClient.PutAsync(TenantApiEndpoints.TenantsUrl(), tenant);
    }

    /// <inheritdoc />
    public virtual async Task DeleteAsync(RootServiceContext context, int tenantId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (tenantId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(tenantId));
        }

        await HttpClient.DeleteAsync(TenantApiEndpoints.TenantsUrl(), tenantId);
    }

    /// <inheritdoc />
    public virtual async Task<List<TAction>> GetSystemScriptActionsAsync<TAction>(int tenantId,
        FunctionType functionType)
        where TAction : ActionInfo
    {
        var url = TenantApiEndpoints.TenantActionsUrl(tenantId)
            .AddQueryString(nameof(functionType), functionType);
        return await HttpClient.GetCollectionAsync<TAction>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<DataTable> ExecuteReportQueryAsync(int tenantId, string methodName,
        string culture, Dictionary<string, string> parameters = null)
    {
        if (tenantId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(tenantId));
        }
        if (string.IsNullOrWhiteSpace(methodName))
        {
            throw new ArgumentException(nameof(methodName));
        }

        var uri = TenantApiEndpoints.TenantQueriesUrl(tenantId)
            .AddQueryString(nameof(methodName), methodName)
            .AddQueryString(nameof(culture), culture);
        // use of POST instead of GET according RFC7231
        // https://datatracker.ietf.org/doc/html/rfc7231#section-4.3.1
        return parameters != null && parameters.Any() ?
            await HttpClient.PostAsync<Dictionary<string, string>, DataTable>(uri, parameters) :
            await HttpClient.PostAsync<DataTable>(uri);
    }

    #region Shared Regulations

    /// <inheritdoc />
    public virtual async Task<List<T>> GetSharedRegulationsAsync<T>(int tenantId, int? divisionId) where T : class, IRegulation
    {
        if (tenantId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(tenantId));
        }

        var url = TenantApiEndpoints.TenantSharedRegulationsUrl(tenantId)
            .AddQueryString(nameof(divisionId), divisionId);
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    #endregion

    #region Attributes

    /// <inheritdoc />
    public virtual async Task<string> GetAttributeAsync(RootServiceContext context, int tenantId, string attributeName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (tenantId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(tenantId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        return await HttpClient.GetAttributeAsync(TenantApiEndpoints.TenantAttributeUrl(tenantId, attributeName));
    }

    /// <inheritdoc />
    public virtual async Task SetAttributeAsync(RootServiceContext context, int tenantId, string attributeName, string attributeValue)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (tenantId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(tenantId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        await HttpClient.PostAttributeAsync(TenantApiEndpoints.TenantAttributesUrl(tenantId), attributeValue);
    }

    /// <inheritdoc />
    public virtual async Task DeleteAttributeAsync(RootServiceContext context, int tenantId, string attributeName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (tenantId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(tenantId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        await HttpClient.DeleteAttributeAsync(TenantApiEndpoints.TenantAttributesUrl(tenantId));
    }

    #endregion

}