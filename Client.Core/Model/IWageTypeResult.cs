using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The wage type result client object</summary>
// ReSharper disable UnusedMemberInSuper.Global
public interface IWageTypeResult : IModel, IAttributeObject, IEquatable<IWageTypeResult>
{
    /// <summary>The payroll result id (immutable)</summary>
    int PayrollResultId { get; set; }

    /// <summary>The wage type id (immutable)</summary>
    int WageTypeId { get; set; }

    /// <summary>The wage type number (immutable)</summary>
    decimal WageTypeNumber { get; set; }

    /// <summary>The wage type name (immutable)</summary>
    string WageTypeName { get; set; }

    /// <summary>The localized wage type names (immutable)</summary>
    Dictionary<string, string> WageTypeNameLocalizations { get; set; }

    /// <summary>The value type (immutable)</summary>
    ValueType ValueType { get; set; }

    /// <summary>The wage type result value (immutable)</summary>
    decimal Value { get; set; }

    /// <summary>The wage type result culture name based on RFC 4646</summary>
    string Culture { get; set; }

    /// <summary>The starting date for the value (immutable)</summary>
    DateTime Start { get; set; }

    /// <summary>The ending date for the value (immutable)</summary>
    DateTime End { get; set; }

    /// <summary>The result tags</summary>
    List<string> Tags { get; set; }

    /// <summary>Test if value is almost equal value using a test precision</summary>
    /// <param name="compare">The value to compare</param>
    /// <param name="precision">The test precision</param>
    /// <returns>True for almost equal values</returns>
    bool AlmostEqualValue(decimal? compare, int precision);
}