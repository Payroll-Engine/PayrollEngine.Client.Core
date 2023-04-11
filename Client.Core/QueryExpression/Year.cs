namespace PayrollEngine.Client.QueryExpression;

/// <summary>Year function expression</summary>
public class Year : FunctionBase
{
    /// <summary>Constructor</summary>
    /// <param name="expression">The query expression</param>
    public Year(string expression) :
        base($"{QuerySpecification.YearFunction}({expression})")
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="innerFunction">The inner function</param>
    public Year(FunctionBase innerFunction) :
        base(innerFunction)
    {
    }
}
