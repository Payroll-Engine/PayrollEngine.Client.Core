using System;

namespace PayrollEngine.Client.QueryExpression;

/// <summary>Query order by, default order is ascending</summary>
public class OrderBy
{
    /// <summary>The query order by expression</summary>
    public string Expression { get; }

    /// <summary>Constructor</summary>
    /// <param name="expression">The query order expression</param>
    /// <param name="direction">The order direction</param>
    public OrderBy(string expression, OrderDirection? direction = null)
    {
        if (string.IsNullOrWhiteSpace(expression))
        {
            throw new ArgumentException(nameof(expression));
        }

        if (direction == null)
        {
            Expression = expression;
        }
        else
            switch (direction.Value)
            {
                case OrderDirection.Ascending:
                    Expression = $"{expression} {QuerySpecification.OrderByAscending}";
                    break;
                case OrderDirection.Descending:
                    Expression = $"{expression} {QuerySpecification.OrderByDescending}";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        {
        }
    }

    /// <summary>And with another order by expression</summary>
    /// <param name="orderBy">The order by clause</param>
    /// <returns>Updated query order by</returns>
    public virtual OrderBy AndThenBy(OrderBy orderBy)
    {
        return new($"{Expression}, {orderBy.Expression}");
    }

    /// <summary>Implicit order by to string converter</summary>
    /// <param name="orderBy">The order by clause</param>
    public static implicit operator string(OrderBy orderBy) =>
        orderBy.Expression;

    /// <inheritdoc />
    public override string ToString() => Expression;
}