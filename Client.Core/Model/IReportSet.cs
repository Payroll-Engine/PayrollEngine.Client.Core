using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The report set client object</summary>
public interface IReportSet : IReport, IEquatable<IReportSet>
{
    /// <summary>The regulation id</summary>
    int RegulationId { get; set; }

    /// <summary>The report parameters</summary>
    List<ReportParameter> Parameters { get; set; }
        
    /// <summary>The report templates</summary>
    List<ReportTemplate> Templates { get; set; }

}