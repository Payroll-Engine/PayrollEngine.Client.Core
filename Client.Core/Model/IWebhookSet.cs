using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The Webhook client object</summary>
public interface IWebhookSet : IWebhook
{
    /// <summary>The webhook messages</summary>
    List<WebhookMessage> Messages { get; set; }
}