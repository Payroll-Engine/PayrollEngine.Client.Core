using System;

namespace PayrollEngine.Client.QueryExpression;

/// <summary>Equal status filter expression</summary>
public class EqualStatus : Equals
{
    /// <summary>Constructor</summary>
    /// <param name="status">The object status</param>
    public EqualStatus(ObjectStatus status) :
        base(QuerySpecification.StatusOperation, Enum.GetName(status))
    {
    }
}