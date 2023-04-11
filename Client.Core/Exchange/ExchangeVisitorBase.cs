using System;
using System.Linq;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Exchange;

/// <summary>Exchange visitor base class</summary>
public abstract class ExchangeVisitorBase
{
    /// <summary>The exchange model</summary>
    public Model.Exchange Exchange { get; }

    /// <summary>Initializes a new instance of the <see cref="ExchangeVisitorBase"/> class</summary>
    /// <remarks>Content is loaded from the working folder</remarks>
    /// <param name="exchange">The exchange model</param>
    protected ExchangeVisitorBase(Model.Exchange exchange)
    {
        Exchange = exchange ?? throw new ArgumentNullException(nameof(exchange));

        var hasTenants = exchange.Tenants == null || exchange.Tenants.Any();
        var hasRegulationPermissions = exchange.RegulationPermissions == null || exchange.RegulationPermissions.Any();
        if (!hasTenants && !hasRegulationPermissions)
        {
            throw new PayrollException("Missing exchange data");
        }
    }

    /// <summary>Visit the exchange model</summary>
    protected virtual async Task VisitAsync()
    {
        await VisitExchangeTenantsAsync();
        await VisitRegulationPermissionsAsync();

        // payrun jobs and payroll results
        if (Exchange.Tenants != null && Exchange.Tenants.Any())
        {
            foreach (var tenant in Exchange.Tenants)
            {
                await VisitPayrunJobsAsync(tenant);
                await VisitPayrollResultsAsync(tenant);
            }
        }
    }

    #region Tenant

    /// <summary>Visit the exchange tenants</summary>
    protected virtual async Task VisitExchangeTenantsAsync()
    {
        if (Exchange.Tenants != null && Exchange.Tenants.Any())
        {
            foreach (var tenant in Exchange.Tenants)
            {
                await VisitExchangeTenantAsync(tenant);
            }
        }
    }

    /// <summary>Visit the exchange tenant</summary>
    /// <param name="tenant">The exchange tenant</param>
    protected virtual async Task VisitExchangeTenantAsync(IExchangeTenant tenant)
    {
        await VisitUsersAsync(tenant);
        await VisitDivisionsAsync(tenant);
        await VisitTasksAsync(tenant);
        await VisitWebhooksAsync(tenant);
        await VisitRegulationsAsync(tenant);
        await VisitEmployeesAsync(tenant);
        await VisitPayrollsAsync(tenant);
        await VisitPayrunsAsync(tenant);
    }

    #endregion

    #region Regulation Permission

    /// <summary>Visit the regulation permissions</summary>
    protected virtual async Task VisitRegulationPermissionsAsync()
    {
        if (Exchange.RegulationPermissions != null && Exchange.RegulationPermissions.Any())
        {
            foreach (var permission in Exchange.RegulationPermissions)
            {
                await VisitRegulationPermissionAsync(permission);
            }
        }
    }

    /// <summary>Visit the regulation permission</summary>
    /// <param name="permission">The regulation permission</param>
    protected virtual async Task VisitRegulationPermissionAsync(IRegulationPermission permission)
    {
        await Task.Run(() => { });
    }

    #endregion

    #region User

    /// <summary>Visit the users</summary>
    /// <param name="tenant">The tenant</param>
    protected virtual async Task VisitUsersAsync(IExchangeTenant tenant)
    {
        if (tenant.Users != null)
        {
            foreach (var user in tenant.Users)
            {
                await VisitUserAsync(tenant, user);
            }
        }
    }

    /// <summary>Visit the user</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="user">The user</param>
    protected virtual async Task VisitUserAsync(IExchangeTenant tenant, IUser user)
    {
        await Task.Run(() => { });
    }

    #endregion

    #region Division

    /// <summary>Visit the divisions</summary>
    /// <param name="tenant">The tenant</param>
    protected virtual async Task VisitDivisionsAsync(IExchangeTenant tenant)
    {
        if (tenant.Divisions != null)
        {
            foreach (var division in tenant.Divisions)
            {
                await VisitDivisionAsync(tenant, division);
            }
        }
    }

    /// <summary>Visit the division</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="division">The division</param>
    protected virtual async Task VisitDivisionAsync(IExchangeTenant tenant, IDivision division)
    {
        await Task.Run(() => { });
    }

    #endregion

    #region Task

