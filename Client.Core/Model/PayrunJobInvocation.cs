using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payrun job client object</summary>
public class PayrunJobInvocation : IPayrunJobInvocation
{
    /// <inheritdoc/>
    [Required]
    public string Name { get; set; }

    /// <inheritdoc/>
    public string Owner { get; set; }

    /// <inheritdoc/>
    public int PayrunId { get; set; }

    /// <inheritdoc/>
    [Required]
    public string PayrunName { get; set; }

    /// <inheritdoc/>
    public int UserId { get; set; }

    /// <inheritdoc/>
    [Required]
    public string UserIdentifier { get; set; }

    /// <inheritdoc/>
    public int? PayrunJobId { get; set; }

    /// <inheritdoc/>
    public int? ParentJobId { get; set; }

    /// <inheritdoc/>
    public List<RetroPayrunJob> RetroJobs { get; set; }

    /// <inheritdoc/>
    public List<string> Tags { get; set; }

    /// <inheritdoc/>
    public string Forecast { get; set; }

    /// <inheritdoc/>
    public RetroPayMode RetroPayMode { get; set; }

    /// <inheritdoc/>
    public PayrunJobStatus JobStatus { get; set; }

    /// <inheritdoc/>
    public PayrunJobResult JobResult { get; set; }

    /// <inheritdoc/>
    [Required]
    public DateTime PeriodStart { get; set; }

    /// <inheritdoc/>
    public DateTime? EvaluationDate { get; set; }

    /// <inheritdoc/>
    [Required]
    public string Reason { get; set; }

    /// <inheritdoc/>
    public LogLevel LogLevel { get; set; } = LogLevel.Warning;

    /// <inheritdoc/>
    public List<string> EmployeeIdentifiers { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public PayrunJobInvocation()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public PayrunJobInvocation(PayrunJobInvocation copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IPayrunJobInvocation compare) =>
        CompareTool.EqualProperties(this, compare);
}