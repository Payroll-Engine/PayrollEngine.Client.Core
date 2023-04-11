namespace PayrollEngine.Client;

/// <summary>Employee case api endpoints</summary>
public static class EmployeeCaseApiEndpoints
{
    /// <summary>The employees URL</summary>
    public static string EmployeesUrl(int tenantId) =>
        $"{TenantApiEndpoints.TenantUrl(tenantId)}/employees";

    /// <summary>The employee URL</summary>
    public static string EmployeeUrl(int tenantId, int employeeId) =>
        $"{EmployeesUrl(tenantId)}/{employeeId}";

    /// <summary>The employe attributes URL</summary>
    public static string EmployeeAttributesUrl(int tenantId, int employeeId) =>
        $"{EmployeeUrl(tenantId, employeeId)}/{ApiEndpoints.AttributesPath()}";

    /// <summary>The employe attribute URL</summary>
    public static string EmployeeAttributeUrl(int tenantId, int employeeId, string attributeName) =>
        $"{EmployeeAttributesUrl(tenantId, employeeId)}/{attributeName}";

    /// <summary>The employee cases URL</summary>
    public static string EmployeeCasesUrl(int tenantId, int employeeId) =>
        $"{EmployeeUrl(tenantId, employeeId)}/cases";

    /// <summary>The employee case slots URL</summary>
    public static string EmployeeCaseSlotsUrl(int tenantId, int employeeId) =>
        $"{EmployeeCasesUrl(tenantId, employeeId)}/slots";

    /// <summary>The employee case URL</summary>
    public static string EmployeeCaseUrl(int tenantId, int employeeId, int employeeCaseId) =>
        $"{EmployeeCasesUrl(tenantId, employeeId)}/{employeeCaseId}";

    /// <summary>The employee case values URL</summary>
    public static string EmployeeCaseValuesUrl(int tenantId, int employeeId) =>
        $"{EmployeeCasesUrl(tenantId, employeeId)}/values";

    /// <summary>The employee case documents URL</summary>
    public static string EmployeeCaseDocumentsUrl(int tenantId, int employeeId, int caseValueId) =>
        $"{EmployeeCasesUrl(tenantId, employeeId)}/{caseValueId}/documents";

    /// <summary>The employee case document URL</summary>
    public static string EmployeeCaseDocumentUrl(int tenantId, int employeeId, int caseValueId, int documentId) =>
        $"{EmployeeCaseDocumentsUrl(tenantId, employeeId, caseValueId)}/{documentId}";

    /// <summary>The employee case changes URL</summary>
    public static string EmployeeCaseChangesUrl(int tenantId, int employeeId) =>
        $"{EmployeeCasesUrl(tenantId, employeeId)}/changes";

    /// <summary>The employee case changes values URL</summary>
    public static string EmployeeCaseChangesValuesUrl(int tenantId, int employeeId) =>
        $"{EmployeeCaseChangesUrl(tenantId, employeeId)}/values";

    /// <summary>The employee case change URL</summary>
    public static string EmployeeCaseChangeUrl(int tenantId, int employeeId, int employeeCaseChangeId) =>
        $"{EmployeeCaseChangesUrl(tenantId, employeeId)}/{employeeCaseChangeId}";
}