namespace PayrollEngine.Client.Script;

/// <summary>
/// Report script parser
/// </summary>
public interface IReportScriptParser
{

    /// <summary>Gets the report build script</summary>
    /// <param name="query">The script code query</param>
    /// <param name="regulationName">The regulation name</param>
    /// <param name="reportName">Name of the report</param>
    /// <returns>The report build script</returns>
    string GetReportBuildScript(ScriptCodeQuery query, string regulationName, string reportName);

    /// <summary>Gets the report start script</summary>
    /// <param name="query">The script code query</param>
    /// <param name="regulationName">The regulation name</param>
    /// <param name="reportName">Name of the report</param>
    /// <returns>The report start script</returns>
    string GetReportStartScript(ScriptCodeQuery query, string regulationName, string reportName);

    /// <summary>Gets the report end script</summary>
    /// <param name="query">The script code query</param>
    /// <param name="regulationName">The regulation name</param>
    /// <param name="reportName">Name of the report</param>
    /// <returns>The report end script</returns>
    string GetReportEndScript(ScriptCodeQuery query, string regulationName, string reportName);
}