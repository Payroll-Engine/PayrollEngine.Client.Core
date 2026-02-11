// ReSharper disable UnusedMemberInSuper.Global
namespace PayrollEngine.Client.Model;

/// <summary>
/// A lookup range bracket with computed bounds
/// </summary>
public interface ILookupRangeBracket
{
    /// <summary>
    /// The lookup value key
    /// </summary>
    string Key { get; set; }

    /// <summary>
    /// The lookup value as JSON
    /// </summary>
    string Value { get; set; }

    /// <summary>
    /// The range start value
    /// </summary>
    decimal RangeStart { get; set; }

    /// <summary>
    /// The range end value (unbound bracket: Decimal.MaxValue)
    /// </summary>
    decimal RangeEnd { get; set; }

    /// <summary>
    /// The brackets range value
    /// </summary>
    /// <remarks>
    /// Threshold lookup: value within the matching bracket.
    /// Progressive lookup: matching bracket value range, except the last one which the value within his range.
    /// Other: null.
    /// </remarks>
    decimal? RangeValue { get; set; }
}