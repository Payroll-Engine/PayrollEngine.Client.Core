
namespace PayrollEngine.Client.Service;

/// <summary>Payroll service context</summary>
public class PayrollServiceContext : TenantServiceContext
{
    /// <summary>Initializes a new instance of the <see cref="PayrollServiceContext"/> class</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="payrollId">The payroll id</param>
    public PayrollServiceContext(int tenantId, int payrollId) :
        base(tenantId)
    {
        PayrollId = payrollId;
    }

    /// <summary>The payroll id</summary>
    public int PayrollId { get; }
}