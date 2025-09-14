using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll payrun job service</summary>
// ReSharper disable UnusedMemberInSuper.Global
public interface IPayrunJobService : ICrudService<IPayrunJob, TenantServiceContext, Query>, IAttributeService<TenantServiceContext>
{
    /// <summary>Query employee payrun jobs</summary>
    /// <param name="context">The service context</param>
    /// <param name="employeeId">The employee id</param>
    /// <param name="query">The query parameters</param>
    /// <returns>Payrun jobs of the employee</returns>
    Task<List<T>> QueryEmployeePayrunJobsAsync<T>(TenantServiceContext context,
        int employeeId, Query query = null) where T : class, IPayrunJob;


    /// <summary>Query employee payrun jobs count</summary>
    /// <param name="context">The service context</param>
    /// <param name="employeeId">The employee id</param>
    /// <param name="query">The query parameters</param>
    /// <returns>Payrun jobs of the employee</returns>
    Task<long> QueryEmployeePayrunJobsCountAsync(TenantServiceContext context, int employeeId, Query query = null);


    /// <summary>Query items and count of objects</summary>
    /// <param name="context">The service context</param>
    /// <param name="employeeId">The employee id</param>
    /// <param name="query">The query</param>
    /// <returns>List and count of objects</returns>
    Task<QueryResult<T>> QueryEmployeePayrunJobsCountAsync<T>(TenantServiceContext context, int employeeId,
        Query query = null) where T : class, IPayrunJob;

    /// <summary>Get payrun job by identifier</summary>
    /// <param name="context">The service context</param>
    /// <param name="name">The payrun job name</param>
    /// <returns>The payrun job, null if missing</returns>
    Task<T> GetAsync<T>(TenantServiceContext context, string name) where T : class, IPayrunJob;

    /// <summary>Get the status of a payrun job</summary>
    /// <param name="context">The service context</param>
    /// <param name="payrunJobId">The payrun job id</param>
    /// <returns>The payrun job status</returns>
    Task<string> GetJobStatusAsync(TenantServiceContext context, int payrunJobId);

    /// <summary>Start a payrun job, if the payroll has no previous draft payrun</summary>
    /// <param name="context">The service context</param>
    /// <param name="jobInvocation">The payrun job invocation</param>
    /// <returns>The newly created payrun job</returns>
    Task<T> StartJobAsync<T>(TenantServiceContext context, PayrunJobInvocation jobInvocation) where T : class, IPayrunJob;

    /// <summary>Get the status of a payrun job</summary>
    /// <param name="context">The service context</param>
    /// <param name="payrunJobId">The payrun job id</param>
    /// <param name="jobStatus">The target payrun job status</param>
    /// <param name="userId">The user id</param>
    /// <param name="reason">The change reason id</param>
    /// <param name="patchMode">The patch mode</param>
    /// <returns>The payrun job status</returns>
    Task ChangeJobStatusAsync(TenantServiceContext context, int payrunJobId,
        PayrunJobStatus jobStatus, int userId, string reason, bool patchMode);
}