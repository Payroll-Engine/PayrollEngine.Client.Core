using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll payrun parameter client object</summary>
public class PayrunParameter : Model, IPayrunParameter, IEquatable<PayrunParameter>
{
    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    public string Description { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <inheritdoc/>
    public bool Mandatory { get; set; }

    /// <inheritdoc/>
    public string Value { get; set; }

    /// <inheritdoc/>
    public ValueType ValueType { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance of the <see cref="PayrunParameter"/> class</summary>
    public PayrunParameter()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="PayrunParameter"/> class</summary>
    /// <param name="copySource">The copy source.</param>
    public PayrunParameter(PayrunParameter copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(PayrunParameter compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Name} {base.ToString()}";
}