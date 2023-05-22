
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

    #region Shares

    /// <summary>The shares regulations URL</summary>
    public static string SharesRegulationsUrl() => $"{ApiPath}/shares/regulations";
    /// <summary>The shares regulation URL</summary>
    public static string SharesRegulationUrl(int shareId) =>
        $"{SharesRegulationsUrl()}/{shareId}";

    /// <summary>The shared regulation attributes URL</summary>
    public static string SharesRegulationAttributesUrl(int shareId) =>
        $"{TenantApiEndpoints.TenantUrl(shareId)}/{AttributesPath()}";
    /// <summary>The shared regulation attribute URL</summary>
    public static string SharesRegulationAttributeUrl(int shareId, string attributeName) =>
        $"{SharesRegulationAttributesUrl(shareId)}/{attributeName}";

    #endregion

}