using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll case relation client object</summary>
public class CaseRelation : ModelBase, ICaseRelation
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

    /// <inheritdoc/>
    public virtual bool Equals(ICaseRelation compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(ICaseRelation compare)
    {
        return string.Equals(SourceCaseName, compare?.SourceCaseName) &&
               string.Equals(SourceCaseSlot, compare?.SourceCaseSlot) &&
               string.Equals(TargetCaseName, compare?.TargetCaseName) &&
               string.Equals(TargetCaseSlot, compare?.TargetCaseSlot);
    }

    /// <inheritdoc/>
    public override string GetUiString() =>
        $"{SourceCaseName}.{SourceCaseSlot} > {TargetCaseName}.{TargetCaseSlot}";
}