using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll task object</summary>
public class Log : Model, ILog
{
    /// <inheritdoc/>
    public LogLevel Level { get; set; }

    /// <inheritdoc/>
    public string Message { get; set; }

    /// <inheritdoc/>
    public string User { get; set; }

    /// <inheritdoc/>
    public string Error { get; set; }

    /// <inheritdoc/>
    public string Comment { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string Owner { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string OwnerType { get; set; }

    /// <inheritdoc/>
    public Log()
    {
    }

    /// <inheritdoc/>
    public Log(ILog copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ILog compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public override string ToString() =>
        $"{Level}: {Message} {base.ToString()}";
}