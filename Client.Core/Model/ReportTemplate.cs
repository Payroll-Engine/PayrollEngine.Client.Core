using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll report template client object</summary>
public class ReportTemplate : ModelBase, IReportTemplate
{
    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(100)]
    public string Name { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(101)]
    public string Culture { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public string Content { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public string ContentFile { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(104)]
    public string ContentType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public string Schema { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public string SchemaFile { get; set; }

    /// <inheritdoc/>
    [StringLength(256)]
    [JsonPropertyOrder(107)]
    public string Resource { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public OverrideType OverrideType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(109)]
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance of the <see cref="ReportTemplate"/> class</summary>
    public ReportTemplate()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="ReportTemplate"/> class</summary>
    /// <param name="copySource">The copy source.</param>
    public ReportTemplate(ReportTemplate copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IReportTemplate compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public override string GetUiString() => Culture;
}