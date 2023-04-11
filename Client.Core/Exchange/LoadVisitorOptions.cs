using System;

namespace PayrollEngine.Client.Exchange;

/// <summary>Load visitor options <see cref="ImportExchangeVisitor"/></summary>
[Flags]
public enum LoadVisitorOptions
{
    /// <summary>No visitor options</summary>
    None = 0x0000,
    /// <summary>Load target object</summary>
    TargetLoad = 0x0001,
    /// <summary>Load scripts from the working folder</summary>
    ScriptLoad = 0x0002,
    /// <summary>Load case documents from the working folder</summary>
    CaseDocumentLoad = 0x0004,

    /// <summary>Load report templates from the working folder</summary>
    ReportTemplateLoad = 0x0010,
    /// <summary>Load report schemas from the working folder</summary>
    ReportSchemaLoad = 0x0020,

    /// <summary>Lookup validation</summary>
    LookupValidation = 0x0100,

    /// <summary>All visitor options</summary>
    All = TargetLoad |
          ScriptLoad |
          CaseDocumentLoad |
          ReportTemplateLoad |
          ReportSchemaLoad |
          LookupValidation
}