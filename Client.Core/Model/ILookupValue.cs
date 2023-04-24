using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>Represents a value within a lookup</summary>
public interface ILookupValue : IModel, IKeyEquatable<ILookupValue>
{
    /// <summary>The lookup key</summary>
    string Key { get; set; }

    /// <summary>The lookup value as JSON</summary>
    string Value { get; set; }

    /// <summary>The localized lookup values</summary>
    Dictionary<string, string> ValueLocalizations { get; set; }

    /// <summary>The lookup range value</summary>
    decimal? RangeValue { get; set; }
}