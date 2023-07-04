using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The collector custom result client object</summary>
public class CollectorCustomResult : ModelBase, ICollectorCustomResult
{
    /// <inheritdoc/>
    public int CollectorResultId { get; set; }

    /// <inheritdoc/>
    [Required]
    public string CollectorName { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> CollectorNameLocalizations { get; set; }

    /// <inheritdoc/>
    [Required]
    public string Source { get; set; }

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
    public CollectorCustomResult()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CollectorCustomResult(CollectorCustomResult copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ICollectorCustomResult compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public override string GetUiString() => CollectorName;

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{GetUiString()} {Source}={Value} [{Start}-{End}] {base.ToString()}";
}