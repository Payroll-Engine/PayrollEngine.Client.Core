using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll layer client object</summary>
public class PayrollLayer : Model, IPayrollLayer
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
    public PayrollLayer(IPayrollLayer copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IPayrollLayer compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(IPayrollLayer compare) =>
        Level == compare?.Level &&
        Priority == compare.Priority &&
        string.Equals(RegulationName, compare.RegulationName);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Level}.{Priority} -> {RegulationName} {base.ToString()}";
}