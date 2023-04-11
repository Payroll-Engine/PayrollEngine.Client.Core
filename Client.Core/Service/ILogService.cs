using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll log service</summary>
public interface ILogService : ICrudService<ILog, TenantServiceContext, Query>
{
}