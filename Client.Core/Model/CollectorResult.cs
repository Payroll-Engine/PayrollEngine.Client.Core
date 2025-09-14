using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The collector result client object</summary>
public class CollectorResult : ModelBase, ICollectorResult
{
    /// <inheritdoc/>
    [JsonPropertyOrder(100)]
    public int PayrollResultId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public int CollectorId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public string CollectorName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public Dictionary<string, string> CollectorNameLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public CollectMode CollectMode { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public bool Negated { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public ValueType ValueType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public decimal Value { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public string Culture { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(109)]
    public DateTime Start { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(110)]
    public DateTime End { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(111)]
    public List<string> Tags { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(112)]
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public CollectorResult()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CollectorResult(CollectorResult copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ICollectorResult compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool AlmostEqualValue(decimal? compare, int precision) =>
        compare.HasValue && Value.AlmostEquals(compare.Value, precision);

    /// <inheritdoc/>
    public override string GetUiString() => CollectorName;

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{CollectorName}={Value:##.####} {base.ToString()}";
}