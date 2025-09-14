using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll case field client object</summary>
// ReSharper disable UnusedMemberInSuper.Global
public interface ICaseFieldSet : ICaseField, IEquatable<ICaseFieldSet>
{
    /// <summary>The case field display name</summary>
    string DisplayName { get; set; }

    /// <summary>The case slot</summary>
    string CaseSlot { get; set; }

    /// <summary>The localized case slots</summary>
    Dictionary<string, string> CaseSlotLocalizations { get; set; }

    /// <summary>The case value (JSON format)</summary>
    string Value { get; set; }

    /// <summary>The starting date for the value</summary>
    DateTime? Start { get; set; }

    /// <summary>The ending date for the value</summary>
    DateTime? End { get; set; }

    /// <summary>Cancellation date</summary>
    DateTime? CancellationDate { get; set; }
}