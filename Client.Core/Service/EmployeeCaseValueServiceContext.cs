using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Employee service context</summary>
public class EmployeeCaseValueServiceContext : EmployeeServiceContext
{
    /// <summary>Initializes a new instance of the <see cref="EmployeeCaseValueServiceContext"/> class</summary>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="employeeId">The employee id</param>
    /// <param name="caseValueId">The case value id</param>
    public EmployeeCaseValueServiceContext(int tenantId, int employeeId, int caseValueId) :
        base(tenantId, employeeId)
    {
        CaseValueId = caseValueId;
    }

    /// <summary>Initializes a new instance of the <see cref="EmployeeCaseValueServiceContext"/> class</summary>
    /// <param name="tenant">The tenant</param>
    /// <param name="employee">The employee</param>
    /// <param name="caseValue">The case value</param>
    public EmployeeCaseValueServiceContext(ITenant tenant, IEmployee employee, ICaseValue caseValue) :
        this(tenant.Id, employee.Id, caseValue.Id)
    {
    }

    /// <summary>The case value id</summary>
    public int CaseValueId { get; }
}