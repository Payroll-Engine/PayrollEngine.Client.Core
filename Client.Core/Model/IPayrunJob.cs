using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payrun job client object</summary>
public interface IPayrunJob : IModel, IAttributeObject, IEquatable<IPayrunJob>
{
    /// <summary>The job name (immutable)</summary>
    string Name { get; set; }

    /// <summary>The job owner (immutable)</summary>
    public string Owner { get; set; }

    /// <summary>The payrun id (immutable)</summary>
    int PayrunId { get; set; }

    /// <summary>The payrun name (client only)</summary>
    string PayrunName { get; set; }

    /// <summary>The payroll id (immutable)</summary>
    int PayrollId { get; set; }

    /// <summary>The payroll name (client only)</summary>
    string PayrollName { get; set; }

    /// <summary>The division id (immutable)</summary>
    int DivisionId { get; set; }

    /// <summary>The user id (immutable)</summary>
    int UserId { get; set; }

    /// <summary>The parent payrun job id, e.g. the parent retro pay run job (immutable)</summary>
    int? ParentJobId { get; set; }

    /// <summary>The user unique identifier (client only)</summary>
    string UserIdentifier { get; set; }

    /// <summary>The job tags (immutable)</summary>
    List<string> Tags { get; set; }

    /// <summary>The forecast name (immutable)</summary>
    string Forecast { get; set; }

    /// <summary>The payrun retro pay mode (immutable)</summary>
    RetroPayMode RetroPayMode { get; set; }

    /// <summary>The payrun job status (immutable)</summary>
    PayrunJobStatus JobStatus { get; set; }

    /// <summary>The payrun job result</summary>
    PayrunJobResult JobResult { get; set; }

    /// <summary>The culture including the calendar</summary>
    string Culture { get; set; }

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

    /// <summary>The evaluation date (immutable)</summary>
    DateTime? EvaluationDate { get; set; }

    /// <summary>The execution reason (immutable)</summary>
    string Reason { get; set; }

    /// <summary>Total employee count (immutable)</summary>
    int TotalEmployeeCount { get; set; }

    /// <summary>Processed employee count (immutable)</summary>
    int ProcessedEmployeeCount { get; set; }

    /// <summary>The job start date (immutable)</summary>
    DateTime JobStart { get; set; }

    /// <summary>The job end date (immutable)</summary>
    DateTime? JobEnd { get; set; }

    /// <summary>The job message (immutable)</summary>
    string Message { get; set; }

    /// <summary>The job error message (immutable)</summary>
    string ErrorMessage { get; set; }

    /// <summary>The payrun employees</summary>
    List<PayrunJobEmployee> Employees { get; set; }
}