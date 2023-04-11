using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll case field service</summary>
public interface ICaseFieldService : ICrudService<ICaseField, CaseServiceContext, Query>
{
    /// <summary>Get case field by name</summary>
    /// <param name="context">The service context</param>
    /// <param name="name">The case field identifier</param>
    /// <returns>The case field, null if missing</returns>
    Task<T> GetAsync<T>(CaseServiceContext context, string name) where T : class, ICaseField;
}