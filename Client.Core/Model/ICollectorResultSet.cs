using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The collector result set client object</summary>
public interface ICollectorResultSet : ICollectorResult, IEquatable<ICollectorResultSet>
{
    /// <summary>The collector custom results (immutable)</summary>
    List<CollectorCustomResult> CustomResults { get; set; }
}