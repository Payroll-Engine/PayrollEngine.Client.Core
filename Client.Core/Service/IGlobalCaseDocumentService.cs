using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll global case document service</summary>
public interface IGlobalCaseDocumentService : IReadService<ICaseDocument, CaseValueServiceContext, Query>;