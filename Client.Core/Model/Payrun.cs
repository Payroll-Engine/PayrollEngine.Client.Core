﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payrun client object</summary>
public class Payrun : Model, IPayrun, IEquatable<Payrun>
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
    public Payrun(Payrun copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(Payrun compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Name} {base.ToString()}";
}