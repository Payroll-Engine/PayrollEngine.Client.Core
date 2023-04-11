using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>Payroll case change setup client object</summary>
public interface ICaseChangeSetup : IModel
{
    /// <summary>The change user id</summary>
    int UserId { get; set; }

    /// <summary>The change user unique identifier (client only)</summary>
    string UserIdentifier { get; set; }

    /// <summary>The change employee id (immutable)</summary>
    int? EmployeeId { get; set; }

    /// <summary>The employee identifier (client only)</summary>
    string EmployeeIdentifier { get; set; }

    /// <summary>The division id (immutable)
    /// If present, this values overrides all case value divisions  <see cref="CaseValue.DivisionId"/></summary>
    int? DivisionId { get; set; }

    /// <summary>The division name (client only)</summary>
    string DivisionName { get; set; }

    /// <summary>The case to cancel, the root case specifies the target case</summary>
    int? CancellationId { get; set; }

    /// <summary>The creation date from the case to cancel, the root case specifies the target case</summary>
    DateTime? CancellationCreated { get; set; }

    /// <summary>The change reason</summary>
    string Reason { get; set; }

    /// <summary>The forecast name</summary>
    string Forecast { get; set; }

    /// <summary>The setup root case</summary>
    CaseSetup Case { get; set; }

    /// <summary>The case validation issues</summary>
    List<CaseValidationIssue> Issues { get; set; }
}