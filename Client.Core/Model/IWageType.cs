using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll wage type client object</summary>
public interface IWageType : IModel, IAttributeObject, IKeyEquatable<IWageType>
{
    /// <summary>The wage type number (immutable)</summary>
    decimal WageTypeNumber { get; set; }

    /// <summary>The wage type name</summary>
    string Name { get; set; }

    /// <summary>The localized wage type names</summary>
    Dictionary<string, string> NameLocalizations { get; set; }

    /// <summary>The wage type description</summary>
    string Description { get; set; }

    /// <summary>The localized wage type descriptions</summary>
    Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <summary>The override type</summary>
    OverrideType OverrideType { get; set; }

    /// <summary>The value type, default is value type money</summary>
    ValueType ValueType { get; set; }

    /// <summary>The wage type calendar (fallback: employee calendar)</summary>
    string Calendar { get; set; }

    /// <summary>Associated collectors</summary>
    List<string> Collectors { get; set; }

    /// <summary>Associated collector groups</summary>
    List<string> CollectorGroups { get; set; }

    /// <summary>Expression: calculates of the wage type value</summary>
    string ValueExpression { get; set; }

    /// <summary>Expression: calculates of the wage type value file</summary>
    string ValueExpressionFile { get; set; }

    /// <summary>Expression: calculates of the wage type result attributes</summary>
    string ResultExpression { get; set; }

    /// <summary>Expression: calculates of the wage type result attributes file</summary>
    string ResultExpressionFile { get; set; }

    /// <summary>The wage type clusters</summary>
    List<string> Clusters { get; set; }
}