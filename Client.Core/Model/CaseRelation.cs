using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll case relation client object</summary>
public class CaseRelation : ModelBase, ICaseRelation
{
    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(100)]
    public string SourceCaseName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public Dictionary<string, string> SourceCaseNameLocalizations { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(102)]
    public string SourceCaseSlot { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public Dictionary<string, string> SourceCaseSlotLocalizations { get; set; }

    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(104)]
    public string TargetCaseName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public Dictionary<string, string> TargetCaseNameLocalizations { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(106)]
    public string TargetCaseSlot { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public Dictionary<string, string> TargetCaseSlotLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public string BuildExpression { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(109)]
    public string BuildExpressionFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(110)]
    public string ValidateExpression { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(111)]
    public string ValidateExpressionFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(112)]
    public OverrideType OverrideType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(113)]
    public int Order { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(114)]
    public List<string> BuildActions { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(115)]
    public List<string> ValidateActions { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(116)]
    public List<string> Clusters { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(117)]
    public Dictionary<string, object> Attributes { get; set; }

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