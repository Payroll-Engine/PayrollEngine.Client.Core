using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll employee service</summary>
public interface IEmployeeService : ICrudService<IEmployee, TenantServiceContext, DivisionQuery>, IAttributeService<TenantServiceContext>
{
    /// <summary>Get employee by identifier</summary>
    /// <param name="context">The service context</param>
    /// <param name="identifier">The employee identifier</param>
    /// <returns>The employee, null if missing</returns>
    Task<T> GetAsync<T>(TenantServiceContext context, string identifier) where T : class, IEmployee;
}