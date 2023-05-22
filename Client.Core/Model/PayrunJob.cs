using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payrun job client object</summary>
public class PayrunJob : Model, IPayrunJob
{
    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    /// <inheritdoc/>
    public string Owner { get; set; }

    /// <inheritdoc/>
    public int PayrunId { get; set; }

    /// <inheritdoc/>
    [Required]
    public string PayrunName { get; set; }

    /// <inheritdoc/>
    public int PayrollId { get; set; }

    /// <inheritdoc/>
    [Required]
    public string PayrollName { get; set; }

    /// <inheritdoc/>
    public int DivisionId { get; set; }

    /// <inheritdoc/>
    public int CreatedUserId { get; set; }

    /// <inheritdoc/>
    public int? ReleasedUserId { get; set; }

    /// <inheritdoc/>
    public int? ProcessedUserId { get; set; }

    /// <inheritdoc/>
    public int? FinishedUserId { get; set; }

    /// <inheritdoc/>
    [Required]
    public string UserIdentifier { get; set; }

    /// <inheritdoc/>
    public List<string> Tags { get; set; }

    /// <inheritdoc/>
    public int? ParentJobId { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string Forecast { get; set; }

    /// <inheritdoc/>
    public RetroPayMode RetroPayMode { get; set; }

    /// <inheritdoc/>
    public PayrunJobStatus JobStatus { get; set; }

    /// <inheritdoc/>
    public PayrunJobResult JobResult { get; set; }

    /// <inheritdoc/>
    public string Culture { get; set; }

    /// <inheritdoc/>
    [Required]
    public string CycleName { get; set; }

    /// <inheritdoc/>
    [Required]
    public DateTime CycleStart { get; set; }

    /// <inheritdoc/>
    public DateTime CycleEnd { get; set; }

    /// <inheritdoc/>
    [Required]
    public string PeriodName { get; set; }

    /// <inheritdoc/>
    [Required]
    public DateTime PeriodStart { get; set; }

    /// <inheritdoc/>
    [Required]
    public DateTime PeriodEnd { get; set; }

    /// <inheritdoc/>
    [Required]
    public DateTime? EvaluationDate { get; set; }

    /// <summary>The job release date (immutable)</summary>
    public DateTime? Released { get; set; }

    /// <inheritdoc/>
    public DateTime? Processed { get; set; }

    /// <inheritdoc/>
    public DateTime? Finished { get; set; }

    /// <inheritdoc/>
    [Required]
    public string CreatedReason { get; set; }

    /// <inheritdoc/>
    public string ReleasedReason { get; set; }

    /// <inheritdoc/>
    public string ProcessedReason { get; set; }

    /// <inheritdoc/>
    public string FinishedReason { get; set; }

    /// <inheritdoc/>
    public int TotalEmployeeCount { get; set; }

    /// <inheritdoc/>
    public int ProcessedEmployeeCount { get; set; }

    /// <inheritdoc/>
    public DateTime JobStart { get; set; }

    /// <inheritdoc/>
    public DateTime? JobEnd { get; set; }

    /// <inheritdoc/>
    public string Message { get; set; }

    /// <inheritdoc/>
    public string ErrorMessage { get; set; }

    /// <inheritdoc/>
    public List<PayrunJobEmployee> Employees { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public PayrunJob()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public PayrunJob(PayrunJob copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IPayrunJob compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>
    /// Get job time period
    /// </summary>
    /// <returns>Date period from the job start until the job end</returns>
    public DatePeriod GetEvaluationPeriod() =>
        new(PeriodStart, PeriodEnd);

    /// <inheritdoc/>
    public override string GetUiString() => Name;

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Name} ({JobStatus}) {base.ToString()}";
}