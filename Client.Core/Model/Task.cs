using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll task object</summary>
public class Task : ModelBase, ITask, INameObject
{
    /// <summary>The task name</summary>
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string Category { get; set; }

    /// <inheritdoc/>
    [Required]
    public string Instruction { get; set; }

    /// <inheritdoc/>
    public int ScheduledUserId { get; set; }

    /// <inheritdoc/>
    public string ScheduledUserIdentifier { get; set; }

    /// <inheritdoc/>
    [Required]
    public DateTime Scheduled { get; set; }

    /// <inheritdoc/>
    public int? CompletedUserId { get; set; }

    /// <inheritdoc/>
    public string CompletedUserIdentifier { get; set; }

    /// <inheritdoc/>
    public DateTime? Completed { get; set; }

    /// <summary>
    /// Custom attributes
    /// </summary>
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