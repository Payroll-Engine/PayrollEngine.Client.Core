using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>Payroll result value</summary>
public interface IPayrollResultValue : IEquatable<IPayrollResultValue>
{
    /// <summary>The payroll result id</summary>
    int PayrollResultId { get; set; }

    /// <summary>The result creation date</summary>
    DateTime Created { get; set; }

    #region Result

    /// <summary>The result kind: wage type or collector</summary>
    ResultKind ResultKind { get; set; }

    /// <summary>The result id (e.g. the collector id)</summary>
    int ResultId { get; set; }
    
    /// <summary>The result parent id (e.g. wage typo on custom wage type)</summary>
    int ResultParentId { get; set; }
    
    /// <summary>The result creation date</summary>
    DateTime ResultCreated { get; set; }

    /// <summary>The result number (e.g. wage type number)</summary>
    decimal ResultNumber { get; set; }

    /// <summary>The result start date</summary>
    DateTime ResultStart { get; set; }

    /// <summary>The result end date</summary>
    DateTime ResultEnd { get; set; }

    /// <summary>The kind name, wage type number or collect type</summary>
    [StringLength(128)]
    string KindName { get; set; }

    /// <summary>The result type</summary>
    ValueType ResultType { get; set; }

    /// <summary>The result tags</summary>
    List<string> ResultTags { get; set; }

    /// <summary>The result value (JSON)</summary>
    [StringLength(128)]
    string ResultValue { get; set; }

    /// <summary>The result numeric value</summary>
    decimal? ResultNumericValue { get; set; }

    /// <summary>The result attributes</summary>
    Dictionary<string, object> Attributes { get; set; }

    #endregion

    #region Job

    /// <summary>The payrun job id</summary>
    int JobId { get; set; }

    /// <summary>The payrun job name</summary>
    [StringLength(128)]
    string JobName { get; set; }

    /// <summary>The payrun job reason</summary>
    [StringLength(128)]
    string JobReason { get; set; }

    /// <summary>The payrun job status</summary>
    PayrunJobStatus JobStatus { get; set; }

    /// <summary>The forecast name (immutable)</summary>
    string Forecast { get; set; }

    /// <summary>The cycle name (immutable)</summary>
    string CycleName { get; set; }

    /// <summary>The period name (immutable)</summary>
    string PeriodName { get; set; }

    /// <summary>The period start date</summary>
    DateTime PeriodStart { get; set; }

    /// <summary>The period end date</summary>
    DateTime PeriodEnd { get; set; }

    #endregion

    #region Payrun

    /// <summary>The payrun id</summary>
    int PayrunId { get; set; }

    /// <summary>The payrun name</summary>
    [StringLength(128)]
    string PayrunName { get; set; }

    #endregion

    #region Payroll

    /// <summary>The payroll id</summary>
    int PayrollId { get; set; }

    /// <summary>The division id</summary>
    int DivisionId { get; set; }

    /// <summary>The division name</summary>
    [StringLength(128)]
    string DivisionName { get; set; }

    /// <summary>The division culture</summary>
    [StringLength(128)]
    string Culture { get; set; }

    /// <summary>The payroll name</summary>
    [StringLength(128)]
    string PayrollName { get; set; }

    #endregion

    #region User

    /// <summary>The user id</summary>
    int UserId { get; set; }

    /// <summary>The user identifier</summary>
    [StringLength(128)]
    string UserIdentifier { get; set; }

    #endregion

    #region Employee

    /// <summary>The employee id</summary>
    int EmployeeId { get; set; }

    /// <summary>The employee identifier</summary>
    [StringLength(128)]
    string EmployeeIdentifier { get; set; }

    #endregion
}