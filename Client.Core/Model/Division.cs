using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll division client object</summary>
public class Division : ModelBase, IDivision, INameObject
{
    /// <summary>The division name</summary>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(100)]
    public string Name { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(102)]
    public string Culture { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(103)]
    public string Calendar { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public Division()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public Division(Division copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IDivision compare) =>
        CompareTool.EqualProperties(this, compare);
    
    /// <inheritdoc/>
    public virtual bool EqualKey(IDivision compare) =>
        string.Equals(Name, compare?.Name);
    
    /// <inheritdoc/>
    public override string GetUiString() => Name;
}