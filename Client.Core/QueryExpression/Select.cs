using System;

namespace PayrollEngine.Client.QueryExpression;

/// <summary>Query select</summary>
public class Select
{
    /// <summary>The query select expression</summary>
    public string Expression { get; }

    /// <summary>Constructor</summary>
    /// <param name="fields">The query select fields</param>
    public Select(params string[] fields)
    {
        if (fields == null)
        {
            throw new ArgumentNullException(nameof(fields));
        }
        Expression = string.Join(',', fields);
    }

    /// <summary>Implicit select to string converter</summary>
    /// <param name="select">The select clause</param>
    public static implicit operator string(Select select) =>
        select.ToString();

    /// <inheritdoc />
    public override string ToString() => Expression;
}