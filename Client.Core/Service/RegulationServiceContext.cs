
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

    /// <summary>The regulation id</summary>
    public int RegulationId { get; }
}