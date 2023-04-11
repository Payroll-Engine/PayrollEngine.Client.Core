using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll case change value service</summary>
public interface IPayrollCaseChangeValueService : IReadService<ICaseChangeCaseValue, PayrollServiceContext, PayrollCaseChangeQuery>
{
}