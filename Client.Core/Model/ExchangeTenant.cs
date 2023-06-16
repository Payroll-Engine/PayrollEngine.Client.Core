using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The tenant client object</summary>
public class ExchangeTenant : Tenant, IExchangeTenant
{
    /// <inheritdoc/>
    public List<User> Users { get; set; }

    /// <inheritdoc/>
    public List<Calendar> Calendars { get; set; }

    /// <inheritdoc/>
    public List<Division> Divisions { get; set; }

    /// <inheritdoc/>
    public List<Task> Tasks { get; set; }

    /// <inheritdoc/>
    public List<WebhookSet> Webhooks { get; set; }

    /// <inheritdoc/>
    public List<RegulationSet> Regulations { get; set; }

    /// <inheritdoc/>
    public List<CaseChange> GlobalCases { get; set; }

    /// <inheritdoc/>
    public List<CaseValue> GlobalValues { get; set; }

    /// <inheritdoc/>
    public List<CaseChange> NationalCases { get; set; }

    /// <inheritdoc/>
    public List<CaseValue> NationalValues { get; set; }

    /// <inheritdoc/>
    public List<CaseChange> CompanyCases { get; set; }

    /// <inheritdoc/>
    public List<CaseValue> CompanyValues { get; set; }

    /// <inheritdoc/>
    public List<EmployeeSet> Employees { get; set; }

    /// <inheritdoc/>
    public List<PayrollSet> Payrolls { get; set; }

    /// <inheritdoc/>
    public List<Payrun> Payruns { get; set; }


    /// <inheritdoc/>
    public List<PayrunJob> PayrunJobs { get; set; }

    /// <inheritdoc/>
    public List<PayrunJobInvocation> PayrunJobInvocations { get; set; }

    /// <inheritdoc/>
    public List<PayrollResultSet> PayrollResults { get; set; }

    /// <summary>Initializes a new instance</summary>
    public ExchangeTenant()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public ExchangeTenant(ExchangeTenant copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual void Import(IExchangeTenant source)
    {
        // users
        if (source.Users != null)
        {
            Users ??= new();
            Users.AddRange(source.Users);
        }

        // calendars
        if (source.Calendars != null)
        {
            Calendars ??= new();
            Calendars.AddRange(source.Calendars);
        }

        // divisions
        if (source.Divisions != null)
        {
            Divisions ??= new();
            Divisions.AddRange(source.Divisions);
        }

        // tasks
        if (source.Tasks != null)
        {
            Tasks ??= new();
            Tasks.AddRange(source.Tasks);
        }

        // webhooks
        if (source.Webhooks != null)
        {
            Webhooks ??= new();
            Webhooks.AddRange(source.Webhooks);
        }

        // regulations
        if (source.Regulations != null)
        {
            Regulations ??= new();
            Regulations.AddRange(source.Regulations);
        }

        // global cases
        if (source.GlobalCases != null)
        {
            GlobalCases ??= new();
            GlobalCases.AddRange(source.GlobalCases);
        }

        // global values
        if (source.GlobalValues != null)
        {
            GlobalValues ??= new();
            GlobalValues.AddRange(source.GlobalValues);
        }

        // national cases
        if (source.NationalCases != null)
        {
            NationalCases ??= new();
            NationalCases.AddRange(source.NationalCases);
        }

        // national values
        if (source.NationalValues != null)
        {
            NationalValues ??= new();
            NationalValues.AddRange(source.NationalValues);
        }

        // company cases
        if (source.CompanyCases != null)
        {
            CompanyCases ??= new();
            CompanyCases.AddRange(source.CompanyCases);
        }

        // company values
        if (source.CompanyValues != null)
        {
            CompanyValues ??= new();
            CompanyValues.AddRange(source.CompanyValues);
        }

        // employees
        if (source.Employees != null)
        {
            Employees ??= new();
            Employees.AddRange(source.Employees);
        }

        // payrolls
        if (source.Payrolls != null)
        {
            Payrolls ??= new();
            Payrolls.AddRange(source.Payrolls);
        }

        // payruns
        if (source.Payruns != null)
        {
            Payruns ??= new();
            Payruns.AddRange(source.Payruns);
        }

        // payrun jobs
        if (source.PayrunJobs != null)
        {
            PayrunJobs ??= new();
            PayrunJobs.AddRange(source.PayrunJobs);
        }

        // payrun job invocations
        if (source.PayrunJobInvocations != null)
        {
            PayrunJobInvocations ??= new();
            PayrunJobInvocations.AddRange(source.PayrunJobInvocations);
        }

        // payroll results
        if (source.PayrollResults != null)
        {
            PayrollResults ??= new();
            PayrollResults.AddRange(source.PayrollResults);
        }
    }

    /// <inheritdoc/>
    public virtual bool Equals(IExchangeTenant compare) =>
        CompareTool.EqualProperties(this, compare);
}