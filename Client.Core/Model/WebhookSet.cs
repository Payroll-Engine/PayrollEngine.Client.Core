using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The Webhook set client object</summary>
public class WebhookSet : Webhook, IWebhookSet, IEquatable<WebhookSet>
{
    /// <inheritdoc/>
    public List<WebhookMessage> Messages { get; set; }

    /// <summary>Initializes a new instance</summary>
    public WebhookSet()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public WebhookSet(Webhook copySource) :
        base(copySource)
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public WebhookSet(WebhookSet copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(WebhookSet compare) =>
        CompareTool.EqualProperties(this, compare);
}