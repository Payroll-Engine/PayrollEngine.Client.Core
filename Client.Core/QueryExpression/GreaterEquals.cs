namespace PayrollEngine.Client.QueryExpression;

/// <summary>Greater than or equals filter expression</summary>
public class GreaterEquals : ConditionFilter
{
    /// <summary>Constructor</summary>
    /// <param name="field">The query field name</param>
    /// <param name="value">The query value</param>
    public GreaterEquals(string field, object value) :
        base(field, QuerySpecification.GreaterEqualsFilter, value)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The field function</param>
    /// <param name="value">The query value</param>
    public GreaterEquals(FunctionBase field, object value) :
        this(field.Expression, value)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The query field name</param>
    /// <param name="value">The expression function</param>
    public GreaterEquals(string field, FunctionBase value) :
        this(field, value?.Expression)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The field function</param>
    /// <param name="value">The value function</param>
    public GreaterEquals(FunctionBase field, FunctionBase value) :
        this(field.Expression, value?.Expression)
    {
    }
}