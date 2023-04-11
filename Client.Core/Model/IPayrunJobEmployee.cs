
namespace PayrollEngine.Client.Model;

/// <summary>The payrun job client object</summary>
public interface IPayrunJobEmployee : IModel
{
    /// <summary>The employee id (immutable)</summary>
    int EmployeeId { get; set; }
}