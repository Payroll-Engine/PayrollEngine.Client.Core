using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The employee client object</summary>
public interface IEmployee : IModel, IAttributeObject, IKeyEquatable<IEmployee>
{
    /// <summary>The employee identifier</summary>
    string Identifier { get; set; }

    /// <summary>The first name of the employee</summary>
    string FirstName { get; set; }

    /// <summary>The last name of the employee</summary>
    string LastName { get; set; }

    /// <summary>Employee division names</summary>
    List<string> Divisions { get; set; }

    /// <summary>The employee culture name based on RFC 4646 (fallback: division culture)</summary>
    string Culture { get; set; }

    /// <summary>The employee calendar (fallback: division calendar)</summary>
    string Calendar { get; set; }
}