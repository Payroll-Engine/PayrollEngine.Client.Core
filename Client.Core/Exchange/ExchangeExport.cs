using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using PayrollEngine.Client.QueryExpression;
using PayrollEngine.Client.Service.Api;
using Task = PayrollEngine.Client.Model.Task;

namespace PayrollEngine.Client.Exchange;

/// <summary>Export tenant to exchange</summary>
public sealed class ExchangeExport
{
    /// <summary>The payroll http client</summary>
    public PayrollHttpClient HttpClient { get; }

    /// <summary>The export options</summary>
    public ExchangeExportOptions ExportOptions { get; }

    /// <summary>The export namespace</summary>
    public string Namespace { get; }

    /// <summary>Initializes a new instance of the <see cref="ExchangeExport"/> class</summary>
    /// <param name="httpClient">The payroll http client</param>
    /// <param name="exportOptions">The export options</param>
    /// <param name="namespace">The export namespace</param>
    public ExchangeExport(PayrollHttpClient httpClient, ExchangeExportOptions exportOptions = null,
        string @namespace = null)
    {
        HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        ExportOptions = exportOptions ?? new();
        Namespace = @namespace;
    }

    /// <summary>Export tenant to exchange</summary>
    /// <returns>The exchange model</returns>
    public async Task<Client.Model.Exchange> ExportAsync(int tenantId) =>
        await ExportAsync(new[] { tenantId });

    /// <summary>Export tenants to exchange</summary>
    /// <returns>The exchange model</returns>
    public async Task<Client.Model.Exchange> ExportAsync(IEnumerable<int> tenantIds)
    {
        if (tenantIds == null)
        {
            throw new ArgumentNullException(nameof(tenantIds));
        }
        var ids = tenantIds.ToList();

        var exchange = new Client.Model.Exchange
        {
            // regulation shares
            RegulationShares = await LoadRegulationSharesAsync()
        };

        // tenants
        foreach (var id in ids)
        {
            var tenant = await LoadTenantAsync(id);
            if (tenant == null)
            {
                throw new PayrollException($"Unknown tenant with id {id}");
            }

            exchange.Tenants ??= new();
            exchange.Tenants.Add(tenant);
        }

        // namespace change
        if (!string.IsNullOrWhiteSpace(Namespace) && exchange.Tenants != null)
        {
            exchange.ChangeNamespace(Namespace);
        }

        return exchange;
    }

    private bool ExportActive(string[] filter)
    {
        if (!ExportOptions.HasAnyFilter)
        {
            return true;
        }
        return filter != null && filter.Length > 0;
    }

    #region Regulation Shares

    private async Task<List<RegulationShare>> LoadRegulationSharesAsync()
    {
        var shares = await new RegulationShareService(HttpClient).QueryAsync<RegulationShare>(new());
        var shareList = shares?.ToList();
        return shareList != null && shareList.Any() ? shareList : null;
    }

    #endregion

    #region Tenant

    private async Task<ExchangeTenant> LoadTenantAsync(int tenantId)
    {
        var tenant = await new TenantService(HttpClient).GetAsync<ExchangeTenant>(new(), tenantId);
        if (tenant != null)
        {
            // users
            if (ExportActive(ExportOptions.Users))
            {
                tenant.Users = await LoadUsersAsync(tenant.Id);
            }

            // divisions
            if (ExportActive(ExportOptions.Divisions))
            {
                tenant.Divisions = await LoadDivisionsAsync(tenant.Id);
            }

            // tasks
            if (ExportActive(ExportOptions.Tasks))
            {
                tenant.Tasks = await LoadTasksAsync(tenant.Id);
            }

            // webhooks
            if (ExportActive(ExportOptions.Webhooks))
            {
                tenant.Webhooks = await LoadWebhooksAsync(tenant.Id);
            }

            // regulation
            if (ExportActive(ExportOptions.Regulations))
            {
                tenant.Regulations = await LoadRegulationsAsync(tenant.Id);
            }

            // payroll
            if (ExportActive(ExportOptions.Payrolls))
            {
                tenant.Payrolls = await LoadPayrollsAsync(tenant.Id);
            }

            // case values
            if (ExportOptions.ExportGlobalCaseValues)
            {
                tenant.GlobalValues = await LoadGlobalCaseValuesAsync(tenant.Id);
            }
            if (ExportOptions.ExportNationalCaseValues)
            {
                tenant.NationalValues = await LoadNationalCaseValuesAsync(tenant.Id);

            }
            if (ExportOptions.ExportCompanyCaseValues)
            {
                tenant.CompanyValues = await LoadCompanyCaseValuesAsync(tenant.Id);
            }

            // employees, including employee case values
            if (ExportActive(ExportOptions.Employees))
            {
                tenant.Employees = await LoadEmployeesAsync(tenant.Id);

                // payrun and jobs
            }

            // payrun
            if (ExportActive(ExportOptions.Payruns))
            {
                tenant.Payruns = await LoadPayrunsAsync(tenant.Id);
            }

            // payrun jobs
            if (ExportActive(ExportOptions.PayrunJobs))
            {
                tenant.PayrunJobs = await LoadPayrunJobsAsync(tenant.Id);
            }

            // payroll results
            if (ExportOptions.ExportPayrollResults)
            {
                tenant.PayrollResults = await LoadPayrollResultsAsync(tenant.Id);
            }

            // reset tenant data on filtered export
            if (ExportOptions.HasAnyFilter)
            {
                tenant.Culture = null;
                tenant.Calendar = null;
                tenant.Attributes = null;
            }
        }

        return tenant;
    }

