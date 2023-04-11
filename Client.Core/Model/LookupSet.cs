using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>Lookup including the lookup value</summary>
public class LookupSet : Lookup, ILookupSet, IEquatable<LookupSet>
{
    /// <inheritdoc/>
    [Required]
    public List<LookupValue> Values { get; set; }

    /// <summary>Initializes a new instance</summary>
    public LookupSet()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public LookupSet(LookupSet copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(LookupSet compare) =>
        CompareTool.EqualProperties(this, compare);
}