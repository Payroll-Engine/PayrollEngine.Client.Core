namespace PayrollEngine.Client.QueryExpression;

/// <summary>Greater than filter expression</summary>
public class Greater : ConditionFilter
{
    /// <summary>Constructor</summary>
    /// <param name="field">The query field name</param>
    /// <param name="value">The query value</param>
    public Greater(string field, object value) :
        base(field, QuerySpecification.GreaterFilter, value)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The field function</param>
    /// <param name="value">The query value</param>
    public Greater(FunctionBase field, object value) :
        this(field.Expression, value)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The query field name</param>
    /// <param name="value">The expression function</param>
    public Greater(string field, FunctionBase value) :
        this(field, value.Expression)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="field">The field function</param>
    /// <param name="value">The value function</param>
    public Greater(FunctionBase field, FunctionBase value) :
        this(field.Expression, value.Expression)
    {
    }
}