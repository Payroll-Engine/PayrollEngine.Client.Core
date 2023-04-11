using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PayrollEngine.Client.Model;
using PayrollEngine.Client.Script;
using PayrollEngine.Client.Service;
using PayrollEngine.Client.Service.Api;
using PayrollEngine.Serialization;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Exchange;

/// <summary>Import exchange from JSON file to Payroll API</summary>
public sealed class ExchangeImport : ImportExchangeVisitor
{
    /// <summary>The update mode</summary>
    public UpdateMode UpdateMode { get; }

    /// <summary>The data import mode</summary>
    public DataImportMode ImportMode { get; }

    /// <summary>Initializes a new instance of the <see cref="ExchangeImport"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    /// <param name="exchange">The exchange model</param>
    /// <param name="scriptParser">The script parser</param>
    /// <param name="updateMode">The update mode (default: update)</param>
    /// <param name="importMode">The data import mode (default: single)</param>
    public ExchangeImport(PayrollHttpClient httpClient, Model.Exchange exchange, IScriptParser scriptParser,
        UpdateMode updateMode = UpdateMode.Update, DataImportMode importMode = DataImportMode.Single) :
        base(httpClient, exchange, scriptParser, LoadVisitorOptions.All)
    {
        var hasTenants = exchange.Tenants == null || exchange.Tenants.Any();
        var hasRegulationPermissions =
            exchange.RegulationPermissions == null || exchange.RegulationPermissions.Any();
        if (!hasTenants && !hasRegulationPermissions)
        {
            throw new PayrollException("Missing import data");
        }

        UpdateMode = updateMode;
        ImportMode = importMode;
    }

    /// <summary>Import payroll</summary>
    public async Task ImportAsync() => await VisitAsync();

    private async Task UpsertObjectAsync<T>(string url, T newObject, T existingObject)
        where T : IModel =>
        await HttpClient.UpsertObjectAsync(url, newObject, existingObject, UpdateMode, Exchange.CreatedObjectDate);

