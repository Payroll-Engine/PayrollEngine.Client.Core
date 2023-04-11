using System;
using System.Collections.Generic;
using System.Linq;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Exchange;

/// <summary>Payroll client exchange namespace update</summary>
public class NamespaceUpdateTool : ExchangeVisitor
{
    private string CurrentNamespace { get; set; }
    private string Namespace { get; }

    /// <summary>Initializes a new instance of the <see cref="NamespaceUpdateTool"/> class</summary>
    /// <remarks>Content is loaded from the working folder</remarks>
    /// <param name="exchange">The exchange model</param>
    /// <param name="namespace">The target namespace name</param>
    public NamespaceUpdateTool(Model.Exchange exchange, string @namespace) :
        base(exchange)
    {
        if (string.IsNullOrWhiteSpace(@namespace))
        {
            throw new ArgumentException(nameof(@namespace));
        }
        Namespace = @namespace;
    }

    /// <summary>Update the namespace</summary>
    public void UpdateNamespace() =>
        Task.Run(UpdateNamespaceAsync).Wait();

    /// <summary>Update the namespace</summary>
    public virtual async Task UpdateNamespaceAsync()
    {
        // update source with multiple tenant identifiers
        var tenants = Exchange.Tenants?.GroupBy(x => x.Identifier).ToList();
        if (tenants == null)
        {
            return;
        }
        if (tenants.Count != 1)
        {
            throw new PayrollException("Multiple exchange tenants not supported for namespace change");
        }
        var currentNamespace = tenants.First().Key;

        // keep existing namespace
        if (string.IsNullOrWhiteSpace(currentNamespace) ||
            string.Equals(currentNamespace, Namespace))
        {
            // do not process the base method
            return;
        }

        var previousNamespace = CurrentNamespace;
        try
        {
            CurrentNamespace = currentNamespace;
            await VisitAsync();
        }
        finally
        {
            CurrentNamespace = previousNamespace;
        }
    }

    /// <inheritdoc />
    protected override async Task VisitExchangeTenantAsync(IExchangeTenant tenant)
    {
        tenant.Identifier = ApplyNamespace(tenant.Identifier);
        await base.VisitExchangeTenantAsync(tenant);
    }

    /// <inheritdoc />
    protected override async Task VisitDivisionAsync(IExchangeTenant tenant, IDivision division)
    {
        division.Name = ApplyNamespace(division.Name);
        await base.VisitDivisionAsync(tenant, division);
    }

    /// <inheritdoc />
    protected override async Task VisitEmployeeAsync(IExchangeTenant tenant, IEmployeeSet employee)
    {
        employee.Identifier = ApplyNamespace(employee.Identifier);
        ApplyNamespace(employee.Divisions);
        await base.VisitEmployeeAsync(tenant, employee);
    }

    /// <inheritdoc />
    protected override async Task VisitUserAsync(IExchangeTenant tenant, IUser user)
    {
        user.Identifier = ApplyNamespace(user.Identifier);
        await base.VisitUserAsync(tenant, user);
    }

