using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The Webhook client object</summary>
public class Webhook : ModelBase, IWebhook, INameObject
{
    /// <summary>The webhook name</summary>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(100)]
    public string Name { get; set; }

    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(101)]
    public string ReceiverAddress { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(102)]
    public WebhookAction Action { get; set; }

    /// <summary>
    /// The secret used to sign the webhook messages (write-only, never returned by the backend).
    /// On update: null keeps the current secret, an empty value removes it, any other value replaces it.
    /// </summary>
    [StringLength(256)]
    [JsonPropertyOrder(104)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Secret { get; set; }

    /// <summary>Indicates whether the webhook messages are signed (read-only)</summary>
    [JsonPropertyOrder(105)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Signed { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public Webhook()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public Webhook(Webhook copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IWebhook compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(IWebhook compare) =>
        string.Equals(Name, compare?.Name);

    /// <inheritdoc/>
    public override string GetUiString() => Name;
}