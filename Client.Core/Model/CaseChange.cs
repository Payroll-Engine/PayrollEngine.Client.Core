using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>Payroll case value change client object</summary>
public class CaseChange : ModelBase, ICaseChange
{
    /// <inheritdoc/>
    [JsonPropertyOrder(100)]
    public int UserId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public string UserIdentifier { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public int PayrollId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public string PayrollName { get; set; }

    /// <summary>The employee id, mandatory for employee case changes (immutable)</summary>
    [JsonPropertyOrder(104)]
    public int? EmployeeId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public int? DivisionId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public string DivisionName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public CaseCancellationType CancellationType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public int? CancellationId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(109)]
    public DateTime? CancellationDate { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(110)]
    public string Reason { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(111)]
    public string ValidationCaseName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(112)]
    public string Forecast { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(113)]
    public List<CaseValue> Values { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(114)]
    public List<CaseValue> IgnoredValues { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(115)]
    public List<CaseValidationIssue> Issues { get; set; }

    /// <summary>Initializes a new instance</summary>
    public CaseChange()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CaseChange(CaseChange copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ICaseChange compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public override string GetUiString() => ValidationCaseName;
}