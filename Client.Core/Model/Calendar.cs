using System.ComponentModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll calendar client object</summary>
public class Calendar : ModelBase, ICalendar, INameObject
{
    /// <summary>The calendar name</summary>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(100)]
    public string Name { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(102)]
    public CalendarTimeUnit CycleTimeUnit { get; set; } = CalendarTimeUnit.Year;

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(103)]
    public CalendarTimeUnit PeriodTimeUnit { get; set; } = CalendarTimeUnit.CalendarMonth;

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    [DefaultValue(CalendarTimeMap.Period)]
    public CalendarTimeMap TimeMap { get; set; } = CalendarTimeMap.Period;

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    [DefaultValue(Month.January)]
    public Month? FirstMonthOfYear { get; set; } = Month.January;

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public decimal? PeriodDayCount { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public CalendarWeekRule? YearWeekRule { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
    public DayOfWeek? FirstDayOfWeek { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(109)]
    public CalendarWeekMode WeekMode { get; set; } = CalendarWeekMode.Week;

    /// <inheritdoc/>
    [JsonPropertyOrder(110)]
    [DefaultValue(true)]
    public bool WorkMonday { get; set; } = true;

    /// <inheritdoc/>
    [JsonPropertyOrder(111)]
    [DefaultValue(true)]
    public bool WorkTuesday { get; set; } = true;

    /// <inheritdoc/>
    [JsonPropertyOrder(112)]
    [DefaultValue(true)]
    public bool WorkWednesday { get; set; } = true;

    /// <inheritdoc/>
    [JsonPropertyOrder(113)]
    [DefaultValue(true)]
    public bool WorkThursday { get; set; } = true;

    /// <inheritdoc/>
    [JsonPropertyOrder(114)]
    [DefaultValue(true)]
    public bool WorkFriday { get; set; } = true;

    /// <inheritdoc/>
    [JsonPropertyOrder(115)]
    public bool WorkSaturday { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(116)]
    public bool WorkSunday { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(117)]
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