using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The tenant client object</summary>
public class ExchangeTenant : Tenant, IExchangeTenant
{
    /// <inheritdoc/>
    [JsonPropertyOrder(200)]
    public List<User> Users { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(201)]
    public List<Calendar> Calendars { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(202)]
    public List<Division> Divisions { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(203)]
    public List<EmployeeSet> Employees { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(204)]
    public List<Task> Tasks { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(205)]
    public List<WebhookSet> Webhooks { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(206)]
    public List<RegulationSet> Regulations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(210)]
    public List<CaseChange> GlobalCases { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(211)]
    public List<CaseValue> GlobalValues { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(212)]
    public List<CaseChange> NationalCases { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(213)]
    public List<CaseValue> NationalValues { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(214)]
    public List<CaseChange> CompanyCases { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(215)]
    public List<CaseValue> CompanyValues { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(300)]
    public List<PayrollSet> Payrolls { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(301)]
    public List<Payrun> Payruns { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(302)]
    public List<PayrunJob> PayrunJobs { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(303)]
    public List<PayrunJobInvocation> PayrunJobInvocations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(304)]
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
            Users ??= [];
            Users.AddRange(source.Users);
        }

        // calendars
        if (source.Calendars != null)
        {
            Calendars ??= [];
            Calendars.AddRange(source.Calendars);
        }

        // divisions
        if (source.Divisions != null)
        {
            Divisions ??= [];
            Divisions.AddRange(source.Divisions);
        }

        // tasks
        if (source.Tasks != null)
        {
            Tasks ??= [];
            Tasks.AddRange(source.Tasks);
        }

        // webhooks
        if (source.Webhooks != null)
        {
            Webhooks ??= [];
            Webhooks.AddRange(source.Webhooks);
        }

        // regulations
        if (source.Regulations != null)
        {
            Regulations ??= [];
            Regulations.AddRange(source.Regulations);
        }

        // global cases
        if (source.GlobalCases != null)
        {
            GlobalCases ??= [];
            GlobalCases.AddRange(source.GlobalCases);
        }

        // global values
        if (source.GlobalValues != null)
        {
            GlobalValues ??= [];
            GlobalValues.AddRange(source.GlobalValues);
        }

        // national cases
        if (source.NationalCases != null)
        {
            NationalCases ??= [];
            NationalCases.AddRange(source.NationalCases);
        }

        // national values
        if (source.NationalValues != null)
        {
            NationalValues ??= [];
            NationalValues.AddRange(source.NationalValues);
        }

        // company cases
        if (source.CompanyCases != null)
        {
            CompanyCases ??= [];
            CompanyCases.AddRange(source.CompanyCases);
        }

        // company values
        if (source.CompanyValues != null)
        {
            CompanyValues ??= [];
            CompanyValues.AddRange(source.CompanyValues);
        }

        // employees
        if (source.Employees != null)
        {
            Employees ??= [];
            Employees.AddRange(source.Employees);
        }

        // payrolls
        if (source.Payrolls != null)
        {
            Payrolls ??= [];
            Payrolls.AddRange(source.Payrolls);
        }

        // payruns
        if (source.Payruns != null)
        {
            Payruns ??= [];
            Payruns.AddRange(source.Payruns);
        }

        // payrun jobs
        if (source.PayrunJobs != null)
        {
            PayrunJobs ??= [];
            PayrunJobs.AddRange(source.PayrunJobs);
        }

        // payrun job invocations
        if (source.PayrunJobInvocations != null)
        {
            PayrunJobInvocations ??= [];
            PayrunJobInvocations.AddRange(source.PayrunJobInvocations);
        }

        // payroll results
        if (source.PayrollResults != null)
        {
            PayrollResults ??= [];
            PayrollResults.AddRange(source.PayrollResults);
        }
    }

    /// <inheritdoc/>
    public virtual bool Equals(IExchangeTenant compare) =>
        CompareTool.EqualProperties(this, compare);
}