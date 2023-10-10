using System;
using System.Globalization;
using PayrollEngine.Client.Model;
using PayrollEngine.Client.Service;
using PayrollEngine.Client.Service.Api;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Script;

/// <summary>Export payroll from Payroll API to JSON file</summary>
public sealed class ScriptRebuild
{
    /// <summary>The Payroll http client</summary>
    public PayrollHttpClient HttpClient { get; }

    /// <summary>The tenant id</summary>
    public int TenantId { get; }

    /// <summary>Initializes a new instance of the <see cref="ScriptRebuild"/> class</summary>
    /// <param name="httpClient">The payroll http client</param>
    /// <param name="tenantId">The tenant id</param>
    public ScriptRebuild(PayrollHttpClient httpClient, int tenantId)
    {
        HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        TenantId = tenantId;
    }

    /// <summary>Rebuild a regulation</summary>
    /// <param name="regulationName">The item name or identifier</param>
    public async Task RebuildRegulationAsync(string regulationName)
    {

        // tenant
        var tenant = await new TenantService(HttpClient).GetAsync<Tenant>(new(), TenantId);
        if (tenant == null)
        {
            throw new PayrollException($"Unknown tenant with id {TenantId}");
        }

        // regulation
        var tenantContext = new TenantServiceContext(TenantId);
        var regulation = await new RegulationService(HttpClient).GetAsync<Regulation>(tenantContext, regulationName);
        if (regulation == null)
        {
            throw new PayrollException($"Unknown regulation {regulationName}");
        }

        // rebuild
        var regulationContext = new RegulationServiceContext(TenantId, regulation.Id);
        await RebuildRegulationAsync(tenant, regulationContext);
    }

