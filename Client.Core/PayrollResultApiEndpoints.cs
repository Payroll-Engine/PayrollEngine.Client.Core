namespace PayrollEngine.Client;

/// <summary>Payroll result case api endpoints</summary>
public static class PayrollResultApiEndpoints
{
    /// <summary>The payroll results URL</summary>
    public static string PayrollResultsUrl(int tenantId) =>
        $"{TenantApiEndpoints.TenantUrl(tenantId)}/payrollresults";

    /// <summary>The payroll result URL</summary>
    public static string PayrollResultUrl(int tenantId, int payrollResultId) =>
        $"{PayrollResultsUrl(tenantId)}/{payrollResultId}";

    /// <summary>The payroll collector results URL</summary>
    public static string PayrollCollectorsResultsUrl(int tenantId, int payrollResultId) =>
        $"{PayrollResultUrl(tenantId, payrollResultId)}/collectors";

    /// <summary>The payroll collector result URL</summary>
    public static string PayrollCollectorResultUrl(int tenantId, int payrollResultId, int collectorResultId) =>
        $"{PayrollCollectorsResultsUrl(tenantId, payrollResultId)}/{collectorResultId}";

    /// <summary>The payroll collector custom results URL</summary>
    public static string PayrollCollectorCustomResultsUrl(int tenantId, int payrollResultId, int collectorResultId) =>
        $"{PayrollCollectorResultUrl(tenantId, payrollResultId, collectorResultId)}/custom";

    /// <summary>The payroll wage type results URL</summary>
    public static string PayrollWageTypeResultsUrl(int tenantId, int payrollResultId) =>
        $"{PayrollResultUrl(tenantId, payrollResultId)}/wagetypes";

    /// <summary>The payroll wage type result URL</summary>
    public static string PayrollWageTypeResultUrl(int tenantId, int payrollResultId, int wageTypeResultId) =>
        $"{PayrollWageTypeResultsUrl(tenantId, payrollResultId)}/{wageTypeResultId}";

    /// <summary>The payroll wage type custom results URL</summary>
    public static string PayrollWageTypeCustomResultsUrl(int tenantId, int payrollResultId, int wageTypeResultId) =>
        $"{PayrollWageTypeResultUrl(tenantId, payrollResultId, wageTypeResultId)}/custom";

    /// <summary>The payroll payrun results URL</summary>
    public static string PayrollPayrunResultsUrl(int tenantId, int payrollResultId) =>
        $"{PayrollResultUrl(tenantId, payrollResultId)}/payruns";

    /// <summary>The payroll payrun result URL</summary>
    public static string PayrollPayrunResultUrl(int tenantId, int payrollResultId, int payrunResultId) =>
        $"{PayrollPayrunResultsUrl(tenantId, payrollResultId)}/{payrunResultId}";

    /// <summary>The payroll result values URL</summary>
    public static string PayrollResultValuesUrl(int tenantId) =>
        $"{PayrollResultsUrl(tenantId)}/values";

    /// <summary>The payroll result sets URL</summary>
    public static string PayrollResultSetsUrl(int tenantId) =>
        $"{PayrollResultsUrl(tenantId)}/sets";

    /// <summary>The payroll result set URL</summary>
    public static string PayrollResultSetUrl(int tenantId, int resultId) =>
        $"{PayrollResultSetsUrl(tenantId)}/{resultId}";
}