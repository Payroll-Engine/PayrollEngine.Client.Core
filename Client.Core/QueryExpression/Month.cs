namespace PayrollEngine.Client.QueryExpression;

/// <summary>Month function expression</summary>
public class Month : FunctionBase
{
    /// <summary>Constructor</summary>
    /// <param name="expression">The query expression</param>
    public Month(string expression) :
        base($"{QuerySpecification.MonthFunction}({expression})")
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="innerFunction">The inner function</param>
    public Month(FunctionBase innerFunction) :
        base(innerFunction)
    {
    }
}
