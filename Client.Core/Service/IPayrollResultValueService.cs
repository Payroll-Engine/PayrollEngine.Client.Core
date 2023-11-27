using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll result value service</summary>
public interface IPayrollResultValueService : IReadService<IPayrollResultValue, PayrollResultValueServiceContext, Query>;