using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll payrun service</summary>
public interface IReportSetService : ICreateService<IReportSet, RegulationServiceContext, Query>
{
    /// <summary>Get report set by request</summary>
    /// <param name="context">The service context</param>
    /// <param name="reportId">The report id</param>
    /// <param name="reportRequest">The report request</param>
    /// <returns>The report set</returns>
    Task<T> GetAsync<T>(RegulationServiceContext context, int reportId, ReportRequest reportRequest) where T : class, IReportSet;

    /// <summary>Get report set by name</summary>
    /// <param name="context">The service context</param>
    /// <param name="name">The id of the report</param>
    /// <param name="reportRequest">The report request</param>
    /// <returns>The report set</returns>
    Task<T> GetAsync<T>(RegulationServiceContext context, string name, ReportRequest reportRequest = null) where T : class, IReportSet;
}