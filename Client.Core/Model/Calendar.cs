using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll calendar client object</summary>
public class Calendar : ModelBase, ICalendar, INameObject
{
    /// <summary>The calendar name</summary>
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    [Required]
    public CalendarTimeUnit CycleTimeUnit { get; set; } = CalendarTimeUnit.Year;

    /// <inheritdoc/>
    [Required]
    public CalendarTimeUnit PeriodTimeUnit { get; set; } = CalendarTimeUnit.CalendarMonth;

    /// <inheritdoc/>
    public CalendarTimeMap TimeMap { get; set; } = CalendarTimeMap.Period;

    /// <inheritdoc/>
    public Month? FirstMonthOfYear { get; set; } = Month.January;

    /// <inheritdoc/>
    public decimal? PeriodDayCount { get; set; }

    /// <inheritdoc/>
    public CalendarWeekRule? YearWeekRule { get; set; }

    /// <inheritdoc/>
    public DayOfWeek? FirstDayOfWeek { get; set; }

    /// <inheritdoc/>
    public CalendarWeekMode WeekMode { get; set; } = CalendarWeekMode.Week;

    /// <inheritdoc/>
    public bool WorkMonday { get; set; } = true;

    /// <inheritdoc/>
    public bool WorkTuesday { get; set; } = true;

    /// <inheritdoc/>
    public bool WorkWednesday { get; set; } = true;

    /// <inheritdoc/>
    public bool WorkThursday { get; set; } = true;

    /// <inheritdoc/>
    public bool WorkFriday { get; set; } = true;

    /// <inheritdoc/>
    public bool WorkSaturday { get; set; }

    /// <inheritdoc/>
    public bool WorkSunday { get; set; }

    /// <inheritdoc/>
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