using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using PayrollEngine.Data;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll tenant service</summary>
public interface ITenantService : ICrudService<ITenant, RootServiceContext, Query>, IAttributeService<RootServiceContext>
{
    /// <summary>Get tenant by identifier</summary>
    /// <param name="context">The service context</param>
    /// <param name="identifier">The tenant identifier</param>
    /// <returns>The tenant, null if missing</returns>
    Task<T> GetAsync<T>(RootServiceContext context, string identifier) where T : class, ITenant;
        
    /// <summary>Get tenant shared regulations</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="divisionId">The division id</param>
    /// <returns>The tenant shared regulations</returns>
    Task<List<T>> GetSharedRegulationsAsync<T>(int tenantId, int? divisionId) where T : class, IRegulation;
        
    /// <summary>Get the system script actions</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="functionType">The function type</param>
    /// <returns>The tenant shared regulations</returns>
    Task<List<TAction>> GetSystemScriptActionsAsync<TAction>(int tenantId, FunctionType functionType = FunctionType.All)
        where TAction : ActionInfo;

    /// <summary>Execute a report query</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="methodName">The query method</param>
    /// <param name="language">The data language</param>
    /// <param name="parameters">The query parameters</param>
    /// <returns>The resulting data table</returns>
    Task<DataTable> ExecuteReportQueryAsync(int tenantId, string methodName, Language? language,
        Dictionary<string, string> parameters = null);

    #region Calendar

    /// <summary>Get tenant calendar period</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="calculationMode">The calculation mode</param>
    /// <param name="periodMoment">The moment within the payrun period (default: now)</param>
    /// <param name="calendar">The calendar configuration (default: tenant calendar)</param>
    /// <param name="culture">The culture to use (default: tenant culture)</param>
    /// <param name="offset">The offset:<br />
    /// less than zero: past<br />
    /// zero: current (default)<br />
    /// greater than zero: future<br /></param>
    /// <returns>The calendar period</returns>
    Task<DatePeriod> GetCalendarPeriodAsync(int tenantId,
        CalendarCalculationMode calculationMode, DateTime? periodMoment = null,
        CalendarConfiguration calendar = null, string culture = null, int? offset = null);

    /// <summary>Get tenant calendar cycle</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="calculationMode">The calculation mode</param>
    /// <param name="cycleMoment">The moment within the payrun cycle (default: now)</param>
    /// <param name="calendar">The calendar configuration (default: tenant calendar)</param>
    /// <param name="culture">The culture to use (default: tenant culture)</param>
    /// <param name="offset">The offset:<br />
    /// less than zero: past<br />
    /// zero: current (default)<br />
    /// greater than zero: future<br /></param>
    /// <returns>The calendar cycle</returns>
    Task<DatePeriod> GetCalendarCycleAsync(int tenantId,
        CalendarCalculationMode calculationMode, DateTime? cycleMoment = null,
        CalendarConfiguration calendar = null, string culture = null, int? offset = null);

    /// <summary>Calculate calendar value</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="calculationMode">The calculation mode</param>
    /// <param name="value">The value to calculate</param>
    /// <param name="evaluationDate">The period evaluation date (default: now)</param>
    /// <param name="evaluationPeriodDate">The date within the evaluation period (default: evaluation date)</param>
    /// <param name="calendar">The calendar configuration (default: tenant calendar)</param>
    /// <param name="culture">The culture to use (default: tenant culture)</param>
    /// <returns>The calendar value</returns>
    Task<decimal?> CalculateCalendarValueAsync(int tenantId,
        CalendarCalculationMode calculationMode, decimal value,
        DateTime? evaluationDate = null, DateTime? evaluationPeriodDate = null,
        CalendarConfiguration calendar = null, string culture = null);

    #endregion

}