using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payrun result client object</summary>
public class PayrunResult : ModelBase, IPayrunResult, INameObject
{
    /// <inheritdoc/>
    [JsonPropertyOrder(100)]
    public int PayrollResultId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public string Source { get; set; }

    /// <summary>The payrun result name</summary>
    [Required]
    [JsonPropertyOrder(102)]
    public string Name { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public string Slot { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public ValueType ValueType { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(106)]
    public string Value { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public string Culture { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public decimal? NumericValue { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(109)]
    public DateTime Start { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(110)]
    public DateTime End { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(111)]
    public List<string> Tags { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(112)]
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public PayrunResult()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public PayrunResult(PayrunResult copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IPayrunResult compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public override string GetUiString() => Name;

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Name}={Value} [{Start}-{End}] {base.ToString()}";
}