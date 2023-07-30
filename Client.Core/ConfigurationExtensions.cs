using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PayrollEngine.Client;

/// <summary>Extension methods for <see cref="IConfiguration"/></summary>
public static class ConfigurationExtensions
{
    /// <summary>Get the http configuration</summary>
    public static async Task<PayrollHttpConfiguration> GetHttpConfigurationAsync(this IConfiguration configuration)
    {
        // priority 1: shared configuration
        var httpConfiguration = await SharedHttpConfiguration.GetHttpConfigurationAsync();
        if (httpConfiguration != null)
        {
            return httpConfiguration;
        }

        // priority 2: application configuration
        httpConfiguration = configuration.GetConfiguration<PayrollHttpConfiguration>();
        return httpConfiguration;
    }
}