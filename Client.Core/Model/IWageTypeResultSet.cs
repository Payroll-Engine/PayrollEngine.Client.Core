using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The wage type result set client object</summary>
public interface IWageTypeResultSet : IWageTypeResult, IEquatable<IWageTypeResultSet>
{
    /// <summary>The wage type custom results (immutable)</summary>
    List<WageTypeCustomResult> CustomResults { get; set; }
}