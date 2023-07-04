using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll webhook service</summary>
public class WebhookService : ServiceBase, IWebhookService
{
    /// <summary>Initializes a new instance of the <see cref="WebhookService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public WebhookService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> QueryAsync<T>(TenantServiceContext context, Query query = null)
        where T : class, IWebhook
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(TenantApiEndpoints.WebhooksUrl(context.TenantId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc />
    public virtual async Task<long> QueryCountAsync(TenantServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(TenantApiEndpoints.WebhooksUrl(context.TenantId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc />
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(TenantServiceContext context, Query query = null)
        where T : class, IWebhook
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(TenantApiEndpoints.WebhooksUrl(context.TenantId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc />
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, int webhookId) where T : class, IWebhook
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (webhookId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(webhookId));
        }

        return await HttpClient.GetAsync<T>(TenantApiEndpoints.WebhookUrl(context.TenantId, webhookId));
    }

    /// <inheritdoc />
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, string name) where T : class, IWebhook
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
        var uri = query.AppendQueryString(TenantApiEndpoints.WebhooksUrl(context.TenantId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc />
    public virtual async Task<T> CreateAsync<T>(TenantServiceContext context, T webhook) where T : class, IWebhook
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (webhook == null)
        {
            throw new ArgumentNullException(nameof(webhook));
        }

        return await HttpClient.PostAsync(TenantApiEndpoints.WebhooksUrl(context.TenantId), webhook);
    }

    /// <inheritdoc />
    public virtual async Task UpdateAsync<T>(TenantServiceContext context, T webhook) where T : class, IWebhook
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (webhook == null)
        {
            throw new ArgumentNullException(nameof(webhook));
        }

        await HttpClient.PutAsync(TenantApiEndpoints.WebhooksUrl(context.TenantId), webhook);
    }

    /// <inheritdoc />
    public virtual async Task DeleteAsync(TenantServiceContext context, int webhookId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (webhookId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(webhookId));
        }

        await HttpClient.DeleteAsync(TenantApiEndpoints.WebhooksUrl(context.TenantId), webhookId);
    }

    /// <inheritdoc />
    public virtual async Task<string> GetAttributeAsync(TenantServiceContext context, int webhookId, string attributeName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (webhookId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(webhookId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        return await HttpClient.GetAttributeAsync(TenantApiEndpoints.WebhookAttributeUrl(context.TenantId, webhookId,
            attributeName));
    }

    /// <inheritdoc />
    public virtual async Task SetAttributeAsync(TenantServiceContext context, int webhookId, string attributeName, string attributeValue)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (webhookId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(webhookId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        await HttpClient.PostAttributeAsync(TenantApiEndpoints.WebhookAttributeUrl(context.TenantId, webhookId,
            attributeName), attributeValue);
    }

    /// <inheritdoc />
    public virtual async Task DeleteAttributeAsync(TenantServiceContext context, int webhookId, string attributeName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (webhookId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(webhookId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        await HttpClient.DeleteAttributeAsync(TenantApiEndpoints.WebhookAttributeUrl(context.TenantId, webhookId,
            attributeName));
    }
}