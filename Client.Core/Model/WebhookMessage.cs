using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The Webhook message client object</summary>
public class WebhookMessage : ModelBase, IWebhookMessage
{
    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(100)]
    public string ActionName { get; set; }

    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(101)]
    public string ReceiverAddress { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(102)]
    public DateTime RequestDate { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(103)]
    public string RequestMessage { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public string RequestOperation { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public DateTime ResponseDate { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public int ResponseStatus { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
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