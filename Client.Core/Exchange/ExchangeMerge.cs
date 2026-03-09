using System;
using System.Linq;
using Task = System.Threading.Tasks.Task;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Exchange;

/// <summary>Payroll exchange merge</summary>
public class ExchangeMerge : Visitor
{
    /// <summary>
    /// Target exchange model
    /// </summary>
    public Client.Model.Exchange Target { get; set; }

    /// <summary>Initializes a new instance of the <see cref="NamespaceUpdateTool"/> class</summary>
    /// <remarks>Merge source exchange</remarks>
    public ExchangeMerge(Client.Model.Exchange source) :
        base(source)
    {
    }

    /// <summary>Merge source exchange to target exchange</summary>
    /// <param name="target">Merge target exchange</param>
    public async Task MergeToAsync(Client.Model.Exchange target)
    {
        if (Target != null)
        {
            throw new PayrollException("Merge source is already set. Nested merge is not supported.");
        }

        try
        {
            Target = target;

            // schema must be unique
            if (!string.IsNullOrWhiteSpace(Exchange.Schema))
            {
                if (!string.IsNullOrWhiteSpace(Target.Schema) &&
                    !string.Equals(Target.Schema, Exchange.Schema, StringComparison.OrdinalIgnoreCase))
                {
                    throw new PayrollException("Multiple schemas are not supported.");
                }
                if (string.IsNullOrWhiteSpace(Target.Schema))
                {
                    Target.Schema = Exchange.Schema;
                }
            }

            // oldest object created date 
            if (Exchange.CreatedObjectDate != null)
            {
                if (Target.CreatedObjectDate == null ||
                    Exchange.CreatedObjectDate < Target.CreatedObjectDate)
                {
                    Target.CreatedObjectDate = Exchange.CreatedObjectDate;
                }
            }

            await VisitAsync();
        }
        finally
        {
            Target = null;
        }
    }

    /// <inheritdoc />
    protected override async Task VisitExchangeTenantAsync(IExchangeTenant tenant)
    {
        Target.Tenants ??= [];
        if (!Target.Tenants.Any(x => string.Equals(x.Identifier, tenant.Identifier)))
        {
            Target.Tenants.Add(tenant as ExchangeTenant);
        }
        await base.VisitExchangeTenantAsync(tenant);
    }

