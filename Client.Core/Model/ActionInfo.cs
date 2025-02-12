using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>Cluster set</summary>
public class ActionInfo : IEquatable<ActionInfo>
{
    /// <summary>The extension function type</summary>
    public FunctionType FunctionType { get; set; }

    /// <summary>The action namespace</summary>
    public string Namespace { get; set; }

    /// <summary>The action name</summary>
    [Required]
    public string Name { get; set; }

    /// <summary>The full name</summary>
    public string FullName => $"{Namespace}.{Name}";

    /// <summary>The action description</summary>
    public string Description { get; set; }

    /// <summary>The action categories</summary>
    public List<string> Categories { get; set; }

    /// <summary>Action source </summary>
    public ActionSource Source { get; set; }

    /// <summary>The action parameters </summary>
    public List<ActionParameterInfo> Parameters { get; set; }

    /// <summary>The action issues</summary>
    public List<ActionIssueInfo> Issues { get; set; }

    /// <summary>Initializes a new instance</summary>
    public ActionInfo()
    {
    }

    /// <summary>Initializes a new instance with  the class type</summary>
    /// <param name="classType">The function class type</param>
    public ActionInfo(Type classType)
    {
        var functionTypeName = classType.Name.RemoveFromEnd("Function");
        if (!Enum.TryParse<FunctionType>(functionTypeName, out var functionType))
        {
            throw new ArgumentException($"Unknown function type: {classType}.", nameof(classType));
        }
        FunctionType = functionType;
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public ActionInfo(ActionInfo copySource) =>
        CopyTool.CopyProperties(copySource, this);

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(ActionInfo compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() => Name;
}