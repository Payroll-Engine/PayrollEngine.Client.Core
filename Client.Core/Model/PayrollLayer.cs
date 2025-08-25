using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll layer client object</summary>
public class PayrollLayer : ModelBase, IPayrollLayer
{
    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(100)]
    public int Level { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public int Priority { get; set; } = 1;

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(102)]
    public string RegulationName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
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

    /// <inheritdoc/>
    public virtual bool Equals(IPayrollLayer compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(IPayrollLayer compare) =>
        Level == compare?.Level &&
        Priority == compare.Priority &&
        string.Equals(RegulationName, compare.RegulationName);

    /// <inheritdoc/>
    public override string GetUiString() =>
        $"{RegulationName} [{Level}.{Priority}]";
}