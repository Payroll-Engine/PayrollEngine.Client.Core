namespace PayrollEngine.Client;

/// <summary>Regulation api endpoints</summary>
public static class RegulationApiEndpoints
{
    /// <summary>The regulations URL</summary>
    public static string RegulationsUrl(int tenantId) =>
        $"{TenantApiEndpoints.TenantUrl(tenantId)}/regulations";

    /// <summary>The regulation URL</summary>
    public static string RegulationUrl(int tenantId, int regulationId) =>
        $"{RegulationsUrl(tenantId)}/{regulationId}";

    /// <summary>The regulations cases URL</summary>
    public static string RegulationsCasesUrl(int tenantId) =>
        $"{RegulationsUrl(tenantId)}/cases";

    /// <summary>The regulation cases URL</summary>
    public static string RegulationCasesUrl(int tenantId, int regulationId) =>
        $"{RegulationUrl(tenantId, regulationId)}/cases";

    /// <summary>The regulation case URL</summary>
    public static string RegulationCaseUrl(int tenantId, int regulationId, int caseId) =>
        $"{RegulationCasesUrl(tenantId, regulationId)}/{caseId}";

    /// <summary>The regulation case rebuild URL</summary>
    public static string RegulationCaseRebuildUrl(int tenantId, int regulationId, int caseId) =>
        $"{RegulationCaseUrl(tenantId, regulationId, caseId)}/rebuild";

    /// <summary>The regulations cases, case field URL</summary>
    public static string RegulationsCasesCaseFieldUrl(int tenantId, string caseFieldName) =>
        $"{RegulationsCasesUrl(tenantId)}/{caseFieldName}";

    /// <summary>The regulation case fields URL</summary>
    public static string RegulationCaseFieldsUrl(int tenantId, int regulationId, int caseId) =>
        $"{RegulationCaseUrl(tenantId, regulationId, caseId)}/fields";

    /// <summary>The regulation case field URL</summary>
    public static string RegulationCaseFieldUrl(int tenantId, int regulationId, int caseId, int caseField) =>
        $"{RegulationCaseFieldsUrl(tenantId, regulationId, caseId)}/{caseField}";

    /// <summary>The regulation case relations URL</summary>
    public static string RegulationCaseRelationsUrl(int tenantId, int regulationId) =>
        $"{RegulationUrl(tenantId, regulationId)}/caserelations";

    /// <summary>The regulation case relation URL</summary>
    public static string RegulationCaseRelationUrl(int tenantId, int regulationId, int caseRelationId) =>
        $"{RegulationCaseRelationsUrl(tenantId, regulationId)}/{caseRelationId}";

    /// <summary>The regulation case relation rebuild URL</summary>
    public static string RegulationCaseRelationRebuildUrl(int tenantId, int regulationId, int caseRelationId) =>
        $"{RegulationCaseRelationUrl(tenantId, regulationId, caseRelationId)}/rebuild";

    /// <summary>The regulation wage types URL</summary>
    public static string RegulationWageTypesUrl(int tenantId, int regulationId) =>
        $"{RegulationUrl(tenantId, regulationId)}/wagetypes";

    /// <summary>The regulation wage type URL</summary>
    public static string RegulationWageTypeUrl(int tenantId, int regulationId, int wageTypeId) =>
        $"{RegulationWageTypesUrl(tenantId, regulationId)}/{wageTypeId}";

    /// <summary>The regulation wage type rebuild URL</summary>
    public static string RegulationWageTypeRebuildUrl(int tenantId, int regulationId, int wageTypeId) =>
        $"{RegulationWageTypeUrl(tenantId, regulationId, wageTypeId)}/rebuild";

    /// <summary>The regulation collectors URL</summary>
    public static string RegulationCollectorsUrl(int tenantId, int regulationId) =>
        $"{RegulationUrl(tenantId, regulationId)}/collectors";

    /// <summary>The regulation collector URL</summary>
    public static string RegulationCollectorUrl(int tenantId, int regulationId, int collectorId) =>
        $"{RegulationCollectorsUrl(tenantId, regulationId)}/{collectorId}";

    /// <summary>The regulation collector rebuild URL</summary>
    public static string RegulationCollectorRebuildUrl(int tenantId, int regulationId, int collectorId) =>
        $"{RegulationCollectorUrl(tenantId, regulationId, collectorId)}/rebuild";

    /// <summary>The regulation lookups URL</summary>
    public static string RegulationLookupsUrl(int tenantId, int regulationId) =>
        $"{RegulationUrl(tenantId, regulationId)}/lookups";

    /// <summary>The regulation lookup URL</summary>
    public static string RegulationLookupUrl(int tenantId, int regulationId, int lookupId) =>
        $"{RegulationLookupsUrl(tenantId, regulationId)}/{lookupId}";

    /// <summary>The regulation lookup sets URL</summary>
    public static string RegulationLookupSetsUrl(int tenantId, int regulationId) =>
        $"{RegulationLookupsUrl(tenantId, regulationId)}/sets";

    /// <summary>The regulation lookup set URL</summary>
    public static string RegulationLookupSetUrl(int tenantId, int regulationId, int lookupId) =>
        $"{RegulationLookupSetsUrl(tenantId, regulationId)}/{lookupId}";

