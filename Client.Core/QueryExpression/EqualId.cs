namespace PayrollEngine.Client.QueryExpression;

/// <summary>Equal id filter expression</summary>
public class EqualId : Equals
{
    /// <summary>Constructor</summary>
    /// <param name="value">The query id</param>
    public EqualId(int value) :
        base("Id", value)
    {
    }
}