using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PayrollEngine.Data;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll report client object</summary>
public class Report : ModelBase, IReport, INameObject
{
    /// <summary>The report name</summary>
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
    public string Category { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public ReportAttributeMode AttributeMode { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public UserType UserType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public string BuildExpression { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public string BuildExpressionFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(109)]
    public string StartExpression { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(110)]
    public string StartExpressionFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(111)]
    public string EndExpression { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(112)]
    public string EndExpressionFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(113)]
    public OverrideType OverrideType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(114)]
    public Dictionary<string, string> Queries { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(115)]
    public List<DataRelation> Relations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(116)]
    public List<string> Clusters { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(117)]
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public Report()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public Report(Report copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IReport compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(IReport compare) =>
        string.Equals(Name, compare?.Name);

    /// <inheritdoc/>
    public override string GetUiString() => Name;
}