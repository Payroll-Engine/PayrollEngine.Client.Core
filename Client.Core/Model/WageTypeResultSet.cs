using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The wage type result set client object</summary>
public class WageTypeResultSet : WageTypeResult, IWageTypeResultSet, IEquatable<WageTypeResultSet>
{
    /// <inheritdoc/>
    public List<WageTypeCustomResult> CustomResults { get; set; }

    /// <summary>Initializes a new instance</summary>
    public WageTypeResultSet()
    {
    }

    /// <inheritdoc/>
    public WageTypeResultSet(WageTypeResult copySource) :
        base(copySource)
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public WageTypeResultSet(WageTypeResultSet copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(WageTypeResultSet compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{CustomResults?.Count} custom {base.ToString()}";
}