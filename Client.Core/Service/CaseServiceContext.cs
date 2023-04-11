
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

    /// <summary>The case id</summary>
    public int CaseId { get; }
}