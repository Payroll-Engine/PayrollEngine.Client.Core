
namespace PayrollEngine.Client.Service;

/// <summary>Lookup service context</summary>
public class LookupServiceContext : RegulationServiceContext
{
    /// <summary>Initializes a new instance of the <see cref="LookupServiceContext"/> class</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="regulationId">The lookup id</param>
    /// <param name="lookupId">The case id</param>
    public LookupServiceContext(int tenantId, int regulationId, int lookupId) :
        base(tenantId, regulationId)
    {
        LookupId = lookupId;
    }

    /// <summary>The lookup id</summary>
    public int LookupId { get; }
}