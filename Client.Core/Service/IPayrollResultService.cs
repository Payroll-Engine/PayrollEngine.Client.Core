using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll result service</summary>
public interface IPayrollResultService : IReadService<IPayrollResult, TenantServiceContext, Query>
{

    #region Collector Result

    /// <summary>Query payroll collector results</summary>
    /// <param name="context">The service context</param>
    /// <param name="query">Query parameters</param>
    /// <returns>Payroll collector results</returns>
    Task<List<TCollectorResult>> QueryCollectorResultsAsync<TCollectorResult>(PayrollResultServiceContext context,
        Query query = null) where TCollectorResult : class, ICollectorResult;

    /// <summary>Query count of payroll collector results</summary>
    /// <param name="context">The service context</param>
    /// <param name="query">The query</param>
    /// <returns>Count of payroll collector results</returns>
    Task<long> QueryCollectorResultsCountAsync(PayrollResultServiceContext context, Query query = null);

    /// <summary>Query items and count of payroll collector results</summary>
    /// <param name="context">The service context</param>
    /// <param name="query">The query</param>
    /// <returns>List and count of payroll collector results</returns>
    Task<QueryResult<TCollectorResult>> QueryCollectorResultsResultAsync<TCollectorResult>(PayrollResultServiceContext context,
        Query query = null) where TCollectorResult : class, ICollectorResult;

    /// <summary>Query payroll collector custom results</summary>
    /// <param name="context">The service context</param>
    /// <param name="payrollResultId">The payroll result id</param>
    /// <param name="collectorResultId">The collector result id</param>
    /// <param name="query">Query parameters</param>
    /// <returns>List and count of payroll collector custom results</returns>
    Task<List<TCollectorCustomResult>> QueryCollectorCustomResultsAsync<TCollectorCustomResult>(TenantServiceContext context,
        int payrollResultId, int collectorResultId, Query query = null) where TCollectorCustomResult : class, ICollectorCustomResult;

    #endregion

    #region Wage Type Result

    /// <summary>Query payroll wage type results</summary>
    /// <param name="context">The service context</param>
    /// <param name="query">Query parameters</param>
    /// <returns>Payroll wage type results</returns>
    Task<List<TWageTypeResult>> QueryWageTypeResultsAsync<TWageTypeResult>(PayrollResultServiceContext context,
        Query query = null) where TWageTypeResult : class, IWageTypeResult;

    /// <summary>Query count of payroll wage type results</summary>
    /// <param name="context">The service context</param>
    /// <param name="query">The query</param>
    /// <returns>Count of payroll wage type results</returns>
    Task<long> QueryWageTypeResultsCountAsync(PayrollResultServiceContext context, Query query = null);

    /// <summary>Query items and count of payroll wage type results</summary>
    /// <param name="context">The service context</param>
    /// <param name="query">The query</param>
    /// <returns>List and count of payroll wage type results</returns>
    Task<QueryResult<TWageTypeResult>> QueryWageTypeResultsResultAsync<TWageTypeResult>(PayrollResultServiceContext context,
        Query query = null) where TWageTypeResult : class, IWageTypeResult;

    /// <summary>Query payroll wage type custom results</summary>
    /// <param name="context">The service context</param>
    /// <param name="payrollResultId">The payroll result id</param>
    /// <param name="wageTypeResultId">The wage type result id</param>
    /// <param name="query">Query parameters</param>
    /// <returns>List and count of payroll wage type custom results</returns>
    Task<List<TWageTypeCustomResult>> QueryWageTypeCustomResultsAsync<TWageTypeCustomResult>(TenantServiceContext context,
        int payrollResultId, int wageTypeResultId, Query query = null) where TWageTypeCustomResult : class, IWageTypeCustomResult;

    #endregion

    #region Payrun Result

    /// <summary>Query payroll payrun results</summary>
    /// <param name="context">The service context</param>
    /// <param name="query">Query parameters</param>
    /// <returns>Payroll payrun results</returns>
    Task<List<TPayrunResult>> QueryPayrunResultsAsync<TPayrunResult>(PayrollResultServiceContext context,
        Query query = null) where TPayrunResult : class, IPayrunResult;

    /// <summary>Query count of payroll payrun results</summary>
    /// <param name="context">The service context</param>
    /// <param name="query">The query</param>
    /// <returns>Count of payroll payrun results</returns>
    Task<long> QueryPayrunResultsCountAsync(PayrollResultServiceContext context, Query query = null);

    /// <summary>Query items and count of payroll payrun results</summary>
    /// <param name="context">The service context</param>
    /// <param name="query">The query</param>
    /// <returns>List and count of payroll payrun results</returns>
    Task<QueryResult<TPayrunResult>> QueryPayrunResultsResultAsync<TPayrunResult>(PayrollResultServiceContext context,
        Query query = null) where TPayrunResult : class, IPayrunResult;

    #endregion

    #region Result Sets

    /// <summary>Query payroll result sets</summary>
    /// <param name="context">The service context</param>
    /// <param name="query">Query parameters</param>
    /// <returns>The payroll results sets</returns>
    Task<List<TPayrollResultSet>> QueryPayrollResultSetsAsync<TPayrollResultSet>(TenantServiceContext context,
        Query query = null) where TPayrollResultSet : class, IPayrollResultSet;

    /// <summary>Query count of payroll result sets</summary>
    /// <param name="context">The service context</param>
    /// <param name="query">The query</param>
    /// <returns>Count of payroll result sets</returns>
    Task<long> QueryPayrollResultSetsCountAsync(TenantServiceContext context, Query query = null);

    /// <summary>Query items and count of payroll result sets</summary>
    /// <param name="context">The service context</param>
    /// <param name="query">The query</param>
    /// <returns>List and count of payroll result sets</returns>
    Task<QueryResult<TPayrollResultSet>> QueryPayrollResultSetsResultAsync<TPayrollResultSet>(
        TenantServiceContext context, Query query = null) where TPayrollResultSet : class, IPayrollResultSet;

    /// <summary>Get payroll result set</summary>
    /// <param name="context">The service context</param>
    /// <returns>A payroll results set</returns>
    Task<TPayrollResultSet> GetPayrollResultSetAsync<TPayrollResultSet>(PayrollResultServiceContext context)
        where TPayrollResultSet : class, IPayrollResultSet;

    #endregion

}