using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll report template service</summary>
public class ReportTemplateService : Service, IReportTemplateService
{
    /// <summary>Initializes a new instance of the <see cref="ReportTemplateService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public ReportTemplateService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> QueryAsync<T>(ReportServiceContext context, ReportTemplateQuery query = null) where T : class, IReportTemplate
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;

        var requestUri = RegulationApiEndpoints.RegulationReportTemplatesUrl(context.TenantId, context.RegulationId, context.ReportId)
            .AddQueryString(nameof(ReportTemplateQuery.Culture), query.Culture);
        return await HttpClient.GetCollectionAsync<T>(requestUri);
    }

    /// <inheritdoc />
    public virtual async Task<long> QueryCountAsync(ReportServiceContext context, ReportTemplateQuery query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;

        var requestUri = RegulationApiEndpoints.RegulationReportTemplatesUrl(context.TenantId, context.RegulationId, context.ReportId)
            .AddQueryString(nameof(ReportTemplateQuery.Culture), query.Culture);
        return await HttpClient.GetAsync<long>(requestUri);
    }

    /// <inheritdoc />
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(ReportServiceContext context, ReportTemplateQuery query = null) where T : class, IReportTemplate
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
    
        var requestUri = RegulationApiEndpoints.RegulationReportTemplatesUrl(context.TenantId, context.RegulationId, context.ReportId)
            .AddQueryString(nameof(ReportTemplateQuery.Culture), query.Culture);
        return await HttpClient.GetAsync<QueryResult<T>>(requestUri);
    }

    /// <inheritdoc />
    public virtual async Task<T> GetAsync<T>(ReportServiceContext context, int templateId) where T : class, IReportTemplate
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (templateId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(templateId));
        }

        return await HttpClient.GetAsync<T>(RegulationApiEndpoints.RegulationReportTemplateUrl(context.TenantId, context.RegulationId,
            context.ReportId, templateId));
    }

    /// <inheritdoc />
    public virtual async Task<T> CreateAsync<T>(ReportServiceContext context, T template) where T : class, IReportTemplate
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (template == null)
        {
            throw new ArgumentNullException(nameof(template));
        }

        return await HttpClient.PostAsync(
            RegulationApiEndpoints.RegulationReportTemplatesUrl(context.TenantId, context.RegulationId, context.ReportId), template);
    }

    /// <inheritdoc />
    public virtual async Task UpdateAsync<T>(ReportServiceContext context, T template) where T : class, IReportTemplate
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (template == null)
        {
            throw new ArgumentNullException(nameof(template));
        }

        await HttpClient.PutAsync(RegulationApiEndpoints.RegulationReportTemplatesUrl(context.TenantId, context.RegulationId, context.ReportId), template);
    }

    /// <inheritdoc />
    public virtual async Task DeleteAsync(ReportServiceContext context, int templateId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (templateId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(templateId));
        }

        await HttpClient.DeleteAsync(
            RegulationApiEndpoints.RegulationReportTemplatesUrl(context.TenantId, context.RegulationId, context.ReportId), templateId);
    }
}