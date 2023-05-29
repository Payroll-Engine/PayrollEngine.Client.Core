namespace PayrollEngine.Client.QueryExpression;

/// <summary>Equal name filter expression</summary>
public class EqualName : Equals
{
    /// <summary>Constructor</summary>
    /// <param name="value">The query name</param>
    public EqualName(string value) :
        base("Name", value)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="value">The value function</param>
    public EqualName(FunctionBase value) :
        this(value?.Expression)
    {
    }
}