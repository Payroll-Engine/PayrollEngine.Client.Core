using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll case value client object (immutable)</summary>
public class CaseValueSetup : CaseValue, ICaseValueSetup, IEquatable<CaseValueSetup>
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

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(CaseValueSetup compare) =>
        CompareTool.EqualProperties(this, compare);
}