    /// <inheritdoc />
    protected override async Task VisitRegulationShareAsync(IRegulationShare share)
    {
        if (!Target.RegulationShares.Any(x =>
                string.Equals(x.ProviderTenantIdentifier, share.ProviderTenantIdentifier, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(x.ProviderRegulationName, share.ProviderRegulationName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(x.ConsumerTenantIdentifier, share.ConsumerTenantIdentifier, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(x.ConsumerDivisionName, share.ConsumerDivisionName, StringComparison.OrdinalIgnoreCase)))
        {
            Target.RegulationShares.Add(share as RegulationShare);
        }
        await base.VisitRegulationShareAsync(share);
    }

    /// <inheritdoc />
    protected override async Task VisitUserAsync(IExchangeTenant tenant, IUser user)
    {
        var targetTenant = GetTargetTenant(tenant);
        targetTenant.Users ??= [];

        if (!targetTenant.Users.Any(x => string.Equals(x.Identifier, tenant.Identifier)))
        {
            targetTenant.Users.Add(user as User);
        }
        await base.VisitUserAsync(tenant, user);
    }

    /// <inheritdoc />
    protected override async Task VisitCalendarAsync(IExchangeTenant tenant, ICalendar calendar)
    {
        var targetTenant = GetTargetTenant(tenant);
        targetTenant.Calendars ??= [];

        if (!targetTenant.Calendars.Any(x => string.Equals(x.Name, calendar.Name)))
        {
            targetTenant.Calendars.Add(calendar as Calendar);
        }
        await base.VisitCalendarAsync(tenant, calendar);
    }

    /// <inheritdoc />
    protected override async Task VisitDivisionAsync(IExchangeTenant tenant, IDivision division)
    {
        var targetTenant = GetTargetTenant(tenant);
        targetTenant.Divisions ??= [];

        if (!targetTenant.Divisions.Any(x => string.Equals(x.Name, division.Name)))
        {
            targetTenant.Divisions.Add(division as Division);
        }
        await base.VisitDivisionAsync(tenant, division);
    }

    /// <inheritdoc />
    protected override async Task VisitTaskAsync(IExchangeTenant tenant, ITask task)
    {
        var targetTenant = GetTargetTenant(tenant);
        targetTenant.Tasks ??= [];

        if (!targetTenant.Tasks.Any(x => string.Equals(x.Name, task.Name)))
        {
            targetTenant.Tasks.Add(task as Client.Model.Task);
        }
        await base.VisitTaskAsync(tenant, task);
    }

    /// <inheritdoc />
    protected override async Task VisitWebhookAsync(IExchangeTenant tenant, IWebhookSet webhook)
    {
        var targetTenant = GetTargetTenant(tenant);
        targetTenant.Webhooks ??= [];

        if (!targetTenant.Webhooks.Any(x => string.Equals(x.Name, webhook.Name)))
        {
            targetTenant.Webhooks.Add(webhook as WebhookSet);
        }
        await base.VisitWebhookAsync(tenant, webhook);
    }

    /// <inheritdoc />
    protected override async Task VisitRegulationAsync(IExchangeTenant tenant, IRegulationSet regulation)
    {
        var targetTenant = GetTargetTenant(tenant);
        targetTenant.Regulations ??= [];

        if (!targetTenant.Regulations.Any(x => string.Equals(x.Name, regulation.Name)))
        {
            targetTenant.Regulations.Add(regulation as RegulationSet);
        }
        await base.VisitRegulationAsync(tenant, regulation);
    }

    /// <inheritdoc />
    protected override async Task VisitLookupAsync(IExchangeTenant tenant, IRegulationSet regulation, ILookupSet lookup)
    {
        var targetRegulation = GetTargetRegulation(tenant, regulation);
        targetRegulation.Lookups ??= [];

        if (!targetRegulation.Lookups.Any(x => string.Equals(x.Name, lookup.Name)))
        {
            targetRegulation.Lookups.Add(lookup as LookupSet);
        }
        await base.VisitLookupAsync(tenant, regulation, lookup);
    }

    /// <inheritdoc />
    protected override async Task VisitLookupValueAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ILookupSet lookup, ILookupValue lookupValue)
    {
        var targetRegulation = GetTargetRegulation(tenant, regulation);
        var targetLookup = targetRegulation.Lookups.First(x => string.Equals(x.Name, lookup.Name));
        targetLookup.Values ??= [];

        if (!targetLookup.Values.Any(x => string.Equals(x.Key, lookupValue.Key) && string.Equals(x.Value, lookupValue.Value)))
        {
            targetLookup.Values.Add(lookupValue as LookupValue);
        }
        await base.VisitLookupValueAsync(tenant, regulation, lookup, lookupValue);
    }

    /// <inheritdoc />
    protected override async Task VisitCaseAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICaseSet caseSet)
    {
        var targetRegulation = GetTargetRegulation(tenant, regulation);
        targetRegulation.Cases ??= [];

        if (!targetRegulation.Cases.Any(x => string.Equals(x.Name, caseSet.Name)))
        {
            targetRegulation.Cases.Add(caseSet as CaseSet);
        }
        await base.VisitCaseAsync(tenant, regulation, caseSet);
    }

    /// <inheritdoc />
    protected override async Task VisitCaseFieldAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICaseSet caseSet, ICaseField caseField)
    {
        var targetRegulation = GetTargetRegulation(tenant, regulation);
        var targetCase = targetRegulation.Cases.First(x => string.Equals(x.Name, caseSet.Name));
        targetCase.Fields ??= [];

        if (!targetCase.Fields.Any(x => string.Equals(x.Name, caseField.Name)))
        {
            targetCase.Fields.Add(caseField as CaseFieldSet);
        }
        await base.VisitCaseFieldAsync(tenant, regulation, caseSet, caseField);
    }

