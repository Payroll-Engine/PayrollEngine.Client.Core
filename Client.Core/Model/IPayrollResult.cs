using System;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll result info client object</summary>
public interface IPayrollResult : IModel, IEquatable<IPayrollResult>
{
    /// <summary>The payroll id (immutable)</summary>
    int PayrollId { get; set; }

    /// <summary>The payrun id (immutable)</summary>
    int PayrunId { get; set; }

    /// <summary>The payrun job id (immutable)</summary>
    int PayrunJobId { get; set; }

    /// <summary>The payrun job name (client only)</summary>
    string PayrunJobName { get; set; }

    /// <summary>The employee id (immutable)</summary>
    int EmployeeId { get; set; }

    /// <summary>The employee identifier (client only)</summary>
    string EmployeeIdentifier { get; set; }

    /// <summary>The division id (immutable)</summary>
    int DivisionId { get; set; }

    /// <summary>The cycle name (immutable)</summary>
    string CycleName { get; set; }

    /// <summary>The cycle start date (immutable)</summary>
    DateTime CycleStart { get; set; }

    /// <summary>The cycle end date (immutable)</summary>
    DateTime CycleEnd { get; set; }

    /// <summary>The period name (immutable)</summary>
    string PeriodName { get; set; }

    /// <summary>The period start date (immutable)</summary>
    DateTime PeriodStart { get; set; }

    /// <summary>The period end date (immutable)</summary>
    DateTime PeriodEnd { get; set; }
}