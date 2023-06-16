using System;
using System.Globalization;

namespace PayrollEngine.Client.Model;

/// <summary>
/// Extension methods for the <see cref="Calendar"/>
/// </summary>
public static class CalendarExtensions
{
    /// <summary>Returns the week of year for the specified DateTime</summary>
    /// <param name="calendar">The payroll calendar</param>
    /// <param name="culture">The culture</param>
    /// <param name="moment">The moment of the week</param>
    /// <returns> The returned value is an integer between 1 and 53</returns>
    public static int GetWeekOfYear(this Calendar calendar, CultureInfo culture, DateTime moment)
    {
        var firstDayOfWeek = calendar.FirstDayOfWeek.HasValue ?
            (System.DayOfWeek)calendar.FirstDayOfWeek.Value :
            culture.DateTimeFormat.FirstDayOfWeek;
        return culture.Calendar.GetWeekOfYear(moment, (System.Globalization.CalendarWeekRule)calendar.WeekMode,
            firstDayOfWeek);
    }
}