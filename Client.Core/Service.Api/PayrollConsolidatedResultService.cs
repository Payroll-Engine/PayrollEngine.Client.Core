using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll payroll consolidated result service</summary>
public class PayrollConsolidatedResultService : ServiceBase, IPayrollConsolidatedResultService
{
    /// <summary>Initializes a new instance of the <see cref="PayrollConsolidatedResultService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public PayrollConsolidatedResultService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<TConsolidatedPayrollResult> QueryPayrollResultAsync<TConsolidatedPayrollResult>(TenantServiceContext context,
        int payrunId, int employeeId, DateTime periodStart, int? divisionId, string forecast, PayrunJobStatus jobStatus, IEnumerable<string> tags)
        where TConsolidatedPayrollResult : class, IConsolidatedPayrollResult
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrunId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunId));
        }
        if (employeeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeId));
        }

        var uri = PayrollConsolidatedResultApiEndpoints.PayrollConsolidatedResultsUrl(context.TenantId)
            .AddQueryString(nameof(payrunId), payrunId)
            .AddQueryString(nameof(employeeId), employeeId)
            .AddQueryString(nameof(periodStart), periodStart)
            .AddQueryString(nameof(divisionId), divisionId)
            .AddQueryString(nameof(forecast), forecast)
            .AddQueryString(nameof(jobStatus), jobStatus)
            .AddCollectionQueryString(nameof(tags), tags);
        return await HttpClient.GetAsync<TConsolidatedPayrollResult>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TCollectorResult>> GetCollectorResultsAsync<TCollectorResult>(TenantServiceContext context,
        int payrunId, int employeeId, IEnumerable<DateTime> periodStarts, int? divisionId, IEnumerable<string> collectorNames,
        string forecast, PayrunJobStatus jobStatus, IEnumerable<string> tags, DateTime? evaluationDate) where TCollectorResult : class, ICollectorResult
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrunId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunId));
        }
        if (employeeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeId));
        }

        var uri = PayrollConsolidatedResultApiEndpoints.PayrollConsolidatedCollectorResultsUrl(context.TenantId)
            .AddQueryString(nameof(payrunId), payrunId)
            .AddQueryString(nameof(employeeId), employeeId)
            .AddCollectionQueryString(nameof(periodStarts), periodStarts)
            .AddQueryString(nameof(divisionId), divisionId)
            .AddCollectionQueryString(nameof(collectorNames), collectorNames)
            .AddQueryString(nameof(forecast), forecast)
            .AddQueryString(nameof(jobStatus), jobStatus)
            .AddCollectionQueryString(nameof(tags), tags);
        return await HttpClient.GetCollectionAsync<TCollectorResult>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TWageTypeResult>> QueryWageTypeResultsAsync<TWageTypeResult>(TenantServiceContext context,
        int payrunId, int employeeId, IEnumerable<DateTime> periodStarts, int? divisionId,
        IEnumerable<decimal> wageTypeNumbers, string forecast, PayrunJobStatus jobStatus, IEnumerable<string> tags,
        DateTime? evaluationDate) where TWageTypeResult : class, IWageTypeResult
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrunId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunId));
        }
        if (employeeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeId));
        }

        var uri = PayrollConsolidatedResultApiEndpoints.PayrollConsolidatedWageTypeResultsUrl(context.TenantId)
            .AddQueryString(nameof(payrunId), payrunId)
            .AddQueryString(nameof(employeeId), employeeId)
            .AddCollectionQueryString(nameof(periodStarts), periodStarts)
            .AddQueryString(nameof(divisionId), divisionId)
            .AddCollectionQueryString(nameof(wageTypeNumbers), wageTypeNumbers)
            .AddQueryString(nameof(forecast), forecast)
            .AddQueryString(nameof(jobStatus), jobStatus)
            .AddCollectionQueryString(nameof(tags), tags);
        return await HttpClient.GetCollectionAsync<TWageTypeResult>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TPayrunResult>> QueryPayrunResultsAsync<TPayrunResult>(TenantServiceContext context,
        int payrunId, int employeeId, IEnumerable<DateTime> periodStarts, int? divisionId,
        IEnumerable<string> names, string forecast,PayrunJobStatus jobStatus, IEnumerable<string> tags,
        DateTime? evaluationDate) where TPayrunResult : class, IPayrunResult
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrunId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrunId));
        }
        if (employeeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeId));
        }

        var uri = PayrollConsolidatedResultApiEndpoints.PayrollConsolidatedWageTypeResultsUrl(context.TenantId)
            .AddQueryString(nameof(payrunId), payrunId)
            .AddQueryString(nameof(employeeId), employeeId)
            .AddCollectionQueryString(nameof(periodStarts), periodStarts)
            .AddQueryString(nameof(divisionId), divisionId)
            .AddCollectionQueryString(nameof(names), names)
            .AddQueryString(nameof(forecast), forecast)
            .AddQueryString(nameof(jobStatus), jobStatus)
            .AddCollectionQueryString(nameof(tags), tags);
        return await HttpClient.GetCollectionAsync<TPayrunResult>(uri);
    }
}