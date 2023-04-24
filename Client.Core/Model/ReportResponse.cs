using System;
using System.Collections.Generic;
using PayrollEngine.Data;

namespace PayrollEngine.Client.Model;

/// <summary>The report response client object</summary>
public class ReportResponse : IEquatable<ReportResponse>
{
    /// <summary>The report queries, key is the query name and value the api operation name</summary>
    public Dictionary<string, string> Queries { get; set; }

    /// <summary>The report relations</summary>
    public List<DataRelation> Relations { get; set; }

    /// <summary>The report parameters</summary>
    public Dictionary<string, string> Parameters { get; set; }

    /// <summary>The report name</summary>
    public string ReportName { get; set; }

    /// <summary>The report language</summary>
    public Language Language { get; set; }

    /// <summary>The report user identifier</summary>
    public string User { get; set; }

    /// <summary>The report result data, a serialized data set</summary>
    public DataSet Result { get; set; }

    /// <summary>Initializes a new instance of the <see cref="ReportResponse"/> class</summary>
    public ReportResponse()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="ReportResponse"/> class</summary>
    /// <param name="copySource">The copy source.</param>
    public ReportResponse(ReportResponse copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ReportResponse compare) =>
        CompareTool.EqualProperties(this, compare);
}