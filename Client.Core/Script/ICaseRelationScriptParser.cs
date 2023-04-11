namespace PayrollEngine.Client.Script;

/// <summary>
/// Case relation script parser
/// </summary>
public interface ICaseRelationScriptParser
{
    /// <summary>Gets the case relation build script</summary>
    /// <param name="query">The script code query</param>
    /// <param name="regulationName">Name of the regulation</param>
    /// <param name="sourceCaseName">Name of the source case</param>
    /// <param name="targetCaseName">Name of the target case</param>
    /// <param name="sourceCaseSlot">Name of the source case slot</param>
    /// <param name="targetCaseSlot">Name of the target case slot</param>
    /// <returns>The case relation build script</returns>
    string GetCaseRelationBuildScript(ScriptCodeQuery query, string regulationName,
        string sourceCaseName, string targetCaseName,
        string sourceCaseSlot = null, string targetCaseSlot = null);

    /// <summary>Gets the case validate script</summary>
    /// <param name="query">The script code query</param>
    /// <param name="regulationName">Name of the regulation</param>
    /// <param name="sourceCaseName">Name of the source case</param>
    /// <param name="targetCaseName">Name of the target case</param>
    /// <param name="sourceCaseSlot">Name of the source case slot</param>
    /// <param name="targetCaseSlot">Name of the target case slot</param>
    /// <returns>The case relation validate script</returns>
    string GetCaseRelationValidateScript(ScriptCodeQuery query, string regulationName,
        string sourceCaseName, string targetCaseName,
        string sourceCaseSlot = null, string targetCaseSlot = null);
}