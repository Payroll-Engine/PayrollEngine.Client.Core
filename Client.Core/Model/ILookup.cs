using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll lookup client object identified by his unique name.
/// The lookup contains one or more columns and the ValueColumn indicates.</summary>
public interface ILookup : IModel, IAttributeObject, IKeyEquatable<ILookup>
{
    /// <summary>The unique lookup name (immutable)</summary>
    string Name { get; set; }

    /// <summary>The localized lookup names</summary>
    Dictionary<string, string> NameLocalizations { get; set; }

    /// <summary>The lookup description</summary>
    string Description { get; set; }

    /// <summary>The localized lookup descriptions</summary>
    Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <summary>The override type</summary>
    OverrideType OverrideType { get; set; }

    /// <summary>Lookup range mode</summary>
    LookupRangeMode RangeMode { get; set; }

    /// <summary>The lookup range size</summary>
    decimal? RangeSize { get; set; }
}