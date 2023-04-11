namespace PayrollEngine.Client.QueryExpression;

/// <summary>Hour function expression</summary>
public class Hour : FunctionBase
{
    /// <summary>Constructor</summary>
    /// <param name="expression">The query expression</param>
    public Hour(string expression) :
        base($"{QuerySpecification.HourFunction}({expression})")
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="innerFunction">The inner function</param>
    public Hour(FunctionBase innerFunction) :
        base(innerFunction)
    {
    }
}
