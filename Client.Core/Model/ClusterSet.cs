using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>Cluster set</summary>
public class ClusterSet : IEquatable<ClusterSet>
{
    /// <summary>The filter name</summary>
    public string Name { get; set; }

    /// <summary>The included clusters</summary>
    public List<string> IncludeClusters { get; set; }

    /// <summary>The excluded clusters</summary>
    public List<string> ExcludeClusters { get; set; }

    /// <summary>Initializes a new instance</summary>
    public ClusterSet()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public ClusterSet(ClusterSet copySource) =>
        CopyTool.CopyProperties(copySource, this);

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(ClusterSet compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() => Name;
}