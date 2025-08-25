using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll task object</summary>
public class Log : ModelBase, ILog
{
    /// <inheritdoc/>
    [JsonPropertyOrder(100)]
    public LogLevel Level { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(101)]
    public string Message { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public string User { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(103)]
    public string Error { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public string Comment { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(105)]
    public string Owner { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    [JsonPropertyOrder(106)]
    public string OwnerType { get; set; }

    /// <inheritdoc/>
    public Log()
    {
    }

    /// <inheritdoc/>
    public Log(Log copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ILog compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public override string GetUiString() => Message;

    /// <inheritdoc/>
    public override string ToString() =>
        $"{Level}: {Message} {base.ToString()}";
}