using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll case value client object (immutable)</summary>
public class CaseValueSetup : CaseValue, ICaseValueSetup
{

    /// <inheritdoc/>
    public List<CaseDocument> Documents { get; set; }

    /// <summary>Initializes a new instance</summary>
    public CaseValueSetup()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CaseValueSetup(CaseValueSetup copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ICaseValueSetup compare) =>
        CompareTool.EqualProperties(this, compare);
}