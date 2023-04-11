
namespace PayrollEngine.Client.Exchange;

/// <summary>Extension methods for <see cref="Exchange"/></summary>
public static class ExchangeExtensions
{
    /// <summary>Append the given object query key and value to the URI</summary>
    /// <param name="exchange">The exchange model</param>
    /// <param name="namespace">The target namespace name</param>
    /// <returns>The combined result</returns>
    public static void ChangeNamespace(this Model.Exchange exchange, string @namespace) =>
        new NamespaceUpdateTool(exchange, @namespace).UpdateNamespace();
}