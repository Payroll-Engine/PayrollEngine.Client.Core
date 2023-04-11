using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll case client object</summary>
public interface ICase : IModel, IAttributeObject
{
    /// <summary>The type of he case (immutable)</summary>
    CaseType CaseType { get; set; }

    /// <summary>The case name (immutable)</summary>
    string Name { get; set; }

    /// <summary>The localized case names</summary>
    Dictionary<string, string> NameLocalizations { get; set; }

    /// <summary>Synonyms for the case name</summary>
    List<string> NameSynonyms { get; set; }

    /// <summary>The case description</summary>
    string Description { get; set; }

    /// <summary>The localized case descriptions</summary>
    Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <summary>The default case change reason</summary>
    string DefaultReason { get; set; }

    /// <summary>The localized default case change reasons</summary>
    Dictionary<string, string> DefaultReasonLocalizations { get; set; }

    /// <summary>The base case name</summary>
    string BaseCase { get; set; }

    /// <summary>The base case fields</summary>
    List<CaseFieldReference> BaseCaseFields { get; set; }

    /// <summary>The override type</summary>
    OverrideType OverrideType { get; set; }

    /// <summary>The cancellation type</summary>
    CaseCancellationType CancellationType { get; set; }

    /// <summary>The expression used to build a case</summary>
    string AvailableExpression { get; set; }

    /// <summary>The expression used to build a case file</summary>
    string AvailableExpressionFile { get; set; }

    /// <summary>The expression used to build a case</summary>
    string BuildExpression { get; set; }

    /// <summary>The expression used to build a case file</summary>
    string BuildExpressionFile { get; set; }

    /// <summary>The case validate expression</summary>
    string ValidateExpression { get; set; }

    /// <summary>The case validate expression file</summary>
    string ValidateExpressionFile { get; set; }

    /// <summary>The case lookups</summary>
    List<string> Lookups { get; set; }

    /// <summary>The case slots</summary>
    List<CaseSlot> Slots { get; set; }

    /// <summary>The case available actions</summary>
    List<string> AvailableActions { get; set; }

    /// <summary>The case build actions</summary>
    List<string> BuildActions { get; set; }

    /// <summary>The case validate actions</summary>
    List<string> ValidateActions { get; set; }

    /// <summary>The case clusters</summary>
    List<string> Clusters { get; set; }
}