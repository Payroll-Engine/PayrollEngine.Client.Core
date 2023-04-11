using System;

namespace PayrollEngine.Client.QueryExpression;

/// <summary>Query expression base</summary>
public abstract class ExpressionBase
{
    /// <summary>The query expression</summary>
    public string Expression { get; }

    /// <summary>Constructor</summary>
    /// <param name="expression">The query expression</param>
    protected ExpressionBase(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
        {
            throw new ArgumentException(nameof(expression));
        }
        Expression = expression;
    }
    
    /// <summary>Implicit function to string converter</summary>
    /// <param name="function">The function</param>
    public static implicit operator string(ExpressionBase function) =>
        function.Expression;

    /// <inheritdoc />
    public override string ToString() => Expression;
}
