using System;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll calendar service</summary>
public interface ICalendarService : ICrudService<ICalendar, TenantServiceContext, Query>
{
    /// <summary>Get calendar by name</summary>
    /// <param name="context">The service context</param>
    /// <param name="name">The calendar name</param>
    /// <returns>The calendar, null if missing</returns>
    Task<T> GetAsync<T>(TenantServiceContext context, string name) where T : class, ICalendar;

    /// <summary>Get tenant calendar period</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="cultureName">The culture to use (default: tenant culture)</param>
    /// <param name="calendarName">The calendar name</param>
    /// <param name="periodMoment">The moment within the payrun period (default: now)</param>
    /// <param name="offset">The offset:<br />
    /// less than zero: past<br />
    /// zero: current (default)<br />
    /// greater than zero: future<br /></param>
    /// <returns>The calendar period</returns>
    Task<DatePeriod> GetPeriodAsync(int tenantId, string cultureName = null, string calendarName = null,
        DateTime? periodMoment = null, int? offset = null);

    /// <summary>Get tenant calendar cycle</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="cultureName">The culture to use (default: tenant culture)</param>
    /// <param name="calendarName">The calendar name</param>
    /// <param name="cycleMoment">The moment within the payrun cycle (default: now)</param>
    /// <param name="offset">The offset:<br />
    /// less than zero: past<br />
    /// zero: current (default)<br />
    /// greater than zero: future<br /></param>
    /// <returns>The calendar cycle</returns>
    Task<DatePeriod> GetCycleAsync(int tenantId, string cultureName = null, string calendarName = null,
        DateTime? cycleMoment = null, int? offset = null);

    /// <summary>Calculate calendar value</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="value">The value to calculate</param>
    /// <param name="cultureName">The culture to use (default: tenant culture)</param>
    /// <param name="calendarName">The calendar name</param>
    /// <param name="evaluationDate">The period evaluation date (default: now)</param>
    /// <param name="evaluationPeriodDate">The date within the evaluation period (default: evaluation date)</param>
    /// <returns>The calendar value</returns>
    Task<decimal?> CalculateValueAsync(int tenantId, decimal value, string cultureName = null,
        string calendarName = null, DateTime? evaluationDate = null, DateTime? evaluationPeriodDate = null);
}