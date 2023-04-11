using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The employee client object</summary>
public class Employee : Model, IEmployee, IEquatable<Employee>
{
    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    public string Identifier { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string FirstName { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string LastName { get; set; }

    /// <inheritdoc/>
    public Language Language { get; set; }

    /// <inheritdoc/>
    public List<string> Divisions { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string Culture { get; set; }

    /// <inheritdoc/>
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

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(Employee compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Identifier} {base.ToString()}";
}