using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payrun client object</summary>
public class Payrun : ModelBase, IPayrun, INameObject
{
    /// <inheritdoc/>
    [JsonPropertyOrder(100)]
    public int PayrollId { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(101)]
    public string PayrollName { get; set; }

    /// <summary>The payrun name</summary>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(102)]
    public string Name { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public string DefaultReason { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public Dictionary<string, string> DefaultReasonLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public string StartExpression { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public string StartExpressionFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public string EmployeeAvailableExpression { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(109)]
    public string EmployeeAvailableExpressionFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(110)]
    public string EmployeeStartExpression { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(111)]
    public string EmployeeStartExpressionFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(112)]
    public string EmployeeEndExpression { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(113)]
    public string EmployeeEndExpressionFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(114)]
    public string WageTypeAvailableExpression { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(115)]
    public string WageTypeAvailableExpressionFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(116)]
    public string EndExpression { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(117)]
    public string EndExpressionFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(118)]
    public RetroTimeType RetroTimeType { get; set; }

    /// <summary>The payrun parameters</summary>
    [JsonPropertyOrder(100)]
    public List<PayrunParameter> PayrunParameters { get; set; }

    /// <summary>Initializes a new instance</summary>
    public Payrun()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public Payrun(Payrun copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IPayrun compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(IPayrun compare) =>
        string.Equals(PayrollName, compare?.PayrollName) &&
        string.Equals(Name, compare?.Name);

    /// <inheritdoc/>
    public override string GetUiString() => Name;
}