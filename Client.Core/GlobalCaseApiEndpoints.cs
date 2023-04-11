namespace PayrollEngine.Client;

/// <summary>Global case api endpoints</summary>
public static class GlobalCaseApiEndpoints
{
    /// <summary>The global cases URL</summary>
    public static string GlobalCasesUrl(int tenantId) =>
        $"{TenantApiEndpoints.TenantUrl(tenantId)}/globalcases";

    /// <summary>The global case slots URL</summary>
    public static string GlobalCaseSlotsUrl(int tenantId) =>
        $"{GlobalCasesUrl(tenantId)}/slots";

    /// <summary>The global case URL</summary>
    public static string GlobalCaseUrl(int tenantId, int globalCaseId) =>
        $"{GlobalCasesUrl(tenantId)}/{globalCaseId}";

    /// <summary>The global case values URL</summary>
    public static string GlobalCaseValuesUrl(int tenantId) =>
        $"{GlobalCasesUrl(tenantId)}/values";

    /// <summary>The global case documents URL</summary>
    public static string GlobalCaseDocumentsUrl(int tenantId, int caseValueId) =>
        $"{GlobalCasesUrl(tenantId)}/{caseValueId}/documents";

    /// <summary>The global case document URL</summary>
    public static string GlobalCaseDocumentUrl(int tenantId, int caseValueId, int documentId) =>
        $"{GlobalCaseDocumentsUrl(tenantId, caseValueId)}/{documentId}";

    /// <summary>The global case changes URL</summary>
    public static string GlobalCaseChangesUrl(int tenantId) =>
        $"{GlobalCasesUrl(tenantId)}/changes";

    /// <summary>The global case changes values URL</summary>
    public static string GlobalCaseChangesValuesUrl(int tenantId) =>
        $"{GlobalCaseChangesUrl(tenantId)}/values";

    /// <summary>The global case change URL</summary>
    public static string GlobalCaseChangeUrl(int tenantId, int globalCaseChangeId) =>
        $"{GlobalCaseChangesUrl(tenantId)}/{globalCaseChangeId}";
}