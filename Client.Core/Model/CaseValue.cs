using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll case value client object (immutable)</summary>
public class CaseValue : ModelBase, ICaseValue
{
    /// <inheritdoc/>
    [JsonPropertyOrder(100)]
    public int? DivisionId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public int? EmployeeId { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(102)]
    public string DivisionName { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(103)]
    public string CaseName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public Dictionary<string, string> CaseNameLocalizations { get; set; }

    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(105)]
    public string CaseFieldName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(100)]
    public Dictionary<string, string> CaseFieldNameLocalizations { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(106)]
    public string CaseSlot { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public Dictionary<string, string> CaseSlotLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public ValueType ValueType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(109)]
    public string Value { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(110)]
    public decimal? NumericValue { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(111)]
    public string Culture { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(112)]
    public CaseRelationReference CaseRelation { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(113)]
    public DateTime? CancellationDate { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(114)]
    public DateTime? Start { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(115)]
    public DateTime? End { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(116)]
    public string Forecast { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(117)]
    public List<string> Tags { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(118)]
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

    /// <inheritdoc/>
    public virtual bool Equals(ICaseValue compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public override string GetUiString() =>
        string.IsNullOrWhiteSpace(CaseSlot) ?
            $"{CaseName}{CaseFieldName}" :
            $"{CaseName}{CaseFieldName} [{CaseSlot}]";

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{GetUiString()} {base.ToString()}";
}