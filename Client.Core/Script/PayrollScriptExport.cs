using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using PayrollEngine.Client.Service.Api;

namespace PayrollEngine.Client.Script;

/// <summary>Export payroll scripts fro development</summary>
public sealed class PayrollScriptExport
{
    /// <summary>The default c# namespace</summary>
    public static readonly string DefaultNamespace = "Payroll.Script";

    private Assembly Assembly { get; }

    /// <summary>The Payroll http client</summary>
    public PayrollHttpClient HttpClient { get; }

    /// <summary>The export context</summary>
    public ScriptExportContext Context { get; }

    /// <summary>Initializes a new instance of the <see cref="PayrollScriptExport"/> class</summary>
    /// <param name="httpClient">The payroll http client</param>
    /// <param name="context">The export context</param>
    public PayrollScriptExport(PayrollHttpClient httpClient, ScriptExportContext context)
    {
        Assembly = GetType().Assembly;
        HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>Export regulation scripts</summary>
    /// <param name="regulationId">The regulation id</param>
    /// <returns>The regulation scripts</returns>
    public async Task<List<DevelopmentScript>> ExportAsync(int regulationId)
    {
        switch (Context.ScriptObject)
        {
            case ScriptExportObject.All:
                var scripts = new List<DevelopmentScript>();
                scripts.AddRange(await BuildScriptsAsync(regulationId));
                scripts.AddRange(await BuildCaseScriptsAsync(regulationId));
                scripts.AddRange(await BuildCaseRelationScriptsAsync(regulationId));
                scripts.AddRange(await BuildCollectorScriptsAsync(regulationId));
                scripts.AddRange(await BuildWageTypeScriptsAsync(regulationId));
                scripts.AddRange(await BuildReportScriptsAsync(regulationId));
                return scripts;
            case ScriptExportObject.GlobalScript:
                return await BuildScriptsAsync(regulationId);
            case ScriptExportObject.Case:
                return await BuildCaseScriptsAsync(regulationId);
            case ScriptExportObject.CaseRelation:
                return await BuildCaseRelationScriptsAsync(regulationId);
            case ScriptExportObject.Collector:
                return await BuildCollectorScriptsAsync(regulationId);
            case ScriptExportObject.WageType:
                return await BuildWageTypeScriptsAsync(regulationId);
            case ScriptExportObject.Report:
                return await BuildReportScriptsAsync(regulationId);
            default:
                throw new ArgumentOutOfRangeException(nameof(Context.ScriptObject), Context.ScriptObject, null);
        }
    }

    private async Task<List<DevelopmentScript>> BuildScriptsAsync(int regulationId)
    {
        var devScripts = new List<DevelopmentScript>();
        var scripts = (await new ScriptService(HttpClient).QueryAsync<Model.Script>(
            new(Context.Tenant.Id, regulationId))).ToList();
        foreach (var script in scripts)
        {
            devScripts.Add(new()
            {
                FunctionTypes = [..script.FunctionTypes],
                OwnerId = script.Id,
                ScriptName = script.Name,
                ClassName = script.Name,
                Content = script.Value
            });
        }
        return devScripts;
    }

    private async Task<List<DevelopmentScript>> BuildCaseScriptsAsync(int regulationId)
    {
        var scripts = new List<DevelopmentScript>();
        var cases = (await new CaseService(HttpClient).QueryAsync<Case>(
            new(Context.Tenant.Id, regulationId))).ToList();
        foreach (var @case in cases)
        {
            if (Context.ExportMode == ScriptExportMode.Existing && !@case.HasAnyScript())
            {
                continue;
            }

            // case variables
            var variables = new Dictionary<string, string>
            {
                { "CaseName", @case.Name }
            };

            // apply scripts
            AddScript(scripts, @case.Id, @case.Name, FunctionType.CaseAvailable, @case.AvailableExpression, variables);
            AddScript(scripts, @case.Id, @case.Name, FunctionType.CaseBuild, @case.BuildExpression, variables);
            AddScript(scripts, @case.Id, @case.Name, FunctionType.CaseValidate, @case.ValidateExpression, variables);
        }
        return scripts;
    }

    private async Task<List<DevelopmentScript>> BuildCaseRelationScriptsAsync(int regulationId)
    {
        var scripts = new List<DevelopmentScript>();
        var caseRelations = (await new CaseRelationService(HttpClient).QueryAsync<CaseRelation>(
            new(Context.Tenant.Id, regulationId))).ToList();
        foreach (var caseRelation in caseRelations)
        {
            if (Context.ExportMode == ScriptExportMode.Existing && !caseRelation.HasAnyScript())
            {
                continue;
            }

            // case relation variables
            var variables = new Dictionary<string, string>
            {
                { "SourceCaseName", caseRelation.SourceCaseName },
                { "TargetCaseName", caseRelation.TargetCaseName }
            };

            // apply scripts
            var identifier = $"{caseRelation.SourceCaseName}{caseRelation.TargetCaseName}";
            AddScript(scripts, caseRelation.Id, identifier, FunctionType.CaseRelationBuild, caseRelation.BuildExpression, variables);
            AddScript(scripts, caseRelation.Id, identifier, FunctionType.CaseRelationValidate, caseRelation.ValidateExpression, variables);
        }
        return scripts;
    }

    private async Task<List<DevelopmentScript>> BuildCollectorScriptsAsync(int regulationId)
    {
        var scripts = new List<DevelopmentScript>();
        var collectors = (await new CollectorService(HttpClient).QueryAsync<Collector>(
            new(Context.Tenant.Id, regulationId))).ToList();
        foreach (var collector in collectors)
        {
            if (Context.ExportMode == ScriptExportMode.Existing && !collector.HasAnyScript())
            {
                continue;
            }

            // collector variables
            var variables = new Dictionary<string, string>
            {
                { "CollectorName", collector.Name }
            };

            // apply scripts
            AddScript(scripts, collector.Id, collector.Name, FunctionType.CollectorStart, collector.StartExpression, variables);
            AddScript(scripts, collector.Id, collector.Name, FunctionType.CollectorApply, collector.ApplyExpression, variables);
            AddScript(scripts, collector.Id, collector.Name, FunctionType.CollectorEnd, collector.EndExpression, variables);
        }
        return scripts;
    }

    private async Task<List<DevelopmentScript>> BuildWageTypeScriptsAsync(int regulationId)
    {
        var scripts = new List<DevelopmentScript>();
        var wageTypes = (await new WageTypeService(HttpClient).QueryAsync<WageType>(
            new(Context.Tenant.Id, regulationId))).ToList();
        foreach (var wageType in wageTypes)
        {
            if (Context.ExportMode == ScriptExportMode.Existing && !wageType.HasAnyScript())
            {
                continue;
            }

            // wage type variables
            var variables = new Dictionary<string, string>
            {
                { "WageTypeNumber", $"{wageType.WageTypeNumber:0.####}" }
            };

            // apply scripts
            AddScript(scripts, wageType.Id, wageType.Name, FunctionType.WageTypeValue, wageType.ValueExpression, variables);
            AddScript(scripts, wageType.Id, wageType.Name, FunctionType.WageTypeResult, wageType.ResultExpression, variables);
        }
        return scripts;
    }

    private async Task<List<DevelopmentScript>> BuildReportScriptsAsync(int regulationId)
    {
        var scripts = new List<DevelopmentScript>();
        var reports = (await new ReportService(HttpClient).QueryAsync<Report>(
            new(Context.Tenant.Id, regulationId))).ToList();
        foreach (var report in reports)
        {
            if (Context.ExportMode == ScriptExportMode.Existing && !report.HasAnyScript())
            {
                continue;
            }

            // report variables
            var variables = new Dictionary<string, string>
            {
                { "ReportName", report.Name }
            };

            // apply scripts
            AddScript(scripts, report.Id, report.Name, FunctionType.ReportBuild, report.BuildExpression, variables);
            AddScript(scripts, report.Id, report.Name, FunctionType.ReportStart, report.StartExpression, variables);
            AddScript(scripts, report.Id, report.Name, FunctionType.ReportEnd, report.EndExpression, variables);
        }
        return scripts;
    }

    private void AddScript(List<DevelopmentScript> scripts, int ownerId, string scriptName,
        FunctionType functionType, string expression, Dictionary<string, string> customVariables)

    {
        if (string.IsNullOrWhiteSpace(scriptName))
        {
            throw new ArgumentException(nameof(scriptName));
        }

        // no scripting expression
        if (Context.ExportMode == ScriptExportMode.Existing && string.IsNullOrWhiteSpace(expression))
        {
            return;
        }

        // template from embedded assembly resources
        var resource = $"{nameof(Script)}\\{functionType}Function.Template.cs";
        var template = Assembly.GetEmbeddedFile(resource);
        if (string.IsNullOrWhiteSpace(template))
        {
            Log.Warning($"Missing script function template from resource {resource}");
            return;
        }

        // expression
        expression = GetFunctionCode(expression);

        // ensure safe type name
        scriptName = GetSafeName(scriptName);

        // apply common variables
        var className = $"{scriptName}{functionType}Function";
        var content = template.Replace("$TenantIdentifier$", Context.Tenant.Identifier)
            .Replace("$UserIdentifier$", Context.User.Identifier)
            .Replace("$EmployeeIdentifier$", Context.Employee.Identifier)
            .Replace("$PayrollName$", Context.Payroll.Name)
            .Replace("$RegulationName$", Context.Regulation.Name)
            .Replace("$Namespace$", string.IsNullOrWhiteSpace(Context.Namespace) ?
                DefaultNamespace : Context.Namespace)
            .Replace("$Expression$", string.IsNullOrWhiteSpace(expression) ?
                "// place your code here" : expression)
            .Replace("$ClassName$", className);

        // apply custom variables
        if (customVariables != null)
        {
            foreach (var customVariable in customVariables)
            {
                content = content.Replace($"${customVariable.Key}$", customVariable.Value);
            }
        }

        // add script
        scripts.Add(new()
        {
            FunctionType = functionType,
            OwnerId = ownerId,
            ScriptName = scriptName,
            ClassName = className,
            Content = content
        });
    }

    private static string GetFunctionCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return null;
        }
        if (!HasReturnStatement(code))
        {
            code = $"return {code}";
        }

        return code.EnsureEnd(";");
    }

    // ignore multi line statement
    private static bool HasReturnStatement(string code) =>
        code.Contains(';') || code.StartsWith("return");

    private static string GetSafeName(string name)
    {
        var invalids = Path.GetInvalidFileNameChars();
        var safeName = string.Join("_", name.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.')
            .Replace(" ", string.Empty)
            .Replace(".", string.Empty);
        return safeName;
    }

}