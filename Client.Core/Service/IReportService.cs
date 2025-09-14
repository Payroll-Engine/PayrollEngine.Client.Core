using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service;

/// <summary>Regulation report service</summary>
// ReSharper disable UnusedMemberInSuper.Global
public interface IReportService : ICrudService<IReport, RegulationServiceContext, Query>
{
    /// <summary>Get report by name</summary>
    /// <param name="context">The service context</param>
    /// <param name="name">The payroll name</param>
    /// <returns>The payroll, null if missing</returns>
    Task<T> GetAsync<T>(RegulationServiceContext context, string name) where T : class, IReport;

    /// <summary>Execute a report</summary>
    /// <param name="context">The service context</param>
    /// <param name="reportId">The id of the report</param>
    /// <param name="request">The report execute request</param>
    /// <returns>The report response including the report data</returns>
    Task<ReportResponse> ExecuteReportAsync(RegulationServiceContext context, int reportId, ReportRequest request);

    /// <summary>Rebuild the report</summary>
    /// <param name="context">The service context</param>
    /// <param name="reportId">The report id</param>
    Task RebuildAsync(RegulationServiceContext context, int reportId);
}