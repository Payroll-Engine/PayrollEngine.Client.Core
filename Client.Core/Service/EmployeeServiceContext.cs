
namespace PayrollEngine.Client.Service;

/// <summary>Employee service context</summary>
public class EmployeeServiceContext : TenantServiceContext
{
    /// <summary>Initializes a new instance of the <see cref="EmployeeServiceContext"/> class</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="employeeId">The employee id</param>
    public EmployeeServiceContext(int tenantId, int employeeId) :
        base(tenantId)
    {
        EmployeeId = employeeId;
    }

    /// <summary>The employee id</summary>
    public int EmployeeId { get; }
}