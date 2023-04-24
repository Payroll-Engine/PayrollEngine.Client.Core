using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The Webhook set client object</summary>
public class WebhookSet : Webhook, IWebhookSet
{
    /// <inheritdoc/>
    public List<WebhookMessage> Messages { get; set; }

    /// <summary>Initializes a new instance</summary>
    public WebhookSet()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public WebhookSet(IWebhook copySource) :
        base(copySource)
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public WebhookSet(IWebhookSet copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IWebhookSet compare) =>
        CompareTool.EqualProperties(this, compare);
}