using System;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The lookup settings</summary>
public class LookupSettings : IEquatable<LookupSettings>
{
    /// <summary>The lookup name</summary>
    [Required]
    public string LookupName { get; set; }

    /// <summary>The lookup value field name</summary>
    public string ValueFieldName { get; set; }

    /// <summary>The lookup text/display field name</summary>
    public string TextFieldName { get; set; }

    /// <summary>Initializes a new instance</summary>
    public LookupSettings()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="source">The copy source</param>
    public LookupSettings(LookupSettings source)
    {
        LookupName = source.LookupName;
        ValueFieldName = source.ValueFieldName;
        TextFieldName = source.TextFieldName;
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(LookupSettings compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        LookupName;
}