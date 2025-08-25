using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll collector client object</summary>
public class Collector : ModelBase, ICollector, INameObject
{
    /// <summary>The collector name</summary>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(100)]
    public string Name { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public CollectMode CollectMode { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public bool Negated { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public OverrideType OverrideType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public ValueType ValueType { get; set; } = ValueType.Decimal;

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public List<string> CollectorGroups { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public decimal? Threshold { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public decimal? MinResult { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(109)]
    public decimal? MaxResult { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(110)]
    public string StartExpression { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(111)]
    public string StartExpressionFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(112)]
    public string ApplyExpression { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(113)]
    public string ApplyExpressionFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(114)]
    public string EndExpression { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(115)]
    public string EndExpressionFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(116)]
    public List<string> Clusters { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(117)]
    public Dictionary<string, object> Attributes { get; set; }

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