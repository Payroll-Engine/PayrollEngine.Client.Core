
namespace PayrollEngine.Client.QueryExpression;

/// <summary>Equal inactive status filter expression</summary>
public class InactiveStatus : EqualStatus
{
    /// <summary>Constructor</summary>
    public InactiveStatus() :
        base(ObjectStatus.Inactive)
    {
    }
}
