using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>Payroll case value change client object</summary>
public class CaseChange : Model, ICaseChange
{
    /// <inheritdoc/>
    public int UserId { get; set; }

    /// <inheritdoc/>
    public string UserIdentifier { get; set; }

    /// <inheritdoc/>
    public int PayrollId { get; set; }

    /// <inheritdoc/>
    public string PayrollName { get; set; }

    /// <summary>The employee id, mandatory for employee case changes (immutable)</summary>
    public int? EmployeeId { get; set; }

    /// <inheritdoc/>
    public int? DivisionId { get; set; }

    /// <inheritdoc/>
    public string DivisionName { get; set; }

    /// <inheritdoc/>
    public CaseCancellationType CancellationType { get; set; }

    /// <inheritdoc/>
    public int? CancellationId { get; set; }

    /// <inheritdoc/>
    public DateTime? CancellationDate { get; set; }

    /// <inheritdoc/>
    public string Reason { get; set; }

    /// <inheritdoc/>
    public string ValidationCaseName { get; set; }

    /// <inheritdoc/>
    public string Forecast { get; set; }

    /// <inheritdoc/>
    public List<CaseValue> Values { get; set; }

    /// <inheritdoc/>
    public List<CaseValue> IgnoredValues { get; set; }

    /// <inheritdoc/>
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