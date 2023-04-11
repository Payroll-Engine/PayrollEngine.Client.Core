namespace PayrollEngine.Client.QueryExpression;

/// <summary>Ends with filter expression</summary>
public class EndsWith : Filter
{
    /// <summary>Constructor</summary>
    /// <param name="field">The query field name</param>
    /// <param name="expression">The query expression</param>
    public EndsWith(string field, string expression) :
        base($"{QuerySpecification.EndsWithFunction}({field},'{expression}')")
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The field function</param>
    /// <param name="expression">The query expression</param>
    public EndsWith(FunctionBase field, string expression) :
        this(field.Expression, expression)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The query field name</param>
    /// <param name="value">The expression function</param>
    public EndsWith(string field, FunctionBase value) :
        this(field, value.Expression)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The field function</param>
    /// <param name="value">The value function</param>
    public EndsWith(FunctionBase field, FunctionBase value) :
        this(field.Expression, value.Expression)
    {
    }
}
