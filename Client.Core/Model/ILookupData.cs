using System;

namespace PayrollEngine.Client.Model;

/// <summary>Localized lookup for UI cases like list/grid selections</summary>
public interface ILookupData : IEquatable<ILookupData>
{
    /// <summary>The lookup name</summary>
    string Name { get; set; }

    /// <summary>The culture of the values</summary>
    string Culture { get; set; }

    /// <summary>The lookup values</summary>
    LookupValueData[] Values { get; set; }

    /// <summary>The lookup range size</summary>
    decimal? RangeSize { get; set; }
}