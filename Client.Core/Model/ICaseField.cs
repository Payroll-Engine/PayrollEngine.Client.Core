using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll case field client object</summary>
public interface ICaseField : IModel, IAttributeObject, IKeyEquatable<ICaseField>
{
    /// <summary>The case field name (immutable)</summary>
    string Name { get; set; }

    /// <summary>The localized case field names</summary>
    Dictionary<string, string> NameLocalizations { get; set; }

    /// <summary>The case field description</summary>
    string Description { get; set; }

    /// <summary>The localized case field descriptions</summary>
    Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <summary>The value type of the case field</summary>
    ValueType ValueType { get; set; }

    /// <summary>The value scope</summary>
    ValueScope ValueScope { get; set; }

    /// <summary>The date period type</summary>
    CaseFieldTimeType TimeType { get; set; }

    /// <summary>The date unit type</summary>
    CaseFieldTimeUnit TimeUnit { get; set; }

    /// <summary>The period aggregation type for <see cref="CaseFieldTimeType.Period"/></summary>
    CaseFieldAggregationType PeriodAggregation { get; set; }

    /// <summary>The override type</summary>
    OverrideType OverrideType { get; set; }

    /// <summary>The cancellation mode</summary>
    CaseFieldCancellationMode CancellationMode { get; set; }

    /// <summary>The case value creation mode</summary>
    CaseValueCreationMode ValueCreationMode { get; set; }

    /// <summary>The case field culture name based on RFC 4646</summary>
    string Culture { get; set; }

    /// <summary>Mandatory case field value</summary>
    bool ValueMandatory { get; set; }

    /// <summary>The case field order</summary>
    int Order { get; set; }

    /// <summary>The start date type</summary>
    CaseFieldDateType StartDateType { get; set; }

    /// <summary>The end date type</summary>
    CaseFieldDateType EndDateType { get; set; }

    /// <summary>The end date mandatory state</summary>
    bool EndMandatory { get; set; }

    /// <summary>The default start value of the case field (date or expression)</summary>
    string DefaultStart { get; set; }

    /// <summary>The default end value of the case field (date or expression)</summary>
    string DefaultEnd { get; set; }

    /// <summary>The default value of the case field (JSON format)</summary>
    string DefaultValue { get; set; }

    /// <summary>The case field tags</summary>
    List<string> Tags { get; set; }

    /// <summary>The lookup settings</summary>
    LookupSettings LookupSettings { get; set; }

    /// <summary>The case field clusters</summary>
    List<string> Clusters { get; set; }

    /// <summary>The case field build actions</summary>
    List<string> BuildActions { get; set; }

    /// <summary>The case field validate actions</summary>
    List<string> ValidateActions { get; set; }

    /// <summary>Custom value attributes</summary>
    Dictionary<string, object> ValueAttributes { get; set; }
}