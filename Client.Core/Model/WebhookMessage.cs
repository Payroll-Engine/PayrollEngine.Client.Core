using System;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The Webhook message client object</summary>
public class WebhookMessage : ModelBase, IWebhookMessage
{
    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    public string ActionName { get; set; }

    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    public string ReceiverAddress { get; set; }

    /// <inheritdoc/>
    [Required]
    public DateTime RequestDate { get; set; }

    /// <inheritdoc/>
    [Required]
    public string RequestMessage { get; set; }

    /// <inheritdoc/>
    public string RequestOperation { get; set; }

    /// <inheritdoc/>
    public DateTime ResponseDate { get; set; }

    /// <inheritdoc/>
    public int ResponseStatus { get; set; }

    /// <inheritdoc/>
    public string ResponseMessage { get; set; }

    /// <summary>Initializes a new instance</summary>
    public WebhookMessage()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public WebhookMessage(WebhookMessage copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IWebhookMessage compare) =>
        CompareTool.EqualProperties(this, compare);
    
    /// <inheritdoc/>
    public override string GetUiString() => ActionName;

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{ActionName} {RequestDate}: {ResponseStatus} {base.ToString()}";
}