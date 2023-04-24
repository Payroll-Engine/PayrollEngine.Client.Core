using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The Webhook client object</summary>
public interface IWebhookSet : IWebhook, IEquatable<IWebhookSet>
{
    /// <summary>The webhook messages</summary>
    List<WebhookMessage> Messages { get; set; }
}