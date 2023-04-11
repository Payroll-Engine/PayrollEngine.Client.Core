using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll lookup set service</summary>
public interface ILookupSetService : ICreateService<ILookupSet, RegulationServiceContext, Query>
{
    /// <summary>Get lookup set by name</summary>
    /// <param name="context">The service context</param>
    /// <param name="name">The lookup name</param>
    /// <returns>The lookup set, null if missing</returns>
    Task<T> GetAsync<T>(RegulationServiceContext context, string name) where T : class, ILookupSet;

    /// <summary>Create multiple lookup sets</summary>
    /// <param name="context">The service context</param>
    /// <param name="lookupSets">The lookup sets</param>
    /// <returns>New created lookup</returns>
    Task CreateAsync<T>(RegulationServiceContext context, IEnumerable<T> lookupSets) where T : class, ILookupSet;
}