namespace PayrollEngine.Client;

/// <summary>Tenant api endpoints</summary>
public static class TenantApiEndpoints
{
    /// <summary>The tenants URL</summary>
    public static string TenantsUrl() => $"{ApiEndpoints.ApiPath}/tenants";

    /// <summary>The tenant URL</summary>
    public static string TenantUrl(int tenantId) =>
        $"{TenantsUrl()}/{tenantId}";

    /// <summary>The tenant attributes URL</summary>
    public static string TenantAttributesUrl(int tenantId) =>
        $"{TenantUrl(tenantId)}/{ApiEndpoints.AttributesPath()}";

    /// <summary>The tenant attribute URL</summary>
    public static string TenantAttributeUrl(int tenantId, string attributeName) =>
        $"{TenantAttributesUrl(tenantId)}/{attributeName}";

    /// <summary>The tenant shared regulations URL</summary>
    public static string TenantSharedRegulationsUrl(int tenantId) =>
        $"{TenantUrl(tenantId)}/shared/regulations";

    /// <summary>The tenant system URL</summary>
    public static string TenantSystemUrl(int tenantId) =>
        $"{TenantUrl(tenantId)}/system";

    /// <summary>The tenant actions URL</summary>
    public static string TenantActionsUrl(int tenantId) =>
        $"{TenantSystemUrl(tenantId)}/actions";

    /// <summary>The tenant action properties URL</summary>
    public static string TenantActionPropertiesUrl(int tenantId) =>
        $"{TenantSystemUrl(tenantId)}/properties";

    /// <summary>The tenant queries URL</summary>
    public static string TenantQueriesUrl(int tenantId) =>
        $"{TenantUrl(tenantId)}/queries";

    /// <summary>The calendars URL</summary>
    public static string CalendarsUrl(int tenantId) =>
        $"{TenantUrl(tenantId)}/calendars";

    /// <summary>The calendar URL</summary>
    public static string CalendarUrl(int tenantId, int calendarId) =>
        $"{CalendarsUrl(tenantId)}/{calendarId}";

    /// <summary>The calendars period URL</summary>
    public static string TenantCalendarsPeriodUrl(int tenantId) =>
        $"{CalendarsUrl(tenantId)}/period";

    /// <summary>The calendar cycle URL</summary>
    public static string TenantCalendarsCycleUrl(int tenantId) =>
        $"{CalendarsUrl(tenantId)}/cycle";

    /// <summary>The calendar value URL</summary>
    public static string TenantCalendarsValueUrl(int tenantId) =>
        $"{CalendarsUrl(tenantId)}/value";

    /// <summary>The users URL</summary>
    public static string UsersUrl(int tenantId) =>
        $"{TenantUrl(tenantId)}/users";

    /// <summary>The user URL</summary>
    public static string UserUrl(int tenantId, int userId) =>
        $"{UsersUrl(tenantId)}/{userId}";

    /// <summary>The user password URL</summary>
    public static string UserPasswordUrl(int tenantId, int userId) =>
        $"{UserUrl(tenantId, userId)}/password";

    /// <summary>The user attributes URL</summary>
    public static string UserAttributesUrl(int tenantId, int userId) =>
        $"{UserUrl(tenantId, userId)}/{ApiEndpoints.AttributesPath()}";

    /// <summary>The user attribute URL</summary>
    public static string UserAttributeUrl(int tenantId, int userId, string attributeName) =>
        $"{UserAttributesUrl(tenantId, userId)}/{attributeName}";

    /// <summary>The divisions URL</summary>
    public static string DivisionsUrl(int tenantId) =>
        $"{TenantUrl(tenantId)}/divisions";

    /// <summary>The division URL</summary>
    public static string DivisionUrl(int tenantId, int divisionId) =>
        $"{DivisionsUrl(tenantId)}/{divisionId}";

    /// <summary>The tasks URL</summary>
    public static string TasksUrl(int tenantId) =>
        $"{TenantUrl(tenantId)}/tasks";

    /// <summary>The task URL</summary>
    public static string TaskUrl(int tenantId, int taskId) =>
        $"{TasksUrl(tenantId)}/{taskId}";

    /// <summary>The logs URL</summary>
    public static string LogsUrl(int tenantId) =>
        $"{TenantUrl(tenantId)}/logs";

    /// <summary>The log URL</summary>
    public static string LogUrl(int tenantId, int logId) =>
        $"{LogsUrl(tenantId)}/{logId}";

    /// <summary>The report logs URL</summary>
    public static string ReportLogsUrl(int tenantId) =>
        $"{TenantUrl(tenantId)}/reportlogs";

    /// <summary>The report log URL</summary>
    public static string ReportLogUrl(int tenantId, int reportLogId) =>
        $"{ReportLogsUrl(tenantId)}/{reportLogId}";

    /// <summary>The webhooks URL</summary>
    public static string WebhooksUrl(int tenantId) =>
        $"{TenantUrl(tenantId)}/webhooks";

    /// <summary>The webhook URL</summary>
    public static string WebhookUrl(int tenantId, int webhookId) =>
        $"{WebhooksUrl(tenantId)}/{webhookId}";

    /// <summary>The webhook attributes URL</summary>
    public static string WebhookAttributesUrl(int tenantId, int webhookId) =>
        $"{WebhookUrl(tenantId, webhookId)}/{ApiEndpoints.AttributesPath()}";

    /// <summary>The webhook attribute URL</summary>
    public static string WebhookAttributeUrl(int tenantId, int webhookId, string attributeName) =>
        $"{WebhookAttributesUrl(tenantId, webhookId)}/{attributeName}";

    /// <summary>The webhook messages URL</summary>
    public static string WebhookMessagesUrl(int tenantId, int webhookId) =>
        $"{WebhookUrl(tenantId, webhookId)}/messages";

    /// <summary>The webhook message URL</summary>
    public static string WebhookMessageUrl(int tenantId, int webhookId, int webhookMessageId) =>
        $"{WebhookMessagesUrl(tenantId, webhookId)}/{webhookMessageId}";
}