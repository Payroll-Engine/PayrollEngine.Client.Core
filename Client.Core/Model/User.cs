using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll user client object</summary>
public class User : ModelBase, IUser, IIdentifierObject
{
    /// <summary>The user identifier</summary>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(100)]
    public string Identifier { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(101)]
    public string Password { get; set; }

    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(102)]
    public string FirstName { get; set; }

    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(103)]
    public string LastName { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(104)]
    public string Culture { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public UserType UserType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public User()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public User(User copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IUser compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(IUser compare) =>
        string.Equals(Identifier, compare?.Identifier);

    /// <inheritdoc/>
    public override string GetUiString() => Identifier;
}