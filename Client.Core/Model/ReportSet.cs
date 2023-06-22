using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>
/// The report set API object
/// </summary>
public class ReportSet : Report, IReportSet
{
    /// <inheritdoc/>
    public int RegulationId { get; set; }

    /// <inheritdoc/>
    public List<ReportParameter> Parameters { get; set; }

    /// <inheritdoc/>
    public List<ReportTemplate> Templates { get; set; }

    /// <summary>Initializes a new instance</summary>
    public ReportSet()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public ReportSet(ReportSet copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public ReportSet(Report copySource) :
        base(copySource)
    {
    }

    /// <inheritdoc/>
    public virtual bool Equals(IReportSet compare) =>
        CompareTool.EqualProperties(this, compare);
}