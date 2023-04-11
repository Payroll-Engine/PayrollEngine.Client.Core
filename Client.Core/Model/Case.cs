using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll case client object</summary>
public class Case : Model, ICase, IEquatable<Case>
{
    /// <inheritdoc/>
    public CaseType CaseType { get; set; }

    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    public List<string> NameSynonyms { get; set; }

    /// <inheritdoc/>
    public string Description { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <inheritdoc/>
    public string DefaultReason { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> DefaultReasonLocalizations { get; set; }

    /// <inheritdoc/>
    public string BaseCase { get; set; }

    /// <inheritdoc/>
    public List<CaseFieldReference> BaseCaseFields { get; set; }

    /// <inheritdoc/>
    public OverrideType OverrideType { get; set; }

    /// <inheritdoc/>
    public CaseCancellationType CancellationType { get; set; }

    /// <inheritdoc/>
    public string AvailableExpression { get; set; }

    /// <inheritdoc/>
    public string AvailableExpressionFile { get; set; }

    /// <inheritdoc/>
    public string BuildExpression { get; set; }

    /// <inheritdoc/>
    public string BuildExpressionFile { get; set; }

    /// <inheritdoc/>
    public string ValidateExpression { get; set; }

    /// <inheritdoc/>
    public string ValidateExpressionFile { get; set; }

    /// <inheritdoc/>
    public List<string> Lookups { get; set; }

    /// <inheritdoc/>
    public List<CaseSlot> Slots { get; set; }

    /// <inheritdoc/>
    public List<string> AvailableActions { get; set; }

    /// <inheritdoc/>
    public List<string> BuildActions { get; set; }

    /// <inheritdoc/>
    public List<string> ValidateActions { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, object> Attributes { get; set; }

    /// <inheritdoc/>
    public List<string> Clusters { get; set; }

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

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(Case compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Name} {base.ToString()}";
}