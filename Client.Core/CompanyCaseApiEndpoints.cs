namespace PayrollEngine.Client;

/// <summary>Company case api endpoints</summary>
public static class CompanyCaseApiEndpoints
{
    /// <summary>The company cases URL</summary>
    public static string CompanyCasesUrl(int tenantId) =>
        $"{TenantApiEndpoints.TenantUrl(tenantId)}/companycases";

    /// <summary>The company case slots URL</summary>
    public static string CompanyCaseSlotsUrl(int tenantId) =>
        $"{CompanyCasesUrl(tenantId)}/slots";

    /// <summary>The company case URL</summary>
    public static string CompanyCaseUrl(int tenantId, int companyCaseId) =>
        $"{CompanyCasesUrl(tenantId)}/{companyCaseId}";

    /// <summary>The company case values URL</summary>
    public static string CompanyCaseValuesUrl(int tenantId) =>
        $"{CompanyCasesUrl(tenantId)}/values";

    /// <summary>The company case documents URL</summary>
    public static string CompanyCaseDocumentsUrl(int tenantId, int caseValueId) =>
        $"{CompanyCasesUrl(tenantId)}/{caseValueId}/documents";

    /// <summary>The company case document URL</summary>
    public static string CompanyCaseDocumentUrl(int tenantId, int caseValueId, int documentId) =>
        $"{CompanyCaseDocumentsUrl(tenantId, caseValueId)}/{documentId}";

    /// <summary>The company case changes URL</summary>
    public static string CompanyCaseChangesUrl(int tenantId) =>
        $"{CompanyCasesUrl(tenantId)}/changes";

    /// <summary>The company case changes values URL</summary>
    public static string CompanyCaseChangesValuesUrl(int tenantId) =>
        $"{CompanyCaseChangesUrl(tenantId)}/values";

    /// <summary>The company case change URL</summary>
    public static string CompanyCaseChangeUrl(int tenantId, int companyCaseChangeId) =>
        $"{CompanyCaseChangesUrl(tenantId)}/{companyCaseChangeId}";
}