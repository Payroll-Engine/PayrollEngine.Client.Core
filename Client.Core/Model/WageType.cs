using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll wage type client object</summary>
public class WageType : ModelBase, IWageType, INameObject
{
    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(100)]
    public decimal WageTypeNumber { get; set; }

    /// <summary>The wage type name</summary>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(100)]
    public string Name { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public string Description { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public OverrideType OverrideType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public ValueType ValueType { get; set; } = ValueType.Money;

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public string Calendar { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public string ValueExpression { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public string ValueExpressionFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(109)]
    public string ResultExpression { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(110)]
    public string ResultExpressionFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(111)]
    public List<string> Collectors { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(112)]
    public List<string> CollectorGroups { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(113)]
    public List<string> Clusters { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(114)]
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public WageType()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public WageType(WageType copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IWageType compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(IWageType compare) =>
        WageTypeNumber == compare.WageTypeNumber;

    /// <inheritdoc/>
    public override string GetUiString() =>
        $"{Name} [{WageTypeNumber:##.####}]";
}