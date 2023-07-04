using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll collector client object</summary>
public class Collector : ModelBase, ICollector, INameObject
{
    /// <summary>The collector name</summary>
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    [Required]
    public CollectType CollectType { get; set; }

    /// <inheritdoc/>
    public OverrideType OverrideType { get; set; }

    /// <inheritdoc/>
    public ValueType ValueType { get; set; } = ValueType.Decimal;

    /// <inheritdoc/>
    public List<string> CollectorGroups { get; set; }

    /// <inheritdoc/>
    public decimal? Threshold { get; set; }

    /// <inheritdoc/>
    public decimal? MinResult { get; set; }

    /// <inheritdoc/>
    public decimal? MaxResult { get; set; }

    /// <inheritdoc/>
    public string StartExpression { get; set; }

    /// <inheritdoc/>
    public string StartExpressionFile { get; set; }

    /// <inheritdoc/>
    public string ApplyExpression { get; set; }

    /// <inheritdoc/>
    public string ApplyExpressionFile { get; set; }

    /// <inheritdoc/>
    public string EndExpression { get; set; }

    /// <inheritdoc/>
    public string EndExpressionFile { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, object> Attributes { get; set; }

    /// <inheritdoc/>
    public List<string> Clusters { get; set; }

    /// <summary>Initializes a new instance</summary>
    public Collector()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public Collector(Collector copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ICollector compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(ICollector compare) =>
        string.Equals(Name, compare?.Name);
        
    /// <inheritdoc/>
    public override string GetUiString() => Name;
}