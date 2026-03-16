using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll backend administration service</summary>
public interface IAdminService
{
    /// <summary>Get backend server information</summary>
    /// <returns>The backend information</returns>
    Task<BackendInformation> GetBackendInformationAsync();
}
