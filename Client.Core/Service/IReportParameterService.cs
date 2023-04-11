using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Regulation report parameter service</summary>
public interface IReportParameterService : ICrudService<IReportParameter, ReportServiceContext, Query>
{
    /// <summary>Get object</summary>
    /// <param name="context">The service context</param>
    /// <param name="name">The report parameter name</param>
    /// <returns>The report parameter, null if missing</returns>
    Task<T> GetAsync<T>(ReportServiceContext context, string name) where T : class, IReportParameter;
}