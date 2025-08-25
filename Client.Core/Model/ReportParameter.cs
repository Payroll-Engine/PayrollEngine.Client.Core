using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll report parameter client object</summary>
public class ReportParameter : ModelBase, IReportParameter, INameObject
{
    /// <summary>The report parameter name</summary>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(100)]
    public string Name { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public string Description { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public bool Mandatory { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public bool Hidden { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public string Value { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public ValueType ValueType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public ReportParameterType ParameterType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(109)]
    public OverrideType OverrideType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(110)]
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance of the <see cref="ReportParameter"/> class</summary>
    public ReportParameter()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="ReportParameter"/> class</summary>
    /// <param name="copySource">The copy source.</param>
    public ReportParameter(ReportParameter copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IReportParameter compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(IReportParameter compare) =>
        string.Equals(Name, compare?.Name);

    /// <inheritdoc/>
    public override string GetUiString() => Name;
}