    /// <summary>Rebuild a regulation object script</summary>
    /// <param name="regulationName">The item name or identifier</param>
    /// <param name="scriptObject">The script object</param>
    /// <param name="objectKey">The object key</param>
    public async Task RebuildRegulationObjectAsync(string regulationName, RegulationScriptObject scriptObject, string objectKey = null)
    {
        if (string.IsNullOrWhiteSpace(regulationName))
        {
            throw new ArgumentException(nameof(regulationName));
        }

        // tenant
        var tenant = await new TenantService(HttpClient).GetAsync<Tenant>(new(), TenantId);
        if (tenant == null)
        {
            throw new PayrollException($"Unknown tenant with id {TenantId}");
        }

        // regulation
        var tenantContext = new TenantServiceContext(TenantId);
        var regulation = await new RegulationService(HttpClient).GetAsync<Regulation>(tenantContext, regulationName);
        if (regulation == null)
        {
            throw new PayrollException($"Unknown regulation {regulationName}");
        }
        var regulationContext = new RegulationServiceContext(TenantId, regulation.Id);
        switch (scriptObject)
        {
            case RegulationScriptObject.Case:
                await RebuildCaseAsync(regulationContext, objectKey);
                break;
            case RegulationScriptObject.CaseRelation:
                await RebuildCaseRelationAsync(regulationContext, objectKey);
                break;
            case RegulationScriptObject.Collector:
                await RebuildCollectorAsync(regulationContext, objectKey);
                break;
            case RegulationScriptObject.WageType:
                await RebuildWageTypeAsync(tenant, regulationContext, objectKey);
                break;
            case RegulationScriptObject.Report:
                await RebuildReportAsync(regulationContext, objectKey);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(scriptObject), scriptObject, null);
        }
    }

    /// <summary>Rebuild a the payrun script</summary>
    /// <param name="payrunName">The payrun name</param>
    public async Task RebuildPayrunAsync(string payrunName)
    {
        if (string.IsNullOrWhiteSpace(payrunName))
        {
            throw new ArgumentException(nameof(payrunName));
        }

        try
        {
            var tenantContext = new TenantServiceContext(TenantId);
            await RebuildPayrunAsync(tenantContext, payrunName);
        }
        catch (Exception exception)
        {
            throw new PayrollException(exception.GetBaseMessage(), exception);
        }
    }

    private async Task RebuildRegulationAsync(Tenant tenant, RegulationServiceContext regulationContext)
    {
        // cases
        await RebuildCaseAsync(regulationContext);
        // case relations
        await RebuildCaseRelationAsync(regulationContext);
        // collectors
        await RebuildCollectorAsync(regulationContext);
        // wage types
        await RebuildWageTypeAsync(tenant, regulationContext);
        // reports
        await RebuildReportAsync(regulationContext);
    }

    private async Task RebuildCaseAsync(RegulationServiceContext regulationContext, string objectKey = null)
    {
        var caseService = new CaseService(HttpClient);
        if (string.IsNullOrWhiteSpace(objectKey))
        {
            var cases = await caseService.QueryAsync<Case>(regulationContext);
            foreach (var @case in cases)
            {
                await caseService.RebuildAsync(regulationContext, @case.Id);
            }
        }
        else
        {
            var @case = await caseService.GetAsync<Case>(regulationContext, objectKey);
            if (@case == null)
            {
                throw new PayrollException($"Unknown case {objectKey}");
            }
            await caseService.RebuildAsync(regulationContext, @case.Id);
        }
    }

    private async Task RebuildCaseRelationAsync(RegulationServiceContext regulationContext, string objectKey = null)
    {
        var caseRelationService = new CaseRelationService(HttpClient);
        if (string.IsNullOrWhiteSpace(objectKey))
        {
            var caseRelations = await caseRelationService.QueryAsync<CaseRelation>(regulationContext);
            foreach (var caseRelation in caseRelations)
            {
                await caseRelationService.RebuildAsync(regulationContext, caseRelation.Id);
            }
        }
        else
        {
            var relatedCases = objectKey.Split(':', StringSplitOptions.RemoveEmptyEntries);
            if (relatedCases.Length != 2)
            {
                throw new ArgumentException($"invalid case relation {objectKey}");
            }

            var reference = objectKey.ReferenceToRelatedCases();
            var caseRelation = await caseRelationService.GetAsync<CaseRelation>(regulationContext, reference.Item1, reference.Item2);
            if (caseRelation == null)
            {
                throw new PayrollException($"Unknown case relation {reference.Item1} to {reference.Item2}");
            }
            await caseRelationService.RebuildAsync(regulationContext, caseRelation.Id);
        }
    }

    private async Task RebuildCollectorAsync(RegulationServiceContext regulationContext, string objectKey = null)
    {
        var collectorService = new CollectorService(HttpClient);
        if (string.IsNullOrWhiteSpace(objectKey))
        {
            var collectors = await collectorService.QueryAsync<Collector>(regulationContext);
            foreach (var collector in collectors)
            {
                await collectorService.RebuildAsync(regulationContext, collector.Id);
            }
        }
        else
        {
            var collector = await collectorService.GetAsync<Collector>(regulationContext, objectKey);
            if (collector == null)
            {
                throw new PayrollException($"Unknown collector {objectKey}");
            }
            await collectorService.RebuildAsync(regulationContext, collector.Id);
        }
    }

    private async Task RebuildWageTypeAsync(Tenant tenant, RegulationServiceContext regulationContext, string objectKey = null)
    {
        var wageTypeService = new WageTypeService(HttpClient);
        if (string.IsNullOrWhiteSpace(objectKey))
        {
            var wageTypes = await wageTypeService.QueryAsync<WageType>(regulationContext);
            foreach (var wageType in wageTypes)
            {
                await wageTypeService.RebuildAsync(regulationContext, wageType.Id);
            }
        }
        else
        {
            // culture
            var culture = CultureInfo.CurrentCulture;
            if (!string.IsNullOrWhiteSpace(tenant.Culture) && !string.Equals(tenant.Culture, culture.Name))
            {
                culture = new CultureInfo(tenant.Culture);
            }

            // wage type
            var wageTypeNumber = decimal.Parse(objectKey, CultureInfo.InvariantCulture);
            var wageType = await wageTypeService.GetAsync<WageType>(regulationContext, wageTypeNumber, culture);
            if (wageType == null)
            {
                throw new PayrollException($"Unknown wage type {objectKey}");
            }

            // rebuild
            await wageTypeService.RebuildAsync(regulationContext, wageType.Id);
        }
    }

    private async Task RebuildReportAsync(RegulationServiceContext regulationContext, string objectKey = null)
    {
        var reportService = new ReportService(HttpClient);
        if (string.IsNullOrWhiteSpace(objectKey))
        {
            var reports = await reportService.QueryAsync<Report>(regulationContext);
            foreach (var report in reports)
            {
                await reportService.RebuildAsync(regulationContext, report.Id);
            }
        }
        else
        {
            var report = await reportService.GetAsync<Report>(regulationContext, objectKey);
            if (report == null)
            {
                throw new PayrollException($"Unknown report {objectKey}");
            }
            await reportService.RebuildAsync(regulationContext, report.Id);
        }
    }

    private async Task RebuildPayrunAsync(TenantServiceContext tenantContext, string payrunName)
    {
        var payrunService = new PayrunService(HttpClient);
        var payrun = await payrunService.GetAsync<Payrun>(tenantContext, payrunName);
        if (payrun == null)
        {
            throw new PayrollException($"Unknown payrun {payrunName}");
        }
    }
}