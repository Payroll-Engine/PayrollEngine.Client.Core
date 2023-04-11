
namespace PayrollEngine.Client.Service;

/// <summary>Case value service context</summary>
public class CaseValueServiceContext : TenantServiceContext
{
    /// <summary>Initializes a new instance of the <see cref="CaseValueServiceContext"/> class</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="caseValueId">The case value id</param>
    public CaseValueServiceContext(int tenantId, int caseValueId) :
        base(tenantId)
    {
        CaseValueId = caseValueId;
    }

    /// <summary>The case value id</summary>
    public int CaseValueId { get; }
}