    /// <summary>The regulations lookup values URL</summary>
    public static string RegulationLookupValuesUrl(int tenantId, int regulationId, int lookupId) =>
        $"{RegulationLookupUrl(tenantId, regulationId, lookupId)}/values";

    /// <summary>The regulation lookups values data URL</summary>
    public static string RegulationLookupsValuesDataUrl(int tenantId, int regulationId, int lookupId) =>
        $"{RegulationLookupValuesUrl(tenantId, regulationId, lookupId)}/data";

    /// <summary>The regulations lookup value URL</summary>
    public static string RegulationLookupValueUrl(int tenantId, int regulationId, int lookupId, int lookupValueId) =>
        $"{RegulationLookupValuesUrl(tenantId, regulationId, lookupId)}/{lookupValueId}";

    /// <summary>The regulation scripts URL</summary>
    public static string RegulationScriptsUrl(int tenantId, int regulationId) =>
        $"{RegulationUrl(tenantId, regulationId)}/scripts";

    /// <summary>The regulation script URL</summary>
    public static string RegulationScriptUrl(int tenantId, int regulationId, int scriptId) =>
        $"{RegulationScriptsUrl(tenantId, regulationId)}/{scriptId}";

    /// <summary>The regulation reports URL</summary>
    public static string RegulationReportsUrl(int tenantId, int regulationId) =>
        $"{RegulationUrl(tenantId, regulationId)}/reports";

    /// <summary>The regulation report URL</summary>
    public static string RegulationReportUrl(int tenantId, int regulationId, int reportId) =>
        $"{RegulationReportsUrl(tenantId, regulationId)}/{reportId}";

    /// <summary>The regulation report rebuild URL</summary>
    public static string RegulationReportRebuildUrl(int tenantId, int regulationId, int reportId) =>
        $"{RegulationReportUrl(tenantId, regulationId, reportId)}/rebuild";

    /// <summary>The regulation report execute URL</summary>
    public static string RegulationReportExecuteUrl(int tenantId, int regulationId, int reportId) =>
        $"{RegulationReportUrl(tenantId, regulationId, reportId)}/execute";

    /// <summary>The regulation report sets URL</summary>
    public static string RegulationReportSetsUrl(int tenantId, int regulationId) =>
        $"{RegulationReportsUrl(tenantId, regulationId)}/sets";

    /// <summary>The regulation report set URL</summary>
    public static string RegulationReportSetUrl(int tenantId, int regulationId, int reportId) =>
        $"{RegulationReportSetsUrl(tenantId, regulationId)}/{reportId}";

    /// <summary>The regulation report attributes URL</summary>
    public static string RegulationReportAttributesUrl(int tenantId, int regulationId, int reportId) =>
        $"{RegulationReportUrl(tenantId, regulationId, reportId)}/{ApiEndpoints.AttributesPath()}";

    /// <summary>The regulation report attribute URL</summary>
    public static string RegulationReportAttributeUrl(int tenantId, int regulationId, int reportId, string attributeName) =>
        $"{RegulationReportAttributesUrl(tenantId, regulationId, reportId)}/{attributeName}";

    /// <summary>The regulation report parameters URL</summary>
    public static string RegulationReportParametersUrl(int tenantId, int regulationId, int reportId) =>
        $"{RegulationReportUrl(tenantId, regulationId, reportId)}/parameters";

    /// <summary>The regulation report parameter URL</summary>
    public static string RegulationReportParameterUrl(int tenantId, int regulationId, int reportId, int reportParameterId) =>
        $"{RegulationReportParametersUrl(tenantId, regulationId, reportId)}/{reportParameterId}";

    /// <summary>The regulation report parameter attributes URL</summary>
    public static string ReportParameterAttributesUrl(int tenantId, int regulationId, int reportId, int reportParameterId) =>
        $"{RegulationReportParameterUrl(tenantId, regulationId, reportId, reportParameterId)}/{ApiEndpoints.AttributesPath()}";

    /// <summary>The regulation report parameter attribute URL</summary>
    public static string ReportParameterAttributeUrl(int tenantId, int regulationId, int reportId, int reportParameterId, string attributeName) =>
        $"{ReportParameterAttributesUrl(tenantId, regulationId, reportId, reportParameterId)}/{attributeName}";

    /// <summary>The regulation report templates URL</summary>
    public static string RegulationReportTemplatesUrl(int tenantId, int regulationId, int reportId) =>
        $"{RegulationReportUrl(tenantId, regulationId, reportId)}/templates";

    /// <summary>The regulation report template URL</summary>
    public static string RegulationReportTemplateUrl(int tenantId, int regulationId, int reportId, int reportTemplateId) =>
        $"{RegulationReportTemplatesUrl(tenantId, regulationId, reportId)}/{reportTemplateId}";

    /// <summary>The regulation report template attributes URL</summary>
    public static string ReportTemplateAttributesUrl(int tenantId, int regulationId, int reportId, int reportTemplateId) =>
        $"{RegulationReportTemplateUrl(tenantId, regulationId, reportId, reportTemplateId)}/{ApiEndpoints.AttributesPath()}";

    /// <summary>The regulation report template attribute URL</summary>
    public static string ReportTemplateAttributeUrl(int tenantId, int regulationId, int reportId, int reportTemplateId, string attributeName) =>
        $"{ReportTemplateAttributesUrl(tenantId, regulationId, reportId, reportTemplateId)}/{attributeName}";
}