    /// <summary>Visit the task</summary>
    /// <param name="tenant">The tenant</param>
    protected virtual async Task VisitTasksAsync(IExchangeTenant tenant)
    {
        if (tenant.Tasks != null)
        {
            foreach (var task in tenant.Tasks)
            {
                await VisitTaskAsync(tenant, task);
            }
        }
    }

    /// <summary>Visit the task</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="task">The task</param>
    protected virtual async Task VisitTaskAsync(IExchangeTenant tenant, ITask task)
    {
        await Task.Run(() => { });
    }

    #endregion

    #region Webhook

    /// <summary>Visit the webhooks</summary>
    /// <param name="tenant">The tenant</param>
    protected virtual async Task VisitWebhooksAsync(IExchangeTenant tenant)
    {
        if (tenant.Webhooks != null)
        {
            foreach (var webhook in tenant.Webhooks)
            {
                await VisitWebhookAsync(tenant, webhook);
            }
        }
    }

    /// <summary>Visit the webhook</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="webhook">The webhook</param>
    protected virtual async Task VisitWebhookAsync(IExchangeTenant tenant, IWebhook webhook)
    {
        await Task.Run(() => { });
    }

    #endregion

    #region Regulation

    /// <summary>Visit the regulation</summary>
    /// <param name="tenant">The tenant</param>
    protected virtual async Task VisitRegulationsAsync(IExchangeTenant tenant)
    {
        if (tenant.Regulations != null)
        {
            foreach (var payroll in tenant.Regulations)
            {
                await VisitRegulationAsync(tenant, payroll);
            }
        }
    }

    /// <summary>Visit the regulation</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    protected virtual async Task VisitRegulationAsync(IExchangeTenant tenant, IRegulationSet regulation)
    {
        // scripts (import before other scripted objects)
        await VisitScriptsAsync(tenant, regulation);
        await VisitCasesAsync(tenant, regulation);
        await VisitCaseRelationsAsync(tenant, regulation);
        await VisitCollectorsAsync(tenant, regulation);
        await VisitWageTypesAsync(tenant, regulation);
        await VisitLookupsAsync(tenant, regulation);
        await VisitReportsAsync(tenant, regulation);
    }

    #endregion

    #region Lookup

    /// <summary>Visit the lookups</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    protected virtual async Task VisitLookupsAsync(IExchangeTenant tenant, IRegulationSet regulation)
    {
        if (regulation.Lookups != null)
        {
            foreach (var lookup in regulation.Lookups)
            {
                await VisitLookupAsync(tenant, regulation, lookup);
            }
        }
    }

    /// <summary>Visit the lookup</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="lookup">The lookup</param>
    protected virtual async Task VisitLookupAsync(IExchangeTenant tenant, IRegulationSet regulation, ILookupSet lookup)
    {
        await VisitLookupValuesAsync(tenant, regulation, lookup);
    }

    /// <summary>Visit the lookup value</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="lookup">The lookup</param>
    protected virtual async Task VisitLookupValuesAsync(IExchangeTenant tenant, IRegulationSet regulation, ILookupSet lookup)
    {
        if (lookup.Values != null)
        {
            foreach (var row in lookup.Values)
            {
                await VisitLookupValueAsync(tenant, regulation, lookup, row);
            }
        }
    }

    /// <summary>Visit the lookup value</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="lookup">The lookup</param>
    /// <param name="lookupValue">The lookup value</param>
    protected virtual async Task VisitLookupValueAsync(IExchangeTenant tenant, IRegulationSet regulation, ILookupSet lookup, ILookupValue lookupValue)
    {
        await Task.Run(() => { });
    }

    #endregion

    #region Case and Case Field

    /// <summary>Visit the cases</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    protected virtual async Task VisitCasesAsync(IExchangeTenant tenant, IRegulationSet regulation)
    {
        if (regulation.Cases != null)
        {
            foreach (var @case in regulation.Cases)
            {
                await VisitCaseAsync(tenant, regulation, @case);
            }
        }
    }

    /// <summary>Visit the case</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="caseSet">The case</param>
    protected virtual async Task VisitCaseAsync(IExchangeTenant tenant, IRegulationSet regulation, ICaseSet caseSet)
    {
        await VisitCaseFieldsAsync(tenant, regulation, caseSet);
    }

    /// <summary>Visit the case fields</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="caseSet">The case</param>
    protected virtual async Task VisitCaseFieldsAsync(IExchangeTenant tenant, IRegulationSet regulation, ICaseSet caseSet)
    {
        if (caseSet.Fields != null)
        {
            foreach (var caseField in caseSet.Fields)
            {
                await VisitCaseFieldAsync(tenant, regulation, caseSet, caseField);
            }
        }
    }

