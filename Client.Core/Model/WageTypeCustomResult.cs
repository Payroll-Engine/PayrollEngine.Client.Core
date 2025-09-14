using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The wage type custom result client object</summary>
public class WageTypeCustomResult : ModelBase, IWageTypeCustomResult
{
    /// <inheritdoc/>
    [JsonPropertyOrder(100)]
    public int WageTypeResultId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public decimal WageTypeNumber { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public string WageTypeName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public Dictionary<string, string> WageTypeNameLocalizations { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(104)]
    public string Source { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public ValueType ValueType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public decimal Value { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public string Culture { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public DateTime Start { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(109)]
    public DateTime End { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(110)]
    public List<string> Tags { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(111)]
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public WageTypeCustomResult()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public WageTypeCustomResult(WageTypeCustomResult copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IWageTypeCustomResult compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public override string GetUiString() => 
        $"{WageTypeName} [{WageTypeNumber:##.####}]";

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{GetUiString()} {Source}={Value} [{Start}-{End}] {base.ToString()}";
}