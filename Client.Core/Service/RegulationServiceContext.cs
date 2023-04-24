using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Regulation service context</summary>
public class RegulationServiceContext : TenantServiceContext
{
    /// <summary>Initializes a new instance of the <see cref="RegulationServiceContext"/> class</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="regulationId">The regulation id</param>
    public RegulationServiceContext(int tenantId, int regulationId) :
        base(tenantId)
    {
        RegulationId = regulationId;
    }

    /// <summary>Initializes a new instance of the <see cref="RegulationServiceContext"/> class</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    public RegulationServiceContext(ITenant tenant, IRegulation regulation) :
        this(tenant.Id, regulation.Id)
    {
    }

    /// <summary>The regulation id</summary>
    public int RegulationId { get; }
}