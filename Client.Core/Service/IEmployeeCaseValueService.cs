using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll employee case value service</summary>
public interface IEmployeeCaseValueService : IReadService<ICaseValue, EmployeeServiceContext, CaseValueQuery>
{
    /// <summary>
    /// Get all case slots from a specific case field
    /// </summary>
    /// <param name="context">The service context</param>
    /// <param name="caseFieldName">The case field name</param>
    /// <returns>The case values</returns>
    Task<IEnumerable<string>> GetCaseValueSlotsAsync(EmployeeServiceContext context, string caseFieldName);
}