using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PayrollEngine.Client;

/// <summary>Extension methods for <see cref="IConfiguration"/></summary>
public static class ConfigurationExtensions
{
    /// <summary>Get the http configuration</summary>
    public static async Task<PayrollHttpConfiguration> GetHttpConfigurationAsync(this IConfiguration configuration)
    {
        // priority 1: application configuration
        var httpConfiguration = configuration.GetConfiguration<PayrollHttpConfiguration>();

        // priority 2: shared configuration
        var sharedConfigFileName = Environment.GetEnvironmentVariable(SystemSpecification.PayrollConfigurationVariable);
        if (string.IsNullOrWhiteSpace(sharedConfigFileName) || !File.Exists(sharedConfigFileName))
        {
            return httpConfiguration;
        }

        // priority 2: shared configuration
        var sharedConfig = await SharedConfiguration.ReadAsync();
        sharedConfig.TryGetValue(PayrollApiSpecification.BackendUrlSetting, out var backendUrl);
        sharedConfig.TryGetValue(PayrollApiSpecification.BackendPortSetting, out var backendPort);
        var port = 0;
        if (!string.IsNullOrWhiteSpace(backendPort))
        {
            port = int.Parse(backendPort);
        }

        if (httpConfiguration == null)
        {
            // http configuration from environment
            if (!string.IsNullOrWhiteSpace(backendUrl) || port > 0)
            {
                httpConfiguration = new PayrollHttpConfiguration(backendUrl, port);
            }
        }
        else
        {
            // merge config and environment configuration
            if (string.IsNullOrWhiteSpace(httpConfiguration.BaseUrl) && !string.IsNullOrWhiteSpace(backendUrl))
            {
                httpConfiguration.BaseUrl = backendUrl;
            }
            if (httpConfiguration.Port == 0 && port > 0)
            {
                httpConfiguration.Port = port;
            }
        }

        // test
        if (httpConfiguration != null &&
            (string.IsNullOrWhiteSpace(httpConfiguration.BaseUrl) || httpConfiguration.Port == 0))
        {
            return null;
        }

        return httpConfiguration;
    }
}