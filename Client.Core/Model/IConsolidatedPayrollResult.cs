using System;
using System.Collections.Generic;
// ReSharper disable UnusedMemberInSuper.Global

namespace PayrollEngine.Client.Model;

/// <summary>Consolidated payroll result</summary>
public interface IConsolidatedPayrollResult : IEquatable<IConsolidatedPayrollResult>
{
    /// <summary>The wage type results</summary>
    List<WageTypeResultSet> WageTypeResults { get; set; }

    /// <summary>The collector results</summary>
    List<CollectorResult> CollectorResults { get; set; }

    /// <summary>The payrun results</summary>
    List<PayrunResult> PayrunResults { get; set; }
}