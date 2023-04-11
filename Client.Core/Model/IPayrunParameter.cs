using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payrun parameter client object</summary>
public interface IPayrunParameter : IModel, IAttributeObject
{
    /// <summary>The payrun parameter name</summary>
    string Name { get; set; }

    /// <summary>The localized wage type names</summary>
    Dictionary<string, string> NameLocalizations { get; set; }

    /// <summary>The payrun parameter description</summary>
    string Description { get; set; }

    /// <summary>The localized payrun parameter descriptions</summary>
    Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <summary>The parameter mandatory state</summary>
    bool Mandatory { get; set; }

    /// <summary>The parameter value (JSON)</summary>
    string Value { get; set; }

    /// <summary>The parameter value type</summary>
    ValueType ValueType { get; set; }
}