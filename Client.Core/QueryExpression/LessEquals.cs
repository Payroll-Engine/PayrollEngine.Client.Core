namespace PayrollEngine.Client.QueryExpression;

/// <summary>Less than or equals filter expression</summary>
public class LessEquals : ConditionFilter
{
    /// <summary>Constructor</summary>
    /// <param name="field">The query field name</param>
    /// <param name="value">The query value</param>
    public LessEquals(string field, object value) :
        base(field, QuerySpecification.LessEqualsFilter, value)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The field function</param>
    /// <param name="value">The query value</param>
    public LessEquals(FunctionBase field, object value) :
        this(field.Expression, value)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The query field name</param>
    /// <param name="value">The expression function</param>
    public LessEquals(string field, FunctionBase value) :
        this(field, value.Expression)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The field function</param>
    /// <param name="value">The value function</param>
    public LessEquals(FunctionBase field, FunctionBase value) :
        this(field.Expression, value.Expression)
    {
    }
}