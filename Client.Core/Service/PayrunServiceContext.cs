using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payrun service context</summary>
public class PayrunServiceContext : TenantServiceContext
{
    /// <summary>Initializes a new instance of the <see cref="PayrunServiceContext"/> class</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="payrunId">The payrun id</param>
    public PayrunServiceContext(int tenantId, int payrunId) :
        base(tenantId)
    {
        PayrunId = payrunId;
    }

    /// <summary>Initializes a new instance of the <see cref="PayrunServiceContext"/> class</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="payrun">The payrun</param>
    public PayrunServiceContext(ITenant tenant, IPayrun payrun) :
        this(tenant.Id, payrun.Id)
    {
    }

    /// <summary>The payrun id</summary>
    public int PayrunId { get; }
}