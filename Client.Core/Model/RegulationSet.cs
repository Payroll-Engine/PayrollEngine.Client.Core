using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll regulation set client object</summary>
public class RegulationSet : Regulation, IRegulationSet
{
    /// <inheritdoc/>
    public List<CaseSet> Cases { get; set; }

    /// <inheritdoc/>
    public List<CaseRelation> CaseRelations { get; set; }

    /// <inheritdoc/>
    public List<WageType> WageTypes { get; set; }

    /// <inheritdoc/>
    public List<Collector> Collectors { get; set; }

    /// <inheritdoc/>
    public List<LookupSet> Lookups { get; set; }

    /// <inheritdoc/>
    public List<Script> Scripts { get; set; }

    /// <inheritdoc/>
    public List<ReportSet> Reports { get; set; }

    /// <summary>Initializes a new instance</summary>
    public RegulationSet()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public RegulationSet(IRegulation copySource) :
        base(copySource)
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public RegulationSet(IRegulationSet copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IRegulationSet compare) =>
        CompareTool.EqualProperties(this, compare);
}