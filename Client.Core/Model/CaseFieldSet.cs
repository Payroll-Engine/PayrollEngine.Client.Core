using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The case field including the slot and values</summary>
public class CaseFieldSet : CaseField, ICaseFieldSet
{
    /// <inheritdoc/>
    [StringLength(128)]
    public string DisplayName { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string CaseSlot { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> CaseSlotLocalizations { get; set; }

    /// <inheritdoc/>
    public string Value { get; set; }

    /// <inheritdoc/>
    public DateTime? Start { get; set; }

    /// <inheritdoc/>
    public DateTime? End { get; set; }

    /// <inheritdoc/>
    public DateTime? CancellationDate { get; set; }

    /// <summary>Test for existing value</summary>
    [JsonIgnore]
    public bool HasValue => !string.IsNullOrWhiteSpace(Value);

    /// <summary>Initializes a new instance</summary>
    public CaseFieldSet()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CaseFieldSet(CaseFieldSet copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public CaseFieldSet(CaseField caseField) :
        base(caseField)
    {
    }

    /// <inheritdoc/>
    public virtual bool Equals(ICaseFieldSet compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Get native value</summary>
    /// <param name="culture">The culture</param>
    /// <returns>The .net value</returns>
    public virtual object GetValue(CultureInfo culture) =>
        ValueConvert.ToValue(Value, ValueType, culture);

    /// <summary>Set native value</summary>
    public virtual void SetValue(object value) =>
        Value = ValueConvert.ToJson(value);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        !string.IsNullOrWhiteSpace(Value) ? $" ({Value}) {base.ToString()}" : base.ToString();
}