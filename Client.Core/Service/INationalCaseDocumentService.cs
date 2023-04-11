using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll national case document service</summary>
public interface INationalCaseDocumentService : IReadService<ICaseDocument, CaseValueServiceContext, Query>
{
}