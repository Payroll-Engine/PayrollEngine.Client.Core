using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The case value (immutable)</summary>
// ReSharper disable UnusedMemberInSuper.Global
public interface ICaseValue : IModel, IAttributeObject, IEquatable<ICaseValue>
{
    /// <summary>The division id (immutable), Mandatory for case values with local value scope <see cref="CaseField.ValueScope"/></summary>
    int? DivisionId { get; set; }

    /// <summary>The employee id, mandatory for employee case changes (immutable)</summary>
    int? EmployeeId { get; set; }

    /// <summary>The associated case field name</summary>
    string DivisionName { get; set; }

    /// <summary>The associated case name</summary>
    string CaseName { get; set; }

    /// <summary>The localized case names</summary>
    Dictionary<string, string> CaseNameLocalizations { get; set; }

    /// <summary>The associated case field name</summary>
    string CaseFieldName { get; set; }

    /// <summary>The localized case field names</summary>
    Dictionary<string, string> CaseFieldNameLocalizations { get; set; }

    /// <summary>The case slot</summary>
    string CaseSlot { get; set; }

    /// <summary>The localized case slots</summary>
    Dictionary<string, string> CaseSlotLocalizations { get; set; }

    /// <summary>The type of the value</summary>
    ValueType ValueType { get; set; }

    /// <summary>The case value (JSON format)</summary>
    string Value { get; set; }

    /// <summary>The case numeric value</summary>
    decimal? NumericValue { get; set; }

    /// <summary>The case value culture name based on RFC 4646</summary>
    string Culture { get; set; }

    /// <summary>The case relation</summary>
    CaseRelationReference CaseRelation { get; set; }

    /// <summary>Cancellation date</summary>
    DateTime? CancellationDate { get; set; }

    /// <summary>The starting date for the value</summary>
    DateTime? Start { get; set; }

    /// <summary>The ending date for the value</summary>
    DateTime? End { get; set; }

    /// <summary>The forecast name</summary>
    string Forecast { get; set; }

    /// <summary>The case value tags</summary>
    List<string> Tags { get; set; }
}