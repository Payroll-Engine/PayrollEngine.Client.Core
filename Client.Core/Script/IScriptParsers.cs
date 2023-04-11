namespace PayrollEngine.Client.Script;

/// <summary>
/// Payroll script parser
/// </summary>
public interface IScriptParser
{
    /// <summary>The case script parser</summary>
    ICaseScriptParser CaseParser { get; }

    /// <summary>The case relation script parser</summary>
    ICaseRelationScriptParser CaseRelationParser { get; }

    /// <summary>The case relation script parser</summary>
    IWageTypeScriptParser WageTypeParser { get; }

    /// <summary>The case relation script parser</summary>
    ICollectorScriptParser CollectorParser { get; }

    /// <summary>The case relation script parser</summary>
    IPayrunScriptParser PayrunParser { get; }

    /// <summary>The case relation script parser</summary>
    IReportScriptParser ReportParser { get; }
}