namespace PayrollEngine.Client;

/// <summary>Payrun and payrun job api endpoints</summary>
public static class PayrunApiEndpoints
{
    /// <summary>The payruns URL</summary>
    public static string PayrunsUrl(int tenantId) =>
        $"{TenantApiEndpoints.TenantUrl(tenantId)}/payruns";

    /// <summary>The payrun URL</summary>
    public static string PayrunUrl(int tenantId, int payrunId) =>
        $"{PayrunsUrl(tenantId)}/{payrunId}";

    /// <summary>The payrun rebuild URL</summary>
    public static string PayrunRebuildUrl(int tenantId, int payrunId) =>
        $"{PayrunUrl(tenantId, payrunId)}/rebuild";

    /// <summary>The payrun parameters URL</summary>
    public static string PayrunParametersUrl(int tenantId, int payrunId) =>
        $"{PayrunUrl(tenantId, payrunId)}/parameters";

    /// <summary>The payrun parameter URL</summary>
    public static string PayrunParameterUrl(int tenantId, int payrunId, int payrunParameterId) =>
        $"{PayrunUrl(tenantId, payrunId)}/{payrunParameterId}";

    /// <summary>The payrun jobs URL</summary>
    public static string PayrunJobsUrl(int tenantId) =>
        $"{PayrunsUrl(tenantId)}/jobs";

    /// <summary>The payrun job employees URL</summary>
    public static string PayrunJobEmployeesUrl(int tenantId) =>
        $"{PayrunJobsUrl(tenantId)}/employees";

    /// <summary>The payrun job employee URL</summary>
    public static string PayrunJobEmployeeUrl(int tenantId, int employeeId) =>
        $"{PayrunJobEmployeesUrl(tenantId)}/{employeeId}";

    /// <summary>The payrun job URL</summary>
    public static string PayrunJobUrl(int tenantId, int payrunJobId) =>
        $"{PayrunJobsUrl(tenantId)}/{payrunJobId}";

    /// <summary>The payrun job attributes URL</summary>
    public static string PayrunJobAttributesUrl(int tenantId, int payrunJobId) =>
        $"{PayrunJobUrl(tenantId, payrunJobId)}/{ApiEndpoints.AttributesPath()}";

    /// <summary>The payrun job attribute URL</summary>
    public static string PayrunJobAttributeUrl(int tenantId, int payrunJobId, string attributeName) =>
        $"{PayrunJobAttributesUrl(tenantId, payrunJobId)}/{attributeName}";

    /// <summary>The payrun job status URL</summary>
    public static string PayrunJobStatusUrl(int tenantId, int payrunJobId) =>
        $"{PayrunJobUrl(tenantId, payrunJobId)}/status";
}