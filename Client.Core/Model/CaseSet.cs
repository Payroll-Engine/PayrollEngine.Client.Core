using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll derived case</summary>
public class CaseSet : Case, ICaseSet
{
    /// <inheritdoc/>
    [StringLength(128)]
    public string DisplayName { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string CaseSlot { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> CaseSlotLocalizations { get; set; }

    /// <inheritdoc/>
    public DateTime? CancellationDate { get; set; }

    /// <inheritdoc/>
    public List<CaseFieldSet> Fields { get; set; }

    /// <inheritdoc/>
    public List<CaseSet> RelatedCases { get; set; }

    /// <summary>Initializes a new instance</summary>
    public CaseSet()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CaseSet(CaseSet copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CaseSet(Case copySource) :
        base(copySource)
    {
    }

    /// <inheritdoc/>
    public virtual bool Equals(ICaseSet compare) =>
        CompareTool.EqualProperties(this, compare);
}