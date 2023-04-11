using System;

namespace PayrollEngine.Client.QueryExpression;

/// <summary>Object status expression</summary>
public class Status
{
    /// <summary>The status expression</summary>
    public string Expression { get; }

    /// <summary>Constructor</summary>
    /// <param name="status">The object status</param>
    public Status(ObjectStatus status)
    {
        Expression = Enum.GetName(status);
    }
}
