using System;
using System.IO;
using System.Threading.Tasks;

namespace PayrollEngine.Client;

/// <summary>Http configuration from environment <see cref="SystemSpecification.PayrollConfigurationVariable"/></summary>
public static class SharedHttpConfiguration
{
    /// <summary>Get the http configuration</summary>
    public static async Task<PayrollHttpConfiguration> GetHttpConfigurationAsync()
    {
        var sharedConfigFileName = Environment.GetEnvironmentVariable(SystemSpecification.PayrollConfigurationVariable);
        if (string.IsNullOrWhiteSpace(sharedConfigFileName) || !File.Exists(sharedConfigFileName))
        {
            return null;
        }
        var sharedConfig = await SharedConfiguration.ReadAsync();
        var backendUrl = SharedConfiguration.GetSharedValue(sharedConfig, PayrollApiSpecification.BackendUrlSetting);
        var backendPort = SharedConfiguration.GetSharedValue(sharedConfig, PayrollApiSpecification.BackendPortSetting);
        var port = 0;
        if (!string.IsNullOrWhiteSpace(backendPort))
        {
            port = int.Parse(backendPort);
        }

        if (string.IsNullOrWhiteSpace(backendUrl) || port <= 0)
        {
            return null;
        }
        return new PayrollHttpConfiguration(backendUrl, port);
    }
}