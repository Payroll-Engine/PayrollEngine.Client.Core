using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>Payroll case change setup client object</summary>
public class CaseChangeSetup : ModelBase, ICaseChangeSetup
{
    /// <inheritdoc/>
    [JsonPropertyOrder(100)]
    public int UserId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public string UserIdentifier { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public int? EmployeeId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public string EmployeeIdentifier { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public int? DivisionId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public string DivisionName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public int? CancellationId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public DateTime? CancellationCreated { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public string Reason { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(109)]
    public string Forecast { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(110)]
    public CaseSetup Case { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(111)]
    public List<CaseValidationIssue> Issues { get; set; }

    /// <summary>Initializes a new instance</summary>
    public CaseChangeSetup()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CaseChangeSetup(CaseChangeSetup copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ICaseChangeSetup compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public override string GetUiString() => Case?.CaseName;
}