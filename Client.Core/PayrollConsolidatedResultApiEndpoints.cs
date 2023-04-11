namespace PayrollEngine.Client;

/// <summary>Payroll result case api endpoints</summary>
public static class PayrollConsolidatedResultApiEndpoints
{
    /// <summary>The payroll consolidated results URL</summary>
    public static string PayrollConsolidatedResultsUrl(int tenantId) =>
        $"{PayrollResultApiEndpoints.PayrollResultsUrl(tenantId)}/consolidated";

    /// <summary>The payroll consolidated collector results URL</summary>
    public static string PayrollConsolidatedCollectorResultsUrl(int tenantId) =>
        $"{PayrollConsolidatedResultsUrl(tenantId)}/collectors";

    /// <summary>The payroll consolidated wage type results URL</summary>
    public static string PayrollConsolidatedWageTypeResultsUrl(int tenantId) =>
        $"{PayrollConsolidatedResultsUrl(tenantId)}/wagetypes";

    /// <summary>The payroll consolidated payrun results URL</summary>
    public static string PayrollConsolidatedPayrunResultsUrl(int tenantId) =>
        $"{PayrollConsolidatedResultsUrl(tenantId)}/casevalues";
}