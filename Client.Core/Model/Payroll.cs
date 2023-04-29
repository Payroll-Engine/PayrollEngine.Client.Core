using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PayrollEngine.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll client object</summary>
public class Payroll : Model, IPayroll, INameObject
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
    [JsonConverter(typeof(StringNullableEnumConverter<CalendarCalculationMode>))]
    public CalendarCalculationMode CalendarCalculationMode { get; set; }

    /// <inheritdoc/>
    public Country? CountryName { get; set; }

    /// <inheritdoc/>
    public int Country { get; set; }

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
    public Payroll(IPayroll copySource) :
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

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Name} {base.ToString()}";
}