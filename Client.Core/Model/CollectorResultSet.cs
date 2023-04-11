using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The collector result set client object</summary>
public class CollectorResultSet : CollectorResult, ICollectorResultSet, IEquatable<CollectorResultSet>
{
    /// <inheritdoc/>
    public List<CollectorCustomResult> CustomResults { get; set; }

    /// <summary>Initializes a new instance</summary>
    public CollectorResultSet()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CollectorResultSet(CollectorResult copySource) :
        base(copySource)
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CollectorResultSet(CollectorResultSet copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(CollectorResultSet compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{CustomResults?.Count} periods {base.ToString()}";
}