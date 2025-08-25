using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll task object</summary>
public class Task : ModelBase, ITask, INameObject
{
    /// <summary>The task name</summary>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(100)]
    public string Name { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(102)]
    public string Category { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(103)]
    public string Instruction { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public int ScheduledUserId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public string ScheduledUserIdentifier { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(106)]
    public DateTime Scheduled { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public int? CompletedUserId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public string CompletedUserIdentifier { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(109)]
    public DateTime? Completed { get; set; }

    /// <summary>Custom attributes</summary>
    [JsonPropertyOrder(110)]
    public Dictionary<string, object> Attributes { get; set; }

    /// <inheritdoc/>
    public Task()
    {
    }

    /// <inheritdoc/>
    public Task(Task copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ITask compare) =>
        CompareTool.EqualProperties(this, compare);
   
    /// <inheritdoc/>
    public override string GetUiString() => Name;
}