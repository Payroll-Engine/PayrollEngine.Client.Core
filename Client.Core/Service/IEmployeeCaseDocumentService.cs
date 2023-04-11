using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll employee case document service</summary>
public interface IEmployeeCaseDocumentService : IReadService<ICaseDocument, EmployeeCaseValueServiceContext, Query>
{
}