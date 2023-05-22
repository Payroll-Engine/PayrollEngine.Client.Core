using System;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>Cluster set</summary>
public class ActionIssueInfo : IEquatable<ActionIssueInfo>
{
    /// <summary>The action name</summary>
    [Required]
    public string Name { get; set; }

    /// <summary>The action issue message</summary>
    public string Message { get; set; }

    /// <summary>The action issue description</summary>
    public int ParameterCount { get; set; }

    /// <summary>Initializes a new instance</summary>
    public ActionIssueInfo()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public ActionIssueInfo(ActionIssueInfo copySource) =>
        CopyTool.CopyProperties(copySource, this);

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(ActionIssueInfo compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() => Name;
}