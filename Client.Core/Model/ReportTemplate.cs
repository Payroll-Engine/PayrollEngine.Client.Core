using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll report template client object</summary>
public class ReportTemplate : Model, IReportTemplate, IEquatable<ReportTemplate>
{
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

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(ReportTemplate compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Language} {base.ToString()}";
}