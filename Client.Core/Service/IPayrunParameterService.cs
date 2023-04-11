using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll webhook message service</summary>
public interface IPayrunParameterService : ICrudService<IPayrunParameter, PayrunServiceContext, Query>
{
    /// <summary>Get object</summary>
    /// <param name="context">The service context</param>
    /// <param name="name">The report parameter name</param>
    /// <returns>The report parameter, null if missing</returns>
    Task<T> GetAsync<T>(PayrunServiceContext context, string name) where T : class, IPayrunParameter;
}