using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll wage type service</summary>
public interface IWageTypeService : ICrudService<IWageType, RegulationServiceContext, Query>
{
    /// <summary>Get wage type by number</summary>
    /// <param name="context">The service context</param>
    /// <param name="wageTypeNumber">The wage type number</param>
    /// <returns>The wage type, null if missing</returns>
    Task<T> GetAsync<T>(RegulationServiceContext context, decimal wageTypeNumber) where T : class, IWageType;

    /// <summary>Get wage type by name</summary>
    /// <param name="context">The service context</param>
    /// <param name="name">The wage type name</param>
    /// <returns>The wage type, null if missing</returns>
    Task<T> GetAsync<T>(RegulationServiceContext context, string name) where T : class, IWageType;

    /// <summary>Rebuild the wge type</summary>
    /// <param name="context">The service context</param>
    /// <param name="wageTypeId">The wage type id</param>
    Task RebuildAsync(RegulationServiceContext context, int wageTypeId);
}