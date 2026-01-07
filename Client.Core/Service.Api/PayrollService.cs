using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll  service</summary>
public class PayrollService : ServiceBase, IPayrollService
{
    /// <summary>Initializes a new instance of the <see cref="PayrollService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public PayrollService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    #region Payroll

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(TenantServiceContext context, Query query = null) where T : class, IPayroll
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(PayrollApiEndpoints.PayrollsUrl(context.TenantId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<long> QueryCountAsync(TenantServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(PayrollApiEndpoints.PayrollsUrl(context.TenantId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(TenantServiceContext context, Query query = null) where T : class, IPayroll
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(PayrollApiEndpoints.PayrollsUrl(context.TenantId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, int payrollId) where T : class, IPayroll
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrollId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrollId));
        }

        return await HttpClient.GetAsync<T>(PayrollApiEndpoints.PayrollUrl(context.TenantId, payrollId));
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, string name) where T : class, IPayroll
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(nameof(name));
        }

        // query single item
        var query = QueryFactory.NewNameQuery(name);
        var uri = query.AppendQueryString(PayrollApiEndpoints.PayrollsUrl(context.TenantId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> GetRegulationsAsync<T>(PayrollServiceContext context, DateTime? regulationDate = null,
        DateTime? evaluationDate = null) where T : class, IRegulation
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var requestUri = PayrollApiEndpoints.PayrollRegulationsUrl(context.TenantId, context.PayrollId)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate);
        return await HttpClient.GetCollectionAsync<T>(requestUri);
    }

    /// <inheritdoc/>
    public virtual async Task<T> CreateAsync<T>(TenantServiceContext context, T payroll) where T : class, IPayroll
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payroll == null)
        {
            throw new ArgumentNullException(nameof(payroll));
        }