    /// <inheritdoc />
    protected override async Task VisitCaseAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICaseSet caseSet)
    {
        MapCase(caseSet);
        await base.VisitCaseAsync(tenant, regulation, caseSet);
    }

    /// <inheritdoc />
    protected override async Task VisitCaseChangeSetupAsync(IExchangeTenant tenant, IPayrollSet payroll,
        ICaseChangeSetup caseChangeSetup)
    {
        caseChangeSetup.UserIdentifier = ApplyNamespace(caseChangeSetup.UserIdentifier);
        caseChangeSetup.EmployeeIdentifier = ApplyNamespace(caseChangeSetup.EmployeeIdentifier);
        caseChangeSetup.DivisionName = ApplyNamespace(caseChangeSetup.DivisionName);
        MapCaseSetup(caseChangeSetup.Case);
        await base.VisitCaseChangeSetupAsync(tenant, payroll, caseChangeSetup);
    }

    /// <inheritdoc />
    protected override async Task VisitCaseFieldAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICaseSet caseSet, ICaseField caseField)
    {
        MapCaseField(caseField);
        await base.VisitCaseFieldAsync(tenant, regulation, caseSet, caseField);
    }

    private void MapCaseSetup(CaseSetup caseSetup)
    {
        caseSetup.CaseName = ApplyNamespace(caseSetup.CaseName);

        // values
        if (caseSetup.Values != null && caseSetup.Values.Count > 0)
        {
            foreach (var setup in caseSetup.Values)
            {
                MapCaseValue(setup);
            }
        }

        // related cases
        if (caseSetup.RelatedCases != null && caseSetup.RelatedCases.Count > 0)
        {
            foreach (var relatedCase in caseSetup.RelatedCases)
            {
                // recursive call
                MapCaseSetup(relatedCase);
            }
        }
    }

    private void MapCaseValue(ICaseValueSetup caseValueSetup)
    {
        caseValueSetup.CaseName = ApplyNamespace(caseValueSetup.CaseName);
        caseValueSetup.CaseFieldName = ApplyNamespace(caseValueSetup.CaseFieldName);
    }

    private void MapCase(ICaseSet caseSet)
    {
        caseSet.Name = ApplyNamespace(caseSet.Name);
        ApplyNamespace(caseSet.Clusters);

        // case fields
        if (caseSet.Fields != null && caseSet.Fields.Count > 0)
        {
            foreach (var field in caseSet.Fields)
            {
                MapCaseField(field);
            }
        }

        // related cases
        if (caseSet.RelatedCases != null && caseSet.RelatedCases.Count > 0)
        {
            foreach (var relatedCase in caseSet.RelatedCases)
            {
                // recursive call
                MapCase(relatedCase);
            }
        }
    }

    private void MapCaseField(ICaseField caseField)
    {
        caseField.Name = ApplyNamespace(caseField.Name);
        if (caseField.LookupSettings != null)
        {
            caseField.LookupSettings.LookupName = ApplyNamespace(caseField.LookupSettings.LookupName);
        }
        ApplyNamespace(caseField.Clusters);
    }

    /// <inheritdoc />
    protected override async Task VisitCaseRelationAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICaseRelation caseRelation)
    {
        caseRelation.SourceCaseName = ApplyNamespace(caseRelation.SourceCaseName);
        caseRelation.TargetCaseName = ApplyNamespace(caseRelation.TargetCaseName);
        await base.VisitCaseRelationAsync(tenant, regulation, caseRelation);
    }

    /// <inheritdoc />
    protected override async Task VisitCollectorAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICollector collector)
    {
        collector.Name = ApplyNamespace(collector.Name);
        if (collector.CollectorGroups != null && collector.CollectorGroups.Any())
        {
            ApplyNamespace(collector.CollectorGroups);
        }
        ApplyNamespace(collector.Clusters);
        await base.VisitCollectorAsync(tenant, regulation, collector);
    }

    /// <inheritdoc />
    protected override async Task VisitWageTypeAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IWageType wageType)
    {
        wageType.Name = ApplyNamespace(wageType.Name);
        ApplyNamespace(wageType.Clusters);
        ApplyNamespace(wageType.Collectors);
        ApplyNamespace(wageType.CollectorGroups);
        await base.VisitWageTypeAsync(tenant, regulation, wageType);
    }

    /// <inheritdoc />
    protected override async Task VisitReportAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report)
    {
        report.Name = ApplyNamespace(report.Name);
        ApplyNamespace(report.Clusters);
        await base.VisitReportAsync(tenant, regulation, report);
    }

    /// <inheritdoc />
    protected override async Task VisitReportParameterAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report, IReportParameter parameter)
    {
        parameter.Name = ApplyNamespace(parameter.Name);
        await base.VisitReportParameterAsync(tenant, regulation, report, parameter);
    }

    /// <inheritdoc />
    protected override async Task VisitScriptAsync(IExchangeTenant tenant, IRegulationSet regulation, IScript script)
    {
        script.Name = ApplyNamespace(script.Name);
        await base.VisitScriptAsync(tenant, regulation, script);
    }

    /// <inheritdoc />
    /// <inheritdoc />
    protected override async Task VisitLookupAsync(IExchangeTenant tenant, IRegulationSet regulation, ILookupSet lookup)
    {
        lookup.Name = ApplyNamespace(lookup.Name);
        await base.VisitLookupAsync(tenant, regulation, lookup);
    }

    /// <inheritdoc />
    protected override async Task VisitRegulationAsync(IExchangeTenant tenant, IRegulationSet regulation)
    {
        regulation.Name = ApplyNamespace(regulation.Name);
        await base.VisitRegulationAsync(tenant, regulation);
    }

    /// <inheritdoc />
    protected override async Task VisitPayrollLayerAsync(IExchangeTenant tenant, IPayrollSet payroll, IPayrollLayer layer)
    {
        layer.RegulationName = ApplyNamespace(layer.RegulationName);
        await base.VisitPayrollLayerAsync(tenant, payroll, layer);
    }

    /// <inheritdoc />
    protected override async Task VisitPayrollAsync(IExchangeTenant tenant, IPayrollSet payroll)
    {
        payroll.Name = ApplyNamespace(payroll.Name);
        payroll.DivisionName = ApplyNamespace(payroll.DivisionName);
        await base.VisitPayrollAsync(tenant, payroll);
    }

    /// <inheritdoc />
    protected override async Task VisitPayrunAsync(IExchangeTenant tenant, IPayrun payrun)
    {
        payrun.Name = ApplyNamespace(payrun.Name);
        payrun.PayrollName = ApplyNamespace(payrun.PayrollName);
        await base.VisitPayrunAsync(tenant, payrun);
    }

    /// <inheritdoc />
    protected override async Task VisitPayrunJobInvocationAsync(IExchangeTenant tenant, IPayrunJobInvocation invocation)
    {
        invocation.Name = ApplyNamespace(invocation.Name);
        invocation.PayrunName = ApplyNamespace(invocation.PayrunName);
        invocation.PayrollName = ApplyNamespace(invocation.PayrollName);
        ApplyNamespace(invocation.EmployeeIdentifiers);
        await base.VisitPayrunJobInvocationAsync(tenant, invocation);
    }

    /// <inheritdoc />
    protected override async Task VisitWageTypeResultAsync(IExchangeTenant tenant, IPayrollResultSet payrollResult,
        IWageTypeResultSet wageTypeResult)
    {
        wageTypeResult.WageTypeName = ApplyNamespace(wageTypeResult.WageTypeName);
        await base.VisitWageTypeResultAsync(tenant, payrollResult, wageTypeResult);
    }

    /// <inheritdoc />
    protected override async Task VisitWageTypeCustomResultAsync(IExchangeTenant tenant, IPayrollResultSet payrollResult,
        IWageTypeResultSet wageTypeResult, IWageTypeCustomResult wageTypeCustomResult)
    {
        wageTypeCustomResult.WageTypeName = ApplyNamespace(wageTypeCustomResult.WageTypeName);
        await base.VisitWageTypeCustomResultAsync(tenant, payrollResult, wageTypeResult, wageTypeCustomResult);
    }

    /// <inheritdoc />
    protected override async Task VisitCollectorResultAsync(IExchangeTenant tenant,
        IPayrollResultSet payrollResult, ICollectorResultSet collectorResult)
    {
        collectorResult.CollectorName = ApplyNamespace(collectorResult.CollectorName);
        await base.VisitCollectorResultAsync(tenant, payrollResult, collectorResult);
    }

    /// <inheritdoc />
    protected override async Task VisitCollectorCustomResultAsync(IExchangeTenant tenant, IPayrollResultSet payrollResult,
        ICollectorResultSet collectorResult, ICollectorCustomResult collectorCustomResult)
    {
        collectorCustomResult.CollectorName = ApplyNamespace(collectorCustomResult.CollectorName);
        await base.VisitCollectorCustomResultAsync(tenant, payrollResult, collectorResult, collectorCustomResult);
    }

    /// <inheritdoc />
    protected override async Task VisitPayrunResultAsync(IExchangeTenant tenant,
        IPayrollResultSet payrollResult, IPayrunResult payrunResult)
    {
        payrunResult.Name = ApplyNamespace(payrunResult.Name);
        await base.VisitPayrunResultAsync(tenant, payrollResult, payrunResult);
    }

    /// <inheritdoc />
    protected override async Task VisitTaskAsync(IExchangeTenant tenant, ITask task)
    {
        task.Name = ApplyNamespace(task.Name);
        await base.VisitTaskAsync(tenant, task);
    }

    /// <inheritdoc />
    protected override async Task VisitWebhookAsync(IExchangeTenant tenant, IWebhook webhook)
    {
        webhook.Name = ApplyNamespace(webhook.Name);
        await base.VisitWebhookAsync(tenant, webhook);
    }

    private void ApplyNamespace(IList<string> sources)
    {
        if (sources == null)
        {
            return;
        }
        for (var i = 0; i < sources.Count; i++)
        {
            sources[i] = ApplyNamespace(sources[i]);
        }
    }

    private string ApplyNamespace(string source)
    {
        if (string.IsNullOrWhiteSpace(source) ||
            // missing source match
            !source.StartsWith(CurrentNamespace) ||
            // valid target match
            source.StartsWith(Namespace))
        {
            return source;
        }
        return source.Replace(CurrentNamespace, Namespace);
    }
}