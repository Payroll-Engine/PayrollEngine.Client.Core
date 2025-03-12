using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using PayrollEngine.Client.Script;
using PayrollEngine.Client.Service.Api;
using Calendar = PayrollEngine.Client.Model.Calendar;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Exchange;

/// <summary>Import exchange from JSON file to Payroll API</summary>
public abstract class ExchangeImportVisitor : Visitor
{
    private readonly TextFileCache textFiles = new();

    /// <summary>The visitor load options</summary>
    public ExchangeImportOptions ImportOptions { get; }

    /// <summary>The Payroll http client</summary>
    public PayrollHttpClient HttpClient { get; }

    /// <summary>The script parser</summary>
    public IScriptParser ScriptParser { get; }

    /// <summary>Initializes a new instance of the <see cref="VisitorBase"/> class</summary>
    /// <remarks>Content is loaded from the working folder</remarks>
    /// <param name="httpClient">The Payroll http client</param>
    /// <param name="exchange">The exchange model</param>
    /// <param name="scriptParser">The script parser</param>
    /// <param name="importOptions">The import options</param>
    protected ExchangeImportVisitor(PayrollHttpClient httpClient, Client.Model.Exchange exchange,
        IScriptParser scriptParser, ExchangeImportOptions importOptions = null) :
        base(exchange)
    {
        HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        ScriptParser = scriptParser ?? throw new ArgumentNullException(nameof(scriptParser));
        ImportOptions = importOptions ?? new();
    }

    /// <summary>Load target object</summary>
    protected bool TargetLoad => ImportOptions.TargetLoad;

    /// <summary>Load scripts from the working folder</summary>
    protected bool ScriptLoad => ImportOptions.ScriptLoad;

    /// <summary>Load case documents from the working folder</summary>
    protected bool CaseDocumentLoad => ImportOptions.CaseDocumentLoad;

    /// <summary>Load report templates from file</summary>
    protected bool ReportTemplateLoad => ImportOptions.ReportTemplateLoad;

    /// <summary>Load report templates from the working folder</summary>
    protected bool ReportSchemaLoad => ImportOptions.ReportSchemaLoad;

    /// <summary>Load report schemas from the working folder</summary>
    protected bool LookupValidation => ImportOptions.LookupValidation;

    /// <summary>
    /// Reads a text file as string
    /// </summary>
    /// <param name="fileName">Name of the file</param>
    /// <returns>The file content as string</returns>
    protected string ReadTextFile(string fileName) =>
        textFiles.ReadTextFile(fileName);

    #region Tenant

    /// <summary>Get tenant</summary>
    /// <param name="tenantIdentifier">The tenant identifier</param>
    protected virtual async Task<Tenant> GetTenantAsync(string tenantIdentifier) =>
        await new TenantService(HttpClient).GetAsync<Tenant>(new(), tenantIdentifier);

    /// <summary>Visit the exchange tenant</summary>
    /// <param name="tenant">The exchange tenant</param>
    protected override async Task VisitExchangeTenantAsync(IExchangeTenant tenant)
    {
        // exchange tenant
        var targetTenant = TargetLoad ? await GetTenantAsync(tenant.Identifier) : null;
        if (targetTenant != null && string.IsNullOrWhiteSpace(tenant.Culture))
        {
            tenant.Culture = targetTenant.Culture;
        }

        await SetupTenantAsync(tenant, targetTenant);

        await base.VisitExchangeTenantAsync(tenant);
    }

    /// <summary>Tenant setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="targetTenant">The target tenant</param>
    protected virtual Task SetupTenantAsync(IExchangeTenant tenant, ITenant targetTenant) =>
        Task.CompletedTask;

    #endregion

    #region Users

    /// <summary>Get user</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="userIdentifier">The user identifier</param>
    protected virtual async Task<User> GetUserAsync(int tenantId, string userIdentifier) =>
        await new UserService(HttpClient).GetAsync<User>(new(tenantId), userIdentifier);

    /// <summary>Visit the user</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="user">The user</param>
    protected override async Task VisitUserAsync(IExchangeTenant tenant, IUser user)
    {
        if (user == null || string.IsNullOrWhiteSpace(user.Identifier))
        {
            return;
        }

        // get user
        var target = TargetLoad ? await GetUserAsync(tenant.Id, user.Identifier) : null;

        // setup user
        await SetupUserAsync(tenant, user, target);

        await base.VisitUserAsync(tenant, user);
    }

