using System;

namespace PayrollEngine.Client.Model;

/// <summary>Lookup value date in a specific language</summary>
public class LookupValueData : IEquatable<LookupValueData>
{
    /// <summary>The lookup key</summary>
    public string Key { get; set; }

    /// <summary>The lookup value as JSON</summary>
    public string Value { get; set; }

    /// <summary>The lookup range value</summary>
    public decimal? RangeValue { get; set; }

    /// <summary>Initializes a new instance</summary>
    public LookupValueData()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public LookupValueData(LookupValueData copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(LookupValueData compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Key} {Value}";
}