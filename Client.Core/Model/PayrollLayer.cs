using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll layer client object</summary>
public class PayrollLayer : Model, IPayrollLayer, IEquatable<PayrollLayer>
{
    /// <inheritdoc/>
    [Required]
    public int Level { get; set; }

    /// <inheritdoc/>
    [Required]
    public int Priority { get; set; }

    /// <inheritdoc/>
    [Required]
    public string RegulationName { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public PayrollLayer()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public PayrollLayer(PayrollLayer copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(PayrollLayer compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Level}.{Priority} -> {RegulationName} {base.ToString()}";
}