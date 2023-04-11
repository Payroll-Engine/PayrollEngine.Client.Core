using System;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The case document client object</summary>
public class CaseDocument : Model, ICaseDocument, IEquatable<CaseDocument>
{
    /// <inheritdoc/>
    [Required]
    public string Name { get; set; }

    /// <inheritdoc/>
    public string Content { get; set; }

    /// <inheritdoc/>
    public string ContentFile { get; set; }

    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    public string ContentType { get; set; }

    /// <summary>Initializes a new instance of the <see cref="CaseDocument"/> class</summary>
    public CaseDocument()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="CaseDocument"/> class</summary>
    /// <param name="copySource">The copy source.</param>
    public CaseDocument(CaseDocument copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(CaseDocument compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Name} {base.ToString()}";
}