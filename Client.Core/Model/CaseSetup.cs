using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>Payroll case value change client object</summary>
public class CaseSetup : ModelBase, ICaseSetup
{
    /// <inheritdoc/>
    [Required]
    public string CaseName { get; set; }

    /// <inheritdoc/>
    public string CaseSlot { get; set; }

    /// <inheritdoc/>
    public List<CaseValueSetup> Values { get; set; }

    /// <inheritdoc/>
    public List<CaseSetup> RelatedCases { get; set; }

    /// <summary>Initializes a new instance</summary>
    public CaseSetup()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CaseSetup(CaseSetup copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ICaseSetup compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public override string GetUiString() =>
        string.IsNullOrWhiteSpace(CaseSlot) ? CaseName : $"{CaseSlot} [{CaseSlot}]";
}