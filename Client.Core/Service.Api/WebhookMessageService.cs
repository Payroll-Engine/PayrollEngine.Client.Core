using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll webhook message service</summary>
public class WebhookMessageService : Service, IWebhookMessageService
{
    /// <summary>Initializes a new instance of the <see cref="WebhookMessageService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public WebhookMessageService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> QueryAsync<T>(WebhookServiceContext context, Query query = null) where T : class, IWebhookMessage
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(TenantApiEndpoints.WebhookMessagesUrl(context.TenantId, context.WebhookId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc />
    public virtual async Task<long> QueryCountAsync(WebhookServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(TenantApiEndpoints.WebhookMessagesUrl(context.TenantId, context.WebhookId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc />
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(WebhookServiceContext context, Query query = null) where T : class, IWebhookMessage
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(TenantApiEndpoints.WebhookMessagesUrl(context.TenantId, context.WebhookId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc />
    public virtual async Task<T> GetAsync<T>(WebhookServiceContext context, int webhookMessageId) where T : class, IWebhookMessage
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (webhookMessageId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(webhookMessageId));
        }

        return await HttpClient.GetAsync<T>(TenantApiEndpoints.WebhookMessageUrl(context.TenantId, context.WebhookId,
            webhookMessageId));
    }

    /// <inheritdoc />
    public virtual async Task<T> CreateAsync<T>(WebhookServiceContext context, T webhookMessage) where T : class, IWebhookMessage
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (webhookMessage == null)
        {
            throw new ArgumentNullException(nameof(webhookMessage));
        }

        return await HttpClient.PostAsync(TenantApiEndpoints.WebhookMessagesUrl(context.TenantId, context.WebhookId),
            webhookMessage);
    }

    /// <inheritdoc />
    public virtual async Task UpdateAsync<T>(WebhookServiceContext context, T webhookMessage) where T : class, IWebhookMessage
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (webhookMessage == null)
        {
            throw new ArgumentNullException(nameof(webhookMessage));
        }

        await HttpClient.PutAsync(TenantApiEndpoints.WebhookMessagesUrl(context.TenantId, context.WebhookId),
            webhookMessage);
    }

    /// <inheritdoc />
    public virtual async Task DeleteAsync(WebhookServiceContext context, int webhookMessageId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (webhookMessageId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(webhookMessageId));
        }

        await HttpClient.DeleteAsync(TenantApiEndpoints.WebhookMessagesUrl(context.TenantId, context.WebhookId),
            webhookMessageId);
    }
}