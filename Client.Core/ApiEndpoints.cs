
namespace PayrollEngine.Client;

/// <summary>The backend REST API endpoints</summary>
public static class ApiEndpoints
{

    #region Common

    /// <summary>The api path name</summary>
    internal static readonly string ApiPath = "api";

    /// <summary>The attributes path name</summary>
    public static string AttributesPath() => "attributes";

    #endregion

    #region Admin

    /// <summary>The admin URL</summary>
    public static string AdminUrl() =>
        $"{ApiPath}/admin";

    /// <summary>The admin query methods URL</summary>
    public static string AdminQueryMethods() =>
        $"{AdminUrl()}/querymethods";

    #endregion

    #region Shared

    /// <summary>The shared regulations URL</summary>
    public static string SharedRegulationsUrl() => $"{ApiPath}/shared/regulations";
    /// <summary>The shared regulation permissions URL</summary>
    public static string SharedRegulationPermissionsUrl() =>
        $"{SharedRegulationsUrl()}/permissions";
    /// <summary>The shared regulation permission URL</summary>
    public static string SharedRegulationPermissionUrl(int permissionId) =>
        $"{SharedRegulationPermissionsUrl()}/{permissionId}";

    /// <summary>The shared regulation permission attributes URL</summary>
    public static string SharedRegulationPermissionAttributesUrl(int permissionId) =>
        $"{TenantApiEndpoints.TenantUrl(permissionId)}/{AttributesPath()}";
    /// <summary>The shared regulation permission attribute URL</summary>
    public static string SharedRegulationPermissionAttributeUrl(int permissionId, string attributeName) =>
        $"{SharedRegulationPermissionAttributesUrl(permissionId)}/{attributeName}";

    #endregion

}