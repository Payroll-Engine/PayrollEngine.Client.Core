using System;

namespace PayrollEngine.Client;

/// <summary>Script publish exception</summary>
public class ScriptPublishException : PayrollException
{
    /// <inheritdoc/>
    public ScriptPublishException()
    {
    }

    /// <inheritdoc/>
    public ScriptPublishException(string message) :
        base(message)
    {
    }

    /// <inheritdoc/>
    public ScriptPublishException(string message, Exception innerException) :
        base(message, innerException)
    {
    }
}