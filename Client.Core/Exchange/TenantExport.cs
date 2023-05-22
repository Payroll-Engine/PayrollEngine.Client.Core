using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using PayrollEngine.Client.Service.Api;
using Task = PayrollEngine.Client.Model.Task;

namespace PayrollEngine.Client.Exchange;

/// <summary>Export tenant from Payroll API to JSON file</summary>
public sealed class TenantExport
{
    /// <summary>The payroll http client</summary>
    public PayrollHttpClient HttpClient { get; }

    /// <summary>The tenant id</summary>
    public int TenantId { get; }

    /// <summary>The result export mode</summary>
    public ResultExportMode ExportMode { get; }

    /// <summary>The export namespace</summary>
    public string Namespace { get; }

    /// <summary>Initializes a new instance of the <see cref="TenantExport"/> class</summary>
    /// <param name="httpClient">The payroll http client</param>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="exportMode">The result export mode (default: no result)</param>
    /// <param name="namespace">The export namespace</param>
    public TenantExport(PayrollHttpClient httpClient, int tenantId,
        ResultExportMode exportMode = ResultExportMode.NoResults,
        string @namespace = null)
    {
        HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        TenantId = tenantId;
        ExportMode = exportMode;
        Namespace = @namespace;
    }

    /// <summary>Export payroll</summary>
    /// <returns>The exported provide</returns>
    public async Task<Model.Exchange> ExportAsync()
    {
        // regulation shares
        var regulationShares = await LoadRegulationSharesAsync();

        // tenant
        var tenant = await LoadTenantAsync(TenantId);
        if (tenant == null)
        {
            throw new PayrollException($"Missing tenant with id {TenantId}");
        }
        var exchange = new Model.Exchange
        {
            RegulationShares = regulationShares,
            Tenants = new() { tenant }
        };

        // namespace change
        if (!string.IsNullOrWhiteSpace(Namespace) && exchange.Tenants != null)
        {
            exchange.ChangeNamespace(Namespace);
        }

        return exchange;
    }

    #region Shares

    private async Task<List<RegulationShare>> LoadRegulationSharesAsync()
    {
        var shares = await new RegulationShareService(HttpClient).QueryAsync<RegulationShare>(new());
        return shares?.ToList();
    }

    #endregion

    #region Tenant

    private async Task<ExchangeTenant> LoadTenantAsync(int tenantId)
    {
        var tenant = await new TenantService(HttpClient).GetAsync<ExchangeTenant>(new(), tenantId);
        if (tenant != null)
        {
            // system
            tenant.Users = await LoadUsersAsync(tenant.Id);
            tenant.Divisions = await LoadDivisionsAsync(tenant.Id);
            tenant.Tasks = await LoadTasksAsync(tenant.Id);
            tenant.Webhooks = await LoadWebhooksAsync(tenant.Id);

            // regulation and payroll
            tenant.Regulations = await LoadRegulationsAsync(tenant.Id);
            tenant.Payrolls = await LoadPayrollsAsync(tenant.Id);

            // case values
            tenant.GlobalValues = await LoadGlobalCaseValuesAsync(tenant.Id);
            tenant.NationalValues = await LoadNationalCaseValuesAsync(tenant.Id);
            tenant.CompanyValues = await LoadCompanyCaseValuesAsync(tenant.Id);
            tenant.Employees = await LoadEmployeesAsync(tenant.Id);

            // payrun and jobs
            tenant.Payruns = await LoadPayrunsAsync(tenant.Id);
            tenant.PayrunJobs = await LoadPayrunJobsAsync(tenant.Id);
            if (ExportMode == ResultExportMode.Results)
            {
                tenant.PayrollResults = await LoadPayrollResultsAsync(tenant.Id);
            }
        }
        return tenant;
    }

    #endregion

    #region Users

    private async Task<List<User>> LoadUsersAsync(int tenantId) =>
        (await new UserService(HttpClient).QueryAsync<User>(new(tenantId))).ToList();

    #endregion

    #region Divisions

    private async Task<List<Division>> LoadDivisionsAsync(int tenantId) =>
        (await new DivisionService(HttpClient).QueryAsync<Division>(new(tenantId))).ToList();

    private async Task<Division> LoadDivisionAsync(int tenantId, int divisionId) =>
        await new DivisionService(HttpClient).GetAsync<Division>(new(tenantId), divisionId);

    #endregion

    #region Tasks

    private async Task<List<Task>> LoadTasksAsync(int tenantId) =>
        (await new TaskService(HttpClient).QueryAsync<Task>(new(tenantId))).ToList();

    #endregion


    #region Webhooks

    private async Task<List<WebhookSet>> LoadWebhooksAsync(int tenantId)
    {
        // webhooks
        var webhooks = (await new WebhookService(HttpClient).QueryAsync<WebhookSet>(new(tenantId))).ToList();

        // webhook messages
        foreach (var webhook in webhooks)
        {
            webhook.Messages = await LoadWebhookMessagesAsync(tenantId, webhook.Id);
        }

        return webhooks;
    }

