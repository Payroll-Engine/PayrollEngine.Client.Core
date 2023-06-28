
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll service</summary>
public interface IPayrollService : ICrudService<IPayroll, TenantServiceContext, Query>, IAttributeService<TenantServiceContext>
{

    #region Payroll

    /// <summary>Get payroll by name</summary>
    /// <param name="context">The service context</param>
    /// <param name="name">The payroll name</param>
    /// <returns>The payroll, null if missing</returns>
    Task<T> GetAsync<T>(TenantServiceContext context, string name) where T : class, IPayroll;

    #endregion

    #region Regulations

    /// <summary>Get payroll regulations</summary>
    /// <param name="context">The service context</param>
    /// <param name="regulationDate">The regulation date (default: UTC now)</param>
    /// <param name="evaluationDate">Creation date filter (default: UTC now)</param>
    /// <returns>The payroll regulations, including the shared regulations</returns>
    Task<List<T>> GetRegulationsAsync<T>(PayrollServiceContext context, DateTime? regulationDate = null,
        DateTime? evaluationDate = null) where T : class, IRegulation;

    #endregion

    #region Cases

    /// <summary>Get all active and available cases</summary>
    /// <param name="context">The service context</param>
    /// <param name="userId">The user id</param>
    /// <param name="caseType">The case type</param>
    /// <param name="caseNames">The case names (default: all)</param>
    /// <param name="employeeId">The employee id</param>
    /// <param name="caseSlot">The case slot</param>
    /// <param name="clusterSetName">The cluster set name</param>
    /// <param name="culture">The culture</param>
    /// <param name="regulationDate">The regulation date (default: UTC now)</param>
    /// <param name="evaluationDate">Creation date filter (default: UTC now)</param>
    /// <returns>Available derived cases</returns>
    Task<List<TCase>> GetAvailableCasesAsync<TCase>(PayrollServiceContext context, int userId, CaseType caseType,
        IEnumerable<string> caseNames = null, int? employeeId = null, string caseSlot = null, string clusterSetName = null,
        string culture = null, DateTime? regulationDate = null, DateTime? evaluationDate = null) where TCase : class, ICase;

    /// <summary>Build case with fields and related cases</summary>
    /// <param name="context">The service context</param>
    /// <param name="caseName">The case name</param>
    /// <param name="userId">The user id</param>
    /// <param name="employeeId">The employee id</param>
    /// <param name="clusterSetName">The cluster set name</param>
    /// <param name="culture">The culture</param>
    /// <param name="regulationDate">The regulation date (default: UTC now)</param>
    /// <param name="evaluationDate">Creation date filter (default: UTC now)</param>
    /// <param name="caseChangeSetup">The case change setup (optional)</param>
    /// <returns>The created case set</returns>
    Task<TCaseSet> BuildCaseAsync<TCaseSet>(PayrollServiceContext context, string caseName, int userId,
        int? employeeId = null, string clusterSetName = null, string culture = null,
        DateTime? regulationDate = null, DateTime? evaluationDate = null, ICaseChangeSetup caseChangeSetup = null)
        where TCaseSet : class, ICaseSet;

    #endregion

    #region Case Values

    /// <summary>Get raw case values by date range and case field name</summary>
    /// <param name="context">The service context</param>
    /// <param name="startDate">The time period start date</param>
    /// <param name="endDate">The time period end date</param>
    /// <param name="caseFieldNames">The case field names</param>
    /// <param name="employeeId">The employee id</param>
    /// <param name="caseSlot">The case slot</param>
    /// <returns>The case value periods</returns>
    Task<List<CaseFieldValue>> GetCaseValuesAsync(PayrollServiceContext context, DateTime startDate, DateTime endDate,
        IEnumerable<string> caseFieldNames, int? employeeId = null, string caseSlot = null);

