using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payrun client object</summary>
public class Payrun : Model, IPayrun
{
    /// <inheritdoc/>
    public int PayrollId { get; set; }

    /// <inheritdoc/>
    [Required]
    public string PayrollName { get; set; }

    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    /// <inheritdoc/>
    public string DefaultReason { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> DefaultReasonLocalizations { get; set; }

    /// <inheritdoc/>
    public string StartExpression { get; set; }

    /// <inheritdoc/>
    public string StartExpressionFile { get; set; }

    /// <inheritdoc/>
    public string EmployeeAvailableExpression { get; set; }

    /// <inheritdoc/>
    public string EmployeeAvailableExpressionFile { get; set; }

    /// <inheritdoc/>
    public string EmployeeStartExpression { get; set; }

    /// <inheritdoc/>
    public string EmployeeStartExpressionFile { get; set; }

    /// <inheritdoc/>
    public string EmployeeEndExpression { get; set; }

    /// <inheritdoc/>
    public string EmployeeEndExpressionFile { get; set; }

    /// <inheritdoc/>
    public string WageTypeAvailableExpression { get; set; }

    /// <inheritdoc/>
    public string WageTypeAvailableExpressionFile { get; set; }

    /// <inheritdoc/>
    public string EndExpression { get; set; }

    /// <inheritdoc/>
    public string EndExpressionFile { get; set; }

    /// <inheritdoc/>
    public RetroTimeType RetroTimeType { get; set; }

    /// <inheritdoc/>
    public CalendarConfiguration Calendar { get; set; }

    /// <summary>Initializes a new instance</summary>
    public Payrun()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public Payrun(IPayrun copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IPayrun compare) =>
        CompareTool.EqualProperties(this, compare);
    
    /// <inheritdoc/>
    public virtual bool EqualKey(IPayrun compare) =>
        string.Equals(PayrollName, compare?.PayrollName) &&
        string.Equals(Name, compare?.Name);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Name} {base.ToString()}";
}