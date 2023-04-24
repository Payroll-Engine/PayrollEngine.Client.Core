using System;

namespace PayrollEngine.Client.Model;

/// <summary>The report log client object</summary>
public interface IReportLog : IModel, IEquatable<IReportLog>
{
    /// <summary>The report name (immutable)</summary>
    public string ReportName { get; set; }

    /// <summary>The report date (immutable)</summary>
    public DateTime ReportDate { get; set; }

    /// <summary>The report log key (immutable)</summary>
    public string Key { get; set; }

    /// <summary>The report user (immutable)</summary>
    public string User { get; set; }

    /// <summary>The report message (immutable)</summary>
    public string Message { get; set; }
}