using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>Payroll result value</summary>
public class PayrollResultValue : IPayrollResultValue, IEquatable<PayrollResultValue>
{
    /// <inheritdoc/>
    public int PayrollResultId { get; set; }

    /// <inheritdoc/>
    public DateTime Created { get; set; }

    #region Result

    /// <inheritdoc/>
    public ResultKind ResultKind { get; set; }

    /// <inheritdoc/>
    public int ResultId { get; set; }

    /// <inheritdoc/>
    public int ResultParentId { get; set; }

    /// <inheritdoc/>
    public decimal ResultNumber { get; set; }

    /// <summary>
    /// The result start date
    /// </summary>
    public DateTime ResultStart { get; set; }

    /// <summary>
    /// The result end date
    /// </summary>
    public DateTime ResultEnd { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string KindName { get; set; }

    /// <inheritdoc/>
    public ValueType ResultType { get; set; }

    /// <inheritdoc/>
    public List<string> ResultTags { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string ResultValue { get; set; }

    /// <inheritdoc/>
    public decimal? ResultNumericValue { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, object> Attributes { get; set; }

    #endregion

    #region Job

    /// <inheritdoc/>
    public int JobId { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string JobName { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string JobReason { get; set; }

    /// <inheritdoc/>
    public PayrunJobStatus JobStatus { get; set; }

    /// <inheritdoc/>
    public string Forecast { get; set; }

    /// <inheritdoc/>
    public string CycleName { get; set; }

    /// <inheritdoc/>
    public string PeriodName { get; set; }

    /// <inheritdoc/>
    public DateTime PeriodStart { get; set; }

    /// <inheritdoc/>
    public DateTime PeriodEnd { get; set; }

    #endregion

    #region Payrun

    /// <inheritdoc/>
    public int PayrunId { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string PayrunName { get; set; }

    #endregion

    #region Payroll

    /// <inheritdoc/>
    public int PayrollId { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string PayrollName { get; set; }

    #endregion

    #region Division

    /// <inheritdoc/>
    public int DivisionId { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string DivisionName { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string Culture { get; set; }

    #endregion

    #region User

    /// <inheritdoc/>
    public int UserId { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string UserIdentifier { get; set; }

    #endregion

    #region Employee

    /// <inheritdoc/>
    public int EmployeeId { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string EmployeeIdentifier { get; set; }

    #endregion

    /// <summary>Initializes a new instance</summary>
    public PayrollResultValue()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public PayrollResultValue(PayrollResultValue copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(PayrollResultValue compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() => ResultValue;
}