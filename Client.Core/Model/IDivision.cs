using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll division client object</summary>
public interface IDivision : IModel, IAttributeObject
{
    /// <summary>The division name</summary>
    string Name { get; set; }

    /// <summary>The localized division names</summary>
    Dictionary<string, string> NameLocalizations { get; set; }

    /// <summary>The culture including the calendar, fallback for employee culture</summary>
    string Culture { get; set; }
}