using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll lookup client object identified by his unique name.
/// The lookup contains one or more columns and the ValueColumn indicates.</summary>
public class Lookup : Model, ILookup, IEquatable<Lookup>
{
    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    public string Description { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <inheritdoc/>
    public OverrideType OverrideType { get; set; }

    /// <inheritdoc/>
    public decimal? RangeSize { get; set; }

    /// <summary>Initializes a new instance</summary>
    public Lookup()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public Lookup(Lookup copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(Lookup compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Name} {base.ToString()}";
}