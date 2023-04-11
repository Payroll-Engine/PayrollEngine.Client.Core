using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll set client object</summary>
public class PayrollSet : Payroll, IPayrollSet, IEquatable<PayrollSet>
{
    /// <inheritdoc/>
    public List<PayrollLayer> Layers { get; set; }

    /// <inheritdoc/>
    public List<CaseChangeSetup> Cases { get; set; }

    /// <summary>Initializes a new instance</summary>
    public PayrollSet()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public PayrollSet(Payroll copySource) :
        base(copySource)
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public PayrollSet(PayrollSet copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(PayrollSet compare) =>
        CompareTool.EqualProperties(this, compare);
}