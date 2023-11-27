using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Regulation report log service</summary>
public interface IReportLogService : ICrudService<IReportLog, TenantServiceContext, Query>;