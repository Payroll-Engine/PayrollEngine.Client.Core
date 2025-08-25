using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>
/// The report set API object
/// </summary>
public class ReportSet : Report, IReportSet
{
    /// <inheritdoc/>
    [JsonPropertyOrder(200)]
    public int RegulationId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(201)]
    public List<ReportParameter> Parameters { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(202)]
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