using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll case relation client object</summary>
public class CaseRelation : Model, ICaseRelation, IEquatable<CaseRelation>
{
    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    public string SourceCaseName { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> SourceCaseNameLocalizations { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string SourceCaseSlot { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> SourceCaseSlotLocalizations { get; set; }

    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    public string TargetCaseName { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> TargetCaseNameLocalizations { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string TargetCaseSlot { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> TargetCaseSlotLocalizations { get; set; }

    /// <inheritdoc/>
    public string BuildExpression { get; set; }

    /// <inheritdoc/>
    public string BuildExpressionFile { get; set; }

    /// <inheritdoc/>
    public string ValidateExpression { get; set; }

    /// <inheritdoc/>
    public string ValidateExpressionFile { get; set; }

    /// <inheritdoc/>
    public OverrideType OverrideType { get; set; }

    /// <inheritdoc/>
    public int Order { get; set; }

    /// <inheritdoc/>
    public List<string> BuildActions { get; set; }

    /// <inheritdoc/>
    public List<string> ValidateActions { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, object> Attributes { get; set; }

    /// <inheritdoc/>
    public List<string> Clusters { get; set; }

    /// <summary>Initializes a new instance</summary>
    public CaseRelation()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CaseRelation(CaseRelation copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(CaseRelation compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{SourceCaseName}.{SourceCaseSlot} > {TargetCaseName}.{TargetCaseSlot} {base.ToString()}";
}