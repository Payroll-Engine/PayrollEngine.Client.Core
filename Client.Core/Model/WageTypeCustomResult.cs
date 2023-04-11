﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The wage type custom result client object</summary>
public class WageTypeCustomResult : Model, IWageTypeCustomResult, IEquatable<WageTypeCustomResult>
{
    /// <inheritdoc/>
    public int WageTypeResultId { get; set; }

    /// <inheritdoc/>
    [Required]
    public decimal WageTypeNumber { get; set; }

    /// <inheritdoc/>
    public string WageTypeName { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string> WageTypeNameLocalizations { get; set; }

    /// <inheritdoc/>
    [Required]
    public string Source { get; set; }

    /// <inheritdoc/>
    public ValueType ValueType { get; set; }

    /// <inheritdoc/>
    public decimal Value { get; set; }

    /// <inheritdoc/>
    public DateTime Start { get; set; }

    /// <inheritdoc/>
    public DateTime End { get; set; }

    /// <inheritdoc/>
    public List<string> Tags { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public WageTypeCustomResult()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public WageTypeCustomResult(WageTypeCustomResult copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(WageTypeCustomResult compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Source}={Value} [{Start}-{End}] {base.ToString()}";
}