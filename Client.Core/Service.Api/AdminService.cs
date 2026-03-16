using System.Threading.Tasks;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll backend administration service</summary>
public class AdminService : ServiceBase, IAdminService
{
    /// <summary>Initializes a new instance of the <see cref="AdminService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public AdminService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc />
    public virtual async Task<BackendInformation> GetBackendInformationAsync() =>
        await HttpClient.GetAsync<BackendInformation>(ApiEndpoints.AdminInformationUrl());
}
