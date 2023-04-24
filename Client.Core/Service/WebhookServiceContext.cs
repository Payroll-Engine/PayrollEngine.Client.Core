using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Webhook service context</summary>
public class WebhookServiceContext : TenantServiceContext
{
    /// <summary>Initializes a new instance of the <see cref="WebhookServiceContext"/> class</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="webhookId">The webhook id</param>
    public WebhookServiceContext(int tenantId, int webhookId) :
        base(tenantId)
    {
        WebhookId = webhookId;
    }

    /// <summary>Initializes a new instance of the <see cref="WebhookServiceContext"/> class</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="webhook">The webhook id</param>
    public WebhookServiceContext(ITenant tenant, IWebhook webhook) :
        this(tenant.Id, webhook.Id)
    {
    }

    /// <summary>The webhook id</summary>
    public int WebhookId { get; }
}