    /// <summary>Get payroll case values from a specific time moment</summary>
    /// <param name="context">The service context</param>
    /// <param name="employeeId">The employee id</param>
    /// <param name="caseFieldNames">The case field names</param>
    /// <param name="valueDate">The moment of the value (default: UTC now)</param>
    /// <param name="regulationDate">The regulation date (default: UTC now)</param>
    /// <param name="evaluationDate">The evaluation date (default: value date)</param>
    /// <returns>The payroll case value of the case field</returns>
    Task<List<CaseValue>> GetTimeValuesAsync(PayrollServiceContext context, int? employeeId = null,
        IEnumerable<string> caseFieldNames = null, DateTime? valueDate = null,
        DateTime? regulationDate = null, DateTime? evaluationDate = null);

    /// <summary>Get available case period values</summary>
    /// <param name="context">The service context</param>
    /// <param name="userId">The user id</param>
    /// <param name="caseFieldNames">The case field names</param>
    /// <param name="startDate">The time period start date</param>
    /// <param name="endDate">The time period end date</param>
    /// <param name="employeeId">The employee id, mandatory for employee case</param>
    /// <param name="regulationDate">The regulation date (default: UTC now)</param>
    /// <param name="evaluationDate">Creation date filter (default: UTC now)</param>
    /// <returns>Case period values</returns>
    Task<List<CaseFieldValue>> GetAvailableCaseFieldValuesAsync(PayrollServiceContext context, int userId,
        IEnumerable<string> caseFieldNames, DateTime startDate, DateTime endDate,
        int? employeeId = null, DateTime? regulationDate = null, DateTime? evaluationDate = null);

    /// <summary>Add case change</summary>
    /// <param name="context">The service context</param>
    /// <param name="caseChangeSetup">The case change setup</param>
    /// <returns>The created case change</returns>
    /// 
    Task<TCaseChange> AddCaseAsync<TCaseChangeSetup, TCaseChange>(PayrollServiceContext context, TCaseChangeSetup caseChangeSetup)
        where TCaseChangeSetup : class, ICaseChangeSetup
        where TCaseChange : class, ICaseChange;

    #endregion

    #region Payroll Regulation Items

    /// <summary>Get payroll cases</summary>
    /// <param name="context">The service context</param>
    /// <param name="caseType">The case type (default: all)</param>
    /// <param name="caseNames">The case names</param>
    /// <param name="overrideType">The override type filter (default: active)</param>
    /// <param name="regulationDate">The regulation date (default: UTC now)</param>
    /// <param name="evaluationDate">Creation date filter (default: UTC now)</param>
    /// <returns>Payroll cases</returns>
    Task<List<TCase>> GetCasesAsync<TCase>(PayrollServiceContext context, CaseType? caseType = null, IEnumerable<string> caseNames = null,
        OverrideType? overrideType = null, DateTime? regulationDate = null, DateTime? evaluationDate = null)
        where TCase : class, ICase;

    /// <summary>Get payroll case fields</summary>
    /// <param name="context">The service context</param>
    /// <param name="caseFieldNames">The case field names (default: all)</param>
    /// <param name="overrideType">The override type filter (default: active)</param>
    /// <param name="regulationDate">The regulation date (default: UTC now)</param>
    /// <param name="evaluationDate">Creation date filter (default: UTC now)</param>
    /// <returns>Payroll case fields</returns>
    Task<List<TCaseField>> GetCaseFieldsAsync<TCaseField>(PayrollServiceContext context, IEnumerable<string> caseFieldNames = null,
        OverrideType? overrideType = null, DateTime? regulationDate = null, DateTime? evaluationDate = null)
        where TCaseField : class, ICaseField;

    /// <summary>Get payroll case relations</summary>
    /// <param name="context">The service context</param>
    /// <param name="sourceCaseName">The relation source case name (default: all)</param>
    /// <param name="targetCaseName">The relation target case name (default: all)</param>
    /// <param name="overrideType">The override type filter (default: active)</param>
    /// <param name="regulationDate">The regulation date (default: UTC now)</param>
    /// <param name="evaluationDate">Creation date filter (default: UTC now)</param>
    /// <returns>Payroll case relations</returns>
    Task<List<TCaseRelation>> GetCaseRelationsAsync<TCaseRelation>(PayrollServiceContext context, string sourceCaseName = null,
        string targetCaseName = null, OverrideType? overrideType = null, DateTime? regulationDate = null, DateTime? evaluationDate = null)
        where TCaseRelation : class, ICaseRelation;

