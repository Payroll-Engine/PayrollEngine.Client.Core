using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll payrun parameter client object</summary>
public class PayrunParameter : ModelBase, IPayrunParameter, INameObject
{
    /// <summary>The payrun parameter name</summary>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(100)]
    public string Name { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public string Description { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public bool Mandatory { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public string Value { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public ValueType ValueType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(100)]
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance of the <see cref="PayrunParameter"/> class</summary>
    public PayrunParameter()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="PayrunParameter"/> class</summary>
    /// <param name="copySource">The copy source.</param>
    public PayrunParameter(PayrunParameter copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IPayrunParameter compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(IPayrunParameter compare) =>
        string.Equals(Name, compare?.Name);

    /// <inheritdoc/>
    public override string GetUiString() => Name;
}