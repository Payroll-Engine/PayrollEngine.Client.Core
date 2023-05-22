namespace PayrollEngine.Client.Model;

/// <summary>Extension methods for queries</summary>
public static class ScriptExtensions
{
    /// <summary>Test if a case has any script</summary>
    /// <param name="case">The case to test</param>
    /// <returns>True if any script is present</returns>
    public static bool HasAnyScript(this Case @case) =>
        @case != null &&
        (!string.IsNullOrWhiteSpace(@case.AvailableExpression) ||
         !string.IsNullOrWhiteSpace(@case.BuildExpression) ||
         !string.IsNullOrWhiteSpace(@case.ValidateExpression));

    /// <summary>Test if a case relation has any script</summary>
    /// <param name="caseRelation">The case relation to test</param>
    /// <returns>True if any script is present</returns>
    public static bool HasAnyScript(this CaseRelation caseRelation) =>
        caseRelation != null &&
        (!string.IsNullOrWhiteSpace(caseRelation.BuildExpression) ||
         !string.IsNullOrWhiteSpace(caseRelation.ValidateExpression));

    /// <summary>Test if a collector has any script</summary>
    /// <param name="collector">The collector to test</param>
    /// <returns>True if any script is present</returns>
    public static bool HasAnyScript(this Collector collector) =>
        collector != null &&
        (!string.IsNullOrWhiteSpace(collector.StartExpression) ||
         !string.IsNullOrWhiteSpace(collector.ApplyExpression) ||
         !string.IsNullOrWhiteSpace(collector.EndExpression));

    /// <summary>Test if a wage type has any script</summary>
    /// <param name="wageType">The wage type to test</param>
    /// <returns>True if any script is present</returns>
    public static bool HasAnyScript(this WageType wageType) =>
        wageType != null &&
        (!string.IsNullOrWhiteSpace(wageType.ValueExpression) ||
         !string.IsNullOrWhiteSpace(wageType.ResultExpression));

    /// <summary>Test if a report has any script</summary>
    /// <param name="report">The report to test</param>
    /// <returns>True if any script is present</returns>
    public static bool HasAnyScript(this Report report) =>
        report != null &&
        (!string.IsNullOrWhiteSpace(report.BuildExpression) ||
         !string.IsNullOrWhiteSpace(report.StartExpression) ||
         !string.IsNullOrWhiteSpace(report.EndExpression));

    /// <summary>Test if a payrun has any script</summary>
    /// <param name="payrun">The payrun to test</param>
    /// <returns>True if any script is present</returns>
    public static bool HasAnyScript(this Payrun payrun) =>
        payrun != null &&
        (!string.IsNullOrWhiteSpace(payrun.StartExpression) ||
         !string.IsNullOrWhiteSpace(payrun.EmployeeAvailableExpression) ||
         !string.IsNullOrWhiteSpace(payrun.WageTypeAvailableExpression) ||
         !string.IsNullOrWhiteSpace(payrun.EndExpression));
}