    #endregion

    #region Users

    private async Task<List<User>> LoadUsersAsync(int tenantId)
    {
        // filter
        Query query = null;
        if (ExportOptions.Users != null)
        {
            string filter = null;
            foreach (var user in ExportOptions.Users)
            {
                var condition = new Equals(nameof(User.Identifier), user);
                filter = filter == null ? condition : new Filter(filter).Or(condition);
            }
            query = new() { Filter = filter };
        }
        var users = (await new UserService(HttpClient).QueryAsync<User>(new(tenantId), query)).ToList();
        return users.Any() ? users : null;
    }

    #endregion

    #region Divisions

    private async Task<List<Division>> LoadDivisionsAsync(int tenantId)
    {
        // filter
        Query query = null;
        if (ExportOptions.Divisions != null)
        {
            string filter = null;
            foreach (var division in ExportOptions.Divisions)
            {
                var condition = new Equals(nameof(Division.Name), division);
                filter = filter == null ? condition : new Filter(filter).Or(condition);
            }
            query = new() { Filter = filter };
        }
        var divisions = (await new DivisionService(HttpClient).QueryAsync<Division>(new(tenantId), query)).ToList();
        return divisions.Any() ? divisions : null;
    }

    private async Task<Division> LoadDivisionAsync(int tenantId, int divisionId) =>
        await new DivisionService(HttpClient).GetAsync<Division>(new(tenantId), divisionId);

    #endregion

    #region Tasks

    private async Task<List<Task>> LoadTasksAsync(int tenantId)
    {
        // filter
        Query query = null;
        if (ExportOptions.Tasks != null)
        {
            string filter = null;
            foreach (var task in ExportOptions.Tasks)
            {
                var condition = new Equals(nameof(Task.Name), task);
                filter = filter == null ? condition : new Filter(filter).Or(condition);
            }
            query = new() { Filter = filter };
        }
        var tasks = (await new TaskService(HttpClient).QueryAsync<Task>(new(tenantId), query)).ToList();
        return tasks.Any() ? tasks : null;
    }

    #endregion

    #region Webhooks

    private async Task<List<WebhookSet>> LoadWebhooksAsync(int tenantId)
    {
        // filter
        Query query = null;
        if (ExportOptions.Webhooks != null)
        {
            string filter = null;
            foreach (var webhook in ExportOptions.Webhooks)
            {
                var condition = new Equals(nameof(Webhook.Name), webhook);
                filter = filter == null ? condition : new Filter(filter).Or(condition);
            }
            query = new() { Filter = filter };
        }

        // webhooks
        var webhooks = (await new WebhookService(HttpClient).QueryAsync<WebhookSet>(new(tenantId), query)).ToList();
        if (!webhooks.Any())
        {
            return null;
        }

        // webhook messages
        if (ExportOptions.ExportWebhookMessages)
        {
            foreach (var webhook in webhooks)
            {
                webhook.Messages = await LoadWebhookMessagesAsync(tenantId, webhook.Id);
            }
        }

        return webhooks;
    }

    private async Task<List<WebhookMessage>> LoadWebhookMessagesAsync(int tenantId, int webhookId) =>
        (await new WebhookMessageService(HttpClient).QueryAsync<WebhookMessage>(new(tenantId, webhookId))).ToList();

    #endregion

    #region Regulation

