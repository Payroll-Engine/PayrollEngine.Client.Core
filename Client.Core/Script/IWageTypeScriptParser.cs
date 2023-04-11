namespace PayrollEngine.Client.Script;

/// <summary>
/// Wage type script parser
/// </summary>
public interface IWageTypeScriptParser
{
    /// <summary>Gets the wage type result script</summary>
    /// <param name="query">The script code query</param>
    /// <param name="regulationName">The regulation name</param>
    /// <param name="wageTypeNumber">The wage type number</param>
    /// <returns>The wage type result script</returns>
    string GetWageTypeResultScript(ScriptCodeQuery query, string regulationName, decimal wageTypeNumber);

    /// <summary>Gets the wage type value script</summary>
    /// <param name="query">The script code query</param>
    /// <param name="regulationName">The regulation name</param>
    /// <param name="wageTypeNumber">The wage type number</param>
    /// <returns>The wage type value script</returns>
    string GetWageTypeValueScript(ScriptCodeQuery query, string regulationName, decimal wageTypeNumber);
}