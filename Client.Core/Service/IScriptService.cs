using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll script service</summary>
public interface IScriptService : ICrudService<IScript, RegulationServiceContext, Query>
{
    /// <summary>Get script by name</summary>
    /// <param name="context">The service context</param>
    /// <param name="name">The script identifier</param>
    /// <returns>The script, null if missing</returns>
    Task<T> GetAsync<T>(RegulationServiceContext context, string name) where T : class, IScript;
}