    private async Task<List<RegulationSet>> LoadRegulationsAsync(int tenantId)
    {
        // filter
        Query query = null;
        if (ExportOptions.Regulations != null)
        {
            string filter = null;
            foreach (var regulation in ExportOptions.Regulations)
            {
                var condition = new Equals(nameof(Regulation.Name), regulation);
                filter = filter == null ? condition : new Filter(filter).Or(condition);
            }
            query = new() { Filter = filter };
        }

        // regulations
        var regulations = (await new RegulationService(HttpClient).QueryAsync<RegulationSet>(new(tenantId), query)).ToList();
        if (!regulations.Any())
        {
            return null;
        }

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
        if (!cases.Any())
        {
            return null;
        }

        // case field
        foreach (var @case in cases)
        {
            @case.Fields = await LoadCaseFieldsAsync(tenantId, regulationId, @case.Id);
        }

        return cases;
    }

    private async Task<List<CaseFieldSet>> LoadCaseFieldsAsync(int tenantId, int regulationId, int caseId)
    {
        var caseFields = (await new CaseFieldService(HttpClient).QueryAsync<CaseFieldSet>(new(tenantId, regulationId, caseId)))
            .ToList();
        return caseFields.Any() ? caseFields : null;
    }

    private async Task<List<CaseRelation>> LoadCaseRelationsAsync(int tenantId, int regulationId) =>
        (await new CaseRelationService(HttpClient).QueryAsync<CaseRelation>(new(tenantId, regulationId))).ToList();

    #endregion

    #region Wage Type

    private async Task<List<WageType>> LoadWageTypesAsync(int tenantId, int regulationId)
    {
        var wageTypes = (await new WageTypeService(HttpClient).QueryAsync<WageType>(new(tenantId, regulationId))).ToList();
        return wageTypes.Any() ? wageTypes : null;
    }

    #endregion

    #region Collector

    private async Task<List<Collector>> LoadCollectorsAsync(int tenantId, int regulationId)
    {
        var collectors = (await new CollectorService(HttpClient).QueryAsync<Collector>(new(tenantId, regulationId))).ToList();
        return collectors.Any() ? collectors : null;
    }

    #endregion

    #region Lookups

    private async Task<List<LookupSet>> LoadLookupSetsAsync(int tenantId, int regulationId)
    {
        // lookup sets
        var lookupSets = (await new LookupSetService(HttpClient).QueryAsync<LookupSet>(new(tenantId, regulationId))).ToList();
        if (!lookupSets.Any())
        {
            return null;
        }

        // lookup values
        foreach (var lookup in lookupSets)
        {
            lookup.Values = await LoadLookupValuesAsync(tenantId, regulationId, lookup.Id);
        }

        return lookupSets;
    }

    private async Task<List<LookupValue>> LoadLookupValuesAsync(int tenantId, int regulationId, int lookupId)
    {
        var lookupValues = (await new LookupValueService(HttpClient).QueryAsync<LookupValue>(new(tenantId, regulationId, lookupId))).ToList();
        return lookupValues.Any() ? lookupValues : null;
    }

    #endregion

    #region Script

    private async Task<List<Client.Model.Script>> LoadScriptsCasesAsync(int tenantId, int regulationId)
    {
        var scripts = (await new ScriptService(HttpClient).QueryAsync<Client.Model.Script>(new(tenantId, regulationId))).ToList();
        return scripts.Any() ? scripts : null;
    }

    #endregion

    #region Reports

    private async Task<List<ReportSet>> LoadReportsAsync(int tenantId, int regulationId)
    {
        // reports
        var reports = (await new ReportSetService(HttpClient).QueryAsync<ReportSet>(new(tenantId, regulationId))).ToList();
        if (!reports.Any())
        {
            return null;
        }

        foreach (var report in reports)
        {
            // report parameters
            report.Parameters = await LoadReportParametersAsync(tenantId, regulationId, report.Id);
            // report templates
            report.Templates = await LoadReportTemplatesAsync(tenantId, regulationId, report.Id);
        }

        return reports;
    }

    private async Task<List<ReportParameter>> LoadReportParametersAsync(int tenantId, int regulationId, int reportId)
    {
        var reportParameters = (await new ReportParameterService(HttpClient).QueryAsync<ReportParameter>(new(tenantId, regulationId, reportId))).ToList();
        return reportParameters.Any() ? reportParameters : null;
    }

    private async Task<List<ReportTemplate>> LoadReportTemplatesAsync(int tenantId, int regulationId, int reportId)
    {
        var reportTemplates = (await new ReportTemplateService(HttpClient).QueryAsync<ReportTemplate>(new(tenantId, regulationId, reportId))).ToList();
        return reportTemplates.Any() ? reportTemplates : null;
    }

    #endregion

    #region Case Values

