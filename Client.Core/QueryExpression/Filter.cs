
namespace PayrollEngine.Client.QueryExpression;

/// <summary>Query filter</summary>
public class Filter : ExpressionBase
{
    /// <summary>Constructor</summary>
    /// <param name="expression">The filter expression</param>
    public Filter(string expression) :
        base(expression)
    {
    }

    /// <summary>Group existing filters</summary>
    /// <returns>Updated query filter</returns>
    public virtual Filter Group()
    {
        if (Expression.StartsWith('(') && Expression.EndsWith(')'))
        {
            return this;
        }
        return new($"({Expression})");
    }

    /// <summary>And with another filter expression</summary>
    /// <returns>Updated query filter</returns>
    public virtual Filter And(Filter expression)
    {
        return new($"{Group()} and {expression.Group()}");
    }

    /// <summary>Or with another filter expression</summary>
    /// <returns>Updated query filter</returns>
    public virtual Filter Or(Filter expression)
    {
        return new($"{Expression} or {expression.Expression}");
    }
}