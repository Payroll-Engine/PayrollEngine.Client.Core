using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The employee client object</summary>
public interface IEmployee : IModel, IAttributeObject
{
    /// <summary>The employee identifier</summary>
    string Identifier { get; set; }

    /// <summary>The first name of the employee</summary>
    string FirstName { get; set; }

    /// <summary>The last name of the employee</summary>
    string LastName { get; set; }

    /// <summary>The employees language</summary>
    Language Language { get; set; }

    /// <summary>Employee division names</summary>
    List<string> Divisions { get; set; }

    /// <summary>The culture including the calendar, fallback is the division culture</summary>
    string Culture { get; set; }
}