    /// <inheritdoc />
    protected override async Task VisitCaseRelationAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICaseRelation caseRelation)
    {
        var targetRegulation = GetTargetRegulation(tenant, regulation);
        targetRegulation.CaseRelations ??= [];

        if (!targetRegulation.CaseRelations.Any(x => string.Equals(x.SourceCaseName, caseRelation.SourceCaseName) &&
                                                     string.Equals(x.SourceCaseSlot, caseRelation.SourceCaseSlot) &&
                                                     string.Equals(x.TargetCaseName, caseRelation.TargetCaseName) &&
                                                     string.Equals(x.TargetCaseSlot, caseRelation.TargetCaseSlot)))
        {
            targetRegulation.CaseRelations.Add(caseRelation as CaseRelation);
        }
        await base.VisitCaseRelationAsync(tenant, regulation, caseRelation);
    }

    /// <inheritdoc />
    protected override async Task VisitCollectorAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICollector collector)
    {
        var targetRegulation = GetTargetRegulation(tenant, regulation);
        targetRegulation.Collectors ??= [];

        if (!targetRegulation.Collectors.Any(x => string.Equals(x.Name, collector.Name)))
        {
            targetRegulation.Collectors.Add(collector as Collector);
        }
        await base.VisitCollectorAsync(tenant, regulation, collector);
    }

    /// <inheritdoc />
    protected override async Task VisitWageTypeAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IWageType wageType)
    {
        var targetRegulation = GetTargetRegulation(tenant, regulation);
        targetRegulation.WageTypes ??= [];

        if (targetRegulation.WageTypes.All(x => x.WageTypeNumber != wageType.WageTypeNumber))
        {
            targetRegulation.WageTypes.Add(wageType as WageType);
        }
        await base.VisitWageTypeAsync(tenant, regulation, wageType);
    }

    /// <inheritdoc />
    protected override async Task VisitScriptAsync(IExchangeTenant tenant, IRegulationSet regulation, IScript script)
    {
        var targetRegulation = GetTargetRegulation(tenant, regulation);
        targetRegulation.Scripts ??= [];

        if (!targetRegulation.Scripts.Any(x => string.Equals(x.Name, script.Name)))
        {
            targetRegulation.Scripts.Add(script as Client.Model.Script);
        }
        await base.VisitScriptAsync(tenant, regulation, script);
    }

    /// <inheritdoc />
    protected override async Task VisitReportAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report)
    {
        var targetRegulation = GetTargetRegulation(tenant, regulation);
        targetRegulation.Reports ??= [];

        if (!targetRegulation.Reports.Any(x => string.Equals(x.Name, report.Name)))
        {
            targetRegulation.Reports.Add(report as ReportSet);
        }
        await base.VisitReportAsync(tenant, regulation, report);
    }

    /// <inheritdoc />
    protected override async Task VisitReportParameterAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report, IReportParameter parameter)
    {
        var targetRegulation = GetTargetRegulation(tenant, regulation);
        var targetReport = targetRegulation.Reports.First(x => string.Equals(x.Name, report.Name));
        targetReport.Parameters ??= [];

        if (!targetReport.Parameters.Any(x => string.Equals(x.Name, parameter.Name)))
        {
            targetReport.Parameters.Add(parameter as ReportParameter);
        }
        await base.VisitReportParameterAsync(tenant, regulation, report, parameter);
    }

    /// <inheritdoc />
    protected override async Task VisitReportTemplateAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report, IReportTemplate template)
    {
        var targetRegulation = GetTargetRegulation(tenant, regulation);
        var targetReport = targetRegulation.Reports.First(x => string.Equals(x.Name, report.Name));
        targetReport.Templates ??= [];

        if (!targetReport.Templates.Any(x => string.Equals(x.Name, template.Name)))
        {
            targetReport.Templates.Add(template as ReportTemplate);
        }
        await base.VisitReportTemplateAsync(tenant, regulation, report, template);
    }

    /// <inheritdoc />
    protected override async Task VisitEmployeeAsync(IExchangeTenant tenant, IEmployeeSet employee)
    {
        var targetTenant = GetTargetTenant(tenant);
        targetTenant.Employees ??= [];

        if (!targetTenant.Employees.Any(x => string.Equals(x.Identifier, employee.Identifier)))
        {
            targetTenant.Employees.Add(employee as EmployeeSet);
        }
        await base.VisitEmployeeAsync(tenant, employee);
    }

    /// <inheritdoc />
    protected override async Task VisitPayrollAsync(IExchangeTenant tenant, IPayrollSet payroll)
    {
        var targetTenant = GetTargetTenant(tenant);
        targetTenant.Payrolls ??= [];

        if (!targetTenant.Payrolls.Any(x => string.Equals(x.Name, payroll.Name)))
        {
            targetTenant.Payrolls.Add(payroll as PayrollSet);
        }
        await base.VisitPayrollAsync(tenant, payroll);
    }

    /// <inheritdoc />
    protected override async Task VisitPayrollLayerAsync(IExchangeTenant tenant, IPayrollSet payroll, IPayrollLayer layer)
    {
        var targetPayroll = GetTargetPayroll(tenant, payroll);
        targetPayroll.Layers ??= [];

        if (!targetPayroll.Layers.Any(x => x.Level == layer.Level && x.Priority == layer.Priority))
        {
            targetPayroll.Layers.Add(layer as PayrollLayer);
        }
        await base.VisitPayrollLayerAsync(tenant, payroll, layer);
    }

    /// <inheritdoc />
    protected override async Task VisitCaseChangeSetupAsync(IExchangeTenant tenant, IPayrollSet payroll,
        ICaseChangeSetup caseChangeSetup)
    {
        var targetPayroll = GetTargetPayroll(tenant, payroll);
        targetPayroll.Cases ??= [];

        // compare instance and not values
        if (!targetPayroll.Cases.Contains(caseChangeSetup))
        {
            targetPayroll.Cases.Add(caseChangeSetup as CaseChangeSetup);
        }
        await base.VisitCaseChangeSetupAsync(tenant, payroll, caseChangeSetup);
    }

    /// <inheritdoc />
    protected override async Task VisitPayrunAsync(IExchangeTenant tenant, IPayrun payrun)
    {
        var targetTenant = GetTargetTenant(tenant);
        targetTenant.Payruns ??= [];

        if (!targetTenant.Payruns.Any(x => string.Equals(x.Name, payrun.Name)))
        {
            targetTenant.Payruns.Add(payrun as Payrun);
        }
        await base.VisitPayrunAsync(tenant, payrun);
    }

    /// <inheritdoc />
    protected override async Task VisitPayrunJobAsync(IExchangeTenant tenant, IPayrunJob payrunJob)
    {
        var targetTenant = GetTargetTenant(tenant);
        targetTenant.PayrunJobs ??= [];

        var payrun = targetTenant.Payruns.First(x => string.Equals(x.Name, payrunJob.PayrunName));
        if (!targetTenant.PayrunJobs.Any(x => string.Equals(x.Name, payrun.Name) &&
                                              x.PayrunName == payrun.Name))
        {
            targetTenant.PayrunJobs.Add(payrunJob as PayrunJob);
        }
        await base.VisitPayrunJobAsync(tenant, payrunJob);
    }

    /// <inheritdoc />
    protected override async Task VisitPayrunJobInvocationAsync(IExchangeTenant tenant, IPayrunJobInvocation invocation)
    {
        var targetTenant = GetTargetTenant(tenant);
        targetTenant.PayrunJobInvocations ??= [];

        var targetInvocation = targetTenant.PayrunJobInvocations.FirstOrDefault(x =>
            string.Equals(x.Name, invocation.Name) &&
            string.Equals(x.PayrunName, invocation.PayrunName));
        if (targetInvocation == null)
        {
            targetTenant.PayrunJobInvocations.Add(invocation as PayrunJobInvocation);
        }
        else if (invocation.EmployeeIdentifiers != null && invocation.EmployeeIdentifiers.Any())
        {
            targetInvocation.EmployeeIdentifiers ??= [];
            targetInvocation.EmployeeIdentifiers.AddRange(
                invocation.EmployeeIdentifiers.Where(id => !targetInvocation.EmployeeIdentifiers.Contains(id)));
        }

        await base.VisitPayrunJobInvocationAsync(tenant, invocation);
    }

    /// <inheritdoc />
    protected override async Task VisitPayrollResultAsync(IExchangeTenant tenant, IPayrollResultSet payrollResult)
    {
        var targetTenant = GetTargetTenant(tenant);
        targetTenant.PayrollResults ??= [];
        if (!targetTenant.PayrollResults.Any(x => string.Equals(x.PayrunJobName, payrollResult.PayrunJobName) &&
                                                  string.Equals(x.EmployeeIdentifier, payrollResult.EmployeeIdentifier) &&
                                                  x.RetroPeriodStart == payrollResult.RetroPeriodStart))
        {
            targetTenant.PayrollResults.Add(payrollResult as PayrollResultSet);
        }
        await base.VisitPayrollResultAsync(tenant, payrollResult);
    }

    private PayrollSet GetTargetPayroll(IExchangeTenant tenant, IPayrollSet payroll)
    {
        var targetTenant = GetTargetTenant(tenant);
        return targetTenant.Payrolls.First(x => string.Equals(x.Name, payroll.Name));
    }

    private RegulationSet GetTargetRegulation(IExchangeTenant tenant, IRegulationSet regulation)
    {
        var targetTenant = GetTargetTenant(tenant);
        return targetTenant.Regulations.First(x => string.Equals(x.Name, regulation.Name));
    }

    private ExchangeTenant GetTargetTenant(IExchangeTenant tenant) =>
        Target.Tenants.First(x => string.Equals(x.Identifier, tenant.Identifier));
}