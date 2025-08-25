using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payrun job client object</summary>
public class PayrunJob : ModelBase, IPayrunJob
{
    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(100)]
    public string Name { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public string Owner { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public int PayrunId { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(103)]
    public string PayrunName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public int PayrollId { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(105)]
    public string PayrollName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public int DivisionId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public int CreatedUserId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public int? ReleasedUserId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(109)]
    public int? ProcessedUserId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(110)]
    public int? FinishedUserId { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(111)]
    public string UserIdentifier { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(112)]
    public List<string> Tags { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(113)]
    public int? ParentJobId { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(114)]
    public string Forecast { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(115)]
    public RetroPayMode RetroPayMode { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(116)]
    public PayrunJobStatus JobStatus { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(117)]
    public PayrunJobResult JobResult { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(118)]
    public string Culture { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(119)]
    public string CycleName { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(120)]
    public DateTime CycleStart { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(121)]
    public DateTime CycleEnd { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(122)]
    public string PeriodName { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(123)]
    public DateTime PeriodStart { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(124)]
    public DateTime PeriodEnd { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(125)]
    public DateTime? EvaluationDate { get; set; }

    /// <summary>The job release date (immutable)</summary>
    [JsonPropertyOrder(126)]
    public DateTime? Released { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(127)]
    public DateTime? Processed { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(128)]
    public DateTime? Finished { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(129)]
    public string CreatedReason { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(130)]
    public string ReleasedReason { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(131)]
    public string ProcessedReason { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(132)]
    public string FinishedReason { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(133)]
    public int TotalEmployeeCount { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(134)]
    public int ProcessedEmployeeCount { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(135)]
    public DateTime JobStart { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(136)]
    public DateTime? JobEnd { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(137)]
    public string Message { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(138)]
    public string ErrorMessage { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(139)]
    public List<PayrunJobEmployee> Employees { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(140)]
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