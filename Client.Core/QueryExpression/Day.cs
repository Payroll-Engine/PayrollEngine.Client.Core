namespace PayrollEngine.Client.QueryExpression;

/// <summary>Day function expression</summary>
public class Day : FunctionBase
{
    /// <summary>Constructor</summary>
    /// <param name="expression">The query expression</param>
    public Day(string expression) :
        base($"{QuerySpecification.DayFunction}({expression})")
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="innerFunction">The inner function</param>
    public Day(FunctionBase innerFunction) :
        base(innerFunction)
    {
    }
}
