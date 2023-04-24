using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The report request client object</summary>
public class ReportRequest : IEquatable<ReportRequest>
{
    /// <summary>The report language</summary>
    public Language Language { get; set; }

    /// <summary>The report user id</summary>
    public int UserId { get; set; }

    /// <summary>The report user identifier</summary>
    public string UserIdentifier { get; set; }

    /// <summary>The report parameters</summary>
    public Dictionary<string, string> Parameters { get; set; }

    /// <summary>Initializes a new instance of the <see cref="ReportRequest"/> class</summary>
    public ReportRequest()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="ReportRequest"/> class</summary>
    /// <param name="copySource">The copy source.</param>
    public ReportRequest(ReportRequest copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ReportRequest compare) =>
        CompareTool.EqualProperties(this, compare);
}