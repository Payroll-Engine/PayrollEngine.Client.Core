using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll case field client object</summary>
public class CaseField : Model, ICaseField, IEquatable<CaseField>
{
    /// <inheritdoc/>
    [Required]
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
    public bool Optional { get; set; }

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

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(CaseField compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Name} {base.ToString()}";
}