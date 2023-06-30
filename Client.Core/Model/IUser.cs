namespace PayrollEngine.Client.Model;

/// <summary>The payroll user client object</summary>
public interface IUser : IModel, IAttributeObject, IKeyEquatable<IUser>
{
    /// <summary>The user unique identifier</summary>
    string Identifier { get; set; }

    /// <summary>The user password</summary>
    string Password { get; set; }

    /// <summary>The first name of the user</summary>
    string FirstName { get; set; }

    /// <summary>The last name of the user</summary>
    string LastName { get; set; }

    /// <summary>The user culture name based on RFC 4646</summary>
    string Culture { get; set; }

    /// <summary>The user type</summary>
    UserType UserType { get; set; }
}