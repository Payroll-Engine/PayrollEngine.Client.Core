using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll task service</summary>
public interface ITaskService : ICrudService<ITask, TenantServiceContext, Query>
{
}