namespace PayrollEngine.Client.QueryExpression;

/// <summary>Equal identifier filter expression</summary>
public class EqualIdentifier : Equals
{
    /// <summary>Constructor</summary>
    /// <param name="identifier">The query identifier</param>
    public EqualIdentifier(string identifier) :
        base("Identifier", identifier)
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="identifier">The identifier function</param>
    public EqualIdentifier(FunctionBase identifier) :
        this(identifier.Expression)
    {
    }
}