namespace PayrollEngine.Client;

/// <summary>National case api endpoints</summary>
public static class NationalCaseApiEndpoints
{
    /// <summary>The national cases URL</summary>
    public static string NationalCasesUrl(int tenantId) =>
        $"{TenantApiEndpoints.TenantUrl(tenantId)}/nationalcases";

    /// <summary>The national case slots URL</summary>
    public static string NationalCaseSlotsUrl(int tenantId) =>
        $"{NationalCasesUrl(tenantId)}/slots";

    /// <summary>The national case URL</summary>
    public static string NationalCaseUrl(int tenantId, int nationalCaseId) =>
        $"{NationalCasesUrl(tenantId)}/{nationalCaseId}";

    /// <summary>The national case values URL</summary>
    public static string NationalCaseValuesUrl(int tenantId) =>
        $"{NationalCasesUrl(tenantId)}/values";

    /// <summary>The national case documents URL</summary>
    public static string NationalCaseDocumentsUrl(int tenantId, int caseValueId) =>
        $"{NationalCasesUrl(tenantId)}/{caseValueId}/documents";

    /// <summary>The national case document URL</summary>
    public static string NationalCaseDocumentUrl(int tenantId, int caseValueId, int documentId) =>
        $"{NationalCaseDocumentsUrl(tenantId, caseValueId)}/{documentId}";

    /// <summary>The national case changes URL</summary>
    public static string NationalCaseChangesUrl(int tenantId) =>
        $"{NationalCasesUrl(tenantId)}/changes";

    /// <summary>The national case changes values URL</summary>
    public static string NationalCaseChangesValuesUrl(int tenantId) =>
        $"{NationalCaseChangesUrl(tenantId)}/values";

    /// <summary>The national case change URL</summary>
    public static string NationalCaseChangeUrl(int tenantId, int nationalCaseChangeId) =>
        $"{NationalCaseChangesUrl(tenantId)}/{nationalCaseChangeId}";
}