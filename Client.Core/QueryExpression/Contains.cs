namespace PayrollEngine.Client.QueryExpression;

/// <summary>Contains filter expression</summary>
public class Contains : Filter
{
    /// <summary>Constructor</summary>
    /// <param name="field">The query field name</param>
    /// <param name="expression">The query expression</param>
    public Contains(string field, string expression) :
        base($"{QuerySpecification.ContainsFunction}({field},'{expression}')")
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The field function</param>
    /// <param name="expression">The query expression</param>
    public Contains(FunctionBase field, string expression) :
        this(field.Expression, expression)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The query field name</param>
    /// <param name="value">The expression function</param>
    public Contains(string field, FunctionBase value) :
        this(field, value.Expression)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The field function</param>
    /// <param name="value">The value function</param>
    public Contains(FunctionBase field, FunctionBase value) :
        this(field.Expression, value.Expression)
    {
    }
}
