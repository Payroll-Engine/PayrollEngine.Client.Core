using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll regulation client object</summary>
public class Regulation : ModelBase, IRegulation, INameObject
{
    /// <summary>The regulation name</summary>
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    public int Version { get; set; }

    /// <inheritdoc/>
    public bool SharedRegulation { get; set; }

    /// <inheritdoc/>
    public DateTime? ValidFrom { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string Owner { get; set; }

    /// <inheritdoc/>
    public string Description { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <inheritdoc/>
    public List<string> BaseRegulations { get; set; }

    /// <inheritdoc/>
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