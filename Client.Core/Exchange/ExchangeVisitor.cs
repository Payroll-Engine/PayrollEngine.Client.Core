using System;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Exchange;

/// <summary>Exchange visitor tool</summary>
public class ExchangeVisitor : ExchangeVisitorBase
{
    /// <summary>Initializes a new instance of the <see cref="ExchangeVisitor"/> class</summary>
    /// <remarks>Content is loaded from the working folder</remarks>
    /// <param name="exchange">The exchange model</param>
    public ExchangeVisitor(Model.Exchange exchange) :
        base(exchange)
    {
    }

    /// <summary>Execute the visitor</summary>
    public void Execute() =>
        Task.Run(ExecuteAsync).Wait();

    /// <summary>Execute the visitor</summary>
    public virtual async Task ExecuteAsync() =>
        await base.VisitAsync();

    #region Tenant

    /// <summary>Visit the exchange tenant handler</summary>
    public Action<IExchangeTenant> VisitExchangeTenant { get; set; }

    /// <inheritdoc />
    protected override async Task VisitExchangeTenantAsync(IExchangeTenant tenant)
    {
        VisitExchangeTenant?.Invoke(tenant);
        await base.VisitExchangeTenantAsync(tenant);
    }

    #endregion

    #region Regulation Permission

    /// <summary>Visit the regulation permission handler</summary>
    public Action<IRegulationPermission> VisitRegulationPermission { get; set; }

    /// <inheritdoc />
    protected override async Task VisitRegulationPermissionAsync(IRegulationPermission permission)
    {
        VisitRegulationPermission?.Invoke(permission);
        await base.VisitRegulationPermissionAsync(permission);
    }

    #endregion

    #region User

    /// <summary>Visit the user handler</summary>
    public Action<IExchangeTenant, IUser> VisitUser { get; set; }

    /// <inheritdoc />
    protected override async Task VisitUserAsync(IExchangeTenant tenant, IUser user)
    {
        VisitUser?.Invoke(tenant, user);
        await base.VisitUserAsync(tenant, user);
    }

    #endregion

    #region Division

    /// <summary>Visit the division handler</summary>
    public Action<IExchangeTenant, IDivision> VisitDivision { get; set; }

    /// <inheritdoc />
    protected override async Task VisitDivisionAsync(IExchangeTenant tenant, IDivision division)
    {
        VisitDivision?.Invoke(tenant, division);
        await base.VisitDivisionAsync(tenant, division);
    }

    #endregion

    #region Task

    /// <summary>Visit the task handler</summary>
    public Action<IExchangeTenant, ITask> VisitTask { get; set; }

    /// <inheritdoc />
    protected override async Task VisitTaskAsync(IExchangeTenant tenant, ITask task)
    {
        VisitTask?.Invoke(tenant, task);
        await base.VisitTaskAsync(tenant, task);
    }

    #endregion

    #region Webhook

    /// <summary>Visit the task handler</summary>
    public Action<IExchangeTenant, IWebhook> VisitWebhook { get; set; }

    /// <inheritdoc />
    protected override async Task VisitWebhookAsync(IExchangeTenant tenant, IWebhook webhook)
    {
        VisitWebhook?.Invoke(tenant, webhook);
        await base.VisitWebhookAsync(tenant, webhook);
    }

    #endregion

    #region Regulation

    /// <summary>Visit the regulation handler</summary>
    public Action<IExchangeTenant, IRegulationSet> VisitRegulation { get; set; }

    /// <inheritdoc />
    protected override async Task VisitRegulationAsync(IExchangeTenant tenant, IRegulationSet regulation)
    {
        VisitRegulation?.Invoke(tenant, regulation);
        await base.VisitRegulationAsync(tenant, regulation);
    }

    #endregion

    #region Lookup

    /// <summary>Visit the lookup handler</summary>
    public Action<IExchangeTenant, IRegulationSet, ILookupSet> VisitLookup { get; set; }

    /// <inheritdoc />
    protected override async Task VisitLookupAsync(IExchangeTenant tenant, IRegulationSet regulation, ILookupSet lookup)
    {
        VisitLookup?.Invoke(tenant, regulation, lookup);
        await base.VisitLookupAsync(tenant, regulation, lookup);
    }

