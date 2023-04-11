using System;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll task object</summary>
public class Log : Model, ILog, IEquatable<Log>
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
    public Log(Log copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(Log compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public override string ToString() =>
        $"{Level}: {Message} {base.ToString()}";
}