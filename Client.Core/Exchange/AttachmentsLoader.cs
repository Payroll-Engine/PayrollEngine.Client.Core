using System;
using System.IO;
using System.Linq;
using Task = System.Threading.Tasks.Task;
using PayrollEngine.Client.Model;
using PayrollEngine.Client.Script;

namespace PayrollEngine.Client.Exchange;

/// <summary>Exchange attachment loader</summary>
public class AttachmentsLoader : Visitor
{
    private readonly TextFileCache textFiles = new();

    /// <summary>The script parser</summary>
    private IScriptParser ScriptParser { get; }

    private bool ResetFileName { get; }

    /// <summary>Initializes a new instance of the <see cref="AttachmentsLoader"/> class</summary>
    /// <remarks>Content is loaded from the working folder</remarks>
    /// <param name="exchange">The exchange model</param>
    /// <param name="scriptParser">The script parser</param>
    /// <param name="resetFileName">Reset the file name</param>
    public AttachmentsLoader(Model.Exchange exchange, IScriptParser scriptParser, bool resetFileName = true) :
        base(exchange)
    {
        ScriptParser = scriptParser ?? throw new ArgumentNullException(nameof(scriptParser));
        ResetFileName = resetFileName;
    }

    /// <summary>Read attachments</summary>
    public void Read() =>
        ReadAsync().Wait();

    /// <summary>Read attachments</summary>
    public virtual async Task ReadAsync() => await VisitAsync();

    #region Attachment Overrides

    /// <summary>Case setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="caseSet">The case</param>
    /// <param name="targetCase">The target case</param>
    protected virtual Task SetupCaseAsync(IExchangeTenant tenant, IRegulationSet regulation, ICaseSet caseSet, ICase targetCase)
    {
        // case available expression file
        if (string.IsNullOrWhiteSpace(caseSet.AvailableExpression) && !string.IsNullOrWhiteSpace(caseSet.AvailableExpressionFile))
        {
            caseSet.AvailableExpression = ScriptParser.CaseParser.GetCaseAvailableScript(
                new()
                {
                    TenantIdentifier = tenant.Identifier,
                    SourceCode = ReadTextFile(caseSet.AvailableExpressionFile)
                },
                regulation.Name, caseSet.Name);
            if (ResetFileName)
            {
                caseSet.AvailableExpressionFile = null;
            }
            if (string.IsNullOrWhiteSpace(caseSet.AvailableExpression))
            {
                throw new PayrollException($"Missing or invalid script file {caseSet.AvailableExpressionFile}.");
            }
        }

        // case build expression file
        if (string.IsNullOrWhiteSpace(caseSet.BuildExpression) && !string.IsNullOrWhiteSpace(caseSet.BuildExpressionFile))
        {
            caseSet.BuildExpression = ScriptParser.CaseParser.GetCaseBuildScript(
                new()
                {
                    TenantIdentifier = tenant.Identifier,
                    SourceCode = ReadTextFile(caseSet.BuildExpressionFile)
                },
                regulation.Name, caseSet.Name);
            if (ResetFileName)
            {
                caseSet.BuildExpressionFile = null;
            }
            if (string.IsNullOrWhiteSpace(caseSet.BuildExpression))
            {
                throw new PayrollException($"Missing or invalid script file {caseSet.BuildExpressionFile}.");
            }
        }

        // case validate expression file
        if (string.IsNullOrWhiteSpace(caseSet.ValidateExpression) && !string.IsNullOrWhiteSpace(caseSet.ValidateExpressionFile))
        {
            caseSet.ValidateExpression = ScriptParser.CaseParser.GetCaseValidateScript(
                new()
                {
                    TenantIdentifier = tenant.Identifier,
                    SourceCode = ReadTextFile(caseSet.ValidateExpressionFile)
                },
                regulation.Name, caseSet.Name);
            if (ResetFileName)
            {
                caseSet.ValidateExpressionFile = null;
            }
            if (string.IsNullOrWhiteSpace(caseSet.ValidateExpression))
            {
                throw new PayrollException($"Missing or invalid script file {caseSet.ValidateExpressionFile}.");
            }
        }

        return Task.CompletedTask;
    }

