using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll user client object</summary>
public class User : Model, IUser, IIdentifierObject
{
    /// <summary>The user identifier</summary>
    [Required]
    [StringLength(128)]
    public string Identifier { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string Password { get; set; }

    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    public string FirstName { get; set; }

    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    public string LastName { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string Culture { get; set; }

    /// <inheritdoc/>
    public Language Language { get; set; }

    /// <inheritdoc/>
    public UserType UserType { get; set; }

    /// <inheritdoc/>
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