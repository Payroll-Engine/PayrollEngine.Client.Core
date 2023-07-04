using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll payroll result service</summary>
public class PayrollResultService : ServiceBase, IPayrollResultService
{
    /// <summary>Initializes a new instance of the <see cref="PayrollResultService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public PayrollResultService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    #region Payroll Result

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(TenantServiceContext context, Query query = null) where T : class, IPayrollResult
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(PayrollResultApiEndpoints.PayrollResultsUrl(context.TenantId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(TenantServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(PayrollResultApiEndpoints.PayrollResultsUrl(context.TenantId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(TenantServiceContext context, Query query = null) where T : class, IPayrollResult
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(PayrollResultApiEndpoints.PayrollResultsUrl(context.TenantId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, int payrollResultId) where T : class, IPayrollResult
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrollResultId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrollResultId));
        }

        return await HttpClient.GetAsync<T>(PayrollResultApiEndpoints.PayrollResultUrl(context.TenantId, payrollResultId));
    }

    #endregion

    #region Collector Result

    /// <inheritdoc/>
    public async Task<List<TCollectorResult>> QueryCollectorResultsAsync<TCollectorResult>(PayrollResultServiceContext context,
        Query query = null) where TCollectorResult : class, ICollectorResult
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(PayrollResultApiEndpoints.PayrollCollectorsResultsUrl(context.TenantId, context.PayrollResultId));
        return await HttpClient.GetCollectionAsync<TCollectorResult>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCollectorResultsCountAsync(PayrollResultServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(PayrollResultApiEndpoints.PayrollCollectorsResultsUrl(context.TenantId, context.PayrollResultId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<TCollectorResult>> QueryCollectorResultsResultAsync<TCollectorResult>(
        PayrollResultServiceContext context, Query query = null) where TCollectorResult : class, ICollectorResult
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(PayrollResultApiEndpoints.PayrollCollectorsResultsUrl(context.TenantId, context.PayrollResultId));
        return await HttpClient.GetAsync<QueryResult<TCollectorResult>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TCollectorCustomResult>> QueryCollectorCustomResultsAsync<TCollectorCustomResult>(TenantServiceContext context,
        int payrollResultId, int collectorResultId, Query query = null) where TCollectorCustomResult : class, ICollectorCustomResult
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrollResultId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrollResultId));
        }
        if (collectorResultId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(collectorResultId));
        }

        query ??= new();
        var url = query.AppendQueryString(PayrollResultApiEndpoints.PayrollCollectorCustomResultsUrl(context.TenantId, payrollResultId, collectorResultId));
        return await HttpClient.GetCollectionAsync<TCollectorCustomResult>(url);
    }

    #endregion

    #region Wage Type Result

    /// <inheritdoc/>
    public async Task<List<TWageTypeResult>> QueryWageTypeResultsAsync<TWageTypeResult>(PayrollResultServiceContext context,
        Query query = null) where TWageTypeResult : class, IWageTypeResult
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(PayrollResultApiEndpoints.PayrollWageTypeResultsUrl(context.TenantId, context.PayrollResultId));
        return await HttpClient.GetCollectionAsync<TWageTypeResult>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryWageTypeResultsCountAsync(PayrollResultServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(PayrollResultApiEndpoints.PayrollWageTypeResultsUrl(context.TenantId, context.PayrollResultId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<TWageTypeResult>> QueryWageTypeResultsResultAsync<TWageTypeResult>(
        PayrollResultServiceContext context, Query query = null) where TWageTypeResult : class, IWageTypeResult
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(PayrollResultApiEndpoints.PayrollWageTypeResultsUrl(context.TenantId, context.PayrollResultId));
        return await HttpClient.GetAsync<QueryResult<TWageTypeResult>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TWageTypeCustomResult>> QueryWageTypeCustomResultsAsync<TWageTypeCustomResult>(TenantServiceContext context,
        int payrollResultId, int wageTypeResultId, Query query = null) where TWageTypeCustomResult : class, IWageTypeCustomResult
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrollResultId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrollResultId));
        }
        if (wageTypeResultId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(wageTypeResultId));
        }

        query ??= new();
        var url = query.AppendQueryString(PayrollResultApiEndpoints.PayrollWageTypeCustomResultsUrl(context.TenantId, payrollResultId, wageTypeResultId));
        return await HttpClient.GetCollectionAsync<TWageTypeCustomResult>(url);
    }

    #endregion

    #region Payrun Result

    /// <inheritdoc/>
    public async Task<List<TPayrunResult>> QueryPayrunResultsAsync<TPayrunResult>(PayrollResultServiceContext context,
        Query query = null) where TPayrunResult : class, IPayrunResult
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(PayrollResultApiEndpoints.PayrollPayrunResultsUrl(context.TenantId, context.PayrollResultId));
        return await HttpClient.GetCollectionAsync<TPayrunResult>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryPayrunResultsCountAsync(PayrollResultServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(PayrollResultApiEndpoints.PayrollPayrunResultsUrl(context.TenantId, context.PayrollResultId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<TPayrunResult>> QueryPayrunResultsResultAsync<TPayrunResult>(
        PayrollResultServiceContext context, Query query = null) where TPayrunResult : class, IPayrunResult
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(PayrollResultApiEndpoints.PayrollPayrunResultsUrl(context.TenantId, context.PayrollResultId));
        return await HttpClient.GetAsync<QueryResult<TPayrunResult>>(url);
    }

    #endregion

    #region Result Set

    /// <inheritdoc/>
    public virtual async Task<List<TPayrollResultSet>> QueryPayrollResultSetsAsync<TPayrollResultSet>(
        TenantServiceContext context, Query query = null) where TPayrollResultSet : class, IPayrollResultSet
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(PayrollResultApiEndpoints.PayrollResultSetsUrl(context.TenantId));
        return await HttpClient.GetCollectionAsync<TPayrollResultSet>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryPayrollResultSetsCountAsync(TenantServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(PayrollResultApiEndpoints.PayrollResultSetsUrl(context.TenantId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<TPayrollResultSet>> QueryPayrollResultSetsResultAsync<TPayrollResultSet>(
        TenantServiceContext context, Query query = null) where TPayrollResultSet : class, IPayrollResultSet
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(PayrollResultApiEndpoints.PayrollResultSetsUrl(context.TenantId));
        return await HttpClient.GetAsync<QueryResult<TPayrollResultSet>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<TPayrollResultSet> GetPayrollResultSetAsync<TPayrollResultSet>(PayrollResultServiceContext context)
        where TPayrollResultSet : class, IPayrollResultSet
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        return await HttpClient.GetAsync<TPayrollResultSet>(PayrollResultApiEndpoints.PayrollResultSetUrl(context.TenantId, context.PayrollResultId));
    }

    #endregion

}