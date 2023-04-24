using System;

namespace PayrollEngine.Client.Model;

/// <summary>The payrun job client object</summary>
public interface IPayrunJobEmployee : IModel, IEquatable<IPayrunJobEmployee>
{
    /// <summary>The employee id (immutable)</summary>
    int EmployeeId { get; set; }
}