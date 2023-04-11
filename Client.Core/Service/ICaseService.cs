using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll case service</summary>
public interface ICaseService : ICrudService<ICase, RegulationServiceContext, Query>
{
    /// <summary>Get case by name</summary>
    /// <param name="context">The service context</param>
    /// <param name="name">The case name</param>
    /// <returns>The case, null if missing</returns>
    Task<T> GetAsync<T>(RegulationServiceContext context, string name) where T : class, ICase;

    /// <summary>Rebuild the case</summary>
    /// <param name="context">The service context</param>
    /// <param name="caseId">The case id</param>
    Task RebuildAsync(RegulationServiceContext context, int caseId);
}