        return await HttpClient.PostAsync(PayrollApiEndpoints.PayrollsUrl(context.TenantId), payroll);
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync<T>(TenantServiceContext context, T payroll) where T : class, IPayroll
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payroll == null)
        {
            throw new ArgumentNullException(nameof(payroll));
        }

        await HttpClient.PutAsync(PayrollApiEndpoints.PayrollsUrl(context.TenantId), payroll);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(TenantServiceContext context, int payrollId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrollId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrollId));
        }

        await HttpClient.DeleteAsync(PayrollApiEndpoints.PayrollsUrl(context.TenantId), payrollId);
    }

    #endregion

    #region Cases

    /// <inheritdoc/>
    public virtual async Task<List<TCase>> GetAvailableCasesAsync<TCase>(PayrollServiceContext context, int userId,
        CaseType caseType, IEnumerable<string> caseNames = null, int? employeeId = null, string caseSlot = null,
        string clusterSetName = null, string culture = null, bool? hidden = null,
        DateTime? regulationDate = null, DateTime? evaluationDate = null)
        where TCase : class, ICase
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (userId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(userId));
        }

        var requestUri = PayrollApiEndpoints.PayrollCasesAvailableUrl(context.TenantId, context.PayrollId)
            .AddQueryString(nameof(userId), userId)
            .AddQueryString(nameof(caseType), caseType)
            .AddCollectionQueryString(nameof(caseNames), caseNames)
            .AddQueryString(nameof(employeeId), employeeId)
            .AddQueryString(nameof(caseSlot), caseSlot)
            .AddQueryString(nameof(clusterSetName), clusterSetName)
            .AddQueryString(nameof(culture), culture)
            .AddQueryString(nameof(hidden), hidden)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate);
        return await HttpClient.GetCollectionAsync<TCase>(requestUri);
    }

    /// <inheritdoc/>
    public virtual async Task<TCaseSet> BuildCaseAsync<TCaseSet>(PayrollServiceContext context, string caseName,
        int userId, int? employeeId = null, string clusterSetName = null, string culture = null,
        DateTime? regulationDate = null, DateTime? evaluationDate = null, ICaseChangeSetup caseChangeSetup = null)
        where TCaseSet : class, ICaseSet
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (string.IsNullOrWhiteSpace(caseName))
        {
            throw new ArgumentException(nameof(caseName));
        }
        if (userId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(userId));
        }

        var url = PayrollApiEndpoints.PayrollCaseBuildUrl(context.TenantId, context.PayrollId, caseName)
            .AddQueryString(nameof(userId), userId)
            .AddQueryString(nameof(employeeId), employeeId)
            .AddQueryString(nameof(culture), culture)
            .AddQueryString(nameof(clusterSetName), clusterSetName)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate);

        // use of POST instead of GET according RFC7231
        // https://datatracker.ietf.org/doc/html/rfc7231#section-4.3.1
        return caseChangeSetup != null ?
            await HttpClient.PostAsync<ICaseChangeSetup, TCaseSet>(url, caseChangeSetup) :
            await HttpClient.PostAsync<TCaseSet>(url);
    }

    #endregion

    #region Case Values

    /// <inheritdoc/>
    public virtual async Task<List<CaseFieldValue>> GetCaseValuesAsync(PayrollServiceContext context,
        DateTime startDate, DateTime endDate, IEnumerable<string> caseFieldNames, int? employeeId = null,
        DateTime? regulationDate = null, DateTime? evaluationDate = null, string caseSlot = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var url = PayrollApiEndpoints.PayrollCasesValuesUrl(context.TenantId, context.PayrollId)
            .AddCollectionQueryString(nameof(caseFieldNames), caseFieldNames)
            .AddQueryString(nameof(startDate), startDate)
            .AddQueryString(nameof(endDate), endDate)
            .AddQueryString(nameof(employeeId), employeeId)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate)
            .AddQueryString(nameof(caseSlot), caseSlot);
        return (await HttpClient.GetAsync<IEnumerable<CaseFieldValue>>(url)).ToList();
    }

    /// <inheritdoc/>
    public virtual async Task<List<CaseValue>> GetCaseTimeValuesAsync(PayrollServiceContext context,
        CaseType caseType, int? employeeId = null, IEnumerable<string> caseFieldNames = null,
        DateTime? valueDate = null, DateTime? regulationDate = null, DateTime? evaluationDate = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var url = PayrollApiEndpoints.PayrollCaseValuesTimeUrl(context.TenantId, context.PayrollId)
            .AddQueryString(nameof(caseType), caseType)
            .AddQueryString(nameof(employeeId), employeeId)
            .AddCollectionQueryString(nameof(caseFieldNames), caseFieldNames)
            .AddQueryString(nameof(valueDate), valueDate)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate);
        return await HttpClient.GetCollectionAsync<CaseValue>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<List<CaseFieldValue>> GetAvailableCaseFieldValuesAsync(PayrollServiceContext context,
        int userId, IEnumerable<string> caseFieldNames, DateTime startDate, DateTime endDate, int? employeeId = null,
        DateTime? regulationDate = null, DateTime? evaluationDate = null, string culture = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (userId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(userId));
        }
        if (caseFieldNames == null)
        {
            throw new ArgumentNullException(nameof(caseFieldNames));
        }

        var requestUri = PayrollApiEndpoints.PayrollCasesValuesPeriodsUrl(context.TenantId, context.PayrollId)
            .AddQueryString(nameof(userId), userId)
            .AddCollectionQueryString(nameof(caseFieldNames), caseFieldNames)
            .AddQueryString(nameof(startDate), startDate)
            .AddQueryString(nameof(endDate), endDate)
            .AddQueryString(nameof(employeeId), employeeId)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate)
            .AddQueryString(nameof(culture), culture);
        var periodValues = await HttpClient.GetAsync<CaseFieldValue[]>(requestUri);
        return [.. periodValues];
    }

    /// <inheritdoc/>
    public virtual async Task<TCaseChange> AddCaseAsync<TCaseChangeSetup, TCaseChange>(PayrollServiceContext context, TCaseChangeSetup caseChangeSetup)
        where TCaseChangeSetup : class, ICaseChangeSetup
        where TCaseChange : class, ICaseChange
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (caseChangeSetup == null)
        {
            throw new ArgumentNullException(nameof(caseChangeSetup));
        }

        var requestUri = PayrollApiEndpoints.PayrollCasesUrl(context.TenantId, context.PayrollId);
        requestUri = requestUri.AddQueryString(nameof(caseChangeSetup.EmployeeId), caseChangeSetup.EmployeeId);
        return await HttpClient.PostAsync<TCaseChangeSetup, TCaseChange>(requestUri, caseChangeSetup);
    }

    #endregion

    #region Payroll Regulation Items

    /// <inheritdoc/>
    public virtual async Task<List<TCase>> GetCasesAsync<TCase>(PayrollServiceContext context, CaseType? caseType = null,
        IEnumerable<string> caseNames = null, OverrideType? overrideType = null, DateTime? regulationDate = null, DateTime? evaluationDate = null)
        where TCase : class, ICase
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var uri = PayrollApiEndpoints.PayrollCasesUrl(context.TenantId, context.PayrollId)
            .AddQueryString(nameof(caseType), caseType)
            .AddCollectionQueryString(nameof(caseNames), caseNames)
            .AddQueryString(nameof(overrideType), overrideType)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate);
        return await HttpClient.GetCollectionAsync<TCase>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TCaseField>> GetCaseFieldsAsync<TCaseField>(PayrollServiceContext context, IEnumerable<string> caseFieldNames = null,
        OverrideType? overrideType = null, DateTime? regulationDate = null, DateTime? evaluationDate = null) where TCaseField : class, ICaseField
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var uri = PayrollApiEndpoints.PayrollCaseFieldsUrl(context.TenantId, context.PayrollId)
            .AddCollectionQueryString(nameof(caseFieldNames), caseFieldNames)
            .AddQueryString(nameof(overrideType), overrideType)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate);
        return await HttpClient.GetCollectionAsync<TCaseField>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TCaseRelation>> GetCaseRelationsAsync<TCaseRelation>(PayrollServiceContext context,
        string sourceCaseName = null, string targetCaseName = null, OverrideType? overrideType = null,
        DateTime? regulationDate = null, DateTime? evaluationDate = null) where TCaseRelation : class, ICaseRelation
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var uri = PayrollApiEndpoints.PayrollCaseRelationsUrl(context.TenantId, context.PayrollId)
            .AddQueryString(nameof(sourceCaseName), sourceCaseName)
            .AddQueryString(nameof(targetCaseName), targetCaseName)
            .AddQueryString(nameof(overrideType), overrideType)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate);
        return await HttpClient.GetCollectionAsync<TCaseRelation>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TWageType>> GetWageTypesAsync<TWageType>(PayrollServiceContext context, IEnumerable<decimal> wageTypeNumbers = null,
        OverrideType? overrideType = null, DateTime? regulationDate = null, DateTime? evaluationDate = null) where TWageType : class, IWageType
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var uri = PayrollApiEndpoints.PayrollWageTypesUrl(context.TenantId, context.PayrollId)
            .AddCollectionQueryString(nameof(wageTypeNumbers), wageTypeNumbers)
            .AddQueryString(nameof(overrideType), overrideType)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate);
        return await HttpClient.GetCollectionAsync<TWageType>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TCollector>> GetCollectorsAsync<TCollector>(PayrollServiceContext context, IEnumerable<string> collectorNames = null,
        OverrideType? overrideType = null, DateTime? regulationDate = null, DateTime? evaluationDate = null) where TCollector : class, ICollector
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var uri = PayrollApiEndpoints.PayrollCollectorsUrl(context.TenantId, context.PayrollId)
            .AddCollectionQueryString(nameof(collectorNames), collectorNames)
            .AddQueryString(nameof(overrideType), overrideType)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate);
        return await HttpClient.GetCollectionAsync<TCollector>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TLookup>> GetLookupsAsync<TLookup>(PayrollServiceContext context, IEnumerable<string> lookupNames = null,
        OverrideType? overrideType = null, DateTime? regulationDate = null, DateTime? evaluationDate = null) where TLookup : class, ILookup
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var uri = PayrollApiEndpoints.PayrollLookupsUrl(context.TenantId, context.PayrollId)
            .AddCollectionQueryString(nameof(lookupNames), lookupNames)
            .AddQueryString(nameof(overrideType), overrideType)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate);
        return await HttpClient.GetCollectionAsync<TLookup>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TLookupData>> GetLookupDataAsync<TLookupData>(PayrollServiceContext context,
        IEnumerable<string> lookupNames, DateTime? regulationDate = null, DateTime? evaluationDate = null, string culture = null)
        where TLookupData : class, ILookupData
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (lookupNames == null)
        {
            throw new ArgumentException(nameof(lookupNames));
        }

        var uri = PayrollApiEndpoints.PayrollLookupsDataUrl(context.TenantId, context.PayrollId)
            .AddCollectionQueryString(nameof(lookupNames), lookupNames)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate)
            .AddQueryString(nameof(culture), culture);
        return await HttpClient.GetCollectionAsync<TLookupData>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TLookupValue>> GetLookupValuesAsync<TLookupValue>(PayrollServiceContext context,
        IEnumerable<string> lookupNames = null, IEnumerable<string> lookupKeys = null, DateTime? regulationDate = null,
        DateTime? evaluationDate = null) where TLookupValue : class, ILookupValue
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var uri = PayrollApiEndpoints.PayrollLookupValuesUrl(context.TenantId, context.PayrollId)
            .AddCollectionQueryString(nameof(lookupNames), lookupNames)
            .AddCollectionQueryString(nameof(lookupKeys), lookupKeys)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate);
        return await HttpClient.GetCollectionAsync<TLookupValue>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<LookupValueData> GetLookupValueDataAsync(PayrollServiceContext context,
        string lookupName, string lookupKey = null, decimal? rangeValue = null,
        DateTime? regulationDate = null, DateTime? evaluationDate = null, string culture = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (string.IsNullOrWhiteSpace(lookupName))
        {
            throw new ArgumentException(nameof(lookupName));
        }

        var uri = PayrollApiEndpoints.PayrollLookupValuesDataUrl(context.TenantId, context.PayrollId)
            .AddQueryString(nameof(lookupName), lookupName)
            .AddQueryString(nameof(lookupKey), lookupKey)
            .AddQueryString(nameof(rangeValue), rangeValue)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate)
            .AddQueryString(nameof(culture), culture);
        return await HttpClient.GetAsync<LookupValueData>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TReportSet>> GetReportsAsync<TReportSet>(PayrollServiceContext context,
        IEnumerable<string> reportNames = null, OverrideType? overrideType = null, UserType? userType = null,
        DateTime? regulationDate = null, DateTime? evaluationDate = null) where TReportSet : class, IReportSet
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var uri = PayrollApiEndpoints.PayrollReportsUrl(context.TenantId, context.PayrollId)
            .AddCollectionQueryString(nameof(reportNames), reportNames)
            .AddQueryString(nameof(overrideType), overrideType)
            .AddQueryString(nameof(userType), userType)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate);
        return await HttpClient.GetCollectionAsync<TReportSet>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TReportParameter>> GetReportParametersAsync<TReportParameter>(PayrollServiceContext context,
        IEnumerable<string> reportNames, DateTime? regulationDate = null, DateTime? evaluationDate = null)
        where TReportParameter : class, IReportParameter
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var uri = PayrollApiEndpoints.PayrollReportParametersUrl(context.TenantId, context.PayrollId)
            .AddCollectionQueryString(nameof(reportNames), reportNames)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate);
        return await HttpClient.GetCollectionAsync<TReportParameter>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TReportTemplate>> GetReportTemplatesAsync<TReportTemplate>(PayrollServiceContext context,
        IEnumerable<string> reportNames, string culture = null, DateTime? regulationDate = null, DateTime? evaluationDate = null)
        where TReportTemplate : class, IReportTemplate
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var uri = PayrollApiEndpoints.PayrollReportTemplatesUrl(context.TenantId, context.PayrollId)
            .AddCollectionQueryString(nameof(reportNames), reportNames)
            .AddQueryString(nameof(culture), culture)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate);
        return await HttpClient.GetCollectionAsync<TReportTemplate>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TScript>> GetScriptsAsync<TScript>(PayrollServiceContext context, IEnumerable<string> scriptNames = null,
        OverrideType? overrideType = null, DateTime? regulationDate = null, DateTime? evaluationDate = null) where TScript : class, IScript
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var uri = PayrollApiEndpoints.PayrollScriptsUrl(context.TenantId, context.PayrollId)
            .AddCollectionQueryString(nameof(scriptNames), scriptNames)
            .AddQueryString(nameof(overrideType), overrideType)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate);
        return await HttpClient.GetCollectionAsync<TScript>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TAction>> GetActionsAsync<TAction>(PayrollServiceContext context, IEnumerable<string> scriptNames = null,
        OverrideType? overrideType = null, FunctionType? functionType = null, DateTime? regulationDate = null,
        DateTime? evaluationDate = null) where TAction : ActionInfo
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var uri = PayrollApiEndpoints.PayrollActionsUrl(context.TenantId, context.PayrollId)
            .AddCollectionQueryString(nameof(scriptNames), scriptNames)
            .AddQueryString(nameof(overrideType), overrideType)
            .AddQueryString(nameof(functionType), functionType)
            .AddQueryString(nameof(regulationDate), regulationDate)
            .AddQueryString(nameof(evaluationDate), evaluationDate);
        return await HttpClient.GetCollectionAsync<TAction>(uri);
    }

    #endregion

    #region Attributes

    /// <inheritdoc/>
    public virtual async Task<string> GetAttributeAsync(TenantServiceContext context, int payrollId, string attributeName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrollId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrollId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        return await HttpClient.GetAttributeAsync(PayrollApiEndpoints.PayrollAttributeUrl(context.TenantId, payrollId,
            attributeName));
    }

    /// <inheritdoc/>
    public virtual async Task SetAttributeAsync(TenantServiceContext context, int payrollId, string attributeName, string attributeValue)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrollId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrollId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        await HttpClient.PostAttributeAsync(PayrollApiEndpoints.PayrollAttributeUrl(context.TenantId, payrollId,
            attributeName), attributeValue);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAttributeAsync(TenantServiceContext context, int payrollId, string attributeName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (payrollId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payrollId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        await HttpClient.DeleteAttributeAsync(PayrollApiEndpoints.PayrollAttributeUrl(context.TenantId, payrollId,
            attributeName));
    }

    #endregion

}