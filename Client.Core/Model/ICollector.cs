using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll collector client object</summary>
public interface ICollector : IModel, IAttributeObject, IKeyEquatable<ICollector>
{
    /// <summary>The collector name (immutable)</summary>
    string Name { get; set; }

    /// <summary>The localized collector names</summary>
    Dictionary<string, string> NameLocalizations { get; set; }

    /// <summary>The collect mode (immutable, default: summary)</summary>
    CollectMode CollectMode { get; set; }

    /// <summary>Negated collector result (immutable, default: false)</summary>
    bool Negated { get; set; }

    /// <summary>The override type</summary>
    OverrideType OverrideType { get; set; }

    /// <summary>The value type, default is value type money</summary>
    ValueType ValueType { get; set; }

    /// <summary>Associated collector groups</summary>
    List<string> CollectorGroups { get; set; }

    /// <summary>The threshold value</summary>
    decimal? Threshold { get; set; }

    /// <summary>The minimum allowed value</summary>
    decimal? MinResult { get; set; }

    /// <summary>The maximum allowed value</summary>
    decimal? MaxResult { get; set; }

    /// <summary>Expression used while the collector is started</summary>
    string StartExpression { get; set; }

    /// <summary>Expression used while the collector is started file</summary>
    string StartExpressionFile { get; set; }

    /// <summary>Expression used while applying a value to the collector</summary>
    string ApplyExpression { get; set; }

    /// <summary>Expression used while applying a value to the collector file</summary>
    string ApplyExpressionFile { get; set; }

    /// <summary>Expression used while the collector is ended</summary>
    string EndExpression { get; set; }

    /// <summary>Expression used while the collector is ended file</summary>
    string EndExpressionFile { get; set; }

    /// <summary>The collector clusters</summary>
    List<string> Clusters { get; set; }
}