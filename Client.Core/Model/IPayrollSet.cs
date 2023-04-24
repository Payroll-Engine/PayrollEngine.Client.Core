using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll client object</summary>
public interface IPayrollSet : IPayroll, IEquatable<IPayrollSet>
{
    /// <summary>The payroll layers</summary>
    List<PayrollLayer> Layers { get; set; }

    /// <summary>The case change setups</summary>
    List<CaseChangeSetup> Cases { get; set; }
}