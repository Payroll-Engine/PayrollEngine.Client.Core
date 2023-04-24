using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll result client object</summary>
public interface IPayrollResultSet : IPayrollResult, IEquatable<IPayrollResultSet>
{
    /// <summary>The retro period start date (client only: test payrun results)</summary>
    DateTime? RetroPeriodStart { get; set; }

    /// <summary>The wage type results</summary>
    List<WageTypeResultSet> WageTypeResults { get; set; }

    /// <summary>The collector results</summary>
    List<CollectorResultSet> CollectorResults { get; set; }

    /// <summary>The payrun results</summary>
    List<PayrunResult> PayrunResults { get; set; }

    /// <summary>Determines whether this instance has results</summary>
    /// <returns>Tre if any result is available</returns>
    bool HasResults();
}