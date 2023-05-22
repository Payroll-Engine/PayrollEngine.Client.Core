using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The case document client object</summary>
public class CaseDocument : Model, ICaseDocument, INameObject
{
    /// <summary>The case document name</summary>
    [Required]
    public string Name { get; set; }

    /// <inheritdoc/>
    [Required]
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

    /// <inheritdoc/>
    public virtual bool Equals(ICaseDocument compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public override string GetUiString() => Name;
}