namespace PayrollEngine.Client.Script;

/// <summary>
/// Payrun script parser
/// </summary>
public interface IPayrunScriptParser
{
    /// <summary>Gets the payrun start script</summary>
    /// <param name="query">The script code query</param>
    /// <param name="payrunName">Name of the payrun</param>
    /// <returns>The payrun start script</returns>
    string GetPayrunStartScript(ScriptCodeQuery query, string payrunName);

    /// <summary>Gets the payrun wage type available script</summary>
    /// <param name="query">The script code query</param>
    /// <param name="payrunName">Name of the payrun</param>
    /// <returns>The payrun wage type available script</returns>
    string GetPayrunWageTypeAvailableScript(ScriptCodeQuery query, string payrunName);

    /// <summary>Gets the payrun employee available script</summary>
    /// <param name="query">The script code query</param>
    /// <param name="payrunName">Name of the payrun</param>
    /// <returns>The payrun employee available script</returns>
    string GetPayrunEmployeeAvailableScript(ScriptCodeQuery query, string payrunName);

    /// <summary>Gets the payrun employee start script</summary>
    /// <param name="query">The script code query</param>
    /// <param name="payrunName">Name of the payrun</param>
    /// <returns>The payrun employee start script</returns>
    string GetPayrunEmployeeStartScript(ScriptCodeQuery query, string payrunName);

    /// <summary>Gets the payrun employee end script</summary>
    /// <param name="query">The script code query</param>
    /// <param name="payrunName">Name of the payrun</param>
    /// <returns>The payrun employee end script</returns>
    string GetPayrunEmployeeEndScript(ScriptCodeQuery query, string payrunName);

    /// <summary>Gets the payrun end script</summary>
    /// <param name="query">The script code query</param>
    /// <param name="payrunName">Name of the payrun</param>
    /// <returns>The payrun end script</returns>
    string GetPayrunEndScript(ScriptCodeQuery query, string payrunName);
}