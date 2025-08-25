using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The tenant client object</summary>
public class Tenant : ModelBase, ITenant, IIdentifierObject
{
    /// <summary>The tenant identifier</summary>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(100)]
    public string Identifier { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(101)]
    public string Culture { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(102)]
    public string Calendar { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public Tenant()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public Tenant(Tenant copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ITenant compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(ITenant compare) =>
        string.Equals(Identifier, compare?.Identifier);

    /// <inheritdoc/>
    public override string GetUiString() => Identifier;
}