namespace PayrollEngine.Client.Script;

/// <summary>
/// Collector script parser
/// </summary>
public interface ICollectorScriptParser
{
    /// <summary>Gets the collector start script</summary>
    /// <param name="query">The script code query</param>
    /// <param name="regulationName">Name of the regulation</param>
    /// <param name="collectorName">Name of the collector</param>
    /// <returns>The collector end script</returns>
    string GetCollectorStartScript(ScriptCodeQuery query, string regulationName, string collectorName);

    /// <summary>Gets the collector apply script</summary>
    /// <param name="query">The script code query</param>
    /// <param name="regulationName">Name of the regulation</param>
    /// <param name="collectorName">Name of the collector</param>
    /// <returns>The collector apply script</returns>
    string GetCollectorApplyScript(ScriptCodeQuery query, string regulationName, string collectorName);

    /// <summary>Gets the collector end script</summary>
    /// <param name="query">The script code query</param>
    /// <param name="regulationName">Name of the regulation</param>
    /// <param name="collectorName">Name of the collector</param>
    /// <returns>The collector end script</returns>
    string GetCollectorEndScript(ScriptCodeQuery query, string regulationName, string collectorName);
}