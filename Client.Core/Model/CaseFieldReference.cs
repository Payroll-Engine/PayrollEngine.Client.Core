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

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(CaseFieldReference compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() => Name;
}