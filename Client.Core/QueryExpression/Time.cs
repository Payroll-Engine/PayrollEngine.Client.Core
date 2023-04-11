namespace PayrollEngine.Client.QueryExpression;

/// <summary>Time function expression</summary>
public class Time : FunctionBase
{
    /// <summary>Constructor</summary>
    /// <param name="expression">The query expression</param>
    public Time(string expression) :
        base($"{QuerySpecification.TimeFunction}({expression})")
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="innerFunction">The inner function</param>
    public Time(FunctionBase innerFunction) :
        base(innerFunction)
    {
    }
}
