using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The retro payrun job client object</summary>
public class RetroPayrunJob : IRetroPayrunJob
{
    /// <inheritdoc/>
    [Required]
    public DateTime ScheduleDate { get; set; }

    /// <inheritdoc/>
    public List<string> ResultTags { get; set; }

    /// <summary>Initializes a new instance</summary>
    public RetroPayrunJob()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public RetroPayrunJob(IRetroPayrunJob copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IRetroPayrunJob compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public override string ToString() =>
        $"{ScheduleDate}] {base.ToString()}";
}