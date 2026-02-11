namespace PayrollEngine.Client;

/// <summary>Payroll api endpoints</summary>
public static class PayrollApiEndpoints
{
    /// <summary>The payrolls URL</summary>
    public static string PayrollsUrl(int tenantId) =>
        $"{TenantApiEndpoints.TenantUrl(tenantId)}/payrolls";

    /// <summary>The payroll URL</summary>
    public static string PayrollUrl(int tenantId, int payrollId) =>
        $"{PayrollsUrl(tenantId)}/{payrollId}";

    /// <summary>The payroll attributes URL</summary>
    public static string PayrollAttributesUrl(int tenantId, int payrollId) =>
        $"{PayrollUrl(tenantId, payrollId)}/{ApiEndpoints.AttributesPath()}";

    /// <summary>The payroll attribute URL</summary>
    public static string PayrollAttributeUrl(int tenantId, int payrollId, string attributeName) =>
        $"{PayrollAttributesUrl(tenantId, payrollId)}/{attributeName}";

    /// <summary>The payroll layers URL</summary>
    public static string PayrollLayersUrl(int tenantId, int payrollId) =>
        $"{PayrollsUrl(tenantId)}/{payrollId}/layers";

    /// <summary>The payroll layer URL</summary>
    public static string PayrollLayerUrl(int tenantId, int payrollId, int payrollLayerId) =>
        $"{PayrollLayersUrl(tenantId, payrollId)}/{payrollLayerId}";

    /// <summary>The payroll layer attributes URL</summary>
    public static string PayrollLayerAttributesUrl(int tenantId, int payrollId, int payrollLayerId) =>
        $"{PayrollLayerUrl(tenantId, payrollId, payrollLayerId)}/{ApiEndpoints.AttributesPath()}";

    /// <summary>The payroll layer attribute URL</summary>
    public static string PayrollLayerAttributeUrl(int tenantId, int payrollId, int payrollLayerId, string attributeName) =>
        $"{PayrollLayerAttributesUrl(tenantId, payrollId, payrollLayerId)}/{attributeName}";

    /// <summary>The payroll regulations URL</summary>
    public static string PayrollRegulationsUrl(int tenantId, int payrollId) =>
        $"{PayrollUrl(tenantId, payrollId)}/regulations";

    /// <summary>The payroll cases URL</summary>
    public static string PayrollCasesUrl(int tenantId, int payrollId) =>
        $"{PayrollUrl(tenantId, payrollId)}/cases";

    /// <summary>The payroll available cases URL</summary>
    public static string PayrollCasesAvailableUrl(int tenantId, int payrollId) =>
        $"{PayrollCasesUrl(tenantId, payrollId)}/available";

    /// <summary>The payroll case build URL</summary>
    public static string PayrollCaseBuildUrl(int tenantId, int payrollId, string caseName) =>
        $"{PayrollCasesUrl(tenantId, payrollId)}/build/{caseName}";

    /// <summary>The payroll case change values URL</summary>
    public static string PayrollCaseChangeValuesUrl(int tenantId, int payrollId) =>
        $"{PayrollUrl(tenantId, payrollId)}/changes/values";

    /// <summary>The payroll cases values URL</summary>
    public static string PayrollCasesValuesUrl(int tenantId, int payrollId) =>
        $"{PayrollCasesUrl(tenantId, payrollId)}/values";

    /// <summary>The payroll case value time URL</summary>
    public static string PayrollCaseValuesTimeUrl(int tenantId, int payrollId) =>
        $"{PayrollCasesValuesUrl(tenantId, payrollId)}/time";

    /// <summary>The payroll cases values periods URL</summary>
    public static string PayrollCasesValuesPeriodsUrl(int tenantId, int payrollId) =>
        $"{PayrollCasesValuesUrl(tenantId, payrollId)}/periods";

    /// <summary>The payroll case fields URL</summary>
    public static string PayrollCaseFieldsUrl(int tenantId, int payrollId) =>
        $"{PayrollUrl(tenantId, payrollId)}/casefields";

    /// <summary>The payroll case relations URL</summary>
    public static string PayrollCaseRelationsUrl(int tenantId, int payrollId) =>
        $"{PayrollUrl(tenantId, payrollId)}/caserelations";

    /// <summary>The payroll wage types URL</summary>
    public static string PayrollWageTypesUrl(int tenantId, int payrollId) =>
        $"{PayrollUrl(tenantId, payrollId)}/wagetypes";

    /// <summary>The payroll collectors URL</summary>
    public static string PayrollCollectorsUrl(int tenantId, int payrollId) =>
        $"{PayrollUrl(tenantId, payrollId)}/collectors";

    /// <summary>The payroll lookups URL</summary>
    public static string PayrollLookupsUrl(int tenantId, int payrollId) =>
        $"{PayrollUrl(tenantId, payrollId)}/lookups";

    /// <summary>The payroll lookups data URL</summary>
    public static string PayrollLookupsDataUrl(int tenantId, int payrollId) =>
        $"{PayrollLookupsUrl(tenantId, payrollId)}/data";

    /// <summary>The payroll lookup values URL</summary>
    public static string PayrollLookupValuesUrl(int tenantId, int payrollId) =>
        $"{PayrollLookupsUrl(tenantId, payrollId)}/values";

    /// <summary>The payroll lookup values data URL</summary>
    public static string PayrollLookupValuesDataUrl(int tenantId, int payrollId) =>
        $"{PayrollLookupValuesUrl(tenantId, payrollId)}/data";

    /// <summary>The payroll lookup ranges URL</summary>
    public static string PayrollLookupRangesUrl(int tenantId, int payrollId) =>
        $"{PayrollLookupsUrl(tenantId, payrollId)}/ranges";

    /// <summary>The payroll reports URL</summary>
    public static string PayrollReportsUrl(int tenantId, int payrollId) =>
        $"{PayrollUrl(tenantId, payrollId)}/reports";

    /// <summary>The payroll report parameters URL</summary>
    public static string PayrollReportParametersUrl(int tenantId, int payrollId) =>
        $"{PayrollReportsUrl(tenantId, payrollId)}/parameters";

    /// <summary>The payroll report templates URL</summary>
    public static string PayrollReportTemplatesUrl(int tenantId, int payrollId) =>
        $"{PayrollReportsUrl(tenantId, payrollId)}/templates";

    /// <summary>The payroll scripts URL</summary>
    public static string PayrollScriptsUrl(int tenantId, int payrollId) =>
        $"{PayrollUrl(tenantId, payrollId)}/scripts";

    /// <summary>The payroll actions URL</summary>
    public static string PayrollActionsUrl(int tenantId, int payrollId) =>
        $"{PayrollUrl(tenantId, payrollId)}/actions";
}