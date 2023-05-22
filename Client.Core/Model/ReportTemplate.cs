using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll report template client object</summary>
public class ReportTemplate : Model, IReportTemplate
{
    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    /// <inheritdoc/>
    [Required]
    public Language Language { get; set; }

    /// <inheritdoc/>
    public string Content { get; set; }

    /// <inheritdoc/>
    public string ContentFile { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string ContentType { get; set; }

    /// <inheritdoc/>
    public string Schema { get; set; }

    /// <inheritdoc/>
    public string SchemaFile { get; set; }

    /// <inheritdoc/>
    [StringLength(256)]
    public string Resource { get; set; }

    /// <inheritdoc/>
    public OverrideType OverrideType { get; set; }

    /// <inheritdoc/>
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
    public override string GetUiString() => Language.ToString();
}