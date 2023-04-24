using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The Webhook message client object</summary>
public class WebhookRuntimeMessage : WebhookMessage, IWebhookRuntimeMessage
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
    public WebhookRuntimeMessage(IWebhookRuntimeMessage copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IWebhookRuntimeMessage compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Tenant} {User}: {base.ToString()}";
}