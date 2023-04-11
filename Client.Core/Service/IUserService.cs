using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll user service</summary>
public interface IUserService : ICrudService<IUser, TenantServiceContext, Query>, IAttributeService<TenantServiceContext>
{
    /// <summary>Get user by identifier</summary>
    /// <param name="context">The service context</param>
    /// <param name="identifier">The user identifier</param>
    /// <returns>The user, null if missing</returns>
    Task<T> GetAsync<T>(TenantServiceContext context, string identifier) where T : class, IUser;

    /// <summary>
    /// Test user password
    /// </summary>
    /// <param name="context">The service context</param>
    /// <param name="userId">The user id</param>
    /// <param name="password">The new user password</param>
    /// <returns>True for a valid password</returns>
    Task<bool> TestPasswordAsync(TenantServiceContext context, int userId, string password);

    /// <summary>
    /// Update the user password
    /// </summary>
    /// <param name="context">The service context</param>
    /// <param name="userId">The user id</param>
    /// <param name="password">The new user password, use null to reset the password</param>
    /// <returns>The updated user</returns>
    Task UpdatePasswordAsync(TenantServiceContext context, int userId, string password);
}