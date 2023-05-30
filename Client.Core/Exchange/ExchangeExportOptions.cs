using System.Linq;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Exchange;

/// <summary>Exchange export options</summary>
public class ExchangeExportOptions
{
    /// <summary>Export user identifiers</summary>
    [JsonIgnore]
    public bool HasAnyFilter =>
        (Users != null && Users.Any()) ||
        (Divisions != null && Divisions.Any()) ||
        (Employees != null && Employees.Any()) ||
        (Tasks != null && Tasks.Any()) ||
        (Webhooks != null && Webhooks.Any()) ||
        (Regulations != null && Regulations.Any()) ||
        (Payrolls != null && Payrolls.Any()) ||
        (Payruns != null && Payruns.Any()) ||
        (PayrunJobs != null && PayrunJobs.Any());

    /// <summary>Export user identifiers</summary>
    public string[] Users { get; set; }

    /// <summary>Export division names</summary>
    public string[] Divisions { get; set; }

    /// <summary>Export employee identifiers</summary>
    public string[] Employees { get; set; }

    /// <summary>Export task names</summary>
    public string[] Tasks { get; set; }

    /// <summary>Export webhook names</summary>
    public string[] Webhooks { get; set; }

    /// <summary>Export regulation names</summary>
    public string[] Regulations { get; set; }

    /// <summary>Export payroll names</summary>
    public string[] Payrolls { get; set; }

    /// <summary>Export payrun names</summary>
    public string[] Payruns { get; set; }

    /// <summary>Export payrun job names</summary>
    public string[] PayrunJobs { get; set; }

    /// <summary>Export webhook messages </summary>
    public bool ExportWebhookMessages { get; set; }

    /// <summary>Export global case values</summary>
    public bool ExportGlobalCaseValues { get; set; }

    /// <summary>Export national case values</summary>
    public bool ExportNationalCaseValues { get; set; }

    /// <summary>Export company case values</summary>
    public bool ExportCompanyCaseValues { get; set; }

    /// <summary>Export Global case values</summary>
    public bool ExportEmployeeCaseValues { get; set; }

    /// <summary>Export results</summary>
    public bool ExportPayrollResults { get; set; }
}