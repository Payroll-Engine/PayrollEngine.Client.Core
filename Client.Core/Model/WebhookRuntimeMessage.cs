using System;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The Webhook message client object</summary>
public class WebhookRuntimeMessage : WebhookMessage, IWebhookRuntimeMessage, IEquatable<WebhookRuntimeMessage>
{
    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    public string Tenant { get; set; }

    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    public string User { get; set; }

    /// <summary>Initializes a new instance</summary>
    public WebhookRuntimeMessage()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public WebhookRuntimeMessage(WebhookRuntimeMessage copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(WebhookRuntimeMessage compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Tenant} {User}: {base.ToString()}";
}