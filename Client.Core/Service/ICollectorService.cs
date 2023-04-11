using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll collector service</summary>
public interface ICollectorService : ICrudService<ICollector, RegulationServiceContext, Query>
{
    /// <summary>Get collector by name</summary>
    /// <param name="context">The service context</param>
    /// <param name="name">The collector name</param>
    /// <returns>The collector, null if missing</returns>
    Task<T> GetAsync<T>(RegulationServiceContext context, string name) where T : class, ICollector;

    /// <summary>Rebuild the collector</summary>
    /// <param name="context">The service context</param>
    /// <param name="collectorId">The collector id</param>
    Task RebuildAsync(RegulationServiceContext context, int collectorId);
}