    /// <summary>Visit the lookup value handler</summary>
    public Action<IExchangeTenant, IRegulationSet, ILookupSet, ILookupValue> VisitLookupValue { get; set; }

    /// <inheritdoc />
    protected override async Task VisitLookupValueAsync(IExchangeTenant tenant, IRegulationSet regulation, ILookupSet lookup, ILookupValue lookupValue)
    {
        VisitLookupValue?.Invoke(tenant, regulation, lookup, lookupValue);
        await base.VisitLookupValueAsync(tenant, regulation, lookup, lookupValue);
    }

    #endregion

    #region Case and Case Field

    /// <summary>Visit the case handler</summary>
    public Action<IExchangeTenant, IRegulationSet, ICaseSet> VisitCase { get; set; }

    /// <inheritdoc />
    protected override async Task VisitCaseAsync(IExchangeTenant tenant, IRegulationSet regulation, ICaseSet caseSet)
    {
        VisitCase?.Invoke(tenant, regulation, caseSet);
        await base.VisitCaseAsync(tenant, regulation, caseSet);
    }

    /// <summary>Visit the case field handler</summary>
    public Action<IExchangeTenant, IRegulationSet, ICaseSet, ICaseField> VisitCaseField { get; set; }

    /// <inheritdoc />
    protected override async Task VisitCaseFieldAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICaseSet caseSet, ICaseField caseField)
    {
        VisitCaseField?.Invoke(tenant, regulation, caseSet, caseField);
        await base.VisitCaseFieldAsync(tenant, regulation, caseSet, caseField);
    }

    #endregion

    #region Case Relation

    /// <summary>Visit the case relation handler</summary>
    public Action<IExchangeTenant, IRegulationSet, ICaseRelation> VisitCaseRelation { get; set; }

    /// <inheritdoc />
    protected override async Task VisitCaseRelationAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICaseRelation caseRelation)
    {
        VisitCaseRelation?.Invoke(tenant, regulation, caseRelation);
        await base.VisitCaseRelationAsync(tenant, regulation, caseRelation);
    }

    #endregion

    #region Collector

    /// <summary>Visit the collector handler</summary>
    public Action<IExchangeTenant, IRegulationSet, ICollector> VisitCollector { get; set; }

    /// <inheritdoc />
    protected override async Task VisitCollectorAsync(IExchangeTenant tenant, IRegulationSet regulation, ICollector collector)
    {
        VisitCollector?.Invoke(tenant, regulation, collector);
        await base.VisitCollectorAsync(tenant, regulation, collector);
    }

    #endregion

    #region Wage Type

    /// <summary>Visit the wage type handler</summary>
    public Action<IExchangeTenant, IRegulationSet, IWageType> VisitWageType { get; set; }

    /// <inheritdoc />
    protected override async Task VisitWageTypeAsync(IExchangeTenant tenant, IRegulationSet regulation, IWageType wageType)
    {
        VisitWageType?.Invoke(tenant, regulation, wageType);
        await base.VisitWageTypeAsync(tenant, regulation, wageType);
    }

    #endregion

    #region Script

    /// <summary>Visit the script handler</summary>
    public Action<IExchangeTenant, IRegulationSet, IScript> VisitScript { get; set; }

    /// <inheritdoc />
    protected override async Task VisitScriptAsync(IExchangeTenant tenant, IRegulationSet regulation, IScript script)
    {
        VisitScript?.Invoke(tenant, regulation, script);
        await base.VisitScriptAsync(tenant, regulation, script);
    }

    #endregion

    #region Report

    /// <summary>Visit the report handler</summary>
    public Action<IExchangeTenant, IRegulationSet, IReportSet> VisitReport { get; set; }

    /// <inheritdoc />
    protected override async Task VisitReportAsync(IExchangeTenant tenant, IRegulationSet regulation, IReportSet report)
    {
        VisitReport?.Invoke(tenant, regulation, report);
        await base.VisitReportAsync(tenant, regulation, report);
    }

    /// <summary>Visit the report parameter handler</summary>
    public Action<IExchangeTenant, IRegulationSet, IReportSet, IReportParameter> VisitReportParameter { get; set; }

    /// <inheritdoc />
    protected override async Task VisitReportParameterAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report, IReportParameter parameter)
    {
        VisitReportParameter?.Invoke(tenant, regulation, report, parameter);
        await base.VisitReportParameterAsync(tenant, regulation, report, parameter);
    }

    /// <summary>Visit the report template handler</summary>
    public Action<IExchangeTenant, IRegulationSet, IReportSet, IReportTemplate> VisitReportTemplate { get; set; }

    /// <inheritdoc />
    protected override async Task VisitReportTemplateAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report, IReportTemplate template)
    {
        VisitReportTemplate?.Invoke(tenant, regulation, report, template);
        await base.VisitReportTemplateAsync(tenant, regulation, report, template);
    }

    #endregion

    #region Employee

    /// <summary>Visit the report template handler</summary>
    public Action<IExchangeTenant, IEmployeeSet> VisitEmployee { get; set; }

    /// <inheritdoc />
    protected override async Task VisitEmployeeAsync(IExchangeTenant tenant, IEmployeeSet employee)
    {
        VisitEmployee?.Invoke(tenant, employee);
        await base.VisitEmployeeAsync(tenant, employee);
    }

    #endregion

    #region Payroll

    /// <summary>Visit the payroll handler</summary>
    public Action<IExchangeTenant, IPayrollSet> VisitPayroll { get; set; }

    /// <inheritdoc />
    protected override async Task VisitPayrollAsync(IExchangeTenant tenant, IPayrollSet payroll)
    {
        VisitPayroll?.Invoke(tenant, payroll);
        await base.VisitPayrollAsync(tenant, payroll);
    }

    #endregion

    #region Payroll Layer

    /// <summary>Visit the payroll layer handler</summary>
    public Action<IExchangeTenant, IPayrollSet, IPayrollLayer> VisitPayrollLayer { get; set; }

    /// <inheritdoc />
    protected override async Task VisitPayrollLayerAsync(IExchangeTenant tenant, IPayrollSet payroll,
        IPayrollLayer layer)
    {
        VisitPayrollLayer?.Invoke(tenant, payroll, layer);
        await base.VisitPayrollLayerAsync(tenant, payroll, layer);
    }

    #endregion

    #region Case Change

    /// <summary>Visit the case change setup handler</summary>
    public Action<IExchangeTenant, IPayrollSet, ICaseChangeSetup> VisitCaseChangeSetup { get; set; }

    /// <inheritdoc />
    protected override async Task VisitCaseChangeSetupAsync(IExchangeTenant tenant, IPayrollSet payroll,
        ICaseChangeSetup caseChangeSetup)
    {
        VisitCaseChangeSetup?.Invoke(tenant, payroll, caseChangeSetup);
        await base.VisitCaseChangeSetupAsync(tenant, payroll, caseChangeSetup);
    }

    /// <summary>Visit the case setup handler</summary>
    public Action<IExchangeTenant, IPayrollSet, ICaseChangeSetup, ICaseSetup> VisitCaseSetup { get; set; }

    /// <inheritdoc />
    protected override async Task VisitCaseSetupAsync(IExchangeTenant tenant, IPayrollSet payroll,
        ICaseChangeSetup caseChangeSetup, ICaseSetup caseSetup)
    {
        VisitCaseSetup?.Invoke(tenant, payroll, caseChangeSetup, caseSetup);
        await base.VisitCaseSetupAsync(tenant, payroll, caseChangeSetup, caseSetup);
    }

    /// <summary>Visit the case value handler</summary>
    public Action<IExchangeTenant, IPayrollSet, ICaseChangeSetup, ICaseSetup, ICaseValueSetup> VisitCaseValue { get; set; }

    /// <inheritdoc />
    protected override async Task VisitCaseValueAsync(IExchangeTenant tenant, IPayrollSet payroll,
        ICaseChangeSetup caseChangeSetup, ICaseSetup caseSetup, ICaseValueSetup valueSetup)
    {
        VisitCaseValue?.Invoke(tenant, payroll, caseChangeSetup, caseSetup, valueSetup);
        await base.VisitCaseValueAsync(tenant, payroll, caseChangeSetup, caseSetup, valueSetup);
    }

    #endregion

    #region Payrun

    /// <summary>Visit the payrun handler</summary>
    public Action<IExchangeTenant, IPayrun> VisitPayrun { get; set; }

    /// <inheritdoc />
    protected override async Task VisitPayrunAsync(IExchangeTenant tenant, IPayrun payrun)
    {
        VisitPayrun?.Invoke(tenant, payrun);
        await base.VisitPayrunAsync(tenant, payrun);
    }

    #endregion

    #region PayrunJob

    /// <summary>Visit the payrun job invocation handler</summary>
    public Action<IExchangeTenant, IPayrunJobInvocation> VisitPayrunJobInvocation { get; set; }

    /// <inheritdoc />
    protected override async Task VisitPayrunJobInvocationAsync(IExchangeTenant tenant, IPayrunJobInvocation invocation)
    {
        VisitPayrunJobInvocation?.Invoke(tenant, invocation);
        await base.VisitPayrunJobInvocationAsync(tenant, invocation);
    }

    #endregion

    #region Payroll Result

    /// <summary>Visit the payroll result handler</summary>
    public Action<IExchangeTenant, IPayrollResultSet> VisitPayrollResult { get; set; }

    /// <inheritdoc />
    protected override async Task VisitPayrollResultAsync(IExchangeTenant tenant, IPayrollResultSet payrollResult)
    {
        VisitPayrollResult?.Invoke(tenant, payrollResult);
        await base.VisitPayrollResultAsync(tenant, payrollResult);
    }

    /// <summary>Visit the wage type result handler</summary>
    public Action<IExchangeTenant, IPayrollResultSet, IWageTypeResultSet> VisitWageTypeResult { get; set; }

    /// <inheritdoc />
    protected override async Task VisitWageTypeResultAsync(IExchangeTenant tenant,
        IPayrollResultSet payrollResult, IWageTypeResultSet wageTypeResult)
    {
        VisitWageTypeResult?.Invoke(tenant, payrollResult, wageTypeResult);
        await base.VisitWageTypeResultAsync(tenant, payrollResult, wageTypeResult);
    }

    /// <summary>Visit the wage type custom result handler</summary>
    public Action<IExchangeTenant, IPayrollResultSet, IWageTypeResultSet, IWageTypeCustomResult> VisitWageTypeCustomResult { get; set; }

    /// <inheritdoc />
    protected override async Task VisitWageTypeCustomResultAsync(IExchangeTenant tenant,
        IPayrollResultSet payrollResult, IWageTypeResultSet wageTypeResult,
        IWageTypeCustomResult wageTypeCustomResult)
    {
        VisitWageTypeCustomResult?.Invoke(tenant, payrollResult, wageTypeResult, wageTypeCustomResult);
        await base.VisitWageTypeCustomResultAsync(tenant, payrollResult, wageTypeResult, wageTypeCustomResult);
    }

    /// <summary>Visit the wage type result handler</summary>
    public Action<IExchangeTenant, IPayrollResultSet, ICollectorResultSet> VisitCollectorResult { get; set; }

    /// <inheritdoc />
    protected override async Task VisitCollectorResultAsync(IExchangeTenant tenant,
        IPayrollResultSet payrollResult, ICollectorResultSet collectorResult)
    {
        VisitCollectorResult?.Invoke(tenant, payrollResult, collectorResult);
        await base.VisitCollectorResultAsync(tenant, payrollResult, collectorResult);
    }

    /// <summary>Visit the wage type custom result handler</summary>
    public Action<IExchangeTenant, IPayrollResultSet, ICollectorResultSet, ICollectorCustomResult> VisitCollectorCustomResult { get; set; }

    /// <inheritdoc />
    protected override async Task VisitCollectorCustomResultAsync(IExchangeTenant tenant,
        IPayrollResultSet payrollResult, ICollectorResultSet collectorResult,
        ICollectorCustomResult collectorCustomResult)
    {
        VisitCollectorCustomResult?.Invoke(tenant, payrollResult, collectorResult, collectorCustomResult);
        await base.VisitCollectorCustomResultAsync(tenant, payrollResult, collectorResult, collectorCustomResult);
    }

    /// <summary>Visit the payrun result handler</summary>
    public Action<IExchangeTenant, IPayrollResultSet, IPayrunResult> VisitPayrunResult { get; set; }

    /// <inheritdoc />
    protected override async Task VisitPayrunResultAsync(IExchangeTenant tenant,
        IPayrollResultSet payrollResult, IPayrunResult payrunResult)
    {
        VisitPayrunResult?.Invoke(tenant, payrollResult, payrunResult);
        await base.VisitPayrunResultAsync(tenant, payrollResult, payrunResult);
    }

    #endregion

}