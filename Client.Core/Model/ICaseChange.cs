using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>Payroll case value change client object</summary>
public interface ICaseChange : IModel, IEquatable<ICaseChange>
{
    /// <summary>The change user id</summary>
    int UserId { get; set; }

    /// <summary>The change user unique identifier (client only)</summary>
    string UserIdentifier { get; set; }

    /// <summary>The payroll id</summary>
    int PayrollId { get; set; }

    /// <summary>The payroll name</summary>
    string PayrollName { get; set; }

    /// <summary>The change employee id (immutable)</summary>
    int? EmployeeId { get; set; }

    /// <summary>The division id (immutable)
    /// If present, this values overrides all case value divisions  <see cref="CaseValue.DivisionId"/></summary>
    int? DivisionId { get; set; }

    /// <summary>The division name (client only)</summary>
    string DivisionName { get; set; }

    /// <summary>The cancellation type</summary>
    CaseCancellationType CancellationType { get; set; }

    /// <summary>The cancellation case id (immutable)</summary>
    int? CancellationId { get; set; }

    /// <summary>The cancellation date (immutable)</summary>
    DateTime? CancellationDate { get; set; }

    /// <summary>The change reason</summary>
    string Reason { get; set; }

    /// <summary>The validation case name</summary>
    string ValidationCaseName { get; set; }

    /// <summary>The forecast name</summary>
    string Forecast { get; set; }

    /// <summary>The case values</summary>
    List<CaseValue> Values { get; set; }

    /// <summary>The case validation issues</summary>
    List<CaseValidationIssue> Issues { get; set; }
}