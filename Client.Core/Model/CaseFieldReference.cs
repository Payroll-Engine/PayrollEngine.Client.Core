using System;

namespace PayrollEngine.Client.Model;

/// <summary>Case field reference</summary>
public class CaseFieldReference : IEquatable<CaseFieldReference>
{
    /// <summary>
    /// The case field name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The case field order
    /// </summary>
    public int? Order { get; set; }

    /// <summary>Initializes a new instance</summary>
    public CaseFieldReference()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CaseFieldReference(CaseFieldReference copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(CaseFieldReference compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() => Name;
}