
namespace PayrollEngine.Client.Model;

/// <summary>Numeric value result</summary>
public interface INumericValueResult
{
    /// <summary>The collector custom result value (immutable)</summary>
    /// <remarks>Nullable for tests</remarks>
    decimal? Value { get; set; }

    /// <summary>Test if value is almost equal value using a test precision</summary>
    /// <param name="compare">The value to compare</param>
    /// <param name="precision">The test precision</param>
    /// <returns>True for almost equal values</returns>
    bool AlmostEqualValue(decimal? compare, int precision);
}