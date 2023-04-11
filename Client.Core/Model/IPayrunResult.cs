using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payrun result client object</summary>
public interface IPayrunResult : IModel, IAttributeObject
{
    /// <summary>The payroll result id (immutable)</summary>
    int PayrollResultId { get; set; }

    /// <summary>The result source (immutable)</summary>
    string Source { get; set; }

    /// <summary>The case field name (immutable)</summary>
    string Name { get; set; }

    /// <summary>The localized result names</summary>
    Dictionary<string, string> NameLocalizations { get; set; }

    /// <summary>The result slot (immutable)</summary>
    string Slot { get; set; }

    /// <summary>The value type (immutable)</summary>
    ValueType ValueType { get; set; }

    /// <summary>The result value (immutable)</summary>
    string Value { get; set; }

    /// <summary>The numeric result value (immutable)</summary>
    decimal? NumericValue { get; set; }

    /// <summary>The period starting date for the value</summary>
    DateTime Start { get; set; }

    /// <summary>The period ending date for the value</summary>
    DateTime End { get; set; }

    /// <summary>The result tags</summary>
    List<string> Tags { get; set; }
}