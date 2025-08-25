using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The case document client object</summary>
public class CaseDocument : ModelBase, ICaseDocument, INameObject
{
    /// <summary>The case document name</summary>
    [Required]
    [JsonPropertyOrder(100)]
    public string Name { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public string Content { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public string ContentFile { get; set; }

    /// <inheritdoc/>
    [Required]
    [StringLength(128)]
    [JsonPropertyOrder(103)]
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