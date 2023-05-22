using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll case field client object</summary>
public class CaseField : Model, ICaseField, INameObject
{
    /// <summary>The case field name</summary>
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
    public ValueType ValueType { get; set; }

    /// <inheritdoc/>
    public ValueScope ValueScope { get; set; }

    /// <inheritdoc/>
    public CaseFieldTimeType TimeType { get; set; }

    /// <inheritdoc/>
    public CaseFieldTimeUnit TimeUnit { get; set; }

    /// <inheritdoc/>
    public OverrideType OverrideType { get; set; }

    /// <inheritdoc/>
    public CaseFieldCancellationMode CancellationMode { get; set; }

    /// <inheritdoc/>
    public CaseValueCreationMode ValueCreationMode { get; set; }

    /// <inheritdoc/>
    public bool ValueMandatory { get; set; }

    /// <inheritdoc/>
    public int Order { get; set; }

    /// <inheritdoc/>
    public CaseFieldDateType StartDateType { get; set; }

    /// <inheritdoc/>
    public CaseFieldDateType EndDateType { get; set; }

    /// <inheritdoc/>
    public bool EndMandatory { get; set; }

    /// <inheritdoc/>
    public string DefaultStart { get; set; }

    /// <inheritdoc/>
    public string DefaultEnd { get; set; }

    /// <inheritdoc/>
    public string DefaultValue { get; set; }

    /// <inheritdoc/>
    public List<string> Tags { get; set; }

    /// <inheritdoc/>
    public LookupSettings LookupSettings { get; set; }

    /// <inheritdoc/>
    public List<string> Clusters { get; set; }

    /// <inheritdoc/>
    public List<string> BuildActions { get; set; }

    /// <inheritdoc/>
    public List<string> ValidateActions { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Custom value attributes</summary>
    public Dictionary<string, object> ValueAttributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public CaseField()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public CaseField(CaseField copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ICaseField compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(ICaseField compare) =>
        string.Equals(Name, compare?.Name);

    /// <inheritdoc/>
    public override string GetUiString() => Name;
}