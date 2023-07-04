using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>Payroll case change setup client object</summary>
public class CaseChangeSetup : ModelBase, ICaseChangeSetup
{
    /// <inheritdoc/>
    public int UserId { get; set; }

    /// <inheritdoc/>
    public string UserIdentifier { get; set; }

    /// <inheritdoc/>
    public int? EmployeeId { get; set; }

    /// <inheritdoc/>
    public string EmployeeIdentifier { get; set; }

    /// <inheritdoc/>
    public int? DivisionId { get; set; }

    /// <inheritdoc/>
    public string DivisionName { get; set; }

    /// <inheritdoc/>
    public int? CancellationId { get; set; }

    /// <inheritdoc/>
    public DateTime? CancellationCreated { get; set; }

    /// <inheritdoc/>
    public string Reason { get; set; }

    /// <inheritdoc/>
    public string Forecast { get; set; }

    /// <inheritdoc/>
    [Required]
    public CaseSetup Case { get; set; }

    /// <inheritdoc/>
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