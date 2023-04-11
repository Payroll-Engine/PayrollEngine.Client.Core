using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll case relation client object</summary>
public interface ICaseRelation : IModel, IAttributeObject
{
    /// <summary>The relation source case name (immutable)</summary>
    string SourceCaseName { get; set; }

    /// <summary>The localized source case names</summary>
    Dictionary<string, string> SourceCaseNameLocalizations { get; set; }

    /// <summary>The relation source case slot</summary>
    string SourceCaseSlot { get; set; }

    /// <summary>The localized source case slots</summary>
    Dictionary<string, string> SourceCaseSlotLocalizations { get; set; }

    /// <summary>The relation target case name (immutable)</summary>
    string TargetCaseName { get; set; }

    /// <summary>The localized target case names</summary>
    Dictionary<string, string> TargetCaseNameLocalizations { get; set; }

    /// <summary>The relation target case slot</summary>
    string TargetCaseSlot { get; set; }

    /// <summary>The localized target case slots</summary>
    Dictionary<string, string> TargetCaseSlotLocalizations { get; set; }

    /// <summary>The expression used to build the case relation</summary>
    string BuildExpression { get; set; }

    /// <summary>The expression used to build the case relation file</summary>
    string BuildExpressionFile { get; set; }

    /// <summary>The expression which evaluates if the case relation is valid</summary>
    string ValidateExpression { get; set; }

    /// <summary>The expression which evaluates if the case relation is valid file</summary>
    string ValidateExpressionFile { get; set; }

    /// <summary>The override type</summary>
    OverrideType OverrideType { get; set; }

    /// <summary>The case relation order</summary>
    int Order { get; set; }

    /// <summary>The case relation build actions</summary>
    List<string> BuildActions { get; set; }

    /// <summary>The case relation validate actions</summary>
    List<string> ValidateActions { get; set; }

    /// <summary>The case relation clusters</summary>
    List<string> Clusters { get; set; }
}