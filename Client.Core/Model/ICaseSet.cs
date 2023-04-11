using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll derived case</summary>
public interface ICaseSet : ICase
{
    /// <summary>The case display name</summary>
    string DisplayName { get; set; }

    /// <summary>The case slot</summary>
    string CaseSlot { get; set; }

    /// <summary>The localized case slots</summary>
    Dictionary<string, string> CaseSlotLocalizations { get; set; }

    /// <summary>The cancellation date</summary>
    DateTime? CancellationDate { get; set; }

    /// <summary>The derived case fields</summary>
    List<CaseFieldSet> Fields { get; set; }

    /// <summary>The related cases</summary>
    List<CaseSet> RelatedCases { get; set; }
}