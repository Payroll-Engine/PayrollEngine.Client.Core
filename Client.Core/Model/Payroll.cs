using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll client object</summary>
public class Payroll : ModelBase, IPayroll, INameObject
{
    /// <summary>The payroll name</summary>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(100)]
    public string Name { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public Dictionary<string, string> NameLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public string Description { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public int DivisionId { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public string DivisionName { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public PayrollClusterSets ClusterSet { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(107)]
    public List<ClusterSet> ClusterSets { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(108)]
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
