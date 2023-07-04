using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Regulation share service</summary>
public class RegulationShareService : ServiceBase, IRegulationShareService
{
    /// <summary>Initializes a new instance of the <see cref="RegulationShareService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public RegulationShareService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> QueryAsync<T>(RootServiceContext context, Query query = null) where T : class, IRegulationShare
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(ApiEndpoints.SharesRegulationsUrl());
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
        var url = query.AppendQueryString(ApiEndpoints.SharesRegulationsUrl());
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc />
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(RootServiceContext context, Query query = null) where T : class, IRegulationShare
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(ApiEndpoints.SharesRegulationsUrl());
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc />
    public virtual async Task<T> GetAsync<T>(RootServiceContext context, int shareId) where T : class, IRegulationShare
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (shareId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(shareId));
        }

        return await HttpClient.GetAsync<T>(ApiEndpoints.SharesRegulationUrl(shareId));
    }

    /// <inheritdoc />
    public virtual async Task<T> CreateAsync<T>(RootServiceContext context, T tenant) where T : class, IRegulationShare
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (tenant == null)
        {
            throw new ArgumentNullException(nameof(tenant));
        }

        return await HttpClient.PostAsync(ApiEndpoints.SharesRegulationsUrl(), tenant);
    }

    /// <inheritdoc />
    public virtual async Task UpdateAsync<T>(RootServiceContext context, T share) where T : class, IRegulationShare
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (share == null)
        {
            throw new ArgumentNullException(nameof(share));
        }

        await HttpClient.PutAsync(ApiEndpoints.SharesRegulationsUrl(), share);
    }

    /// <inheritdoc />
    public virtual async Task DeleteAsync(RootServiceContext context, int shareId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (shareId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(shareId));
        }

        await HttpClient.DeleteAsync(ApiEndpoints.SharesRegulationsUrl(), shareId);
    }

    #region Attributes

    /// <inheritdoc />
    public virtual async Task<string> GetAttributeAsync(RootServiceContext context, int shareId, string attributeName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (shareId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(shareId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        return await HttpClient.GetAttributeAsync(ApiEndpoints.SharesRegulationAttributeUrl(shareId, attributeName));
    }

    /// <inheritdoc />
    public virtual async Task SetAttributeAsync(RootServiceContext context, int shareId, string attributeName, string attributeValue)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (shareId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(shareId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        await HttpClient.PostAttributeAsync(ApiEndpoints.SharesRegulationAttributesUrl(shareId), attributeValue);
    }

    /// <inheritdoc />
    public virtual async Task DeleteAttributeAsync(RootServiceContext context, int shareId, string attributeName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (shareId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(shareId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        await HttpClient.DeleteAttributeAsync(ApiEndpoints.SharesRegulationAttributesUrl(shareId));
    }

    #endregion

}