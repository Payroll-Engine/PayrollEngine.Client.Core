using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll client object</summary>
public class Payroll : ModelBase, IPayroll, INameObject
{
    /// <summary>The payroll name</summary>
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    public string Description { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <inheritdoc/>
    public int DivisionId { get; set; }

    /// <inheritdoc/>
    public string DivisionName { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string ClusterSetCase { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string ClusterSetCaseField { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string ClusterSetCollector { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string ClusterSetCollectorRetro { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string ClusterSetWageType { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string ClusterSetWageTypeRetro { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string ClusterSetCaseValue { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string ClusterSetWageTypePeriod { get; set; }

    /// <inheritdoc/>
    public List<ClusterSet> ClusterSets { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public Payroll()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public Payroll(Payroll copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IPayroll compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(IPayroll compare) =>
        string.Equals(Name, compare?.Name);

    /// <inheritdoc/>
    public override string GetUiString() => Name;
}