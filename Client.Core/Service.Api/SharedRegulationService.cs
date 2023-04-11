using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Shared regulation service</summary>
public class SharedRegulationService : Service, ISharedRegulationService
{
    /// <summary>Initializes a new instance of the <see cref="SharedRegulationService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public SharedRegulationService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> QueryAsync<T>(RootServiceContext context, Query query = null) where T : class, IRegulationPermission
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(ApiEndpoints.SharedRegulationPermissionsUrl());
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
        var url = query.AppendQueryString(ApiEndpoints.SharedRegulationPermissionsUrl());
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc />
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(RootServiceContext context, Query query = null) where T : class, IRegulationPermission
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(ApiEndpoints.SharedRegulationPermissionsUrl());
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc />
    public virtual async Task<T> GetAsync<T>(RootServiceContext context, int permissionId) where T : class, IRegulationPermission
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (permissionId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(permissionId));
        }

        return await HttpClient.GetAsync<T>(ApiEndpoints.SharedRegulationPermissionUrl(permissionId));
    }

    /// <inheritdoc />
    public virtual async Task<T> CreateAsync<T>(RootServiceContext context, T tenant) where T : class, IRegulationPermission
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (tenant == null)
        {
            throw new ArgumentNullException(nameof(tenant));
        }

        return await HttpClient.PostAsync(ApiEndpoints.SharedRegulationPermissionsUrl(), tenant);
    }

    /// <inheritdoc />
    public virtual async Task UpdateAsync<T>(RootServiceContext context, T permission) where T : class, IRegulationPermission
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (permission == null)
        {
            throw new ArgumentNullException(nameof(permission));
        }

        await HttpClient.PutAsync(ApiEndpoints.SharedRegulationPermissionsUrl(), permission);
    }

    /// <inheritdoc />
    public virtual async Task DeleteAsync(RootServiceContext context, int permissionId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (permissionId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(permissionId));
        }

        await HttpClient.DeleteAsync(ApiEndpoints.SharedRegulationPermissionsUrl(), permissionId);
    }

    #region Attributes

    /// <inheritdoc />
    public virtual async Task<string> GetAttributeAsync(RootServiceContext context, int permissionId, string attributeName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (permissionId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(permissionId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        return await HttpClient.GetAttributeAsync(ApiEndpoints.SharedRegulationPermissionAttributeUrl(permissionId, attributeName));
    }

    /// <inheritdoc />
    public virtual async Task SetAttributeAsync(RootServiceContext context, int permissionId, string attributeName, string attributeValue)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (permissionId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(permissionId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        await HttpClient.PostAttributeAsync(ApiEndpoints.SharedRegulationPermissionAttributesUrl(permissionId), attributeValue);
    }

    /// <inheritdoc />
    public virtual async Task DeleteAttributeAsync(RootServiceContext context, int permissionId, string attributeName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (permissionId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(permissionId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        await HttpClient.DeleteAttributeAsync(ApiEndpoints.SharedRegulationPermissionAttributesUrl(permissionId));
    }

    #endregion

}