using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>
/// The report set API object
/// </summary>
public class ReportSet : Report, IEquatable<ReportSet>, IReportSet
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

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(ReportSet compare) =>
        CompareTool.EqualProperties(this, compare);
}