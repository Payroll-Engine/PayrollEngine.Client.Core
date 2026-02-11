using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>
/// Result of a lookup range bracket computation
/// </summary>
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
public class LookupRangeResult : ILookupRangeResult
{
    /// <inheritdoc/>
    public string LookupName { get; set; }

    /// <inheritdoc/>
    public LookupRangeMode RangeMode { get; set; }

    /// <inheritdoc/>
    public decimal? RangeSize { get; set; }

    /// <inheritdoc/>
    public List<LookupRangeBracket> Brackets { get; set; }

    /// <inheritdoc/>
    public override string ToString() =>
        LookupName;
}