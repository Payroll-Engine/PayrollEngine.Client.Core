using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll calendar client object</summary>
public class Calendar : Model, ICalendar, INameObject
{
    /// <summary>The division name</summary>
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    /// <summary>The localized division names</summary>
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <summary>The cycle time unit</summary>
    [Required]
    public CalendarTimeUnit CycleTimeUnit { get; set; } = CalendarTimeUnit.Year;

    /// <summary>The period time unit</summary>
    [Required]
    public CalendarTimeUnit PeriodTimeUnit { get; set; } = CalendarTimeUnit.CalendarMonth;

    /// <summary>The time map</summary>
    public CalendarTimeMap TimeMap { get; set; } = CalendarTimeMap.Period;

    /// <summary>The first month of a year</summary>
    public Month? FirstMonthOfYear { get; set; } = Month.January;

    /// <summary>Override the effective month day count</summary>
    public decimal? MonthDayCount { get; set; }

    /// <summary>Override the calendar year start week rule</summary>
    public CalendarWeekRule? YearWeekRule { get; set; }

    /// <summary>Override the calendar first day of week</summary>
    public DayOfWeek? FirstDayOfWeek { get; set; }

    /// <summary>The week mode (default: week)</summary>
    public CalendarWeekMode WeekMode { get; set; } = CalendarWeekMode.Week;

    /// <summary>Work on monday</summary>
    public bool WorkMonday { get; set; } = true;

    /// <summary>Work on tuesday</summary>
    public bool WorkTuesday { get; set; } = true;

    /// <summary>Work on wednesday</summary>
    public bool WorkWednesday { get; set; } = true;

    /// <summary>Work on thursday</summary>
    public bool WorkThursday { get; set; } = true;

    /// <summary>Work on friday</summary>
    public bool WorkFriday { get; set; } = true;

    /// <summary>Work on saturday</summary>
    public bool WorkSaturday { get; set; }

    /// <summary>Work on sunday</summary>
    public bool WorkSunday { get; set; }

    /// <summary>Custom attributes</summary>
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public Calendar()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public Calendar(Calendar copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ICalendar compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(ICalendar compare) =>
        string.Equals(Name, compare?.Name);

    /// <inheritdoc/>
    public override string GetUiString() => Name;
}