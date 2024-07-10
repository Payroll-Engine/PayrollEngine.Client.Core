using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll layer service</summary>
public interface IPayrollLayerService : ICrudService<IPayrollLayer, PayrollServiceContext, Query>, IAttributeService<PayrollServiceContext>
{
    /// <summary>Get payroll layer by identifier</summary>
    /// <param name="context">The service context</param>
    /// <param name="identifier">The Get payroll layer identifier</param>
    /// <returns>The payroll layer, null if missing</returns>
    Task<T> GetAsync<T>(PayrollServiceContext context, string identifier) where T : class, IPayrollLayer;
}