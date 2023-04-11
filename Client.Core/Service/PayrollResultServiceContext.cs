
namespace PayrollEngine.Client.Service;

/// <summary>Payroll service context</summary>
public class PayrollResultServiceContext : TenantServiceContext
{
    /// <summary>Initializes a new instance of the <see cref="PayrollResultServiceContext"/> class</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="payrollResultId">The payroll result id</param>
    public PayrollResultServiceContext(int tenantId, int payrollResultId) :
        base(tenantId)
    {
        PayrollResultId = payrollResultId;
    }

    /// <summary>The payroll result id</summary>
    public int PayrollResultId { get; }
}