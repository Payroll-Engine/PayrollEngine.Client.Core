namespace PayrollEngine.Client.Script;

/// <summary>
/// Case script parser
/// </summary>
public interface ICaseScriptParser
{
    /// <summary>Gets the case available script</summary>
    /// <param name="query">The script code query</param>
    /// <param name="regulationName">Name of the regulation</param>
    /// <param name="caseName">Name of the case</param>
    /// <returns>The case available script</returns>
    string GetCaseAvailableScript(ScriptCodeQuery query, string regulationName,
        string caseName);

    /// <summary>Gets the case build script</summary>
    /// <param name="query">The script code query</param>
    /// <param name="regulationName">Name of the regulation</param>
    /// <param name="caseName">Name of the case</param>
    /// <returns>The case build script</returns>
    string GetCaseBuildScript(ScriptCodeQuery query, string regulationName, string caseName);

    /// <summary>Gets the case validate script</summary>
    /// <param name="query">The script code query</param>
    /// <param name="regulationName">Name of the regulation</param>
    /// <param name="caseName">Name of the case</param>
    /// <returns>The case validate script</returns>
    string GetCaseValidateScript(ScriptCodeQuery query, string regulationName, string caseName);
}