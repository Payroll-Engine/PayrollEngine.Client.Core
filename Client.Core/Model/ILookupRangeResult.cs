using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>
/// Result of a lookup range bracket computation
/// </summary>
public interface ILookupRangeResult
{
    /// <summary>
    /// The lookup name
    /// </summary>
    // ReSharper disable once UnusedMemberInSuper.Global
    string LookupName { get; set; }

    /// <summary>
    /// The lookup range mode
    /// </summary>
    LookupRangeMode RangeMode { get; set; }

    /// <summary>
    /// The lookup range size
    /// </summary>
    decimal? RangeSize { get; set; }

    /// <summary>
    /// Range brackets
    /// </summary>
    List<LookupRangeBracket> Brackets { get; set; }
}