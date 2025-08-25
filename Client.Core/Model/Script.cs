using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll script client object</summary>
public class Script : ModelBase, IScript, INameObject
{
    /// <summary>The script name</summary>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(100)]
    public string Name { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public List<FunctionType> FunctionTypes { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public string Value { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public string ValueFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public OverrideType OverrideType { get; set; }

    /// <summary>Initializes a new instance</summary>
    public Script()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public Script(Script copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IScript compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(IScript compare) =>
        string.Equals(Name, compare?.Name);

    /// <inheritdoc/>
    public override string GetUiString() => Name;
}