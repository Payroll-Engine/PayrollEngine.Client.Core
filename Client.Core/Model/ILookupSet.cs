using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>Lookup including the lookup value</summary>
public interface ILookupSet : ILookup, IEquatable<ILookupSet>
{
    /// <summary>The lookup values</summary>
    List<LookupValue> Values { get; set; }
}