    /// <summary>Get payroll wage types</summary>
    /// <param name="context">The service context</param>
    /// <param name="wageTypeNumbers">The wage type numbers (default: all)</param>
    /// <param name="overrideType">The override type filter (default: active)</param>
    /// <param name="regulationDate">The regulation date (default: UTC now)</param>
    /// <param name="evaluationDate">Creation date filter (default: UTC now)</param>
    /// <returns>Payroll wage types</returns>
    Task<List<TWageType>> GetWageTypesAsync<TWageType>(PayrollServiceContext context, IEnumerable<decimal> wageTypeNumbers = null,
        OverrideType? overrideType = null, DateTime? regulationDate = null, DateTime? evaluationDate = null)
        where TWageType : class, IWageType;

    /// <summary>Get payroll collectors</summary>
    /// <param name="context">The service context</param>
    /// <param name="collectorNames">The collector names filter (Default is all)</param>
    /// <param name="overrideType">The override type filter (default: active)</param>
    /// <param name="regulationDate">The regulation date (default: UTC now)</param>
    /// <param name="evaluationDate">Creation date filter (default: UTC now)</param>
    /// <returns>Payroll collectors</returns>
    Task<List<TCollector>> GetCollectorsAsync<TCollector>(PayrollServiceContext context, IEnumerable<string> collectorNames = null,
        OverrideType? overrideType = null, DateTime? regulationDate = null, DateTime? evaluationDate = null)
        where TCollector : class, ICollector;

    /// <summary>Get payroll lookups</summary>
    /// <param name="context">The service context</param>
    /// <param name="lookupNames">The lookup names filter (default is all)</param>
    /// <param name="overrideType">The override type filter (default: active)</param>
    /// <param name="regulationDate">The regulation date (default: UTC now)</param>
    /// <param name="evaluationDate">Creation date filter (default: UTC now)</param>
    /// <returns>Payroll lookups</returns>
    Task<List<TLookup>> GetLookupsAsync<TLookup>(PayrollServiceContext context, IEnumerable<string> lookupNames = null,
        OverrideType? overrideType = null, DateTime? regulationDate = null, DateTime? evaluationDate = null)
        where TLookup : class, ILookup;

    /// <summary>Get payroll lookup data</summary>
    /// <param name="context">The service context</param>
    /// <param name="lookupNames">The lookup names</param>
    /// <param name="regulationDate">The regulation date (default: UTC now)</param>
    /// <param name="evaluationDate">The evaluation date (default: UTC now)</param>
    /// <param name="culture">The content culture</param>
    /// <returns>The lookup data</returns>
    Task<List<TLookupData>> GetLookupDataAsync<TLookupData>(PayrollServiceContext context, IEnumerable<string> lookupNames,
        DateTime? regulationDate = null, DateTime? evaluationDate = null, string culture = null)
        where TLookupData : class, ILookupData;

    /// <summary>Get payroll lookup values</summary>
    /// <param name="context">The service context</param>
    /// <param name="lookupNames">The lookup names filter (default is all)</param>
    /// <param name="lookupKeys">The lookup-value key filter (default: active)</param>
    /// <param name="regulationDate">The regulation date (default: UTC now)</param>
    /// <param name="evaluationDate">Creation date filter (default: UTC now)</param>
    /// <returns>Payroll lookup values</returns>
    Task<List<TLookupValue>> GetLookupValuesAsync<TLookupValue>(PayrollServiceContext context, IEnumerable<string> lookupNames = null,
        IEnumerable<string> lookupKeys = null, DateTime? regulationDate = null, DateTime? evaluationDate = null)
        where TLookupValue : class, ILookupValue;

