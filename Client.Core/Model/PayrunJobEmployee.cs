using System;

namespace PayrollEngine.Client.Model;

/// <summary>The payrun job client object</summary>
public class PayrunJobEmployee : Model, IEquatable<PayrunJobEmployee>
{
    /// <summary>The employee id (immutable)</summary>
    public int EmployeeId { get; set; }

    /// <summary>Initializes a new instance</summary>
    public PayrunJobEmployee()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public PayrunJobEmployee(PayrunJobEmployee copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(PayrunJobEmployee compare) =>
        CompareTool.EqualProperties(this, compare);
}