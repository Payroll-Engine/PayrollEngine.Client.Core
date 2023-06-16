using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll calendar client object</summary>
public interface ICalendar : IModel, IAttributeObject, IKeyEquatable<ICalendar>
{
    /// <summary>The calendar name</summary>
    string Name { get; set; }

    /// <summary>The localized calendar names</summary>
    Dictionary<string, string> NameLocalizations { get; set; }

    /// <summary>The cycle time unit (default: year)</summary>
    public CalendarTimeUnit CycleTimeUnit { get; set; }

    /// <summary>The period time unit (default: calendar month)</summary>
    public CalendarTimeUnit PeriodTimeUnit { get; set; }

    /// <summary>The time map (default: period)</summary>
    public CalendarTimeMap TimeMap { get; set; }

    /// <summary>The first month of a year  (default: january)</summary>
    public Month? FirstMonthOfYear { get; set; }

    /// <summary>Override the effective month day count</summary>
    public decimal? MonthDayCount { get; set; }

    /// <summary>Override the calendar year start week rule</summary>
    public CalendarWeekRule? YearWeekRule { get; set; }

    /// <summary>Override the calendar first day of week</summary>
    public DayOfWeek? FirstDayOfWeek { get; set; }

    /// <summary>The week mode (default: week)</summary>
    public CalendarWeekMode WeekMode { get; set; }

    /// <summary>Work on monday (default: true), used by <see cref="CalendarWeekMode.WorkWeek"/> </summary>
    public bool WorkMonday { get; set; }

    /// <summary>Work on tuesday (default: true), used by <see cref="CalendarWeekMode.WorkWeek"/> </summary>
    public bool WorkTuesday { get; set; }

    /// <summary>Work on wednesday (default: true), used by <see cref="CalendarWeekMode.WorkWeek"/> </summary>
    public bool WorkWednesday { get; set; }

    /// <summary>Work on thursday (default: true), used by <see cref="CalendarWeekMode.WorkWeek"/> </summary>
    public bool WorkThursday { get; set; }

    /// <summary>Work on friday (default: true), used by <see cref="CalendarWeekMode.WorkWeek"/> </summary>
    public bool WorkFriday { get; set; }

    /// <summary>Work on saturday (default: false), used by <see cref="CalendarWeekMode.WorkWeek"/> </summary>
    public bool WorkSaturday { get; set; }

    /// <summary>Work on sunday (default: false), used by <see cref="CalendarWeekMode.WorkWeek"/> </summary>
    public bool WorkSunday { get; set; }
}
