using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The employee set client object</summary>
public class EmployeeSet : Employee, IEmployeeSet
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
    public EmployeeSet(IEmployee copySource) :
        base(copySource)
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public EmployeeSet(IEmployeeSet copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IEmployeeSet compare) =>
        CompareTool.EqualProperties(this, compare);
}