    /// <summary><inheritdoc/></summary>
    protected override async Task VisitRegulationPermissionAsync(IRegulationPermission permission)
    {
        await base.VisitRegulationPermissionAsync(permission);

        // tenant
        if (permission.TenantId == 0 && !string.IsNullOrWhiteSpace(permission.TenantIdentifier))
        {
            var tenant = await GetTenantAsync(permission.TenantIdentifier);
            permission.TenantId = tenant.Id;
        }
        // regulation
        if (permission.RegulationId == 0 && !string.IsNullOrWhiteSpace(permission.RegulationName))
        {
            var regulation = await GetRegulationAsync(permission.TenantId, permission.RegulationName);
            permission.RegulationId = regulation.Id;
        }
        // permission tenant
        if (permission.PermissionTenantId == 0 && !string.IsNullOrWhiteSpace(permission.PermissionTenantIdentifier))
        {
            var permissionTenant = await GetTenantAsync(permission.PermissionTenantIdentifier);
            permission.PermissionTenantId = permissionTenant.Id;
        }
        // permission division
        if (permission.PermissionDivisionId == 0 && !string.IsNullOrWhiteSpace(permission.PermissionDivisionName))
        {
            var permissionTenant = await GetDivisionAsync(permission.PermissionTenantId, permission.PermissionDivisionName);
            permission.PermissionDivisionId = permissionTenant.Id;
        }

        // get existing tenants
        var permissions = (await new SharedRegulationService(HttpClient).QueryAsync<RegulationPermission>(new())).ToList();
        var targetPermission = permissions.FirstOrDefault(x =>
            permission.TenantId == x.TenantId &&
            permission.RegulationId == x.RegulationId &&
            permission.PermissionTenantId == x.PermissionTenantId &&
            permission.PermissionDivisionId == x.PermissionDivisionId);

        // upsert tenant
        await HttpClient.UpsertObjectAsync(ApiEndpoints.SharedRegulationPermissionsUrl(), permission, targetPermission, UpdateMode,
            Exchange.CreatedObjectDate);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupTenantAsync(IExchangeTenant tenant, ITenant targetTenant)
    {
        await base.SetupTenantAsync(tenant, targetTenant);

        // update tenant
        await HttpClient.UpsertObjectAsync(TenantApiEndpoints.TenantsUrl(), tenant, targetTenant, UpdateMode,
            Exchange.CreatedObjectDate);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupUserAsync(IExchangeTenant tenant, IUser user, IUser targetUser)
    {
        await base.SetupUserAsync(tenant, user, targetUser);

        // update user
        await UpsertObjectAsync(TenantApiEndpoints.UsersUrl(tenant.Id), user, targetUser);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupDivisionAsync(IExchangeTenant tenant, IDivision division, IDivision targetDivision)
    {
        await base.SetupDivisionAsync(tenant, division, targetDivision);

        // upsert object
        await UpsertObjectAsync(TenantApiEndpoints.DivisionsUrl(tenant.Id), division, targetDivision);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task VisitTaskAsync(IExchangeTenant tenant, ITask task)
    {
        await base.VisitTaskAsync(tenant, task);

        // created user
        if (task.ScheduledUserId == 0 && !string.IsNullOrWhiteSpace(task.ScheduledUserIdentifier))
        {
            var createdUser = await GetUserAsync(tenant.Id, task.ScheduledUserIdentifier);
            task.ScheduledUserId = createdUser.Id;
        }

        // completed user
        if (task.CompletedUserId == 0 && !string.IsNullOrWhiteSpace(task.CompletedUserIdentifier))
        {
            var completedUser = await GetUserAsync(tenant.Id, task.CompletedUserIdentifier);
            task.CompletedUserId = completedUser.Id;
        }

        // update task
        await UpsertObjectAsync(TenantApiEndpoints.TasksUrl(tenant.Id), task, null);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupWebhookAsync(IExchangeTenant tenant, IWebhook webhook, IWebhook targetWebhook)
    {
        await base.SetupWebhookAsync(tenant, webhook, targetWebhook);

        // update webhook
        await UpsertObjectAsync(TenantApiEndpoints.WebhooksUrl(tenant.Id), webhook, targetWebhook);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupRegulationAsync(IExchangeTenant tenant, IRegulationSet regulation, IRegulation targetRegulation)
    {
        await base.SetupRegulationAsync(tenant, regulation, targetRegulation);

        // update regulation
        await UpsertObjectAsync(RegulationApiEndpoints.RegulationsUrl(tenant.Id), regulation, targetRegulation);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task VisitLookupsAsync(IExchangeTenant tenant, IRegulationSet regulation)
    {
        if (regulation.Lookups != null)
        {
            switch (ImportMode)
            {
                case DataImportMode.Single:
                    await base.VisitLookupsAsync(tenant, regulation);
                    break;
                case DataImportMode.Bulk:
                    await SetupBulkLookupsAsync(tenant, regulation, regulation.Lookups);
                    break;
            }
        }
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupLookupAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ILookupSet lookup, ILookup targetLookup)
    {
        await base.SetupLookupAsync(tenant, regulation, lookup, targetLookup);

        // update lookup
        await UpsertObjectAsync(RegulationApiEndpoints.RegulationLookupsUrl(tenant.Id, regulation.Id), lookup, targetLookup);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupLookupValueAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ILookupSet lookup, ILookupValue lookupValue, ILookupValue targetLookupValue)
    {
        await base.SetupLookupValueAsync(tenant, regulation, lookup, lookupValue, targetLookupValue);

        // upsert object
        await UpsertObjectAsync(RegulationApiEndpoints.RegulationLookupValuesUrl(tenant.Id, regulation.Id, lookup.Id),
            lookupValue, targetLookupValue);
    }

    private async Task SetupBulkLookupsAsync(IExchangeTenant tenant, IRegulationSet regulation, ICollection<LookupSet> lookups)
    {
        // cleanup existing lookups
        var resLookups = new LookupSetService(HttpClient);
        var context = new RegulationServiceContext(tenant.Id, regulation.Id);
        var existingLookups = await resLookups.QueryAsync<LookupSet>(context);
        foreach (var lookup in lookups)
        {
            // validate lookup
            ValidateLookup(lookup);
            // get existing object
            var existingLookup = existingLookups.FirstOrDefault(x => string.Equals(x.Name, lookup.Name));
            if (existingLookup != null)
            {
                await resLookups.DeleteAsync(context, existingLookup.Id);
            }
        }

        // created date
        if (Exchange.CreatedObjectDate.HasValue)
        {
            foreach (var lookup in lookups)
            {
                // lookup created date
                if (!lookup.Created.IsDefined())
                {
                    lookup.Created = Exchange.CreatedObjectDate.Value;
                }
                // lookup values created date
                foreach (var lookupValue in lookup.Values)
                {
                    if (!lookupValue.Created.IsDefined())
                    {
                        lookupValue.Created = Exchange.CreatedObjectDate.Value;
                    }
                }
            }
        }

        // add lookup sets
        await resLookups.CreateAsync(context, lookups);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupCaseAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICaseSet caseSet, ICase targetCase)
    {
        await base.SetupCaseAsync(tenant, regulation, caseSet, targetCase);

        // update case
        await UpsertObjectAsync(RegulationApiEndpoints.RegulationCasesUrl(tenant.Id, regulation.Id), caseSet, targetCase);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupCaseFieldAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICaseSet caseSet, ICaseField caseField, ICaseField targetCaseField)
    {
        await base.SetupCaseFieldAsync(tenant, regulation, caseSet, caseField, targetCaseField);

        // update case field
        await UpsertObjectAsync(RegulationApiEndpoints.RegulationCaseFieldsUrl(tenant.Id, regulation.Id, caseSet.Id), caseField, targetCaseField);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupCaseRelationAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICaseRelation caseRelation, ICaseRelation targetCaseRelation)
    {
        await base.SetupCaseRelationAsync(tenant, regulation, caseRelation, targetCaseRelation);

        // update case relation
        await UpsertObjectAsync(RegulationApiEndpoints.RegulationCaseRelationsUrl(tenant.Id, regulation.Id), caseRelation, targetCaseRelation);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupCollectorAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICollector collector, ICollector targetCollector)
    {
        await base.SetupCollectorAsync(tenant, regulation, collector, targetCollector);

        // update collector
        await UpsertObjectAsync(RegulationApiEndpoints.RegulationCollectorsUrl(tenant.Id, regulation.Id), collector, targetCollector);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupWageTypeAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IWageType wageType, IWageType targetWageType)
    {
        await base.SetupWageTypeAsync(tenant, regulation, wageType, targetWageType);

        // update wage type
        await UpsertObjectAsync(RegulationApiEndpoints.RegulationWageTypesUrl(tenant.Id, regulation.Id), wageType, targetWageType);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupScriptAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IScript script, IScript targetScript)
    {
        await base.SetupScriptAsync(tenant, regulation, script, targetScript);

        // update script
        await UpsertObjectAsync(RegulationApiEndpoints.RegulationScriptsUrl(tenant.Id, regulation.Id), script, targetScript);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupReportAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report, IReportSet targetReport)
    {
        await base.SetupReportAsync(tenant, regulation, report, targetReport);

        // upsert object
        await UpsertObjectAsync(RegulationApiEndpoints.RegulationReportsUrl(tenant.Id, regulation.Id), report, targetReport);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupReportParameterAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report, IReportParameter parameter, IReportParameter targetParameter)
    {
        await base.SetupReportParameterAsync(tenant, regulation, report, parameter, targetParameter);

        // update report parameter
        await UpsertObjectAsync(RegulationApiEndpoints.RegulationReportParametersUrl(tenant.Id, regulation.Id, report.Id), parameter, targetParameter);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupReportTemplateAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report, IReportTemplate template, IReportTemplate targetTemplate)
    {
        await base.SetupReportTemplateAsync(tenant, regulation, report, template, targetTemplate);

        // update report template
        await UpsertObjectAsync(RegulationApiEndpoints.RegulationReportTemplatesUrl(tenant.Id, regulation.Id, report.Id), template, targetTemplate);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupEmployeeAsync(IExchangeTenant tenant, IEmployeeSet employee, IEmployee targetEmployee)
    {
        await base.SetupEmployeeAsync(tenant, employee, targetEmployee);

        // update employee
        await UpsertObjectAsync(EmployeeCaseApiEndpoints.EmployeesUrl(tenant.Id), employee, targetEmployee);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupPayrollAsync(IExchangeTenant tenant, IPayrollSet payroll, IPayroll targetPayroll)
    {
        await base.SetupPayrollAsync(tenant, payroll, targetPayroll);

        // country
        if (payroll.Country == 0 && payroll.CountryName.HasValue)
        {
            // enum represents the most known countries using the ISO 3166-1 numeric country code
            payroll.Country = (int)payroll.CountryName.Value;
        }

        // task
        if (payroll.DivisionId <= 0 && !string.IsNullOrWhiteSpace(payroll.DivisionName))
        {
            var division = await GetDivisionAsync(tenant.Id, payroll.DivisionName);
            if (division == null)
            {
                throw new PayrollException($"Missing division with name {payroll.DivisionName}");
            }
            payroll.DivisionId = division.Id;
        }

        // update payroll
        await UpsertObjectAsync(PayrollApiEndpoints.PayrollsUrl(tenant.Id), payroll, targetPayroll);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupPayrollLayerAsync(IExchangeTenant tenant, IPayrollSet payroll,
        IPayrollLayer layer, IPayrollLayer targetLayer)
    {
        await base.SetupPayrollLayerAsync(tenant, payroll, layer, targetLayer);

        // update payroll layer
        await UpsertObjectAsync(PayrollApiEndpoints.PayrollLayersUrl(tenant.Id, payroll.Id), layer, targetLayer);
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupCaseChangeAsync(IExchangeTenant tenant, IPayrollSet payroll,
        ICaseChangeSetup caseChangeSetup)
    {
        await base.SetupCaseChangeAsync(tenant, payroll, caseChangeSetup);

        // user
        var user = await GetUserAsync(tenant.Id, caseChangeSetup.UserIdentifier);
        if (user == null)
        {
            throw new PayrollException($"Unknown user with identifier {caseChangeSetup.UserIdentifier}");
        }
        caseChangeSetup.UserId = user.Id;

        // employee
        if (!caseChangeSetup.EmployeeId.HasValue && !string.IsNullOrEmpty(caseChangeSetup.EmployeeIdentifier))
        {
            var employee = await GetEmployeeAsync(tenant.Id, caseChangeSetup.EmployeeIdentifier);
            if (employee == null)
            {
                throw new PayrollException($"Missing case change employee with identifier {caseChangeSetup.EmployeeIdentifier}");
            }
            caseChangeSetup.EmployeeId = employee.Id;
        }

        // case change task by name
        if (!caseChangeSetup.DivisionId.HasValue && !string.IsNullOrWhiteSpace(caseChangeSetup.DivisionName))
        {
            var division = await GetDivisionAsync(tenant.Id, caseChangeSetup.DivisionName);
            if (division == null)
            {
                throw new PayrollException($"Missing case change division with name {caseChangeSetup.DivisionName}");
            }
            caseChangeSetup.DivisionId = division.Id;
        }

        // case change task
        var caseValues = caseChangeSetup.CollectCaseValues();
        foreach (var caseValue in caseValues)
        {
            // ensure same task for the case change and all case values
            if (caseChangeSetup.DivisionId.HasValue)
            {
                caseValue.DivisionId = caseChangeSetup.DivisionId.Value;
            }
            else if (!caseValue.DivisionId.HasValue && !string.IsNullOrWhiteSpace(caseValue.DivisionName))
            {
                var division = await GetDivisionAsync(tenant.Id, caseValue.DivisionName);
                if (division == null)
                {
                    throw new PayrollException($"Missing case value division with name {caseChangeSetup.DivisionName}");
                }
                caseValue.DivisionId = division.Id;
            }
        }

        // created date
        if (Exchange.CreatedObjectDate.HasValue)
        {
            // case change created date
            if (!caseChangeSetup.Created.IsDefined())
            {
                caseChangeSetup.Created = Exchange.CreatedObjectDate.Value;
            }
            // case value created date
            foreach (var caseValue in caseValues)
            {
                if (!caseValue.Created.IsDefined())
                {
                    caseValue.Created = Exchange.CreatedObjectDate.Value;
                }
            }
        }

        // create case change
        await AddCaseAsync(tenant.Id, payroll.Id, caseChangeSetup);
    }

    private async Task AddCaseAsync<T>(int tenantId, int payrollId, T caseChange)
        where T : class, ICaseChangeSetup
    {
        var payrollContext = new PayrollServiceContext(tenantId, payrollId);

        // cancellation: resolve cancellation id by case name and created date
        if (!caseChange.CancellationId.HasValue && caseChange.CancellationCreated.HasValue)
        {
            // find the cancellation case by case name and created date
            var cancellationCase = await new PayrollService(HttpClient).BuildCaseSetAsync<CaseSet>(payrollContext,
                caseChange.Case.CaseName, caseChange.UserId, caseChange.EmployeeId);
            if (cancellationCase == null)
            {
                throw new PayrollException($"Unknown cancellation case {caseChange.Case.CaseName}");
            }

            // find the cancellation case change
            CaseChange cancellationCaseChange = null;
            var query = new CaseChangeQuery
            {
                Filter = $"{nameof(CaseChange.Created)} eq '{caseChange.CancellationCreated.Value.ToUtcString(CultureInfo.CurrentCulture)}'"
            };
            switch (cancellationCase.CaseType)
            {
                case CaseType.Global:
                    cancellationCaseChange = (await new GlobalCaseChangeService(HttpClient).QueryAsync<CaseChange>(
                        new(tenantId), query)).FirstOrDefault();
                    break;
                case CaseType.National:
                    cancellationCaseChange = (await new NationalCaseChangeService(HttpClient).QueryAsync<CaseChange>(
                        new(tenantId), query)).FirstOrDefault();
                    break;
                case CaseType.Company:
                    cancellationCaseChange = (await new CompanyCaseChangeService(HttpClient).QueryAsync<CaseChange>(
                        new(tenantId), query)).FirstOrDefault();
                    break;
                case CaseType.Employee:
                    if (!caseChange.EmployeeId.HasValue)
                    {
                        throw new PayrollException($"Missing employee identifier on case change {caseChange.Case.CaseName}");
                    }
                    cancellationCaseChange = (await new EmployeeCaseChangeService(HttpClient).QueryAsync<CaseChange>(
                        new(tenantId, caseChange.EmployeeId.Value), query)).FirstOrDefault();
                    break;
            }
            if (cancellationCaseChange == null)
            {
                throw new PayrollException($"Unknown cancellation case {caseChange.Case.CaseName} created on {caseChange.CancellationCreated.Value}");
            }

            // setup cancellation id
            caseChange.CancellationId = cancellationCaseChange.Id;
        }

        var newCaseChange = await new PayrollService(HttpClient).AddCaseAsync<T, CaseChange>(payrollContext, caseChange);
        if (newCaseChange.Issues != null && newCaseChange.Issues.Any())
        {
            throw new PayrollException($"Issues on case change {caseChange.Case.CaseName}: {string.Join(',', newCaseChange.Issues.Select(x => x.Message))}");
        }
    }

    /// <summary><inheritdoc/></summary>
    protected override async Task SetupPayrunAsync(IExchangeTenant tenant, IPayrun payrun, IPayrun targetPayrun)
    {
        await base.SetupPayrunAsync(tenant, payrun, targetPayrun);

        // update payrun
        await UpsertObjectAsync(PayrunApiEndpoints.PayrunsUrl(tenant.Id), payrun, targetPayrun);
    }

    private async Task ChangeJobStatusAsync(int tenantId, int userId, int payrunJobId, PayrunJobStatus jobStatus, bool patchMode) =>
        await new PayrunJobService(HttpClient).ChangeJobStatusAsync(new(tenantId), userId, payrunJobId, jobStatus, patchMode);

    /// <summary><inheritdoc/></summary>
    protected override async Task VisitPayrunJobInvocationAsync(IExchangeTenant tenant, IPayrunJobInvocation invocation)
    {
        await base.VisitPayrunJobInvocationAsync(tenant, invocation);

        // payrun
        var payrun = await GetPayrunAsync(tenant.Id, invocation.PayrunName);
        if (payrun == null)
        {
            throw new PayrollException($"Unknown payrun with name {invocation.PayrunName}");
        }
        invocation.PayrunId = payrun.Id;

        // payroll
        var payroll = await GetPayrollAsync(tenant.Id, invocation.PayrollName);
        invocation.PayrollId = payroll.Id;

        // user
        var user = await GetUserAsync(tenant.Id, invocation.UserIdentifier);
        if (user == null)
        {
            throw new PayrollException($"Unknown user with identifier {invocation.UserIdentifier}");
        }
        invocation.UserId = user.Id;

        // create new payrun job (POST only)
        using var response = await HttpClient.PostAsync(PayrunApiEndpoints.PayrunJobsUrl(tenant.Id),
            DefaultJsonSerializer.SerializeJson(invocation));
        var payrunJobId = PayrollHttpClient.GetRecordId(response);
        if (payrunJobId <= 0)
        {
            throw new PayrollException($"Error while creating the payrun job {invocation.Name}");
        }
        invocation.PayrunJobId = payrunJobId;

        var payrunJob = await GetPayrunJobAsync(tenant.Id, payrunJobId);
        if (payrunJob == null)
        {
            throw new PayrollException($"Missing payrun job with id {payrunJobId}");
        }

        // payrun job status
        if (payrunJob.JobStatus != PayrunJobStatus.Abort &&
            invocation.JobStatus != PayrunJobStatus.Draft)
        {
            await ChangeJobStatusAsync(tenant.Id, user.Id, payrunJobId, invocation.JobStatus, true);
        }
    }
}