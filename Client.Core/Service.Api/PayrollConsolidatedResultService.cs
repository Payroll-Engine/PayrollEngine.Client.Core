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
        int employeeId, DateTime periodStart, DateTime periodEnd, int? divisionId, string forecast,
        PayrunJobStatus? jobStatus, IEnumerable<string> tags)
        where TConsolidatedPayrollResult : class, IConsolidatedPayrollResult
    {
        ArgumentNullException.ThrowIfNull(context);
        if (employeeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeId));
        }

        var uri = PayrollConsolidatedResultApiEndpoints.PayrollConsolidatedResultsUrl(context.TenantId)
            .AddQueryString(nameof(employeeId), employeeId)
            .AddQueryString(nameof(periodStart), periodStart)
            .AddQueryString(nameof(periodEnd), periodEnd)
            .AddQueryString(nameof(divisionId), divisionId)
            .AddQueryString(nameof(forecast), forecast)
            .AddQueryString(nameof(jobStatus), jobStatus)
            .AddCollectionQueryString(nameof(tags), tags);
        return await HttpClient.GetAsync<TConsolidatedPayrollResult>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TCollectorResult>> GetCollectorResultsAsync<TCollectorResult>(TenantServiceContext context,
        int employeeId, IEnumerable<DateTime> periodStarts, int? divisionId, IEnumerable<string> collectorNames,
        string forecast, PayrunJobStatus? jobStatus, IEnumerable<string> tags, DateTime? evaluationDate) where TCollectorResult : class, ICollectorResult
    {
        ArgumentNullException.ThrowIfNull(context);
        if (employeeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeId));
        }

        var uri = PayrollConsolidatedResultApiEndpoints.PayrollConsolidatedCollectorResultsUrl(context.TenantId)
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
        int employeeId, IEnumerable<DateTime> periodStarts, int? divisionId,
        IEnumerable<decimal> wageTypeNumbers, string forecast, PayrunJobStatus? jobStatus, IEnumerable<string> tags,
        DateTime? evaluationDate) where TWageTypeResult : class, IWageTypeResult
    {
        ArgumentNullException.ThrowIfNull(context);
        if (employeeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeId));
        }

        var uri = PayrollConsolidatedResultApiEndpoints.PayrollConsolidatedWageTypeResultsUrl(context.TenantId)
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
        int employeeId, IEnumerable<DateTime> periodStarts, int? divisionId,
        IEnumerable<string> resultNames, string forecast, PayrunJobStatus? jobStatus, IEnumerable<string> tags,
        DateTime? evaluationDate) where TPayrunResult : class, IPayrunResult
    {
        ArgumentNullException.ThrowIfNull(context);
        if (employeeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeId));
        }

        var uri = PayrollConsolidatedResultApiEndpoints.PayrollConsolidatedPayrunResultsUrl(context.TenantId)
            .AddQueryString(nameof(employeeId), employeeId)
            .AddCollectionQueryString(nameof(periodStarts), periodStarts)
            .AddQueryString(nameof(divisionId), divisionId)
            .AddCollectionQueryString(nameof(resultNames), resultNames)
            .AddQueryString(nameof(forecast), forecast)
            .AddQueryString(nameof(jobStatus), jobStatus)
            .AddCollectionQueryString(nameof(tags), tags);
        return await HttpClient.GetCollectionAsync<TPayrunResult>(uri);
    }
}