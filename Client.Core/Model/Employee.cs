using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The employee client object</summary>
public class Employee : ModelBase, IEmployee, IIdentifierObject
{
    /// <summary>The employee identifier</summary>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(100)]
    public string Identifier { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(101)]
    public string FirstName { get; set; }

    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(102)]
    public string LastName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public List<string> Divisions { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(104)]
    public string Culture { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(105)]
    public string Calendar { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public Employee()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public Employee(Employee copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IEmployee compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(IEmployee compare) =>
        string.Equals(Identifier, compare?.Identifier);

    /// <inheritdoc/>
    public override string GetUiString() => Identifier;
}