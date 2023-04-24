using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The wage type result client object</summary>
public class WageTypeResult : Model, IWageTypeResult
{
    /// <inheritdoc/>
    public int PayrollResultId { get; set; }

    /// <inheritdoc/>
    public int WageTypeId { get; set; }

    /// <inheritdoc/>
    [Required]
    public decimal WageTypeNumber { get; set; }

    /// <inheritdoc/>
    public string WageTypeName { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> WageTypeNameLocalizations { get; set; }

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
    public WageTypeResult()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public WageTypeResult(IWageTypeResult copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IWageTypeResult compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool AlmostEqualValue(decimal? compare, int precision) =>
        compare.HasValue && Value.AlmostEquals(compare.Value, precision);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{WageTypeNumber:##.####} {Value:##.####} {base.ToString()}";
}