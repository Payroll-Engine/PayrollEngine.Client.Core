using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll division client object</summary>
public interface IDivision : IModel, IAttributeObject, IKeyEquatable<IDivision>
{
    /// <summary>The division name</summary>
    string Name { get; set; }

    /// <summary>The localized division names</summary>
    Dictionary<string, string> NameLocalizations { get; set; }

    /// <summary>The culture including the calendar, fallback for employee culture</summary>
    string Culture { get; set; }

    /// <summary>The division calendar, fallback is the tenant calendar</summary>
    string Calendar { get; set; }
}
