using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll regulation set client object</summary>
public class RegulationSet : Regulation, IRegulationSet
{
    /// <inheritdoc/>
    [JsonPropertyOrder(200)]
    public List<CaseSet> Cases { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(201)]
    public List<CaseRelation> CaseRelations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(202)]
    public List<Collector> Collectors { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(203)]
    public List<WageType> WageTypes { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(204)]
    public List<LookupSet> Lookups { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(205)]
    public List<Script> Scripts { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(206)]
    public List<ReportSet> Reports { get; set; }

    /// <summary>Initializes a new instance</summary>
    public RegulationSet()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public RegulationSet(Regulation copySource) :
        base(copySource)
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public RegulationSet(RegulationSet copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IRegulationSet compare) =>
        CompareTool.EqualProperties(this, compare);
}