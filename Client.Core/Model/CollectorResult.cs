using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The collector result client object</summary>
public class CollectorResult : ModelBase, ICollectorResult
{
    /// <inheritdoc/>
    public int PayrollResultId { get; set; }

    /// <inheritdoc/>
    public int CollectorId { get; set; }

    /// <inheritdoc/>
    public string CollectorName { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> CollectorNameLocalizations { get; set; }

    /// <inheritdoc/>
    public CollectMode CollectMode { get; set; }

    /// <inheritdoc/>
    public bool Negated { get; set; }

    /// <inheritdoc/>
    public ValueType ValueType { get; set; }

    /// <inheritdoc/>
    public decimal Value { get; set; }

    /// <inheritdoc/>
    public DateTime Start { get; set; }

    /// <inheritdoc/>
    public DateTime End { get; set; }

    /// <inheritdoc/>
    public List<string> Tags { get; set; }

    /// <inheritdoc/>
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