using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PayrollEngine.Data;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll report client object</summary>
public class Report : Model, IReport
{
    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    public string Description { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <inheritdoc/>
    public string Category { get; set; }

    /// <inheritdoc/>
    public ReportAttributeMode AttributeMode { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> Queries { get; set; }

    /// <inheritdoc/>
    public List<DataRelation> Relations { get; set; }

    /// <inheritdoc/>
    public string BuildExpression { get; set; }

    /// <inheritdoc/>
    public string BuildExpressionFile { get; set; }

    /// <inheritdoc/>
    public string StartExpression { get; set; }

    /// <inheritdoc/>
    public string StartExpressionFile { get; set; }

    /// <inheritdoc/>
    public string EndExpression { get; set; }

    /// <inheritdoc/>
    public string EndExpressionFile { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, object> Attributes { get; set; }

    /// <inheritdoc/>
    public List<string> Clusters { get; set; }

    /// <summary>Initializes a new instance</summary>
    public Report()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public Report(IReport copySource) :
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

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Name} {base.ToString()}";
}