    /// <summary>Case relation setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="caseRelation">The case relation</param>
    /// <param name="targetCaseRelation">The target case relation</param>
    protected virtual Task SetupCaseRelationAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICaseRelation caseRelation, ICaseRelation targetCaseRelation)
    {
        // case relation build expression file
        if (string.IsNullOrWhiteSpace(caseRelation.BuildExpression) &&
            !string.IsNullOrWhiteSpace(caseRelation.BuildExpressionFile))
        {
            caseRelation.BuildExpression = ScriptParser.CaseRelationParser.GetCaseRelationBuildScript(
                new()
                {
                    TenantIdentifier = tenant.Identifier,
                    SourceCode = ReadTextFile(caseRelation.BuildExpressionFile)
                },
                regulation.Name,
                caseRelation.SourceCaseName, caseRelation.TargetCaseName,
                caseRelation.SourceCaseSlot, caseRelation.TargetCaseSlot);
            if (ResetFileName)
            {
                caseRelation.BuildExpressionFile = null;
            }
            if (string.IsNullOrWhiteSpace(caseRelation.BuildExpression))
            {
                throw new PayrollException(
                    $"Missing or invalid script file {caseRelation.BuildExpressionFile}.");
            }
        }

        // case relation validate expression file
        if (string.IsNullOrWhiteSpace(caseRelation.ValidateExpression) &&
            !string.IsNullOrWhiteSpace(caseRelation.ValidateExpressionFile))
        {
            caseRelation.ValidateExpression = ScriptParser.CaseRelationParser.GetCaseRelationValidateScript(
                new()
                {
                    TenantIdentifier = tenant.Identifier,
                    SourceCode = ReadTextFile(caseRelation.ValidateExpressionFile)
                },
                regulation.Name,
                caseRelation.SourceCaseName, caseRelation.TargetCaseName,
                caseRelation.SourceCaseSlot, caseRelation.TargetCaseSlot);
            if (ResetFileName)
            {
                caseRelation.ValidateExpressionFile = null;
            }
            if (string.IsNullOrWhiteSpace(caseRelation.ValidateExpression))
            {
                throw new PayrollException(
                    $"Missing or invalid script file {caseRelation.ValidateExpressionFile}.");
            }
        }

        return Task.CompletedTask;
    }

    /// <summary>Case relation setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="collector">The collector</param>
    /// <param name="targetCollector">The target collector</param>
    protected virtual Task SetupCollectorAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICollector collector, ICollector targetCollector)
    {
        // collector start expression file
        if (string.IsNullOrWhiteSpace(collector.StartExpression) &&
            !string.IsNullOrWhiteSpace(collector.StartExpressionFile))
        {
            collector.StartExpression = ScriptParser.CollectorParser.GetCollectorStartScript(
                new()
                {
                    TenantIdentifier = tenant.Identifier,
                    SourceCode = ReadTextFile(collector.StartExpressionFile)
                },
                regulation.Name, collector.Name);
            if (ResetFileName)
            {
                collector.StartExpressionFile = null;
            }
            if (string.IsNullOrWhiteSpace(collector.StartExpression))
            {
                throw new PayrollException($"Missing or invalid script file {collector.StartExpressionFile}.");
            }
        }

        // collector apply expression file
        if (string.IsNullOrWhiteSpace(collector.ApplyExpression) &&
            !string.IsNullOrWhiteSpace(collector.ApplyExpressionFile))
        {
            collector.ApplyExpression = ScriptParser.CollectorParser.GetCollectorApplyScript(
                new()
                {
                    TenantIdentifier = tenant.Identifier,
                    SourceCode = ReadTextFile(collector.ApplyExpressionFile)
                },
                regulation.Name, collector.Name);
            if (ResetFileName)
            {
                collector.StartExpressionFile = null;
            }
            if (string.IsNullOrWhiteSpace(collector.ApplyExpressionFile))
            {
                throw new PayrollException($"Missing or invalid script file {collector.ApplyExpressionFile}.");
            }
        }

        // collector end expression file
        if (string.IsNullOrWhiteSpace(collector.EndExpression) &&
            !string.IsNullOrWhiteSpace(collector.EndExpressionFile))
        {
            collector.EndExpression = ScriptParser.CollectorParser.GetCollectorEndScript(
                new()
                {
                    TenantIdentifier = tenant.Identifier,
                    SourceCode = ReadTextFile(collector.EndExpressionFile)
                },
                regulation.Name, collector.Name);
            if (ResetFileName)
            {
                collector.EndExpressionFile = null;
            }
            if (string.IsNullOrWhiteSpace(collector.EndExpression))
            {
                throw new PayrollException($"Missing or invalid script file {collector.EndExpressionFile}.");
            }
        }

        return Task.CompletedTask;
    }

    /// <summary>Wage type setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="wageType">The wage type</param>
    /// <param name="targetWageType">The target wage type</param>
    protected virtual Task SetupWageTypeAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IWageType wageType, IWageType targetWageType)
    {
        // wage type value expression file
        if (string.IsNullOrWhiteSpace(wageType.ValueExpression) &&
            !string.IsNullOrWhiteSpace(wageType.ValueExpressionFile))
        {
            wageType.ValueExpression = ScriptParser.WageTypeParser.GetWageTypeValueScript(
                new()
                {
                    TenantIdentifier = tenant.Identifier,
                    SourceCode = ReadTextFile(wageType.ValueExpressionFile)
                },
                regulation.Name, wageType.WageTypeNumber);
            if (ResetFileName)
            {
                wageType.ValueExpressionFile = null;
            }
            if (string.IsNullOrWhiteSpace(wageType.ValueExpression))
            {
                throw new PayrollException($"Missing or invalid script file {wageType.ValueExpressionFile}.");
            }
        }

        // wage type result expression file
        if (string.IsNullOrWhiteSpace(wageType.ResultExpression) &&
            !string.IsNullOrWhiteSpace(wageType.ResultExpressionFile))
        {
            wageType.ResultExpression = ScriptParser.WageTypeParser.GetWageTypeResultScript(
                new()
                {
                    TenantIdentifier = tenant.Identifier,
                    SourceCode = ReadTextFile(wageType.ResultExpression)
                },
                regulation.Name, wageType.WageTypeNumber);
            if (ResetFileName)
            {
                wageType.ResultExpressionFile = null;
            }
            if (string.IsNullOrWhiteSpace(wageType.ResultExpression))
            {
                throw new PayrollException($"Missing or invalid script file {wageType.ResultExpressionFile}.");
            }
        }

        return Task.CompletedTask;
    }

    /// <summary>Script setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="script">The script</param>
    /// <param name="targetScript">The target script</param>
    protected virtual Task SetupScriptAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IScript script, IScript targetScript)
    {
        // script value file
        if (string.IsNullOrWhiteSpace(script.Value) && !string.IsNullOrWhiteSpace(script.ValueFile))
        {
            script.Value = ReadTextFile(script.ValueFile);
            if (string.IsNullOrWhiteSpace(script.Value))
            {
                throw new PayrollException($"Missing script value in file {script.ValueFile}.");
            }
            if (ResetFileName)
            {
                script.ValueFile = null;
            }
        }

        return Task.CompletedTask;
    }

    /// <summary>Report setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="report">The report</param>
    /// <param name="targetReport">The target report</param>
    protected virtual Task SetupReportAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report, IReportSet targetReport)
    {
        // report build expression file
        if (string.IsNullOrWhiteSpace(report.BuildExpression) &&
            !string.IsNullOrWhiteSpace(report.BuildExpressionFile))
        {
            report.BuildExpression = ScriptParser.ReportParser.GetReportBuildScript(
                new()
                {
                    TenantIdentifier = tenant.Identifier,
                    SourceCode = ReadTextFile(report.BuildExpressionFile)
                },
                regulation.Name, report.Name);
            if (ResetFileName)
            {
                report.BuildExpressionFile = null;
            }
            if (string.IsNullOrWhiteSpace(report.BuildExpression))
            {
                throw new PayrollException($"Missing or invalid script file {report.BuildExpressionFile}.");
            }
        }

        // report start expression file
        if (string.IsNullOrWhiteSpace(report.StartExpression) &&
            !string.IsNullOrWhiteSpace(report.StartExpressionFile))
        {
            report.StartExpression = ScriptParser.ReportParser.GetReportStartScript(
                new()
                {
                    TenantIdentifier = tenant.Identifier,
                    SourceCode = ReadTextFile(report.StartExpressionFile)
                },
                regulation.Name, report.Name);
            if (ResetFileName)
            {
                report.StartExpressionFile = null;
            }
            if (string.IsNullOrWhiteSpace(report.StartExpression))
            {
                throw new PayrollException($"Missing or invalid script file {report.StartExpressionFile}.");
            }
        }

        // report end expression file
        if (string.IsNullOrWhiteSpace(report.EndExpression) &&
            !string.IsNullOrWhiteSpace(report.EndExpressionFile))
        {
            report.EndExpression = ScriptParser.ReportParser.GetReportEndScript(
                new()
                {
                    TenantIdentifier = tenant.Identifier,
                    SourceCode = ReadTextFile(report.EndExpressionFile)
                },
                regulation.Name, report.Name);
            if (ResetFileName)
            {
                report.EndExpressionFile = null;
            }
            if (string.IsNullOrWhiteSpace(report.EndExpression))
            {
                throw new PayrollException($"Missing or invalid script file {report.EndExpressionFile}.");
            }
        }

        return Task.CompletedTask;
    }

    /// <summary>Report template setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="report">The report</param>
    /// <param name="template">The report template</param>
    /// <param name="targetTemplate">The target report template</param>
    protected virtual Task SetupReportTemplateAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report, IReportTemplate template, IReportTemplate targetTemplate)
    {
        // report content file (binary or xsl text file)
        if (string.IsNullOrWhiteSpace(template.Content) &&
            !string.IsNullOrWhiteSpace(template.ContentFile))
        {
            template.Content = ReadTemplateContent(template.ContentFile);

            // content type
            if (string.IsNullOrWhiteSpace(template.ContentType))
            {
                var extension = Path.GetExtension(template.ContentFile);
                if (!string.IsNullOrWhiteSpace(extension))
                {
                    template.ContentType = extension.TrimStart('.');
                }
            }
            if (ResetFileName)
            {
                template.ContentFile = null;
            }
        }

        // report schema file (xsd text file)
        if (string.IsNullOrWhiteSpace(template.Schema) &&
            !string.IsNullOrWhiteSpace(template.SchemaFile))
        {
            template.Schema = ReadTemplateContent(template.SchemaFile);
            if (ResetFileName)
            {
                template.SchemaFile = null;
            }
        }

        return Task.CompletedTask;
    }

    /// <summary>Payrun setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="payrun">The payrun</param>
    /// <param name="targetPayrun">The target payrun</param>
    protected virtual Task SetupPayrunAsync(IExchangeTenant tenant, IPayrun payrun, IPayrun targetPayrun)
    {
        // payrun start expression file
        if (string.IsNullOrWhiteSpace(payrun.StartExpression) &&
            !string.IsNullOrWhiteSpace(payrun.StartExpressionFile))
        {
            payrun.StartExpression = ScriptParser.PayrunParser.GetPayrunStartScript(
                new()
                {
                    TenantIdentifier = tenant.Identifier,
                    SourceCode = ReadTextFile(payrun.StartExpressionFile)
                },
                payrun.Name);
            if (ResetFileName)
            {
                payrun.StartExpressionFile = null;
            }
            if (string.IsNullOrWhiteSpace(payrun.StartExpression))
            {
                throw new PayrollException($"Missing or invalid script file {payrun.StartExpressionFile}.");
            }
        }

        // payrun employee available expression file
        if (string.IsNullOrWhiteSpace(payrun.EmployeeAvailableExpression) &&
            !string.IsNullOrWhiteSpace(payrun.EmployeeAvailableExpressionFile))
        {
            payrun.EmployeeAvailableExpression = ScriptParser.PayrunParser.GetPayrunEmployeeAvailableScript(
                new()
                {
                    TenantIdentifier = tenant.Identifier,
                    SourceCode = ReadTextFile(payrun.EmployeeAvailableExpressionFile)
                },
                payrun.Name);
            if (ResetFileName)
            {
                payrun.EmployeeAvailableExpressionFile = null;
            }
            if (string.IsNullOrWhiteSpace(payrun.EmployeeAvailableExpression))
            {
                throw new PayrollException(
                    $"Missing or invalid script file {payrun.EmployeeAvailableExpressionFile}.");
            }
        }

        // payrun employee start expression file
        if (string.IsNullOrWhiteSpace(payrun.EmployeeStartExpression) &&
            !string.IsNullOrWhiteSpace(payrun.EmployeeStartExpressionFile))
        {
            payrun.EmployeeStartExpression = ScriptParser.PayrunParser.GetPayrunEmployeeStartScript(
                new()
                {
                    TenantIdentifier = tenant.Identifier,
                    SourceCode = ReadTextFile(payrun.EmployeeStartExpressionFile)
                },
                payrun.Name);
            if (ResetFileName)
            {
                payrun.EmployeeStartExpressionFile = null;
            }
            if (string.IsNullOrWhiteSpace(payrun.EmployeeStartExpression))
            {
                throw new PayrollException(
                    $"Missing or invalid script file {payrun.EmployeeStartExpressionFile}.");
            }
        }

        // payrun wage type available expression file
        if (string.IsNullOrWhiteSpace(payrun.WageTypeAvailableExpression) &&
            !string.IsNullOrWhiteSpace(payrun.WageTypeAvailableExpressionFile))
        {
            payrun.WageTypeAvailableExpression = ScriptParser.PayrunParser.GetPayrunWageTypeAvailableScript(
                new()
                {
                    TenantIdentifier = tenant.Identifier,
                    SourceCode = ReadTextFile(payrun.WageTypeAvailableExpressionFile)
                },
                payrun.Name);
            if (ResetFileName)
            {
                payrun.WageTypeAvailableExpressionFile = null;
            }
            if (string.IsNullOrWhiteSpace(payrun.WageTypeAvailableExpression))
            {
                throw new PayrollException(
                    $"Missing or invalid script file {payrun.WageTypeAvailableExpressionFile}.");
            }
        }

        // payrun employee end expression file
        if (string.IsNullOrWhiteSpace(payrun.EmployeeEndExpression) &&
            !string.IsNullOrWhiteSpace(payrun.EmployeeEndExpressionFile))
        {
            payrun.EmployeeEndExpression = ScriptParser.PayrunParser.GetPayrunEmployeeEndScript(
                new()
                {
                    TenantIdentifier = tenant.Identifier,
                    SourceCode = ReadTextFile(payrun.EmployeeEndExpressionFile)
                },
                payrun.Name);
            if (ResetFileName)
            {
                payrun.EmployeeEndExpressionFile = null;
            }
            if (string.IsNullOrWhiteSpace(payrun.EmployeeEndExpression))
            {
                throw new PayrollException(
                    $"Missing or invalid script file {payrun.EmployeeEndExpressionFile}.");
            }
        }

        // payrun end expression file
        if (string.IsNullOrWhiteSpace(payrun.EndExpression) &&
            !string.IsNullOrWhiteSpace(payrun.EndExpressionFile))
        {
            payrun.EndExpression = ScriptParser.PayrunParser.GetPayrunEndScript(
                new()
                {
                    TenantIdentifier = tenant.Identifier,
                    SourceCode = ReadTextFile(payrun.EndExpressionFile)
                },
                payrun.Name);
            if (ResetFileName)
            {
                payrun.EndExpressionFile = null;
            }
            if (string.IsNullOrWhiteSpace(payrun.EndExpression))
            {
                throw new PayrollException($"Missing or invalid script file {payrun.EndExpressionFile}.");
            }
        }

        return Task.CompletedTask;
    }

    #endregion

    #region Helper

    private string ReadTemplateContent(string fileName)
    {
        if (!File.Exists(fileName))
        {
            throw new PayrollException($"Invalid report template file {fileName}.");
        }

        string content;

        // text file
        var firstLine = File.ReadLines(fileName).First();
        if (!string.IsNullOrWhiteSpace(firstLine) && firstLine.Trim().StartsWith("<?xml "))
        {
            content = ReadTextFile(fileName);
        }
        else
        {
            // binary file
            content = BinaryFile.Read(fileName);
        }

        // content test
        if (string.IsNullOrWhiteSpace(content))
        {
            throw new PayrollException($"Missing report template content from file {fileName}.");
        }
        return content;
    }

    /// <summary>
    /// Reads a text file as string
    /// </summary>
    /// <param name="fileName">Name of the file</param>
    /// <returns>The file content as string</returns>
    protected string ReadTextFile(string fileName) =>
        textFiles.ReadTextFile(fileName);

    #endregion

}