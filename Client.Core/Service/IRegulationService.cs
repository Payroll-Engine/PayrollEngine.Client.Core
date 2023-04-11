using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll regulation service</summary>
public interface IRegulationService : ICrudService<IRegulation, TenantServiceContext, Query>
{
    /// <summary>Get regulation by name</summary>
    /// <param name="context">The service context</param>
    /// <param name="name">The regulation name</param>
    /// <returns>The regulation, null if missing</returns>
    Task<T> GetAsync<T>(TenantServiceContext context, string name) where T : class, IRegulation;

    /// <summary>Get case name by case field</summary>
    /// <param name="context">The service context</param>
    /// <param name="caseFieldName">The case field name</param>
    /// <returns>The case name</returns>
    Task<string> GetCaseOfCaseFieldAsync(TenantServiceContext context, string caseFieldName);
}