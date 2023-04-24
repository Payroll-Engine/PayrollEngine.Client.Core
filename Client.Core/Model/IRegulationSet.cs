using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll regulation client object</summary>
public interface IRegulationSet : IRegulation, IEquatable<IRegulationSet>
{
    /// <summary>The regulation cases</summary>
    List<CaseSet> Cases { get; set; }

    /// <summary>The regulation case relations</summary>
    List<CaseRelation> CaseRelations { get; set; }

    /// <summary>The regulation wage types</summary>
    List<WageType> WageTypes { get; set; }

    /// <summary>The regulation collectors</summary>
    List<Collector> Collectors { get; set; }

    /// <summary>The regulation lookups (including lookup values)</summary>
    List<LookupSet> Lookups { get; set; }

    /// <summary>The regulation scripts</summary>
    List<Script> Scripts { get; set; }
        
    /// <summary>The regulation reports</summary>
    List<ReportSet> Reports { get; set; }
}