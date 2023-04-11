
namespace PayrollEngine.Client.QueryExpression;

/// <summary>Equal active status filter expression</summary>
public class ActiveStatus : EqualStatus
{
    /// <summary>Constructor</summary>
    public ActiveStatus() :
        base(ObjectStatus.Active)
    {
    }
}
