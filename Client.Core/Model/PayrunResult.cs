using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payrun result client object</summary>
public class PayrunResult : Model, IPayrunResult, INameObject
{
    /// <inheritdoc/>
    public int PayrollResultId { get; set; }

    /// <inheritdoc/>
    public string Source { get; set; }

    /// <summary>The payrun result name</summary>
    [Required]
    public string Name { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    public string Slot { get; set; }

    /// <inheritdoc/>
    public ValueType ValueType { get; set; }

    /// <inheritdoc/>
    [Required]
    public string Value { get; set; }

    /// <inheritdoc/>
    public decimal? NumericValue { get; set; }

    /// <inheritdoc/>
    [Required]
    public DateTime Start { get; set; }

    /// <inheritdoc/>
    [Required]
    public DateTime End { get; set; }

    /// <inheritdoc/>
    public List<string> Tags { get; set; }

    /// <inheritdoc/>
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