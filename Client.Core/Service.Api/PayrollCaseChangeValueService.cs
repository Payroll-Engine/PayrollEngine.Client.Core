using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll payroll case change value service</summary>
public class PayrollCaseChangeValueService : Service, IPayrollCaseChangeValueService
{
    /// <summary>Initializes a new instance of the <see cref="PayrollCaseChangeValueService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public PayrollCaseChangeValueService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(PayrollServiceContext context, PayrollCaseChangeQuery query = null)
        where T : class, ICaseChangeCaseValue
    {
        var uri = GetPayrollCaseChangeValuesUrl(QueryResultType.Items, context, query);
        return await HttpClient.GetCollectionAsync<T>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(PayrollServiceContext context, PayrollCaseChangeQuery query = null)
    {
        var uri = GetPayrollCaseChangeValuesUrl(QueryResultType.Count, context, query);
        return await HttpClient.GetAsync<long>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(PayrollServiceContext context, PayrollCaseChangeQuery query = null)
        where T : class, ICaseChangeCaseValue
    {
        var uri = GetPayrollCaseChangeValuesUrl(QueryResultType.ItemsWithCount, context, query);
        return await HttpClient.GetAsync<QueryResult<T>>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(PayrollServiceContext context, int objectId) where T : class, ICaseChangeCaseValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (objectId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(objectId));
        }

        Log.Error("Unsupported get request on CaseChangeCaseValue");
        await Task.Run(() => { });
        return default;
    }

    private static string GetPayrollCaseChangeValuesUrl(QueryResultType resultType, PayrollServiceContext context, PayrollCaseChangeQuery query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = resultType;

        if (query.UserId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(query.UserId));
        }
        if (query.CaseType == CaseType.Employee && !query.EmployeeId.HasValue)
        {
            throw new ArgumentException(nameof(query.EmployeeId));
        }

        var uri = query.AppendQueryString(PayrollApiEndpoints.PayrollCaseChangeValuesUrl(context.TenantId, context.PayrollId))
            .AddQueryString(nameof(query.UserId), query.UserId)
            .AddQueryString(nameof(query.CaseType), query.CaseType)
            .AddQueryString(nameof(query.DivisionId), query.DivisionId)
            .AddQueryString(nameof(query.EmployeeId), query.EmployeeId)
            .AddQueryString(nameof(query.ClusterSetName), query.ClusterSetName)
            .AddQueryString(nameof(query.ExcludeGlobal), query.ExcludeGlobal)
            .AddQueryString(nameof(query.Language), query.Language)
            .AddQueryString(nameof(query.RegulationDate), query.RegulationDate)
            .AddQueryString(nameof(query.EvaluationDate), query.EvaluationDate);
        return uri;
    }
}