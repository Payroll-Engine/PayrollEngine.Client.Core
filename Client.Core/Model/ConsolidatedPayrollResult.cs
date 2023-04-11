using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>Consolidated payroll result</summary>
public class ConsolidatedPayrollResult : IConsolidatedPayrollResult, IEquatable<ConsolidatedPayrollResult>
{
    /// <inheritdoc/>
    public List<WageTypeResultSet> WageTypeResults { get; set; }

    /// <inheritdoc/>
    public List<CollectorResult> CollectorResults { get; set; }

    /// <inheritdoc/>
    public List<PayrunResult> PayrunResults { get; set; }

    /// <summary>Initializes a new instance</summary>
    public ConsolidatedPayrollResult()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public ConsolidatedPayrollResult(ConsolidatedPayrollResult copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(ConsolidatedPayrollResult compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{WageTypeResults?.Count} wage types, {CollectorResults?.Count} collectors, {PayrunResults?.Count} case values {base.ToString()}";
}