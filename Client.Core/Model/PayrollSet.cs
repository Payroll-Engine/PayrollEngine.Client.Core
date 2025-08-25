using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll set client object</summary>
public class PayrollSet : Payroll, IPayrollSet
{
    /// <inheritdoc/>
    [JsonPropertyOrder(200)]
    public List<PayrollLayer> Layers { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(201)]
    public List<CaseChangeSetup> Cases { get; set; }

    /// <summary>Initializes a new instance</summary>
    public PayrollSet()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public PayrollSet(Payroll copySource) :
        base(copySource)
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public PayrollSet(PayrollSet copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IPayrollSet compare) =>
        CompareTool.EqualProperties(this, compare);
}