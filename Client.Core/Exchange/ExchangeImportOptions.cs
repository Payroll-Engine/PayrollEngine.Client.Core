
namespace PayrollEngine.Client.Exchange;

/// <summary>Exchange import options <see cref="ExchangeImportVisitor"/></summary>
public class ExchangeImportOptions
{
    /// <summary>Load target object</summary>
    public bool TargetLoad { get; set; } = true;

    /// <summary>Load scripts from the working folder</summary>
    public bool ScriptLoad { get; set; } = true;

    /// <summary>Load case documents from the working folder</summary>
    public bool CaseDocumentLoad { get; set; } = true;

    /// <summary>Load report templates from the working folder</summary>
    public bool ReportTemplateLoad { get; set; } = true;

    /// <summary>Load report schemas from the working folder</summary>
    public bool ReportSchemaLoad { get; set; } = true;

    /// <summary>Lookup validation</summary>
    public bool LookupValidation { get; set; } = true;
}
