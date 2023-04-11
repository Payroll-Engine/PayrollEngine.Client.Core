using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll company case document service</summary>
public interface ICompanyCaseDocumentService : IReadService<ICaseDocument, CaseValueServiceContext, Query>
{
}