    /// <summary>Get payroll lookup value data</summary>
    /// <param name="context">The service context</param>
    /// <param name="lookupName">The lookup name</param>
    /// <param name="lookupKey">The lookup key, optionally with range value</param>
    /// <param name="rangeValue">The lookup range value</param>
    /// <param name="regulationDate">The regulation date (default: UTC now)</param>
    /// <param name="evaluationDate">The evaluation date (default: UTC now)</param>
    /// <param name="culture">The culture</param>
    /// <returns>The lookup value data</returns>
    Task<LookupValueData> GetLookupValueDataAsync(PayrollServiceContext context, string lookupName, string lookupKey = null,
        decimal? rangeValue = null, DateTime? regulationDate = null, DateTime? evaluationDate = null, string culture = null);

    /// <summary>Get payroll report sets</summary>
    /// <param name="context">The service context</param>
    /// <param name="reportNames">The report names filter (default is all)</param>
    /// <param name="overrideType">The override type filter (default: active)</param>
    /// <param name="regulationDate">The regulation date (default: UTC now)</param>
    /// <param name="evaluationDate">Creation date filter (default: UTC now)</param>
    /// <returns>Payroll report sets</returns>
    Task<List<TReportSet>> GetReportsAsync<TReportSet>(PayrollServiceContext context, IEnumerable<string> reportNames = null,
        OverrideType? overrideType = null, DateTime? regulationDate = null, DateTime? evaluationDate = null)
        where TReportSet : class, IReportSet;

    /// <summary>Get payroll report parameters</summary>
    /// <param name="context">The service context</param>
    /// <param name="reportNames">The report names</param>
    /// <param name="regulationDate">The regulation date (default: UTC now)</param>
    /// <param name="evaluationDate">The evaluation date (default: UTC now)</param>
    /// <returns>Payroll report parameters</returns>
    Task<List<TReportParameter>> GetReportParametersAsync<TReportParameter>(PayrollServiceContext context,
        IEnumerable<string> reportNames = null, DateTime? regulationDate = null, DateTime? evaluationDate = null)
        where TReportParameter : class, IReportParameter;

    /// <summary>Get payroll report templates</summary>
    /// <param name="context">The service context</param>
    /// <param name="reportNames">The report names</param>
    /// <param name="culture">The report culture</param>
    /// <param name="regulationDate">The regulation date (default: UTC now)</param>
    /// <param name="evaluationDate">The evaluation date (default: UTC now)</param>
    /// <returns>Payroll report templates</returns>
    Task<List<TReportTemplate>> GetReportTemplatesAsync<TReportTemplate>(PayrollServiceContext context,
        IEnumerable<string> reportNames = null, string culture = null, DateTime? regulationDate = null,
        DateTime? evaluationDate = null) where TReportTemplate : class, IReportTemplate;

    /// <summary>Get payroll scripts</summary>
    /// <param name="context">The service context</param>
    /// <param name="scriptNames">The script names filter (default is all)</param>
    /// <param name="overrideType">The override type filter (default: active)</param>
    /// <param name="regulationDate">The regulation date (default: UTC now)</param>
    /// <param name="evaluationDate">Creation date filter (default: UTC now)</param>
    /// <returns>Payroll scripts</returns>
    Task<List<TScript>> GetScriptsAsync<TScript>(PayrollServiceContext context, IEnumerable<string> scriptNames = null,
        OverrideType? overrideType = null, DateTime? regulationDate = null, DateTime? evaluationDate = null)
        where TScript : class, IScript;

    /// <summary>Get payroll actions</summary>
    /// <param name="context">The service context</param>
    /// <param name="scriptNames">The script names filter (default is all)</param>
    /// <param name="overrideType">The override type filter (default: active)</param>
    /// <param name="functionType">The function type (default: all)</param>
    /// <param name="regulationDate">The regulation date (default: UTC now)</param>
    /// <param name="evaluationDate">Creation date filter (default: UTC now)</param>
    /// <returns>Payroll actions</returns>
    Task<List<TAction>> GetActionsAsync<TAction>(PayrollServiceContext context, IEnumerable<string> scriptNames = null,
        OverrideType? overrideType = null, FunctionType? functionType = null, DateTime? regulationDate = null,
        DateTime? evaluationDate = null)
        where TAction : ActionInfo;

    #endregion

}
