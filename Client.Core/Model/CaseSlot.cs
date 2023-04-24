using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>Case slot</summary>
public class CaseSlot : IEquatable<CaseSlot>
{
    /// <summary>The case slot name</summary>
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    /// <summary>The localized case slot names</summary>
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <summary>Initializes a new instance</summary>
    public CaseSlot()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CaseSlot(CaseSlot copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(CaseSlot compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() => Name;
}