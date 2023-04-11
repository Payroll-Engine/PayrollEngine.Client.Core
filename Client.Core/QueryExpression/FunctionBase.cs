
namespace PayrollEngine.Client.QueryExpression;

/// <summary>Query function base</summary>
public class FunctionBase : ExpressionBase
{
    /// <summary>Constructor</summary>
    /// <param name="expression">The function expression</param>
    protected FunctionBase(string expression) :
        base(expression)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="innerFunction">The inner function</param>
    protected FunctionBase(FunctionBase innerFunction) :
        this(innerFunction.Expression)
    {
    }
}