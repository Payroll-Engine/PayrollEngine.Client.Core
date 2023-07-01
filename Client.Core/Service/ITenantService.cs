using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using PayrollEngine.Data;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll tenant service</summary>
public interface ITenantService : ICrudService<ITenant, RootServiceContext, Query>, IAttributeService<RootServiceContext>
{
    /// <summary>Get tenant by identifier</summary>
    /// <param name="context">The service context</param>
    /// <param name="identifier">The tenant identifier</param>
    /// <returns>The tenant, null if missing</returns>
    Task<T> GetAsync<T>(RootServiceContext context, string identifier) where T : class, ITenant;

    /// <summary>Get tenant shared regulations</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="divisionId">The division id</param>
    /// <returns>The tenant shared regulations</returns>
    Task<List<T>> GetSharedRegulationsAsync<T>(int tenantId, int? divisionId) where T : class, IRegulation;

    /// <summary>Get the system script actions</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="functionType">The function type</param>
    /// <returns>The tenant shared regulations</returns>
    Task<List<TAction>> GetSystemScriptActionsAsync<TAction>(int tenantId, FunctionType functionType = FunctionType.All)
        where TAction : ActionInfo;

    /// <summary>Execute a report query</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="methodName">The query method</param>
    /// <param name="culture">The culture name based on RFC 4646</param>
    /// <param name="parameters">The query parameters</param>
    /// <returns>The resulting data table</returns>
    Task<DataTable> ExecuteReportQueryAsync(int tenantId, string methodName, string culture,
        Dictionary<string, string> parameters = null);
}