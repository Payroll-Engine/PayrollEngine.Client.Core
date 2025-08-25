using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll regulation client object</summary>
public class Regulation : ModelBase, IRegulation, INameObject
{
    /// <summary>The regulation name</summary>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(100)]
    public string Name { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public int Version { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public bool SharedRegulation { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public DateTime? ValidFrom { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(105)]
    public string Owner { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public string Description { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public List<string> BaseRegulations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(109)]
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public Regulation()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public Regulation(Regulation copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IRegulation compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(IRegulation compare) =>
        string.Equals(Name, compare?.Name);
   
    /// <inheritdoc/>
    public override string GetUiString() => Name;
}