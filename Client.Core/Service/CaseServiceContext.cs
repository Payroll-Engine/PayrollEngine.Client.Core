using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Case service context</summary>
public class CaseServiceContext : RegulationServiceContext
{
    /// <summary>Initializes a new instance of the <see cref="CaseServiceContext"/> class</summary>
    /// <param name="tenantId">The employee id</param>
    /// <param name="regulationId">The regulation id</param>
    /// <param name="caseId">The case id</param>
    public CaseServiceContext(int tenantId, int regulationId, int caseId) :
        base(tenantId, regulationId)
    {
        CaseId = caseId;
    }

    /// <summary>Initializes a new instance of the <see cref="CaseServiceContext"/> class</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="case">The case id</param>
    public CaseServiceContext(ITenant tenant, IRegulation regulation, ICase @case) :
        this(tenant.Id, regulation.Id, @case.Id)
    {
    }

    /// <summary>The case id</summary>
    public int CaseId { get; }
}