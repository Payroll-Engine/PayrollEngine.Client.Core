using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll calendar service</summary>
public class CalendarService : ServiceBase, ICalendarService
{
    /// <summary>Initializes a new instance of the <see cref="CalendarService"/> class</summary>
    /// <param name="httpClient">The HTTP client</param>
    public CalendarService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> QueryAsync<T>(TenantServiceContext context, Query query = null) where T : class, ICalendar
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(TenantApiEndpoints.CalendarsUrl(context.TenantId));
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
        var url = query.AppendQueryString(TenantApiEndpoints.CalendarsUrl(context.TenantId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(TenantServiceContext context, Query query = null) where T : class, ICalendar
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(TenantApiEndpoints.CalendarsUrl(context.TenantId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, int calendarId) where T : class, ICalendar
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (calendarId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(calendarId));
        }

        return await HttpClient.GetAsync<T>(TenantApiEndpoints.CalendarUrl(context.TenantId, calendarId));
    }

    /// <inheritdoc/>
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, string name) where T : class, ICalendar
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
        var uri = query.AppendQueryString(TenantApiEndpoints.CalendarsUrl(context.TenantId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc/>
    public virtual async Task<T> CreateAsync<T>(TenantServiceContext context, T calendar) where T : class, ICalendar
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (calendar == null)
        {
            throw new ArgumentNullException(nameof(calendar));
        }

        return await HttpClient.PostAsync(TenantApiEndpoints.CalendarsUrl(context.TenantId), calendar);
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync<T>(TenantServiceContext context, T calendar) where T : class, ICalendar
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (calendar == null)
        {
            throw new ArgumentNullException(nameof(calendar));
        }

        await HttpClient.PutAsync(TenantApiEndpoints.CalendarsUrl(context.TenantId), calendar);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(TenantServiceContext context, int calendarId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (calendarId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(calendarId));
        }

        await HttpClient.DeleteAsync(TenantApiEndpoints.CalendarsUrl(context.TenantId), calendarId);
    }

    #region Calendar

    /// <inheritdoc />
    public virtual async Task<DatePeriod> GetPeriodAsync(int tenantId, string cultureName = null,
        string calendarName = null, DateTime? periodMoment = null, int? offset = null)
    {
        if (tenantId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(tenantId));
        }

        var url = TenantApiEndpoints.TenantCalendarsPeriodsUrl(tenantId)
            .AddQueryString(nameof(cultureName), cultureName)
            .AddQueryString(nameof(calendarName), calendarName)
            .AddQueryString(nameof(periodMoment), periodMoment)
            .AddQueryString(nameof(offset), offset);
        return await HttpClient.GetAsync<DatePeriod>(url);
    }

    /// <inheritdoc />
    public virtual async Task<DatePeriod> GetCycleAsync(int tenantId, string cultureName = null,
        string calendarName = null, DateTime? cycleMoment = null, int? offset = null)
    {
        if (tenantId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(tenantId));
        }

        var url = TenantApiEndpoints.TenantCalendarsCyclesUrl(tenantId)
            .AddQueryString(nameof(cultureName), cultureName)
            .AddQueryString(nameof(calendarName), calendarName)
            .AddQueryString(nameof(cycleMoment), cycleMoment)
            .AddQueryString(nameof(offset), offset);

        return await HttpClient.GetAsync<DatePeriod>(url);
    }

    /// <inheritdoc />
    public virtual async Task<decimal?> CalculateValueAsync(int tenantId, decimal value,
        string cultureName = null, string calendarName = null, DateTime? evaluationDate = null,
        DateTime? evaluationPeriodDate = null)
    {
        if (tenantId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(tenantId));
        }

        var url = TenantApiEndpoints.TenantCalendarsValuesUrl(tenantId)
            .AddQueryString(nameof(cultureName), cultureName)
            .AddQueryString(nameof(calendarName), calendarName)
            .AddQueryString(nameof(value), value)
            .AddQueryString(nameof(evaluationDate), evaluationDate)
            .AddQueryString(nameof(evaluationPeriodDate), evaluationPeriodDate);

        return await HttpClient.GetAsync<decimal?>(url);
    }

    #endregion

}