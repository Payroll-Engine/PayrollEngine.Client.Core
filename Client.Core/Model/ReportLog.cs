using System;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll report log client object</summary>
public class ReportLog : ModelBase, IReportLog
{
    /// <inheritdoc/>
    [JsonPropertyOrder(100)]
    public string ReportName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public DateTime ReportDate { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public string Key { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public string User { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
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

    /// <inheritdoc/>
    public virtual bool Equals(IReportLog compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public override string GetUiString() => ReportName;
}