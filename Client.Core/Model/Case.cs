using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll case client object</summary>
public class Case : ModelBase, ICase, INameObject
{
    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(100)]
    public CaseType CaseType { get; set; }

    /// <summary>The case name</summary>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(101)]
    public string Name { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public List<string> NameSynonyms { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public string Description { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public string DefaultReason { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public Dictionary<string, string> DefaultReasonLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public string BaseCase { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(109)]
    public List<CaseFieldReference> BaseCaseFields { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(110)]
    public OverrideType OverrideType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(111)]
    public CaseCancellationType CancellationType { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(112)]
    public bool Hidden { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(113)]
    public string AvailableExpression { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(114)]
    public string AvailableExpressionFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(115)]
    public string BuildExpression { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(116)]
    public string BuildExpressionFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(117)]
    public string ValidateExpression { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(118)]
    public string ValidateExpressionFile { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(119)]
    public List<string> AvailableActions { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(120)]
    public List<string> BuildActions { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(121)]
    public List<string> ValidateActions { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(122)]
    public List<string> Lookups { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(123)]
    public List<CaseSlot> Slots { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(124)]
    public List<string> Clusters { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(125)]
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public Case()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public Case(Case copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ICase compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(ICase compare) =>
        string.Equals(Name, compare?.Name);

    /// <inheritdoc/>
    public override string GetUiString() => Name;
}