using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll case field client object</summary>
public class CaseField : ModelBase, ICaseField, INameObject
{
    /// <summary>The case field name</summary>
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
    public ValueType ValueType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public ValueScope ValueScope { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public CaseFieldTimeType TimeType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public CaseFieldTimeUnit TimeUnit { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public CaseFieldAggregationType PeriodAggregation { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(109)]
    public OverrideType OverrideType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(110)]
    public CaseFieldCancellationMode CancellationMode { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(111)]
    public CaseValueCreationMode ValueCreationMode { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(112)]
    public string Culture { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(113)]
    public bool ValueMandatory { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(114)]
    public int Order { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(115)]
    public CaseFieldDateType StartDateType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(116)]
    public CaseFieldDateType EndDateType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(117)]
    public bool EndMandatory { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(118)]
    public string DefaultStart { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(119)]
    public string DefaultEnd { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(120)]
    public string DefaultValue { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(121)]
    public List<string> Tags { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(122)]
    public LookupSettings LookupSettings { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(123)]
    public List<string> Clusters { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(124)]
    public List<string> BuildActions { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(125)]
    public List<string> ValidateActions { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(126)]
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Custom value attributes</summary>
    [JsonPropertyOrder(127)]
    public Dictionary<string, object> ValueAttributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public CaseField()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CaseField(CaseField copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ICaseField compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(ICaseField compare) =>
        string.Equals(Name, compare?.Name);

    /// <inheritdoc/>
    public override string GetUiString() => Name;
}