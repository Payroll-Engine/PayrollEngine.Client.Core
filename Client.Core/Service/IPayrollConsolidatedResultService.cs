using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll consolidated result service</summary>
public interface IPayrollConsolidatedResultService
{

    /// <summary>Query consolidated collector results</summary>
    /// <param name="context">The service context</param>
    /// <param name="employeeId">The employee id</param>
    /// <param name="payrunId">The payrun id</param>
    /// <param name="periodStart">Period start date</param>
    /// <param name="divisionId">The division id</param>
    /// <param name="forecast">The forecast name</param>
    /// <param name="jobStatus">The payrun job status</param>
    /// <param name="tags">The result tags</param>
    /// <returns>The period collector results</returns>
    Task<TConsolidatedPayrollResult> QueryPayrollResultAsync<TConsolidatedPayrollResult>(TenantServiceContext context,
        int payrunId, int employeeId, DateTime periodStart, int? divisionId, string forecast, PayrunJobStatus jobStatus,
        IEnumerable<string> tags) where TConsolidatedPayrollResult : class, IConsolidatedPayrollResult;

    /// <summary>Get consolidated collector results</summary>
    /// <param name="context">The service context</param>
    /// <param name="employeeId">The employee id</param>
    /// <param name="payrunId">The payrun id</param>
    /// <param name="periodStarts">Period start dates</param>
    /// <param name="divisionId">The division id</param>
    /// <param name="collectorNames">The collector names</param>
    /// <param name="forecast">The forecast name</param>
    /// <param name="jobStatus">The payrun job status</param>
    /// <param name="tags">The result tags</param>
    /// <param name="evaluationDate">The evaluation date (default: UTC now)</param>
    /// <returns>The period collector results</returns>
    Task<List<TCollectorResult>> GetCollectorResultsAsync<TCollectorResult>(TenantServiceContext context, int payrunId, int employeeId,
        IEnumerable<DateTime> periodStarts, int? divisionId, IEnumerable<string> collectorNames, string forecast, PayrunJobStatus jobStatus,
        IEnumerable<string> tags, DateTime? evaluationDate) where TCollectorResult : class, ICollectorResult;

    /// <summary>Query consolidated wage type results</summary>
    /// <param name="context">The service context</param>
    /// <param name="employeeId">The employee id</param>
    /// <param name="payrunId">The payrun id</param>
    /// <param name="periodStarts">Period start dates</param>
    /// <param name="divisionId">The division id</param>
    /// <param name="wageTypeNumbers">The wage type numbers</param>
    /// <param name="forecast">The forecast name</param>
    /// <param name="jobStatus">The payrun job status</param>
    /// <param name="tags">The result tags</param>
    /// <param name="evaluationDate">The evaluation date (default: UTC now)</param>
    /// <returns>The period wage type results</returns>
    Task<List<TWageTypeResult>> QueryWageTypeResultsAsync<TWageTypeResult>(TenantServiceContext context, int payrunId, int employeeId,
        IEnumerable<DateTime> periodStarts, int? divisionId, IEnumerable<decimal> wageTypeNumbers, string forecast, PayrunJobStatus jobStatus,
        IEnumerable<string> tags, DateTime? evaluationDate) where TWageTypeResult : class, IWageTypeResult;

    /// <summary>Query consolidated payrun results</summary>
    /// <param name="context">The service context</param>
    /// <param name="employeeId">The employee id</param>
    /// <param name="payrunId">The payrun id</param>
    /// <param name="periodStarts">Period start dates</param>
    /// <param name="divisionId">The division id</param>
    /// <param name="names">The result names</param>
    /// <param name="forecast">The forecast name</param>
    /// <param name="tags">The result tags</param>
    /// <param name="jobStatus">The payrun job status</param>
    /// <param name="evaluationDate">The evaluation date (default: UTC now)</param>
    /// <returns>The period payrun results</returns>
    Task<List<TPayrunResult>> QueryPayrunResultsAsync<TPayrunResult>(TenantServiceContext context, int payrunId, int employeeId,
        IEnumerable<DateTime> periodStarts, int? divisionId, IEnumerable<string> names, string forecast, PayrunJobStatus jobStatus,
        IEnumerable<string> tags, DateTime? evaluationDate) where TPayrunResult : class, IPayrunResult;

}