    private async Task<List<WebhookMessage>> LoadWebhookMessagesAsync(int tenantId, int webhookId) =>
        (await new WebhookMessageService(HttpClient).QueryAsync<WebhookMessage>(new(tenantId, webhookId))).ToList();

    #endregion

    #region Regulation

    private async Task<List<RegulationSet>> LoadRegulationsAsync(int tenantId)
    {
        // regulations
        var regulations = (await new RegulationService(HttpClient).QueryAsync<RegulationSet>(new(tenantId))).ToList();
        foreach (var regulation in regulations)
        {
            regulation.Cases = await LoadCasesAsync(tenantId, regulation.Id);
            regulation.CaseRelations = await LoadCaseRelationsAsync(tenantId, regulation.Id);
            regulation.WageTypes = await LoadWageTypesAsync(tenantId, regulation.Id);
            regulation.Collectors = await LoadCollectorsAsync(tenantId, regulation.Id);
            regulation.Lookups = await LoadLookupSetsAsync(tenantId, regulation.Id);
            regulation.Scripts = await LoadScriptsCasesAsync(tenantId, regulation.Id);
            regulation.Reports = await LoadReportsAsync(tenantId, regulation.Id);
        }

        return regulations;
    }

    #endregion

    #region Case

    private async Task<List<CaseSet>> LoadCasesAsync(int tenantId, int regulationId)
    {
        // cases
        var cases = (await new CaseService(HttpClient).QueryAsync<CaseSet>(new(tenantId, regulationId))).ToList();

        // case field
        foreach (var @case in cases)
        {
            @case.Fields = await LoadCaseFieldsAsync(tenantId, regulationId, @case.Id);
        }

        return cases;
    }

    private async Task<List<CaseFieldSet>> LoadCaseFieldsAsync(int tenantId, int regulationId, int caseId) =>
        (await new CaseFieldService(HttpClient).QueryAsync<CaseFieldSet>(new(tenantId, regulationId, caseId))).ToList();

    private async Task<List<CaseRelation>> LoadCaseRelationsAsync(int tenantId, int regulationId) =>
        (await new CaseRelationService(HttpClient).QueryAsync<CaseRelation>(new(tenantId, regulationId))).ToList();

    #endregion

    #region Wage Type

    private async Task<List<WageType>> LoadWageTypesAsync(int tenantId, int regulationId) =>
        (await new WageTypeService(HttpClient).QueryAsync<WageType>(new(tenantId, regulationId))).ToList();

    #endregion

    #region Collector

    private async Task<List<Collector>> LoadCollectorsAsync(int tenantId, int regulationId) =>
        (await new CollectorService(HttpClient).QueryAsync<Collector>(new(tenantId, regulationId))).ToList();

    #endregion

    #region Lookups

    private async Task<List<LookupSet>> LoadLookupSetsAsync(int tenantId, int regulationId)
    {
        // lookup sets
        var lookupSets = (await new LookupSetService(HttpClient).QueryAsync<LookupSet>(new(tenantId, regulationId))).ToList();

        // lookup values
        foreach (var lookup in lookupSets)
        {
            lookup.Values = await LoadLookupValuesAsync(tenantId, regulationId, lookup.Id);
        }

        return lookupSets;
    }

    private async Task<List<LookupValue>> LoadLookupValuesAsync(int tenantId, int regulationId, int lookupId) =>
        (await new LookupValueService(HttpClient).QueryAsync<LookupValue>(new(tenantId, regulationId, lookupId))).ToList();

    #endregion

    #region Script

    private async Task<List<Model.Script>> LoadScriptsCasesAsync(int tenantId, int regulationId) =>
        (await new ScriptService(HttpClient).QueryAsync<Model.Script>(new(tenantId, regulationId))).ToList();

    #endregion

    #region Reports

    private async Task<List<ReportSet>> LoadReportsAsync(int tenantId, int regulationId)
    {
        // reports
        var reports = (await new ReportSetService(HttpClient).QueryAsync<ReportSet>(new(tenantId, regulationId))).ToList();

        foreach (var report in reports)
        {
            // report parameters
            report.Parameters = await LoadReportParametersAsync(tenantId, regulationId, report.Id);
            // report templates
            report.Templates = await LoadReportTemplatesAsync(tenantId, regulationId, report.Id);
        }

        return reports;
    }

    private async Task<List<ReportParameter>> LoadReportParametersAsync(int tenantId, int regulationId, int reportId) =>
        (await new ReportParameterService(HttpClient).QueryAsync<ReportParameter>(new(tenantId, regulationId, reportId))).ToList();

    private async Task<List<ReportTemplate>> LoadReportTemplatesAsync(int tenantId, int regulationId, int reportId) =>
        (await new ReportTemplateService(HttpClient).QueryAsync<ReportTemplate>(new(tenantId, regulationId, reportId))).ToList();

    #endregion

    #region Case Values

