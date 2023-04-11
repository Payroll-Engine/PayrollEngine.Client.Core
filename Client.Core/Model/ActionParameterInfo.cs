using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>Cluster set</summary>
public class ActionParameterInfo : IEquatable<ActionParameterInfo>
{
    /// <summary>The action parameter name</summary>
    public string Name { get; set; }

    /// <summary>The action parameter description</summary>
    public string Description { get; set; }

    /// <summary>The action parameter types</summary>
    public List<string> ValueTypes { get; set; }

    /// <summary>The action parameter source types</summary>
    public List<string> ValueSources { get; set; }

    /// <summary>The action parameter reference types</summary>
    public List<string> ValueReferences { get; set; }

    /// <summary>Initializes a new instance</summary>
    public ActionParameterInfo()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public ActionParameterInfo(ActionParameterInfo copySource) =>
        CopyTool.CopyProperties(copySource, this);

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(ActionParameterInfo compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() => Name;
}