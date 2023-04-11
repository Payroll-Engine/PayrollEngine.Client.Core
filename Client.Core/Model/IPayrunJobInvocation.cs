using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payrun job client object</summary>
public interface IPayrunJobInvocation
{
    /// <summary>The job name (immutable)</summary>
    string Name { get; set; }

    /// <summary>The job owner (immutable)</summary>
    public string Owner { get; set; }

    /// <summary>The payrun id (immutable)</summary>
    int PayrunId { get; set; }

    /// <summary>The payrun name (immutable)</summary>
    string PayrunName { get; set; }

    /// <summary>The payroll id (immutable)</summary>
    int PayrollId { get; set; }

    /// <summary>The payroll name (immutable)</summary>
    string PayrollName { get; set; }

    /// <summary>The user id (immutable)</summary>
    int UserId { get; set; }

    /// <summary>The user identifier (immutable)</summary>
    string UserIdentifier { get; set; }

    /// <summary>The payrun job id (immutable)</summary>
    int? PayrunJobId { get; set; }

    /// <summary>The parent payrun job id, e.g. the parent retro pay run job (immutable)</summary>
    int? ParentJobId { get; set; }

    /// <summary>The retro payrun jobs, requires the ParentJobId (immutable)</summary>
    List<RetroPayrunJob> RetroJobs { get; set; }

    /// <summary>The job tags (immutable)</summary>
    List<string> Tags { get; set; }

    /// <summary>The forecast name (immutable)</summary>
    string Forecast { get; set; }

    /// <summary>The payrun job execution mode (immutable)</summary>
    PayrunJobExecutionMode ExecutionMode { get; set; }

    /// <summary>The payrun retro pay mode (immutable)</summary>
    RetroPayMode RetroPayMode { get; set; }

    /// <summary>The target payrun job status</summary>
    PayrunJobStatus JobStatus { get; set; }

    /// <summary>The payrun job result</summary>
    PayrunJobResult JobResult { get; set; }

    /// <summary>The period start date (immutable)</summary>
    DateTime PeriodStart { get; set; }

    /// <summary>The evaluation date (immutable)</summary>
    DateTime? EvaluationDate { get; set; }

    /// <summary>The execution reason (immutable)</summary>
    string Reason { get; set; }

    /// <summary>The function log level, default is information</summary>
    LogLevel LogLevel { get; set; }

    /// <summary>The payrun employee identifiers</summary>
    List<string> EmployeeIdentifiers { get; set; }

    /// <summary>Payrun job attributes</summary>
    Dictionary<string, object> Attributes { get; set; }
}