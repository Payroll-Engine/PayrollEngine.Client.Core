using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll report set service</summary>
public class ReportSetService : ServiceBase, IReportSetService
{
    /// <summary>Initializes a new instance of the <see cref="ReportService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public ReportSetService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(RegulationServiceContext context, Query query = null) where T : class, IReportSet
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationReportsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(RegulationServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationReportsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(RegulationServiceContext context, Query query = null) where T : class, IReportSet
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationReportsUrl(context.TenantId, context.RegulationId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> CreateAsync<T>(RegulationServiceContext context, T report) where T : class, IReportSet
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (report == null)
        {
            throw new ArgumentNullException(nameof(report));
        }

        return await HttpClient.PostAsync(RegulationApiEndpoints.RegulationReportSetsUrl(context.TenantId, context.RegulationId), report);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(RegulationServiceContext context, int reportId) where T : class, IReportSet =>
        await GetAsync<T>(context, reportId, null);

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(RegulationServiceContext context, int reportId, ReportRequest reportRequest) where T : class, IReportSet
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (reportId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(reportId));
        }

        // query report set
        var uri = RegulationApiEndpoints.RegulationReportSetUrl(context.TenantId, context.RegulationId, reportId);
        // empty body in case of missing report request
        // use of POST instead of GET according RFC7231
        // https://datatracker.ietf.org/doc/html/rfc7231#section-4.3.1
        return reportRequest == null ? 
            await HttpClient.PostAsync<ReportRequest, T>(uri, new ReportRequest()) : 
            await HttpClient.PostAsync<ReportRequest, T>(uri, reportRequest);
    }

    /// <inheritdoc />
    public virtual async Task<T> GetAsync<T>(RegulationServiceContext context, string name, ReportRequest reportRequest = null) where T : class, IReportSet
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(nameof(name));
        }

        // query report id
        var query = QueryFactory.NewNameQuery(name);
        var uri = query.AppendQueryString(RegulationApiEndpoints.RegulationReportsUrl(context.TenantId, context.RegulationId));
        var report = await HttpClient.GetSingleAsync<T>(uri);
        if (report == null)
        {
            return null;
        }

        // query report set
        return await GetAsync<T>(context, report.Id, reportRequest);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(RegulationServiceContext context, int reportId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (reportId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(reportId));
        }

        await HttpClient.DeleteAsync(RegulationApiEndpoints.RegulationReportSetsUrl(context.TenantId, context.RegulationId), reportId);
    }
}