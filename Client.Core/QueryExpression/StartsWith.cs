namespace PayrollEngine.Client.QueryExpression;

/// <summary>Starts with filter expression</summary>
public class StartsWith : Filter
{
    /// <summary>Constructor</summary>
    /// <param name="field">The query field name</param>
    /// <param name="expression">The query expression</param>
    public StartsWith(string field, string expression) :
        base($"{QuerySpecification.StartsWithFunction}({field},'{expression}')")
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The field function</param>
    /// <param name="expression">The query expression</param>
    public StartsWith(FunctionBase field, string expression) :
        this(field.Expression, expression)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The query field name</param>
    /// <param name="value">The expression function</param>
    public StartsWith(string field, FunctionBase value) :
        this(field, value.Expression)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The field function</param>
    /// <param name="value">The value function</param>
    public StartsWith(FunctionBase field, FunctionBase value) :
        this(field.Expression, value.Expression)
    {
    }
}
