using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Lookup service context</summary>
public class LookupServiceContext : RegulationServiceContext
{
    /// <summary>Initializes a new instance of the <see cref="LookupServiceContext"/> class</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="regulationId">The regulation id</param>
    /// <param name="lookupId">The lookup id</param>
    public LookupServiceContext(int tenantId, int regulationId, int lookupId) :
        base(tenantId, regulationId)
    {
        LookupId = lookupId;
    }

    /// <summary>Initializes a new instance of the <see cref="LookupServiceContext"/> class</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="lookup">The lookup</param>
    public LookupServiceContext(ITenant tenant, IRegulation regulation, ILookup lookup) :
        this(tenant.Id, regulation.Id, lookup.Id)
    {
    }

    /// <summary>The lookup id</summary>
    public int LookupId { get; }
}