using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payrun client object</summary>
public interface IPayrun : IModel, IKeyEquatable<IPayrun>
{
    /// <summary>The payroll id (immutable)</summary>
    int PayrollId { get; set; }

    /// <summary>The payroll name (client only)</summary>
    string PayrollName { get; set; }

    /// <summary>The payrun name</summary>
    string Name { get; set; }

    /// <summary>The localized payrun name</summary>
    Dictionary<string, string> NameLocalizations { get; set; }

    /// <summary>The default payrun reason</summary>
    string DefaultReason { get; set; }

    /// <summary>The localized default payrun reasons</summary>
    Dictionary<string, string> DefaultReasonLocalizations { get; set; }

    /// <summary>The payrun start expression</summary>
    string StartExpression { get; set; }

    /// <summary>The payrun start expression file</summary>
    string StartExpressionFile { get; set; }

    /// <summary>The employee available expression</summary>
    string EmployeeAvailableExpression { get; set; }

    /// <summary>The employee available expression file</summary>
    string EmployeeAvailableExpressionFile { get; set; }

    /// <summary>The expression evaluates the employee start</summary>
    string EmployeeStartExpression { get; set; }

    /// <summary>The expression file evaluates the employee start</summary>
    string EmployeeStartExpressionFile { get; set; }

    /// <summary>The expression evaluates the employee end</summary>
    string EmployeeEndExpression { get; set; }

    /// <summary>The expression file evaluates the employee end</summary>
    string EmployeeEndExpressionFile { get; set; }

    /// <summary>The wage type available expression</summary>
    string WageTypeAvailableExpression { get; set; }

    /// <summary>The wage type available expression file</summary>
    string WageTypeAvailableExpressionFile { get; set; }

    /// <summary>The payrun end expression</summary>
    string EndExpression { get; set; }

    /// <summary>The payrun end expression file</summary>
    string EndExpressionFile { get; set; }

    /// <summary>
    /// The number of previous cycles allowed for retro calculation.
    /// 0  = current cycle only (no back cycles).
    /// n  = retro may reach back n complete cycles before the current one.
    /// -1 = unlimited (no cycle boundary) — default.
    /// </summary>
    int RetroBackCycles { get; set; }

    /// <summary>The payrun parameters</summary>
    List<PayrunParameter> PayrunParameters { get; set; }
}