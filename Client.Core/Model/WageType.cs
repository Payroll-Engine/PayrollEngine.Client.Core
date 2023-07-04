using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll wage type client object</summary>
public class WageType : ModelBase, IWageType, INameObject
{
    /// <inheritdoc/>
    [Required]
    public decimal WageTypeNumber { get; set; }

    /// <summary>The wage type name</summary>
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
    public OverrideType OverrideType { get; set; }

    /// <inheritdoc/>
    public ValueType ValueType { get; set; } = ValueType.Money;

    /// <inheritdoc/>
    public string Calendar { get; set; }

    /// <inheritdoc/>
    public List<string> Collectors { get; set; }

    /// <inheritdoc/>
    public List<string> CollectorGroups { get; set; }

    /// <inheritdoc/>
    public string ValueExpression { get; set; }

    /// <inheritdoc/>
    public string ValueExpressionFile { get; set; }

    /// <inheritdoc/>
    public string ResultExpression { get; set; }

    /// <inheritdoc/>
    public string ResultExpressionFile { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, object> Attributes { get; set; }

    /// <inheritdoc/>
    public List<string> Clusters { get; set; }

    /// <summary>Initializes a new instance</summary>
    public WageType()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public WageType(WageType copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IWageType compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(IWageType compare) =>
        WageTypeNumber == compare.WageTypeNumber;
    
    /// <inheritdoc/>
    public override string GetUiString() => 
        $"{Name} [{WageTypeNumber:##.####}]";
}