using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>Payroll case setup client object</summary>
public interface ICaseSetup : IModel, IEquatable<ICaseSetup>
{
    /// <summary>The case name</summary>
    string CaseName { get; set; }

    /// <summary>The case slot</summary>
    string CaseSlot { get; set; }

    /// <summary>The case values</summary>
    List<CaseValueSetup> Values { get; set; }

    /// <summary>The related cases</summary>
    List<CaseSetup> RelatedCases { get; set; }
}