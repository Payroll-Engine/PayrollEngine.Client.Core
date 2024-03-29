﻿using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The report parameter client object</summary>
public interface IReportParameter : IModel, IAttributeObject, IKeyEquatable<IReportParameter>
{
    /// <summary>The report parameter name</summary>
    string Name { get; set; }

    /// <summary>The localized wage type names</summary>
    Dictionary<string, string> NameLocalizations { get; set; }

    /// <summary>The report parameter description</summary>
    string Description { get; set; }

    /// <summary>The localized report parameter descriptions</summary>
    Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <summary>The parameter mandatory state</summary>
    bool Mandatory { get; set; }

    /// <summary>Hidden parameter</summary>
    bool Hidden { get; set; }

    /// <summary>The parameter value (JSON)</summary>
    string Value { get; set; }

    /// <summary>The parameter value type</summary>
    ValueType ValueType { get; set; }

    /// <summary>The parameter type</summary>
    ReportParameterType ParameterType { get; set; }

    /// <summary>The override type</summary>
    OverrideType OverrideType { get; set; }
}