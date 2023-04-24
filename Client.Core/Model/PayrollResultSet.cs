using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll result client object</summary>
public class PayrollResultSet : PayrollResult, IPayrollResultSet
{
    /// <inheritdoc/>
    public DateTime? RetroPeriodStart { get; set; }

    /// <inheritdoc/>
    public List<WageTypeResultSet> WageTypeResults { get; set; }

    /// <inheritdoc/>
    public List<CollectorResultSet> CollectorResults { get; set; }

    /// <inheritdoc/>
    public List<PayrunResult> PayrunResults { get; set; }

    /// <summary>Initializes a new instance</summary>
    public PayrollResultSet()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public PayrollResultSet(IPayrollResult copySource) :
        base(copySource)
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public PayrollResultSet(IPayrollResultSet copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IPayrollResultSet compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool HasResults() =>
        WageTypeResults?.Count > 0 || CollectorResults?.Count > 0 || PayrunResults?.Count > 0;

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{WageTypeResults?.Count} wage types, {CollectorResults?.Count} collectors {base.ToString()}";
}