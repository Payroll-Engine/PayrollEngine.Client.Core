using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The tenant client object</summary>
// ReSharper disable UnusedMemberInSuper.Global
public interface IExchangeTenant : ITenant, IEquatable<IExchangeTenant>
{
    /// <summary>The tenant users</summary>
    List<User> Users { get; set; }

    /// <summary>The tenant calendars</summary>
    List<Calendar> Calendars { get; set; }

    /// <summary>The tenant divisions</summary>
    List<Division> Divisions { get; set; }

    /// <summary>The tenant tasks</summary>
    List<Task> Tasks { get; set; }

    /// <summary>The tenant webhooks</summary>
    List<WebhookSet> Webhooks { get; set; }

    /// <summary>The tenant regulations</summary>
    List<RegulationSet> Regulations { get; set; }

    /// <summary>The tenant global cases</summary>
    List<CaseChange> GlobalCases { get; set; }

    /// <summary>The tenant global values</summary>
    List<CaseValue> GlobalValues { get; set; }

    /// <summary>The tenant national cases</summary>
    List<CaseChange> NationalCases { get; set; }

    /// <summary>The tenant national values</summary>
    List<CaseValue> NationalValues { get; set; }

    /// <summary>The tenant company cases</summary>
    List<CaseChange> CompanyCases { get; set; }

    /// <summary>The tenant company values</summary>
    List<CaseValue> CompanyValues { get; set; }

    /// <summary>The tenant employees</summary>
    List<EmployeeSet> Employees { get; set; }

    /// <summary>The tenant payrolls</summary>
    List<PayrollSet> Payrolls { get; set; }

    /// <summary>The tenant payruns</summary>
    List<Payrun> Payruns { get; set; }

    /// <summary>The tenant payrun jobs</summary>
    List<PayrunJob> PayrunJobs { get; set; }

    /// <summary>The tenant payrun job invocations</summary>
    List<PayrunJobInvocation> PayrunJobInvocations { get; set; }

    /// <summary>The tenant payroll results</summary>
    List<PayrollResultSet> PayrollResults { get; set; }

    /// <summary>The import exchange tenant</summary>
    void Import(IExchangeTenant source);
}