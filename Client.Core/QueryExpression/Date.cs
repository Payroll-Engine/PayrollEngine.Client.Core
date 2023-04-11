namespace PayrollEngine.Client.QueryExpression;

/// <summary>Date function expression</summary>
public class Date : FunctionBase
{
    /// <summary>Constructor</summary>
    /// <param name="expression">The query expression</param>
    public Date(string expression) :
        base($"{QuerySpecification.DateFunction}({expression})")
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="innerFunction">The inner function</param>
    public Date(FunctionBase innerFunction) :
        base(innerFunction)
    {
    }
}
