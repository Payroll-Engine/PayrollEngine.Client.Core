
namespace PayrollEngine.Client.Service;

/// <summary>Tenant service context</summary>
public class PayrollResultValueServiceContext : TenantServiceContext
{
    /// <summary>Initializes a new instance of the <see cref="PayrollResultValueServiceContext"/> class</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="payrollId">The payroll id</param>
    /// <param name="payrunJobId">The payrun job id</param>
    /// <param name="employeeId">The employee id</param>
    /// <param name="divisionId">The division id</param>
    public PayrollResultValueServiceContext(int tenantId, int? payrollId = null, int? payrunJobId = null,
        int? employeeId = null, int? divisionId = null) :
        base(tenantId)
    {
        PayrollId = payrollId;
        PayrunJobId = payrunJobId;
        EmployeeId = employeeId;
        DivisionId = divisionId;
    }

    /// <summary>The payroll id</summary>
    public int? PayrollId { get; }

    /// <summary>The payrun job id</summary>
    public int? PayrunJobId { get; }

    /// <summary>The employee id</summary>
    public int? EmployeeId { get; }

    /// <summary>The division id</summary>
    public int? DivisionId { get; }
}