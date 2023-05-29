namespace PayrollEngine.Client.QueryExpression;

/// <summary>Not equals filter expression</summary>
public class NotEquals : ConditionFilter
{
    /// <summary>Constructor</summary>
    /// <param name="field">The query field name</param>
    /// <param name="value">The query value</param>
    public NotEquals(string field, object value) :
        base(field, QuerySpecification.NotEqualsFilter, value)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The field function</param>
    /// <param name="value">The query value</param>
    public NotEquals(FunctionBase field, object value) :
        this(field.Expression, value)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The query field name</param>
    /// <param name="value">The expression function</param>
    public NotEquals(string field, FunctionBase value) :
        this(field, value?.Expression)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The field function</param>
    /// <param name="value">The value function</param>
    public NotEquals(FunctionBase field, FunctionBase value) :
        this(field.Expression, value?.Expression)
    {
    }
}