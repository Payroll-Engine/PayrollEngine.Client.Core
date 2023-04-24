using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>Lookup including the lookup value</summary>
public class LookupSet : Lookup, ILookupSet
{
    /// <inheritdoc/>
    [Required]
    public List<LookupValue> Values { get; set; }

    /// <summary>Initializes a new instance</summary>
    public LookupSet()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public LookupSet(ILookupSet copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ILookupSet compare) =>
        CompareTool.EqualProperties(this, compare);
}