    /// <summary>Visit the case field</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="caseSet">The case</param>
    /// <param name="caseField">The case field</param>
    protected virtual async Task VisitCaseFieldAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICaseSet caseSet, ICaseField caseField)
    {
        await Task.Run(() => { });
    }

    #endregion

    #region Case Relation

    /// <summary>Visit the case relations</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    protected virtual async Task VisitCaseRelationsAsync(IExchangeTenant tenant, IRegulationSet regulation)
    {
        if (regulation.CaseRelations != null)
        {
            foreach (var caseRelation in regulation.CaseRelations)
            {
                await VisitCaseRelationAsync(tenant, regulation, caseRelation);
            }
        }
    }

    /// <summary>Visit the case relation</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="caseRelation">The case relation</param>
    protected virtual async Task VisitCaseRelationAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICaseRelation caseRelation)
    {
        await Task.Run(() => { });
    }

    #endregion

    #region Collector

    /// <summary>Visit the collectors</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    protected virtual async Task VisitCollectorsAsync(IExchangeTenant tenant, IRegulationSet regulation)
    {
        if (regulation.Collectors != null)
        {
            foreach (var collector in regulation.Collectors)
            {
                await VisitCollectorAsync(tenant, regulation, collector);
            }
        }
    }

    /// <summary>Visit the collector</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="collector">The collector</param>
    protected virtual async Task VisitCollectorAsync(IExchangeTenant tenant, IRegulationSet regulation, ICollector collector)
    {
        await Task.Run(() => { });
    }

    #endregion

    #region Wage Type

    /// <summary>Visit the wage types</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    protected virtual async Task VisitWageTypesAsync(IExchangeTenant tenant, IRegulationSet regulation)
    {
        if (regulation.WageTypes != null)
        {
            foreach (var wageType in regulation.WageTypes)
            {
                await VisitWageTypeAsync(tenant, regulation, wageType);
            }
        }
    }

    /// <summary>Visit the wage type</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="wageType">The wage type</param>
    protected virtual async Task VisitWageTypeAsync(IExchangeTenant tenant, IRegulationSet regulation, IWageType wageType)
    {
        await Task.Run(() => { });
    }

    #endregion

    #region Script

    /// <summary>Visit the scripts</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    protected virtual async Task VisitScriptsAsync(IExchangeTenant tenant, IRegulationSet regulation)
    {
        if (regulation.Scripts != null)
        {
            foreach (var script in regulation.Scripts)
            {
                await VisitScriptAsync(tenant, regulation, script);
            }
        }
    }

    /// <summary>Visit the script</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="script">The script</param>
    protected virtual async Task VisitScriptAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IScript script)
    {
        await Task.Run(() => { });
    }

    #endregion

    #region Report

    /// <summary>Visit the reports</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    protected virtual async Task VisitReportsAsync(IExchangeTenant tenant, IRegulationSet regulation)
    {
        if (regulation.Reports != null)
        {
            foreach (var report in regulation.Reports)
            {
                await VisitReportAsync(tenant, regulation, report);
            }
        }
    }

    /// <summary>Visit the report</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="report">The report</param>
    protected virtual async Task VisitReportAsync(IExchangeTenant tenant, IRegulationSet regulation, IReportSet report)
    {
        await VisitReportParametersAsync(tenant, regulation, report);
        await VisitReportTemplatesAsync(tenant, regulation, report);
    }

    /// <summary>Visit the report parameters</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="report">The report</param>
    protected virtual async Task VisitReportParametersAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report)
    {
        if (report.Parameters != null)
        {
            foreach (var parameter in report.Parameters)
            {
                await VisitReportParameterAsync(tenant, regulation, report, parameter);
            }
        }
    }

    /// <summary>Visit the report parameter</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="report">The report</param>
    /// <param name="parameter">The report parameter</param>
    protected virtual async Task VisitReportParameterAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report, IReportParameter parameter)
    {
        await Task.Run(() => { });
    }

    /// <summary>Visit the report templates</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="report">The report</param>
    protected virtual async Task VisitReportTemplatesAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report)
    {
        if (report.Templates != null)
        {
            foreach (var template in report.Templates)
            {
                await VisitReportTemplateAsync(tenant, regulation, report, template);
            }
        }
    }

    /// <summary>Visit the report template</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="report">The report</param>
    /// <param name="template">The report template</param>
    protected virtual async Task VisitReportTemplateAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report, IReportTemplate template)
    {
        await Task.Run(() => { });
    }

    #endregion

    #region Employee

    /// <summary>Visit the employees</summary>
    /// <param name="tenant">The tenant</param>
    protected virtual async Task VisitEmployeesAsync(IExchangeTenant tenant)
    {
        if (tenant.Employees != null)
        {
            foreach (var employee in tenant.Employees)
            {
                await VisitEmployeeAsync(tenant, employee);
            }
        }
    }

    /// <summary>Visit the employee</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="employee">The employee</param>
    protected virtual async Task VisitEmployeeAsync(IExchangeTenant tenant, IEmployeeSet employee)
    {
        await Task.Run(() => { });
    }

    #endregion

    #region Payroll

    /// <summary>Visit the payrolls</summary>
    /// <param name="tenant">The tenant</param>
    protected virtual async Task VisitPayrollsAsync(IExchangeTenant tenant)
    {
        if (tenant.Payrolls != null)
        {
            foreach (var payroll in tenant.Payrolls)
            {
                await VisitPayrollAsync(tenant, payroll);
            }
        }
    }

    /// <summary>Visit the payroll</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="payroll">The payroll</param>
    protected virtual async Task VisitPayrollAsync(IExchangeTenant tenant, IPayrollSet payroll)
    {
        await VisitPayrollLayersAsync(tenant, payroll);
        await VisitCaseChangeSetupsAsync(tenant, payroll);
    }

    #endregion

    #region Payroll Layer

    /// <summary>Visit the payroll layers</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="payroll">The payroll</param>
    protected virtual async Task VisitPayrollLayersAsync(IExchangeTenant tenant, IPayrollSet payroll)
    {
        if (payroll.Layers != null)
        {
            foreach (var layer in payroll.Layers)
            {
                await VisitPayrollLayerAsync(tenant, payroll, layer);
            }
        }
    }

    /// <summary>Visit the payroll layer</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="payroll">The payroll</param>
    /// <param name="layer">The payroll layer</param>
    protected virtual async Task VisitPayrollLayerAsync(IExchangeTenant tenant, IPayrollSet payroll,
        IPayrollLayer layer)
    {
        await Task.Run(() => { });
    }

    #endregion

    #region Case Change

    /// <summary>Visit the case changes</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="payroll">The payroll</param>
    protected virtual async Task VisitCaseChangeSetupsAsync(IExchangeTenant tenant, IPayrollSet payroll)
    {
        if (payroll.Cases != null)
        {
            foreach (var caseChangeSetup in payroll.Cases)
            {
                await VisitCaseChangeSetupAsync(tenant, payroll, caseChangeSetup);
            }
        }
    }

    /// <summary>Visit the case change</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="payroll">The payroll</param>
    /// <param name="caseChangeSetup">The case change setup</param>
    protected virtual async Task VisitCaseChangeSetupAsync(IExchangeTenant tenant, IPayrollSet payroll,
        ICaseChangeSetup caseChangeSetup)
    {
        // case
        await VisitCaseSetupAsync(tenant, payroll, caseChangeSetup, caseChangeSetup.Case);
    }

    /// <summary>Visit the case change documents</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="payroll">The payroll</param>
    /// <param name="caseChangeSetup">The case change setup</param>
    /// <param name="caseSetup">The case setup</param>
    protected virtual async Task VisitCaseSetupAsync(IExchangeTenant tenant, IPayrollSet payroll,
        ICaseChangeSetup caseChangeSetup, ICaseSetup caseSetup)
    {
        await VisitCaseValuesAsync(tenant, payroll, caseChangeSetup, caseSetup);
        await VisitRelatedCasesAsync(tenant, payroll, caseChangeSetup, caseSetup);
    }

    /// <summary>Visit the related cases</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="payroll">The payroll</param>
    /// <param name="caseChangeSetup">The case change setup</param>
    /// <param name="caseSetup">The case setup</param>
    protected virtual async Task VisitRelatedCasesAsync(IExchangeTenant tenant, IPayrollSet payroll,
        ICaseChangeSetup caseChangeSetup, ICaseSetup caseSetup)
    {
        if (caseSetup.RelatedCases != null && caseSetup.RelatedCases.Any())
        {
            foreach (var relatedCase in caseSetup.RelatedCases)
            {
                // recursive call
                await VisitCaseSetupAsync(tenant, payroll, caseChangeSetup, relatedCase);
            }
        }
    }

    /// <summary>Visit the case values</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="payroll">The payroll</param>
    /// <param name="caseChangeSetup">The case change setup</param>
    /// <param name="caseSetup">The case setup</param>
    protected virtual async Task VisitCaseValuesAsync(IExchangeTenant tenant, IPayrollSet payroll,
        ICaseChangeSetup caseChangeSetup, ICaseSetup caseSetup)
    {
        if (caseSetup.Values != null && caseSetup.Values.Any())
        {
            foreach (var value in caseSetup.Values)
            {
                await VisitCaseValueAsync(tenant, payroll, caseChangeSetup, caseSetup, value);
            }
        }
    }

    /// <summary>Visit the case value</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="payroll">The payroll</param>
    /// <param name="caseChangeSetup">The case change setup</param>
    /// <param name="caseSetup">The case setup</param>
    /// <param name="valueSetup">The case value setup</param>
    protected virtual async Task VisitCaseValueAsync(IExchangeTenant tenant, IPayrollSet payroll,
        ICaseChangeSetup caseChangeSetup, ICaseSetup caseSetup, ICaseValueSetup valueSetup)
    {
        await Task.Run(() => { });
    }

    #endregion

    #region Payrun

    /// <summary>Visit the payruns</summary>
    /// <param name="tenant">The tenant</param>
    protected virtual async Task VisitPayrunsAsync(IExchangeTenant tenant)
    {
        if (tenant.Payruns != null)
        {
            foreach (var payrun in tenant.Payruns)
            {
                await VisitPayrunAsync(tenant, payrun);
            }
        }
    }

    /// <summary>Visit the payrun</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="payrun">The payrun</param>
    protected virtual async Task VisitPayrunAsync(IExchangeTenant tenant, IPayrun payrun)
    {
        await Task.Run(() => { });
    }

    #endregion

    #region PayrunJob

    /// <summary>Visit the payrun jobs</summary>
    /// <param name="tenant">The exchange tenant</param>
    protected virtual async Task VisitPayrunJobsAsync(IExchangeTenant tenant)
    {
        // only payrun job invocations
        await VisitPayrunJobInvocationsAsync(tenant);
    }

    /// <summary>Visit the payrun job invocations</summary>
    /// <param name="tenant">The exchange tenant</param>
    protected virtual async Task VisitPayrunJobInvocationsAsync(IExchangeTenant tenant)
    {
        // payrun job invocations
        if (tenant.PayrunJobInvocations != null)
        {
            foreach (var payrunJobInvocation in tenant.PayrunJobInvocations)
            {
                await VisitPayrunJobInvocationAsync(tenant, payrunJobInvocation);
            }
        }
    }

    /// <summary>Visit the payrun job invocation</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="invocation">The payrun job invocation</param>
    protected virtual async Task VisitPayrunJobInvocationAsync(IExchangeTenant tenant,
        IPayrunJobInvocation invocation)
    {
        await Task.Run(() => { });
    }

    #endregion

    #region Payroll Result

    /// <summary>Visit the payroll results</summary>
    /// <param name="tenant">The exchange tenant</param>
    protected virtual async Task VisitPayrollResultsAsync(IExchangeTenant tenant)
    {
        if (tenant.PayrollResults != null)
        {
            foreach (var payrollResult in tenant.PayrollResults)
            {
                await VisitPayrollResultAsync(tenant, payrollResult);
            }
        }
    }

    /// <summary>Visit the payroll result</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="payrollResult">The payroll result</param>
    protected virtual async Task VisitPayrollResultAsync(IExchangeTenant tenant,
        IPayrollResultSet payrollResult)
    {
        await VisitWageTypeResultsAsync(tenant, payrollResult);
        await VisitCollectorResultsAsync(tenant, payrollResult);
        await VisitPayrunResultsAsync(tenant, payrollResult);
    }

    /// <summary>Visit the wage type results</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="payrollResult">The payroll result</param>
    protected virtual async Task VisitWageTypeResultsAsync(IExchangeTenant tenant,
        IPayrollResultSet payrollResult)
    {
        if (payrollResult.WageTypeResults != null)
        {
            foreach (var wageTypeResult in payrollResult.WageTypeResults)
            {
                await VisitWageTypeResultAsync(tenant, payrollResult, wageTypeResult);
            }
        }
    }

    /// <summary>Visit the wage type result</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="payrollResult">The payroll result</param>
    /// <param name="wageTypeResult">The wage type result</param>
    protected virtual async Task VisitWageTypeResultAsync(IExchangeTenant tenant,
        IPayrollResultSet payrollResult, IWageTypeResultSet wageTypeResult)
    {
        await VisitWageTypeCustomResultsAsync(tenant, payrollResult, wageTypeResult);
    }

    /// <summary>Visit the wage type custom results</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="payrollResult">The payroll result</param>
    /// <param name="wageTypeResult">The wage type result</param>
    protected virtual async Task VisitWageTypeCustomResultsAsync(IExchangeTenant tenant,
        IPayrollResultSet payrollResult, IWageTypeResultSet wageTypeResult)
    {
        if (wageTypeResult.CustomResults != null)
        {
            foreach (var wageTypeCustomResult in wageTypeResult.CustomResults)
            {
                await VisitWageTypeCustomResultAsync(tenant, payrollResult, wageTypeResult, wageTypeCustomResult);
            }
        }
    }

    /// <summary>Visit the wage type custom result</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="payrollResult">The payroll result</param>
    /// <param name="wageTypeResult">The wage type result</param>
    /// <param name="wageTypeCustomResult">The wage type custom result</param>
    protected virtual async Task VisitWageTypeCustomResultAsync(IExchangeTenant tenant,
        IPayrollResultSet payrollResult, IWageTypeResultSet wageTypeResult,
        IWageTypeCustomResult wageTypeCustomResult)
    {
        await Task.Run(() => { });
    }

    /// <summary>Visit the collector results</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="payrollResult">The payroll result</param>
    protected virtual async Task VisitCollectorResultsAsync(IExchangeTenant tenant,
        IPayrollResultSet payrollResult)
    {
        if (payrollResult.CollectorResults != null)
        {
            foreach (var collectorResult in payrollResult.CollectorResults)
            {
                await VisitCollectorResultAsync(tenant, payrollResult, collectorResult);
            }
        }
    }

    /// <summary>Visit the collector result</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="payrollResult">The payroll result</param>
    /// <param name="collectorResult">The collector result</param>
    protected virtual async Task VisitCollectorResultAsync(IExchangeTenant tenant,
        IPayrollResultSet payrollResult, ICollectorResultSet collectorResult)
    {
        await VisitCollectorCustomResultsAsync(tenant, payrollResult, collectorResult);
    }

    /// <summary>Visit the collector custom results</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="payrollResult">The payroll result</param>
    /// <param name="collectorResult">The collector result</param>
    protected virtual async Task VisitCollectorCustomResultsAsync(IExchangeTenant tenant,
        IPayrollResultSet payrollResult, ICollectorResultSet collectorResult)
    {
        if (collectorResult.CustomResults != null)
        {
            foreach (var collectorCustomResult in collectorResult.CustomResults)
            {
                await VisitCollectorCustomResultAsync(tenant, payrollResult, collectorResult, collectorCustomResult);
            }
        }
    }

    /// <summary>Visit the collector custom result</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="payrollResult">The payroll result</param>
    /// <param name="collectorResult">The collector result</param>
    /// <param name="collectorCustomResult">The collector custom result</param>
    protected virtual async Task VisitCollectorCustomResultAsync(IExchangeTenant tenant,
        IPayrollResultSet payrollResult, ICollectorResultSet collectorResult,
        ICollectorCustomResult collectorCustomResult)
    {
        await Task.Run(() => { });
    }

    /// <summary>Visit the payrun results</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="payrollResult">The payroll result</param>
    protected virtual async Task VisitPayrunResultsAsync(IExchangeTenant tenant,
        IPayrollResultSet payrollResult)
    {
        if (payrollResult.PayrunResults != null)
        {
            foreach (var payrunResult in payrollResult.PayrunResults)
            {
                await VisitPayrunResultAsync(tenant, payrollResult, payrunResult);
            }
        }
    }

    /// <summary>Visit the payrun result</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="payrollResult">The payroll result</param>
    /// <param name="payrunResult">The payrun result</param>
    protected virtual async Task VisitPayrunResultAsync(IExchangeTenant tenant,
        IPayrollResultSet payrollResult, IPayrunResult payrunResult)
    {
        await Task.Run(() => { });
    }

    #endregion

}