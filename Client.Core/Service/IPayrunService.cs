using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll payrun service</summary>
public interface IPayrunService : ICrudService<IPayrun, TenantServiceContext, Query>
{
    /// <summary>Get payrun by name</summary>
    /// <param name="context">The service context</param>
    /// <param name="name">The payrun name</param>
    /// <returns>The payrun, null if missing</returns>
    Task<T> GetAsync<T>(TenantServiceContext context, string name) where T : class, IPayrun;
        
    /// <summary>Rebuild the payrun</summary>
    /// <param name="context">The service context</param>
    /// <param name="payrunId">The payrun id</param>
    Task RebuildAsync(TenantServiceContext context, int payrunId);
}