    private async Task<List<CaseValue>> LoadGlobalCaseValuesAsync(int tenantId)
    {
        var caseValues = (await new GlobalCaseValueService(HttpClient).QueryAsync<CaseValue>(new(tenantId))).ToList();
        return caseValues.Any() ? caseValues : null;
    }

    private async Task<List<CaseValue>> LoadNationalCaseValuesAsync(int tenantId)
    {
        var caseValues = (await new NationalCaseValueService(HttpClient).QueryAsync<CaseValue>(new(tenantId))).ToList();
        return caseValues.Any() ? caseValues : null;
    }

    private async Task<List<CaseValue>> LoadCompanyCaseValuesAsync(int tenantId)
    {
        var caseValues = (await new CompanyCaseValueService(HttpClient).QueryAsync<CaseValue>(new(tenantId))).ToList();
        return caseValues.Any() ? caseValues : null;
    }

    #endregion

    #region Employees

    private async Task<List<EmployeeSet>> LoadEmployeesAsync(int tenantId)
    {
        // filter
        DivisionQuery query = null;
        if (ExportOptions.Employees != null)
        {
            string filter = null;
            foreach (var employee in ExportOptions.Employees)
            {
                var condition = new Equals(nameof(Employee.Identifier), employee);
                filter = filter == null ? condition : new Filter(filter).Or(condition);
            }
            query = new() { Filter = filter };
        }

        // employee
        var employees = (await new EmployeeService(HttpClient).QueryAsync<EmployeeSet>(new(tenantId), query)).ToList();
        if (!employees.Any())
        {
            return null;
        }

        // employee case values
        if (ExportOptions.ExportEmployeeCaseValues)
        {
            foreach (var employee in employees)
            {
                employee.Values = await LoadEmployeeCaseValuesAsync(tenantId, employee.Id);
            }
        }

        return employees;
    }

    private async Task<List<CaseValue>> LoadEmployeeCaseValuesAsync(int tenantId, int employeeId)
    {
        var caseValues = (await new EmployeeCaseValueService(HttpClient).QueryAsync<CaseValue>(new(tenantId, employeeId))).ToList();
        return caseValues.Any() ? caseValues : null;
    }

    #endregion

    #region Payroll

    private async Task<List<PayrollSet>> LoadPayrollsAsync(int tenantId)
    {
        // payrolls
        var payrolls = (await new PayrollService(HttpClient).QueryAsync<PayrollSet>(new(tenantId))).ToList();
        if (!payrolls.Any())
        {
            return null;
        }

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

    private async Task<List<PayrollLayer>> LoadPayrollLayersAsync(int tenantId, int payrollId)
    {
        var layers = (await new PayrollLayerService(HttpClient).QueryAsync<PayrollLayer>(new(tenantId, payrollId))).ToList();
        return layers.Any() ? layers : null;
    }

    #endregion

    #region Payrun

    private async Task<List<Payrun>> LoadPayrunsAsync(int tenantId)
    {
        // filter
        Query query = null;
        if (ExportOptions.Payruns != null)
        {
            string filter = null;
            foreach (var payrun in ExportOptions.Payruns)
            {
                var condition = new Equals(nameof(Payrun.Name), payrun);
                filter = filter == null ? condition : new Filter(filter).Or(condition);
            }
            query = new() { Filter = filter };
        }

        var payruns = (await new PayrunService(HttpClient).QueryAsync<Payrun>(new(tenantId), query)).ToList();
        if (!payruns.Any())
        {
            return null;
        }

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
            var parameters = (await new PayrunParameterService(HttpClient).QueryAsync<PayrunParameter>(
                new(tenantId, payrun.Id))).ToList();
            payrun.PayrunParameters = parameters.Any() ? parameters : null;
        }
        return payruns;
    }

    #endregion

    #region Payrun Job

    private async Task<List<PayrunJob>> LoadPayrunJobsAsync(int tenantId)
    {        // filter
        Query query = null;
        if (ExportOptions.PayrunJobs != null)
        {
            string filter = null;
            foreach (var payrunJob in ExportOptions.PayrunJobs)
            {
                var condition = new Equals(nameof(PayrunJob.Name), payrunJob);
                filter = filter == null ? condition : new Filter(filter).Or(condition);
            }
            query = new() { Filter = filter };
        }

        var payrunJobs = (await new PayrunJobService(HttpClient).QueryAsync<PayrunJob>(new(tenantId), query)).ToList();
        if (!payrunJobs.Any())
        {
            return null;
        }

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
        if (!payrollResults.Any())
        {
            return null;
        }

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