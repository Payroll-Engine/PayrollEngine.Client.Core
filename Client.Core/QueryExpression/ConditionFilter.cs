using System;
using System.Globalization;

namespace PayrollEngine.Client.QueryExpression;

/// <summary>Condition filter</summary>
public abstract class ConditionFilter : Filter
{
    /// <summary>The query field name</summary>
    public string Field { get; }

    /// <summary>The query operation</summary>
    public string Operator { get; }

    /// <summary>The query field name</summary>
    public object Value { get; }

    /// <summary>Constructor</summary>
    /// <param name="field">The query field name</param>
    /// <param name="operator">The filter operation</param>
    /// <param name="value">The query value</param>
    protected ConditionFilter(string field, string @operator, object value) :
        base($"{field} {@operator} {GetFilterValue(value)}")
    {
        if (string.IsNullOrWhiteSpace(field))
        {
            throw new ArgumentException(nameof(field));
        }
        if (string.IsNullOrWhiteSpace(@operator))
        {
            throw new ArgumentException(nameof(@operator));
        }
        Field = field;
        Operator = @operator;
        Value = value;
    }

    private static string GetFilterValue(object value) =>
        value switch
        {
            null => null,
            string => $"'{value}'",
            DateTime dateTime => $"'{dateTime.ToUtcString(CultureInfo.InvariantCulture)}'",
            _ => value.ToString()
        };
}