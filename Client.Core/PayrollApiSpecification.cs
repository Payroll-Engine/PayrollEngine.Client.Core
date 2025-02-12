using System;

namespace PayrollEngine.Client
{
    /// <summary>
    /// The Payroll Api specification
    /// </summary>
    public static class PayrollApiSpecification
    {
        /// <summary>Setting name for the payroll backend url</summary>
        public static readonly string BackendUrlSetting = "BackendUrl";

        /// <summary>Setting name for the payroll backend port</summary>
        public static readonly string BackendPortSetting = "BackendPort";

        /// <summary>Setting name for the payroll api key</summary>
        public static readonly string BackendApiKey = "BackendApiKey";

        /// <summary>API key header</summary>
        /// <remarks>see backend specification</remarks>
        public static readonly string ApiKeyHeader = "Api-Key";

        /// <summary>Tenant authorization header</summary>
        /// <remarks>see backend specification</remarks>
        public static readonly string TenantAuthorizationHeader = "Auth-Tenant";

        /// <summary>File name containing the api settings</summary>
        public static readonly string ApiSettingsFileName = "apisettings.json";

        /// <summary>
        /// The current API version
        /// <remarks>Update backend version changes here</remarks>
        /// </summary>
        public static Version CurrentApiVersion => new(1, 0);
    }
}
