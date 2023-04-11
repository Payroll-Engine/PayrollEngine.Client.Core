using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll lookup service</summary>
public interface ILookupService : ICrudService<ILookup, RegulationServiceContext, Query>
{
    /// <summary>Get lookup by name</summary>
    /// <param name="context">The service context</param>
    /// <param name="name">The lookup name</param>
    /// <returns>The lookup, null if missing</returns>
    Task<T> GetAsync<T>(RegulationServiceContext context, string name) where T : class, ILookup;
}