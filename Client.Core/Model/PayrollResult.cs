using System;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll result info client object</summary>
public class PayrollResult : ModelBase, IPayrollResult
{
    /// <inheritdoc/>
    [JsonPropertyOrder(100)]
    public string PayrunJobName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public int PayrunJobId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public int PayrollId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public int PayrunId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public int EmployeeId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public string EmployeeIdentifier { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public int DivisionId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public string CycleName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public DateTime CycleStart { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(109)]
    public DateTime CycleEnd { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(110)]
    public string PeriodName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(111)]
    public DateTime PeriodStart { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(112)]
    public DateTime PeriodEnd { get; set; }

    /// <summary>Initializes a new instance</summary>
    public PayrollResult()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public PayrollResult(PayrollResult copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IPayrollResult compare) =>
        CompareTool.EqualProperties(this, compare);
    
    /// <inheritdoc/>
    public override string GetUiString() => PeriodName;

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"result: {PeriodName} for employee {EmployeeId} on division {DivisionId} {base.ToString()}";
}