using System;

namespace PayrollEngine.Client.Model;

/// <summary>The Webhook message client object</summary>
public interface IWebhookRuntimeMessage : IWebhookMessage, IEquatable<IWebhookRuntimeMessage>
{
    /// <summary>The tenant identifier</summary>
    public string Tenant { get; set; }

    /// <summary>The user identifier</summary>
    public string User { get; set; }
}