    /// <summary>USer setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="user">The user</param>
    /// <param name="targetUser">The target user</param>
    protected virtual Task SetupUserAsync(IExchangeTenant tenant, IUser user, IUser targetUser) =>
        Task.CompletedTask;

    #endregion

    #region Calendars

    /// <summary>Get calendar</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="name">The calendar name</param>
    protected virtual async Task<Calendar> GetCalendarAsync(int tenantId, string name) =>
        await new CalendarService(HttpClient).GetAsync<Calendar>(new(tenantId), name);

    /// <summary>Visit the calendar</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="calendar">The calendar</param>
    protected override async Task VisitCalendarAsync(IExchangeTenant tenant, ICalendar calendar)
    {
        // get calendar
        var target = TargetLoad ? await GetCalendarAsync(tenant.Id, calendar.Name) : null;

        // setup calendar
        await SetupCalendarAsync(tenant, calendar, target);

        await base.VisitCalendarAsync(tenant, calendar);
    }

    /// <summary>USer setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="calendar">The calendar</param>
    /// <param name="targetCalendar">The target calendar</param>
    protected virtual Task SetupCalendarAsync(IExchangeTenant tenant, ICalendar calendar, ICalendar targetCalendar) =>
        Task.CompletedTask;

    #endregion

    #region Divisions

    /// <summary>Get division</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="name">The division identifier</param>
    protected virtual async Task<Division> GetDivisionAsync(int tenantId, string name) =>
        await new DivisionService(HttpClient).GetAsync<Division>(new(tenantId), name);

    /// <summary>Visit the division</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="division">The division</param>
    protected override async Task VisitDivisionAsync(IExchangeTenant tenant, IDivision division)
    {
        // get division
        var target = TargetLoad ? await GetDivisionAsync(tenant.Id, division.Name) : null;

        // setup division
        await SetupDivisionAsync(tenant, division, target);

        await base.VisitDivisionAsync(tenant, division);
    }

    /// <summary>Division setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="division">The division</param>
    /// <param name="targetDivision">The target division</param>
    protected virtual Task SetupDivisionAsync(IExchangeTenant tenant, IDivision division, IDivision targetDivision) =>
        Task.CompletedTask;

    #endregion

    #region Webhooks

    /// <summary>Visit the webhook</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="webhook">The webhook</param>
    protected override async Task VisitWebhookAsync(IExchangeTenant tenant, IWebhook webhook)
    {
        // get webhook
        var target = TargetLoad ? await new WebhookService(HttpClient).GetAsync<Webhook>(
            new(tenant.Id), webhook.Name) : null;

        // setup webhook
        await SetupWebhookAsync(tenant, webhook, target);

        await base.VisitWebhookAsync(tenant, webhook);
    }

    /// <summary>Webhook setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="webhook">The webhook</param>
    /// <param name="targetWebhook">The target webhook</param>
    protected virtual Task SetupWebhookAsync(IExchangeTenant tenant, IWebhook webhook, IWebhook targetWebhook) =>
        Task.CompletedTask;

    #endregion

    #region Regulation

    /// <summary>Get division</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="name">The regulation name</param>
    /// <param name="version">The regulation version</param>
    protected virtual async Task<Regulation> GetRegulationAsync(int tenantId, string name, int? version = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(nameof(name));
        }

