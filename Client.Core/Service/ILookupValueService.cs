using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll lookup value service</summary>
public interface ILookupValueService : ICrudService<ILookupValue, LookupServiceContext, Query>
{
    /// <summary>
    /// Get lookup values data
    /// </summary>
    /// <param name="context">The service context</param>
    /// <param name="culture">The culture</param>
    /// <returns>The lookup value data</returns>
    Task<List<T>> GetLookupValuesDataAsync<T>(LookupServiceContext context,
        string culture = null) where T : LookupValueData;
}