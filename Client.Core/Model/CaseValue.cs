using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll case value client object (immutable)</summary>
public class CaseValue : Model, ICaseValue, IEquatable<CaseValue>
{
    /// <inheritdoc/>
    public int? DivisionId { get; set; }

    /// <inheritdoc/>
    public int? EmployeeId { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string DivisionName { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string CaseName { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> CaseNameLocalizations { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string CaseFieldName { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> CaseFieldNameLocalizations { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string CaseSlot { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> CaseSlotLocalizations { get; set; }

    /// <inheritdoc/>
    public ValueType ValueType { get; set; }

    /// <inheritdoc/>
    public string Value { get; set; }

    /// <inheritdoc/>
    public decimal? NumericValue { get; set; }

    /// <inheritdoc/>
    public CaseRelationReference CaseRelation { get; set; }

    /// <inheritdoc/>
    public DateTime? CancellationDate { get; set; }

    /// <inheritdoc/>
    public DateTime? Start { get; set; }

    /// <inheritdoc/>
    public DateTime? End { get; set; }

    /// <inheritdoc/>
    public string Forecast { get; set; }

    /// <inheritdoc/>
    public List<string> Tags { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public CaseValue()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CaseValue(CaseValue copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(CaseValue compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() => string.IsNullOrWhiteSpace(CaseSlot) ?
        $"{Value} ({CaseName}.{CaseFieldName}) {base.ToString()}" :
        $"{Value} ({CaseName}.{CaseFieldName}[{CaseSlot}]) {base.ToString()}";
}