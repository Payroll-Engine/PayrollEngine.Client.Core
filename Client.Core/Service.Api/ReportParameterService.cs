using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll report parameter service</summary>
public class ReportParameterService : Service, IReportParameterService
{
    /// <summary>Initializes a new instance of the <see cref="ReportParameterService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public ReportParameterService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> QueryAsync<T>(ReportServiceContext context, Query query = null) where T : class, IReportParameter
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationReportParametersUrl(context.TenantId, context.RegulationId, context.ReportId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc />
    public virtual async Task<long> QueryCountAsync(ReportServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationReportParametersUrl(context.TenantId, context.RegulationId, context.ReportId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc />
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(ReportServiceContext context, Query query = null) where T : class, IReportParameter
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(RegulationApiEndpoints.RegulationReportParametersUrl(context.TenantId, context.RegulationId, context.ReportId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc />
    public virtual async Task<T> GetAsync<T>(ReportServiceContext context, int parameterId) where T : class, IReportParameter
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (parameterId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(parameterId));
        }

        return await HttpClient.GetAsync<T>(RegulationApiEndpoints.RegulationReportParameterUrl(context.TenantId, context.RegulationId,
            context.ReportId, parameterId));
    }

    /// <inheritdoc />
    public virtual async Task<T> GetAsync<T>(ReportServiceContext context, string name) where T : class, IReportParameter
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(nameof(name));
        }

        // query single item
        var query = QueryFactory.NewNameQuery(name);
        var uri = query.AppendQueryString(RegulationApiEndpoints.RegulationReportParametersUrl(context.TenantId, context.RegulationId, context.ReportId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc />
    public virtual async Task<T> CreateAsync<T>(ReportServiceContext context, T parameter) where T : class, IReportParameter
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (parameter == null)
        {
            throw new ArgumentNullException(nameof(parameter));
        }

        return await HttpClient.PostAsync(
            RegulationApiEndpoints.RegulationReportParametersUrl(context.TenantId, context.RegulationId, context.ReportId), parameter);
    }

    /// <inheritdoc />
    public virtual async Task UpdateAsync<T>(ReportServiceContext context, T parameter) where T : class, IReportParameter
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (parameter == null)
        {
            throw new ArgumentNullException(nameof(parameter));
        }

        await HttpClient.PutAsync(RegulationApiEndpoints.RegulationReportParametersUrl(context.TenantId, context.RegulationId, context.ReportId), parameter);
    }

    /// <inheritdoc />
    public virtual async Task DeleteAsync(ReportServiceContext context, int parameterId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (parameterId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(parameterId));
        }

        await HttpClient.DeleteAsync(
            RegulationApiEndpoints.RegulationReportParametersUrl(context.TenantId, context.RegulationId, context.ReportId), parameterId);
    }
}