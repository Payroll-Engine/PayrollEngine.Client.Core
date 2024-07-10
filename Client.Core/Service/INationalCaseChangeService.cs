using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll national case change service</summary>
public interface INationalCaseChangeService : IReadService<ICaseChange, TenantServiceContext, CaseChangeQuery>
{
    /// <summary>Query objects</summary>
    /// <param name="context">The service context</param>
    /// <param name="query">The query</param>
    /// <returns>List of objects</returns>
    Task<List<T>> QueryValuesAsync<T>(TenantServiceContext context, CaseChangeQuery query = null) where T : class, ICaseChangeCaseValue;

    /// <summary>Query count of objects</summary>
    /// <param name="context">The service context</param>
    /// <param name="query">The query</param>
    /// <returns>Count of objects</returns>
    Task<long> QueryValuesCountAsync(TenantServiceContext context, CaseChangeQuery query = null);

    /// <summary>Query items and count of objects</summary>
    /// <param name="context">The service context</param>
    /// <param name="query">The query</param>
    /// <returns>List and count of objects</returns>
    Task<QueryResult<T>> QueryValuesResultAsync<T>(TenantServiceContext context, CaseChangeQuery query = null) where T : class, ICaseChangeCaseValue;

    /// <summary>Get national case change by division</summary>
    /// <param name="context">The service context</param>
    /// <param name="query">The case change query</param>
    /// <returns>The new case change</returns>
    Task<List<T>> GetAsync<T>(TenantServiceContext context, CaseChangeQuery query = null) where T : class, ICaseChange;
}