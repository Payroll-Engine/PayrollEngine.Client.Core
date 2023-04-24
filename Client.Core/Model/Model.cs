using System;

namespace PayrollEngine.Client.Model;

/// <summary>Base for all Payroll models</summary>
public abstract class Model : IModel
{
    /// <inheritdoc/>
    public int Id { get; set; }

    /// <inheritdoc/>
    public bool IsExistingObject => Id != 0;

    /// <inheritdoc/>
    public bool IsNewObject => !IsExistingObject;

    /// <inheritdoc/>
    public ObjectStatus Status { get; set; }

    /// <inheritdoc/>
    public DateTime Created { get; set; } = Date.MinValue;

    /// <inheritdoc/>
    public DateTime Updated { get; set; } = Date.MinValue;

    /// <inheritdoc/>
    public UpdateMode UpdateMode { get; set; }

    /// <summary>Initializes a new instance</summary>
    protected Model()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    protected Model(IModel copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"[#{Id}]";
}