        // get existing objects
        var filter = version.HasValue ?
            QueryFactory.NewEqualsFilterQuery(nameof(name), name, nameof(version), version) :
            QueryFactory.NewEqualFilterQuery(nameof(name), name);
        var regulation = (await new RegulationService(HttpClient).QueryAsync<Regulation>(
            new(tenantId), filter)).FirstOrDefault();
        return regulation;
    }

    /// <summary>Visit the regulation</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    protected override async Task VisitRegulationAsync(IExchangeTenant tenant, IRegulationSet regulation)
    {
        // get regulation
        var target = TargetLoad ? await GetRegulationAsync(tenant.Id, regulation.Name, regulation.Version) : null;

        // setup regulation
        await SetupRegulationAsync(tenant, regulation, target);

        await base.VisitRegulationAsync(tenant, regulation);
    }

    /// <summary>Regulation setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="targetRegulation">The target regulation</param>
    protected virtual Task SetupRegulationAsync(IExchangeTenant tenant, IRegulationSet regulation, IRegulation targetRegulation) =>
        Task.CompletedTask;

    #endregion

    #region Lookups

    /// <summary>Visit the lookup</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="lookup">The lookup</param>
    protected override async Task VisitLookupAsync(IExchangeTenant tenant, IRegulationSet regulation, ILookupSet lookup)
    {
        // validate lookup
        if (LookupValidation)
        {
            ValidateLookup(lookup);
        }

        // get lookup
        var target = TargetLoad ? await new LookupService(HttpClient).GetAsync<Lookup>(
            new(tenant.Id, regulation.Id), lookup.Name) : null;

        // setup lookup
        await SetupLookupAsync(tenant, regulation, lookup, target);

        await base.VisitLookupAsync(tenant, regulation, lookup);
    }

    /// <summary>Lookup setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="lookup">The lookup</param>
    /// <param name="targetLookup">The target lookup</param>
    protected virtual Task SetupLookupAsync(IExchangeTenant tenant, IRegulationSet regulation, ILookupSet lookup, ILookup targetLookup) =>
        Task.CompletedTask;

    /// <summary>Visit the lookup value</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="lookup">The lookup</param>
    /// <param name="lookupValue">The lookup value</param>
    protected override async Task VisitLookupValueAsync(IExchangeTenant tenant, IRegulationSet regulation, ILookupSet lookup, ILookupValue lookupValue)
    {
        // key
        if (string.IsNullOrWhiteSpace(lookupValue.Key))
        {
            throw new PayrollException("Lookup value without key.");
        }

        // get lookup value by key and range value
        var query = QueryFactory.NewEqualsFilterQuery(
            nameof(lookupValue.Key), lookupValue.Key,
            nameof(lookupValue.RangeValue), lookupValue.RangeValue);
        var target = TargetLoad ? (await new LookupValueService(HttpClient)
            .QueryAsync<LookupValue>(new(tenant.Id, regulation.Id, lookup.Id), query)).FirstOrDefault() : null;

        // setup lookup value
        await SetupLookupValueAsync(tenant, regulation, lookup, lookupValue, target);

        await base.VisitLookupValueAsync(tenant, regulation, lookup, lookupValue);
    }

    /// <summary>Lookup value setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="lookup">The lookup</param>
    /// <param name="lookupValue">The lookup value</param>
    /// <param name="targetLookupValue">The target lookup value</param>
    protected virtual Task SetupLookupValueAsync(IExchangeTenant tenant, IRegulationSet regulation, ILookupSet lookup,
        ILookupValue lookupValue, ILookupValue targetLookupValue) =>
        Task.CompletedTask;

    /// <summary>Validate the lookup</summary>
    /// <param name="lookup">The lookup</param>
    protected virtual void ValidateLookup(ILookupSet lookup)
    {
        var valueKeys = new Dictionary<int, LookupValue>();
        foreach (var lookupValue in lookup.Values)
        {
            var valueKey = lookupValue.Key.ToPayrollHash(lookupValue.RangeValue);
            if (valueKeys.TryGetValue(valueKey, out var key))
            {
                throw new PayrollException($"Duplicated lookup value: key={key.Key}, range-value={key.RangeValue}.");
            }
            valueKeys.Add(valueKey, lookupValue);
        }
    }

    #endregion

    #region Case and Case Fields

    /// <summary>Visit the case</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="caseSet">The case</param>
    protected override async Task VisitCaseAsync(IExchangeTenant tenant, IRegulationSet regulation, ICaseSet caseSet)
    {
        if (ScriptLoad)
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
                if (string.IsNullOrWhiteSpace(caseSet.ValidateExpression))
                {
                    throw new PayrollException($"Missing or invalid script file {caseSet.ValidateExpressionFile}.");
                }
            }
        }

        // get case
        var target = TargetLoad ? await new CaseService(HttpClient).GetAsync<CaseSet>(
            new(tenant.Id, regulation.Id), caseSet.Name) : null;

        // setup case
        await SetupCaseAsync(tenant, regulation, caseSet, target);

        await base.VisitCaseAsync(tenant, regulation, caseSet);
    }

    /// <summary>Case setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="caseSet">The case</param>
    /// <param name="targetCase">The target case</param>
    protected virtual Task SetupCaseAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICaseSet caseSet, ICase targetCase) =>
        Task.CompletedTask;

    /// <summary>Visit the case field</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="caseSet">The case</param>
    /// <param name="caseField">The case field</param>
    protected override async Task VisitCaseFieldAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICaseSet caseSet, ICaseField caseField)
    {
        // get case field
        var target = TargetLoad ? await new CaseFieldService(HttpClient).GetAsync<CaseField>(
            new(tenant.Id, regulation.Id, caseSet.Id), caseField.Name) : null;

        // setup case field
        await SetupCaseFieldAsync(tenant, regulation, caseSet, caseField, target);

        await base.VisitCaseFieldAsync(tenant, regulation, caseSet, caseField);
    }

    /// <summary>Case field setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="caseSet">The case</param>
    /// <param name="caseField">The case field</param>
    /// <param name="targetCaseField">The target case field</param>
    protected virtual Task SetupCaseFieldAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICaseSet caseSet, ICaseField caseField, ICaseField targetCaseField) =>
        Task.CompletedTask;

    #endregion

    #region Case Relation

    /// <summary>Visit the case relation</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="caseRelation">The case relation</param>
    protected override async Task VisitCaseRelationAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICaseRelation caseRelation)
    {
        if (ScriptLoad)
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
                if (string.IsNullOrWhiteSpace(caseRelation.ValidateExpression))
                {
                    throw new PayrollException(
                        $"Missing or invalid script file {caseRelation.ValidateExpressionFile}.");
                }
            }
        }

        // get case relation
        var target = TargetLoad ? await new CaseRelationService(HttpClient)
            .GetAsync<CaseRelation>(new(tenant.Id, regulation.Id), caseRelation.SourceCaseName,
                caseRelation.TargetCaseName, caseRelation.SourceCaseSlot, caseRelation.TargetCaseSlot) : null;

        // setup case relation
        await SetupCaseRelationAsync(tenant, regulation, caseRelation, target);

        await base.VisitCaseRelationAsync(tenant, regulation, caseRelation);
    }

    /// <summary>Case relation setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="caseRelation">The case relation</param>
    /// <param name="targetCaseRelation">The target case relation</param>
    protected virtual Task SetupCaseRelationAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICaseRelation caseRelation, ICaseRelation targetCaseRelation) =>
        Task.CompletedTask;

    #endregion

    #region Collector

    /// <summary>Visit the collector</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="collector">The collector</param>
    protected override async Task VisitCollectorAsync(IExchangeTenant tenant, IRegulationSet regulation, ICollector collector)
    {
        if (ScriptLoad)
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
                if (string.IsNullOrWhiteSpace(collector.ApplyExpression))
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
                if (string.IsNullOrWhiteSpace(collector.EndExpression))
                {
                    throw new PayrollException($"Missing or invalid script file {collector.EndExpressionFile}.");
                }
            }
        }

        // get collector
        var target = TargetLoad ? await new CollectorService(HttpClient).GetAsync<Collector>(
            new(tenant.Id, regulation.Id), collector.Name) : null;

        // setup collector
        await SetupCollectorAsync(tenant, regulation, collector, target);

        await base.VisitCollectorAsync(tenant, regulation, collector);
    }

    /// <summary>Case relation setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="collector">The collector</param>
    /// <param name="targetCollector">The target collector</param>
    protected virtual Task SetupCollectorAsync(IExchangeTenant tenant, IRegulationSet regulation,
        ICollector collector, ICollector targetCollector) =>
        Task.CompletedTask;

    #endregion

    #region Wage Type

    /// <summary>Visit the wage type</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="wageType">The wage type</param>
    protected override async Task VisitWageTypeAsync(IExchangeTenant tenant, IRegulationSet regulation, IWageType wageType)
    {
        if (ScriptLoad)
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
                if (string.IsNullOrWhiteSpace(wageType.ResultExpression))
                {
                    throw new PayrollException($"Missing or invalid script file {wageType.ResultExpressionFile}.");
                }
            }
        }

        // culture
        var culture = CultureInfo.CurrentCulture;
        if (!string.IsNullOrWhiteSpace(tenant.Culture) && !string.Equals(tenant.Culture, culture.Name))
        {
            culture = new CultureInfo(tenant.Culture);
        }

        // get wage type
        var target = TargetLoad ? await new WageTypeService(HttpClient).GetAsync<WageType>(
            new(tenant.Id, regulation.Id), wageType.WageTypeNumber, culture) : null;

        // setup wage type
        await SetupWageTypeAsync(tenant, regulation, wageType, target);

        await base.VisitWageTypeAsync(tenant, regulation, wageType);
    }

    /// <summary>Wage type setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="wageType">The wage type</param>
    /// <param name="targetWageType">The target wage type</param>
    protected virtual Task SetupWageTypeAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IWageType wageType, IWageType targetWageType) =>
        Task.CompletedTask;

    #endregion

    #region Script

    /// <summary>Visit the script</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="script">The script</param>
    protected override async Task VisitScriptAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IScript script)
    {
        if (ScriptLoad)
        {
            // script value file
            if (string.IsNullOrWhiteSpace(script.Value) && !string.IsNullOrWhiteSpace(script.ValueFile))
            {
                script.Value = ReadTextFile(script.ValueFile);
                if (string.IsNullOrWhiteSpace(script.Value))
                {
                    throw new PayrollException($"Missing script value in file {script.ValueFile}.");
                }
            }
        }

        // get script
        var target = TargetLoad ? await new ScriptService(HttpClient).GetAsync<Client.Model.Script>(
            new(tenant.Id, regulation.Id), script.Name) : null;

        // setup script
        await SetupScriptAsync(tenant, regulation, script, target);

        await base.VisitScriptAsync(tenant, regulation, script);
    }

    /// <summary>Script setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="script">The script</param>
    /// <param name="targetScript">The target script</param>
    protected virtual Task SetupScriptAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IScript script, IScript targetScript) =>
        Task.CompletedTask;

    #endregion

    #region Report

    /// <summary>Visit the script</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="report">The report</param>
    protected override async Task VisitReportAsync(IExchangeTenant tenant, IRegulationSet regulation, IReportSet report)
    {
        if (ScriptLoad)
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
                if (string.IsNullOrWhiteSpace(report.EndExpression))
                {
                    throw new PayrollException($"Missing or invalid script file {report.EndExpressionFile}.");
                }
            }
        }

        // get report
        var target = TargetLoad ? await new ReportSetService(HttpClient).GetAsync<ReportSet>(
            new(tenant.Id, regulation.Id), report.Name) : null;

        // setup report
        await SetupReportAsync(tenant, regulation, report, target);

        await base.VisitReportAsync(tenant, regulation, report);
    }

    /// <summary>Report setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="report">The report</param>
    /// <param name="targetReport">The target report</param>
    protected virtual Task SetupReportAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report, IReportSet targetReport) =>
        Task.CompletedTask;

    /// <summary>Visit the report parameter</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="report">The report</param>
    /// <param name="parameter">The report parameter</param>
    protected override async Task VisitReportParameterAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report, IReportParameter parameter)
    {
        // get report parameter
        var target = TargetLoad ? await new ReportParameterService(HttpClient).GetAsync<ReportParameter>(
            new(tenant.Id, regulation.Id, report.Id), parameter.Name) : null;

        // setup report parameter
        await SetupReportParameterAsync(tenant, regulation, report, parameter, target);

        await base.VisitReportParameterAsync(tenant, regulation, report, parameter);
    }

    /// <summary>Report parameter setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="report">The report</param>
    /// <param name="parameter">The report parameter</param>
    /// <param name="targetParameter">The target report parameter</param>
    protected virtual Task SetupReportParameterAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report, IReportParameter parameter, IReportParameter targetParameter) =>
        Task.CompletedTask;

    /// <summary>Visit the report template</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="report">The report</param>
    /// <param name="template">The report template</param>
    protected override async Task VisitReportTemplateAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report, IReportTemplate template)
    {
        // report content file (binary or xsl text file)
        if (ReportTemplateLoad && string.IsNullOrWhiteSpace(template.Content) &&
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
        }

        // report schema file (xsd text file)
        if (ReportSchemaLoad && string.IsNullOrWhiteSpace(template.Schema) &&
            !string.IsNullOrWhiteSpace(template.SchemaFile))
        {
            template.Schema = ReadTemplateContent(template.SchemaFile);
        }

        // get report template
        var target = TargetLoad ? (await new ReportTemplateService(HttpClient).QueryAsync<ReportTemplate>(
                new(tenant.Id, regulation.Id, report.Id), new() { Culture = template.Culture }))
            .FirstOrDefault() : null;

        // setup report template
        await SetupReportTemplateAsync(tenant, regulation, report, template, target);

        await base.VisitReportTemplateAsync(tenant, regulation, report, template);
    }

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

    /// <summary>Report template setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="regulation">The regulation</param>
    /// <param name="report">The report</param>
    /// <param name="template">The report template</param>
    /// <param name="targetTemplate">The target report template</param>
    protected virtual Task SetupReportTemplateAsync(IExchangeTenant tenant, IRegulationSet regulation,
        IReportSet report, IReportTemplate template, IReportTemplate targetTemplate) =>
        Task.CompletedTask;

    #endregion

    #region Employees

    /// <summary>Get employee</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="identifier">The employee identifier</param>
    protected virtual async Task<Employee> GetEmployeeAsync(int tenantId, string identifier) =>
        await new EmployeeService(HttpClient).GetAsync<Employee>(new(tenantId), identifier);

    /// <summary>Visit the employee</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="employee">The employee</param>
    protected override async Task VisitEmployeeAsync(IExchangeTenant tenant, IEmployeeSet employee)
    {
        // get employee
        var target = TargetLoad ? await new EmployeeService(HttpClient).GetAsync<Employee>(
            new(tenant.Id), employee.Identifier) : null;

        // setup employee
        await SetupEmployeeAsync(tenant, employee, target);

        await base.VisitEmployeeAsync(tenant, employee);
    }

    /// <summary>Employee setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="employee">The employee</param>
    /// <param name="targetEmployee">The target employee</param>
    protected virtual Task SetupEmployeeAsync(IExchangeTenant tenant, IEmployeeSet employee,
        IEmployee targetEmployee) =>
        Task.CompletedTask;

    #endregion

    #region Payroll

    /// <summary>Get payroll</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="payrollName">The payroll name</param>
    protected virtual async Task<Payroll> GetPayrollAsync(int tenantId, string payrollName)
    {
        if (string.IsNullOrWhiteSpace(payrollName))
        {
            throw new ArgumentException(nameof(payrollName));
        }

        // get existing object
        var payroll = TargetLoad ? await new PayrollService(HttpClient).GetAsync<Payroll>(
            new(tenantId), payrollName) : null;
        if (payroll == null)
        {
            throw new PayrollException($"Unknown payroll with name {payrollName}.");
        }
        return payroll;
    }

    /// <summary>Visit the payroll</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="payroll">The payroll</param>
    protected override async Task VisitPayrollAsync(IExchangeTenant tenant, IPayrollSet payroll)
    {
        // division
        if (tenant.Id > 0 && payroll.DivisionId <= 0 && !string.IsNullOrWhiteSpace(payroll.DivisionName))
        {
            var division = await GetDivisionAsync(tenant.Id, payroll.DivisionName);
            if (division == null)
            {
                throw new PayrollException($"Missing division with name {payroll.DivisionName}.");
            }
            payroll.DivisionId = division.Id;
        }

        // get payroll
        var target = TargetLoad ? await new PayrollService(HttpClient).GetAsync<Payroll>(
            new(tenant.Id), payroll.Name) : null;

        // setup payroll
        await SetupPayrollAsync(tenant, payroll, target);

        await base.VisitPayrollAsync(tenant, payroll);
    }

    /// <summary>Payroll setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="payroll">The payroll</param>
    /// <param name="targetPayroll">The target payroll</param>
    protected virtual Task SetupPayrollAsync(IExchangeTenant tenant, IPayrollSet payroll,
        IPayroll targetPayroll) =>
        Task.CompletedTask;

    #endregion

    #region Payroll Layer

    /// <summary>Visit the payroll layer</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="payroll">The payroll</param>
    /// <param name="layer">The payroll layer</param>
    protected override async Task VisitPayrollLayerAsync(IExchangeTenant tenant, IPayrollSet payroll,
        IPayrollLayer layer)
    {
        // get payroll layer by unique regulation name per payroll
        var query = QueryFactory.NewEqualFilterQuery(nameof(layer.RegulationName), layer.RegulationName);
        var target = TargetLoad ? (await new PayrollLayerService(HttpClient).
            QueryAsync<PayrollLayer>(new(tenant.Id, payroll.Id), query)).FirstOrDefault() : null;

        // setup payroll layer
        await SetupPayrollLayerAsync(tenant, payroll, layer, target);

        await base.VisitPayrollLayerAsync(tenant, payroll, layer);
    }

    /// <summary>Payroll layer setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="payroll">The payroll</param>
    /// <param name="layer">The payroll layer</param>
    /// <param name="targetLayer">The target payroll layer</param>
    protected virtual Task SetupPayrollLayerAsync(IExchangeTenant tenant, IPayrollSet payroll,
        IPayrollLayer layer, IPayrollLayer targetLayer) =>
        Task.CompletedTask;

    #endregion

    #region Case Changes

    /// <summary>Visit the case change</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="payroll">The payroll</param>
    /// <param name="caseChangeSetup">The case change</param>
    protected override async Task VisitCaseChangeSetupAsync(IExchangeTenant tenant, IPayrollSet payroll,
        ICaseChangeSetup caseChangeSetup)
    {
        if (tenant.Id > 0)
        {
            // user
            if (!string.IsNullOrWhiteSpace(caseChangeSetup.UserIdentifier))
            {
                var user = await GetUserAsync(tenant.Id, caseChangeSetup.UserIdentifier);
                if (user == null)
                {
                    throw new PayrollException($"Unknown user with identifier {caseChangeSetup.UserIdentifier}.");
                }
                caseChangeSetup.UserId = user.Id;
            }

            // employee
            if (!caseChangeSetup.EmployeeId.HasValue && !string.IsNullOrWhiteSpace(caseChangeSetup.EmployeeIdentifier))
            {
                var employee = await GetEmployeeAsync(tenant.Id, caseChangeSetup.EmployeeIdentifier);
                if (employee == null)
                {
                    throw new PayrollException($"Missing case change employee with identifier {caseChangeSetup.EmployeeIdentifier}.");
                }
                caseChangeSetup.EmployeeId = employee.Id;
            }

            // case change task by name
            if (!caseChangeSetup.DivisionId.HasValue && !string.IsNullOrWhiteSpace(caseChangeSetup.DivisionName))
            {
                var division = await GetDivisionAsync(tenant.Id, caseChangeSetup.DivisionName);
                if (division == null)
                {
                    throw new PayrollException($"Missing case change division with name {caseChangeSetup.DivisionName}.");
                }
                caseChangeSetup.DivisionId = division.Id;
            }

            // case change task
            var caseValues = caseChangeSetup.CollectCaseValues();
            foreach (var caseValue in caseValues)
            {
                // ensure same task for the case change and all case values
                if (caseChangeSetup.DivisionId.HasValue)
                {
                    caseValue.DivisionId = caseChangeSetup.DivisionId.Value;
                }
                else if (!caseValue.DivisionId.HasValue && !string.IsNullOrWhiteSpace(caseValue.DivisionName))
                {
                    var division = await GetDivisionAsync(tenant.Id, caseValue.DivisionName);
                    if (division == null)
                    {
                        throw new PayrollException($"Missing case value division with name {caseChangeSetup.DivisionName}.");
                    }
                    caseValue.DivisionId = division.Id;
                }
            }

            // created date
            if (Exchange.CreatedObjectDate.HasValue)
            {
                // case change created date
                if (!caseChangeSetup.Created.IsDefined())
                {
                    caseChangeSetup.Created = Exchange.CreatedObjectDate.Value;
                }
                // case value created date
                foreach (var caseValue in caseValues)
                {
                    if (!caseValue.Created.IsDefined())
                    {
                        caseValue.Created = Exchange.CreatedObjectDate.Value;
                    }
                }
            }
        }

        // documents
        SetupCaseChangeDocuments(caseChangeSetup.Case);

        // create case change
        await SetupCaseChangeAsync(tenant, payroll, caseChangeSetup);

        await base.VisitCaseChangeSetupAsync(tenant, payroll, caseChangeSetup);
    }

    /// <summary>Case change documents setup</summary>
    /// <param name="caseSetup">The case change</param>
    protected virtual void SetupCaseChangeDocuments(ICaseSetup caseSetup)
    {
        // case value documents
        if (CaseDocumentLoad && caseSetup.Values != null)
        {
            foreach (var value in caseSetup.Values)
            {
                if (value.Documents != null && value.Documents.Any())
                {
                    foreach (var document in value.Documents)
                    {
                        if (string.IsNullOrWhiteSpace(document.Content) && !string.IsNullOrWhiteSpace(document.ContentFile))
                        {
                            document.Content = BinaryFile.Read(document.ContentFile);
                        }
                    }
                }
            }
        }

        // related case documents
        if (caseSetup.RelatedCases != null && caseSetup.RelatedCases.Any())
        {
            foreach (var relatedCase in caseSetup.RelatedCases)
            {
                SetupCaseChangeDocuments(relatedCase);
            }
        }
    }

    /// <summary>Case change setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="payroll">The payroll</param>
    /// <param name="caseChangeSetup">The case change</param>
    protected virtual Task SetupCaseChangeAsync(IExchangeTenant tenant, IPayrollSet payroll,
        ICaseChangeSetup caseChangeSetup) =>
        Task.CompletedTask;

    #endregion

    #region Payrun

    /// <summary>Get payrun</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="payrunName">The payrun name</param>
    protected virtual async Task<Payrun> GetPayrunAsync(int tenantId, string payrunName) =>
        await new PayrunService(HttpClient).GetAsync<Payrun>(new(tenantId), payrunName);

    /// <summary>Get payrun parameter</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="payrunId">The payrun id</param>
    /// <param name="parameterName">The payrun parameter name</param>
    protected virtual async Task<PayrunParameter> GetPayrunParameterAsync(int tenantId, int payrunId,
        string parameterName) =>
        await new PayrunParameterService(HttpClient).GetAsync<PayrunParameter>(new(tenantId, payrunId), parameterName);

    /// <summary>Get payrun job</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="payrunJobId">The payrun job name</param>
    protected virtual async Task<PayrunJob> GetPayrunJobAsync(int tenantId, int payrunJobId) =>
        await new PayrunJobService(HttpClient).GetAsync<PayrunJob>(new(tenantId), payrunJobId);

    /// <summary>Visit the payrun</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="payrun">The payrun</param>
    protected override async Task VisitPayrunAsync(IExchangeTenant tenant, IPayrun payrun)
    {
        if (ScriptLoad)
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
                if (string.IsNullOrWhiteSpace(payrun.EndExpression))
                {
                    throw new PayrollException($"Missing or invalid script file {payrun.EndExpressionFile}.");
                }
            }
        }

        // payroll
        if (tenant.Id > 0 && !string.IsNullOrWhiteSpace(payrun.PayrollName))
        {
            var payroll = await GetPayrollAsync(tenant.Id, payrun.PayrollName);
            payrun.PayrollId = payroll.Id;
        }

        // payrun
        var target = TargetLoad ? await GetPayrunAsync(tenant.Id, payrun.Name) : null;
        await SetupPayrunAsync(tenant, payrun, target);

        await base.VisitPayrunAsync(tenant, payrun);
    }

    /// <summary>Payrun setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="payrun">The payrun</param>
    /// <param name="targetPayrun">The target payrun</param>
    protected virtual Task SetupPayrunAsync(IExchangeTenant tenant, IPayrun payrun,
        IPayrun targetPayrun) =>
        Task.CompletedTask;

    /// <inheritdoc />
    protected override async Task VisitPayrunParameterAsync(IExchangeTenant tenant, IPayrun payrun,
        IPayrunParameter parameter)
    {
        // payrun parameter
        var target = TargetLoad ? await GetPayrunParameterAsync(tenant.Id, payrun.Id, payrun.Name) : null;
        await SetupPayrunParameterAsync(tenant, payrun, parameter, target);

        await base.VisitPayrunParameterAsync(tenant, payrun, parameter);
    }

    /// <summary>Payrun setup</summary>
    /// <param name="tenant">The exchange tenant</param>
    /// <param name="payrun">The payrun</param>
    /// <param name="parameter">The payrun parameter</param>
    /// <param name="targetPayrunParameter">The target payrun parameter</param>
    protected virtual Task SetupPayrunParameterAsync(IExchangeTenant tenant, IPayrun payrun,
        IPayrunParameter parameter, IPayrunParameter targetPayrunParameter) =>
        Task.CompletedTask;

    #endregion

}