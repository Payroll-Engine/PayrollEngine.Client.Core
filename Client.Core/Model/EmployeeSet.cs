using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The employee set client object</summary>
public class EmployeeSet : Employee, IEmployeeSet, IEquatable<EmployeeSet>
{
    /// <inheritdoc/>
    public List<CaseChange> Cases { get; set; }

    /// <inheritdoc/>
    public List<CaseValue> Values { get; set; }

    /// <summary>Initializes a new instance</summary>
    public EmployeeSet()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public EmployeeSet(Employee copySource) :
        base(copySource)
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public EmployeeSet(EmployeeSet copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(EmployeeSet compare) =>
        CompareTool.EqualProperties(this, compare);
}