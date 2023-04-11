namespace PayrollEngine.Client.QueryExpression;

/// <summary>Minute function expression</summary>
public class Minute : FunctionBase
{
    /// <summary>Constructor</summary>
    /// <param name="expression">The query expression</param>
    public Minute(string expression) :
        base($"{QuerySpecification.MinuteFunction}({expression})")
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="innerFunction">The inner function</param>
    public Minute(FunctionBase innerFunction) :
        base(innerFunction)
    {
    }
}
