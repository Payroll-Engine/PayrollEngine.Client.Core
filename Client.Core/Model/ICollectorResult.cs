using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The collector result client object</summary>
public interface ICollectorResult : IModel, IAttributeObject, IEquatable<ICollectorResult>
{
    /// <summary>The payroll result id (immutable)</summary>
    int PayrollResultId { get; set; }

    /// <summary>The collector id (immutable)</summary>
    int CollectorId { get; set; }

    /// <summary>The collector name (immutable)</summary>
    string CollectorName { get; set; }

    /// <summary>The localized collector names (immutable)</summary>
    Dictionary<string, string> CollectorNameLocalizations { get; set; }

    /// <summary>The collect mode (immutable)</summary>
    CollectMode CollectMode { get; set; }

    /// <summary>Negated collector result (immutable)</summary>
    bool Negated { get; set; }

    /// <summary>The value type (immutable)</summary>
    ValueType ValueType { get; set; }

    /// <summary>The collector result value (immutable)</summary>
    decimal Value { get; set; }
    
    /// <summary>The collector result culture name based on RFC 4646</summary>
    string Culture { get; set; }

    /// <summary>The starting date for the value (immutable)</summary>
    DateTime Start { get; set; }

    /// <summary>The ending date for the value (immutable)</summary>
    DateTime End { get; set; }

    /// <summary>The result tags</summary>
    List<string> Tags { get; set; }

    /// <summary>Test if two collector values are almost equals</summary>
    /// <param name="compare">The value to compare</param>
    /// <param name="precision">The test precision</param>
    /// <returns>True for almost equal value</returns>
    bool AlmostEqualValue(decimal? compare, int precision);
}