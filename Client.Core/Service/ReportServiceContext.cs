using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Report service context</summary>
public class ReportServiceContext : RegulationServiceContext
{
    /// <summary>Initializes a new instance of the <see cref="ReportServiceContext"/> class</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="regulationId">The regulation id</param>
    /// <param name="reportId">The report id</param>
    public ReportServiceContext(int tenantId, int regulationId, int reportId) :
        base(tenantId, regulationId)
    {
        ReportId = reportId;
    }

    /// <summary>Initializes a new instance of the <see cref="ReportServiceContext"/> class</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="report">The report id</param>
    public ReportServiceContext(ITenant tenant, IRegulation regulation, IReport report) :
        this(tenant.Id, regulation.Id, report.Id)
    {
    }

    /// <summary>The report id</summary>
    public int ReportId { get; }
}