    private async Task<List<CaseValue>> LoadGlobalCaseValuesAsync(int tenantId) =>
        (await new GlobalCaseValueService(HttpClient).QueryAsync<CaseValue>(new(tenantId))).ToList();

    private async Task<List<CaseValue>> LoadNationalCaseValuesAsync(int tenantId) =>
        (await new NationalCaseValueService(HttpClient).QueryAsync<CaseValue>(new(tenantId))).ToList();

    private async Task<List<CaseValue>> LoadCompanyCaseValuesAsync(int tenantId) =>
        (await new CompanyCaseValueService(HttpClient).QueryAsync<CaseValue>(new(tenantId))).ToList();

    #endregion

    #region Employees

    private async Task<List<EmployeeSet>> LoadEmployeesAsync(int tenantId)
    {
        // employee
        var employees = (await new EmployeeService(HttpClient).QueryAsync<EmployeeSet>(new(tenantId))).ToList();

        // employee case values
        foreach (var employee in employees)
        {
            employee.Values = await LoadEmployeeCaseValuesAsync(tenantId, employee.Id);
        }

        return employees;
    }

    private async Task<List<CaseValue>> LoadEmployeeCaseValuesAsync(int tenantId, int employeeId) =>
        (await new EmployeeCaseValueService(HttpClient).QueryAsync<CaseValue>(new(tenantId, employeeId))).ToList();

    #endregion

    #region Payroll

    private async Task<List<PayrollSet>> LoadPayrollsAsync(int tenantId)
    {
        // payrolls
        var payrolls = (await new PayrollService(HttpClient).QueryAsync<PayrollSet>(new(tenantId))).ToList();
        foreach (var payroll in payrolls)
        {
            // division
            var division = await LoadDivisionAsync(tenantId, payroll.DivisionId);
            if (division == null)
            {
                throw new PayrollException($"Missing division with id {payroll.DivisionId}");
            }
            payroll.DivisionName = division.Name;

            // payroll layers
            payroll.Layers = await LoadPayrollLayersAsync(tenantId, payroll.Id);
        }
        return payrolls;
    }

    #endregion

    #region Payroll Layer

    private async Task<List<PayrollLayer>> LoadPayrollLayersAsync(int tenantId, int payrollId) =>
        (await new PayrollLayerService(HttpClient).QueryAsync<PayrollLayer>(new(tenantId, payrollId))).ToList();

    #endregion

    #region Payrun

    private async Task<List<Payrun>> LoadPayrunsAsync(int tenantId)
    {
        var payruns = (await new PayrunService(HttpClient).QueryAsync<Payrun>(new(tenantId))).ToList();
        foreach (var payrun in payruns)
        {
            // payroll name
            if (payrun.PayrollId > 0)
            {
                var payroll = await new PayrollService(HttpClient).GetAsync<Payroll>(new(tenantId), payrun.PayrollId);
                if (payroll != null)
                {
                    payrun.PayrollName = payroll.Name;
                }
            }

            // payrun parameter
            payrun.PayrunParameters = (await new PayrunParameterService(HttpClient).QueryAsync<PayrunParameter>(
                new(tenantId, payrun.Id))).ToList();
        }
        return payruns;
    }

    #endregion

    #region Payrun Job

    private async Task<List<PayrunJob>> LoadPayrunJobsAsync(int tenantId)
    {
        var payrunJobs = (await new PayrunJobService(HttpClient).QueryAsync<PayrunJob>(new(tenantId))).ToList();
        foreach (var payrunJob in payrunJobs)
        {
            if (payrunJob.PayrollId > 0)
            {
                // payroll name
                var payroll = await new PayrollService(HttpClient).GetAsync<Payroll>(new(tenantId), payrunJob.PayrollId);
                if (payroll == null)
                {
                    throw new PayrollException($"Unknown payroll with id {payrunJob.PayrollId}");
                }
                payrunJob.PayrollName = payroll.Name;

                // division id
                var division = await LoadDivisionAsync(tenantId, payroll.DivisionId);
                if (division == null)
                {
                    throw new PayrollException($"Missing division with id {payroll.DivisionId}");
                }
                payrunJob.DivisionId = division.Id;
            }
        }
        return payrunJobs;
    }

    #endregion

    #region PayrollResult

    private async Task<List<PayrollResultSet>> LoadPayrollResultsAsync(int tenantId)
    {
        // results
        var payrollResults = (await new PayrollResultService(HttpClient).QueryPayrollResultSetsAsync<PayrollResultSet>(new(tenantId))).ToList();
        foreach (var payrollResult in payrollResults)
        {
            // payrun job name
            if (payrollResult.PayrunJobId > 0)
            {
                var payrunJob = await new PayrunJobService(HttpClient).GetAsync<PayrunJob>(new(tenantId), payrollResult.PayrunJobId);
                if (payrunJob != null)
                {
                    payrollResult.PayrunJobName = payrunJob.Name;
                }
            }
        }
        return payrollResults;
    }

    #endregion
}