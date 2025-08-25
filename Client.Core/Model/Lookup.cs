using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll lookup client object identified by his unique name.
/// The lookup contains one or more columns and the ValueColumn indicates.</summary>
public class Lookup : ModelBase, ILookup, INameObject
{
    /// <summary>The lookup name</summary>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(100)]
    public string Name { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(100)]
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public string Description { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public decimal? RangeSize { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public OverrideType OverrideType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public Dictionary<string, object> Attributes { get; set; }

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

    /// <inheritdoc/>
    public virtual bool Equals(ILookup compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(ILookup compare) =>
        string.Equals(Name, compare?.Name);

    /// <inheritdoc/>
    public override string GetUiString() => Name;
}