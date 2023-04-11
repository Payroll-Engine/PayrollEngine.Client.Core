using System;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll report log client object</summary>
public class ReportLog : Model, IReportLog, IEquatable<ReportLog>
{
    /// <inheritdoc/>
    public string ReportName { get; set; }

    /// <inheritdoc/>
    public DateTime ReportDate { get; set; }

    /// <inheritdoc/>
    public string Key { get; set; }

    /// <inheritdoc/>
    public string User { get; set; }

    /// <inheritdoc/>
    public string Message { get; set; }

    /// <summary>Initializes a new instance of the <see cref="ReportLog"/> class</summary>
    public ReportLog()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="ReportLog"/> class</summary>
    /// <param name="copySource">The copy source.</param>
    public ReportLog(ReportLog copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(ReportLog compare) =>
        CompareTool.EqualProperties(this, compare);
}