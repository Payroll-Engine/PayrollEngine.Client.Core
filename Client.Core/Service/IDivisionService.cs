using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll division service</summary>
public interface IDivisionService : ICrudService<IDivision, TenantServiceContext, Query>
{
    /// <summary>Get division by name</summary>
    /// <param name="context">The service context</param>
    /// <param name="name">The division name</param>
    /// <returns>The division, null if missing</returns>
    Task<T> GetAsync<T>(TenantServiceContext context, string name) where T : class, IDivision;
}