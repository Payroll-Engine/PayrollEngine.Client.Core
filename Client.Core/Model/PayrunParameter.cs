using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll payrun parameter client object</summary>
public class PayrunParameter : Model, IPayrunParameter
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
    public PayrunParameter(IPayrunParameter copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IPayrunParameter compare) =>
        CompareTool.EqualProperties(this, compare);
    
    /// <inheritdoc/>
    public virtual bool EqualKey(IPayrunParameter compare) =>
        string.Equals(Name, compare?.Name);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Name} {base.ToString()}";
}