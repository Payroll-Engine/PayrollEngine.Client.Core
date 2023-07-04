using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll payroll result value service</summary>
public class PayrollResultValueService : ServiceBase, IPayrollResultValueService
{
    /// <summary>Initializes a new instance of the <see cref="PayrollResultValueService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public PayrollResultValueService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(PayrollResultValueServiceContext context, Query query = null) where T : class, IPayrollResultValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var uri = GetPayrollResultValuesUrl(QueryResultType.Items, context, query);
        return await HttpClient.GetCollectionAsync<T>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(PayrollResultValueServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var uri = GetPayrollResultValuesUrl(QueryResultType.Count, context, query);
        return await HttpClient.GetAsync<long>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(PayrollResultValueServiceContext context, Query query = null) where T : class, IPayrollResultValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var uri = GetPayrollResultValuesUrl(QueryResultType.ItemsWithCount, context, query);
        return await HttpClient.GetAsync<QueryResult<T>>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(PayrollResultValueServiceContext context, int objectId) where T : class, IPayrollResultValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (objectId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(objectId));
        }

        Log.Error("Unsupported get request on PayrollResultValue");
        await Task.Run(() => { });
        return default;
    }

    private static string GetPayrollResultValuesUrl(QueryResultType resultType, PayrollResultValueServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = resultType;
        var uri = query.AppendQueryString(PayrollResultApiEndpoints.PayrollResultValuesUrl(context.TenantId))
            .AddQueryString(nameof(context.TenantId), context.TenantId)
            .AddQueryString(nameof(context.PayrollId), context.PayrollId)
            .AddQueryString(nameof(context.PayrunJobId), context.PayrunJobId)
            .AddQueryString(nameof(context.EmployeeId), context.EmployeeId)
            .AddQueryString(nameof(context.DivisionId), context.DivisionId);
        return uri;
    }
}