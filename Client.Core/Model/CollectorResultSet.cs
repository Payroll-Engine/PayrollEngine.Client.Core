using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The collector result set client object</summary>
public class CollectorResultSet : CollectorResult, ICollectorResultSet
{
    /// <inheritdoc/>
    [JsonPropertyOrder(200)]
    public List<CollectorCustomResult> CustomResults { get; set; }

    /// <summary>Initializes a new instance</summary>
    public CollectorResultSet()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CollectorResultSet(CollectorResult copySource) :
        base(copySource)
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CollectorResultSet(CollectorResultSet copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ICollectorResultSet compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{CustomResults?.Count} periods {base.ToString()}";
}