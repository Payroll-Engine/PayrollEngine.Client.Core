using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll webhook service</summary>
public interface IWebhookService : ICrudService<IWebhook, TenantServiceContext, Query>, IAttributeService<TenantServiceContext>
{
    /// <summary>Get webhook by name</summary>
    /// <param name="context">The service context</param>
    /// <param name="name">The webhook name</param>
    /// <returns>The webhook, null if missing</returns>
    Task<T> GetAsync<T>(TenantServiceContext context, string name) where T : class, IWebhook;
}