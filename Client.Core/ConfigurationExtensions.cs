using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PayrollEngine.Client;

/// <summary>Extension methods for <see cref="IConfiguration"/></summary>
public static class ConfigurationExtensions
{
    /// <summary>Get the http configuration by priority:
    /// 1. Environment variable payroll api connection
    /// 2. Environment variable payroll api configuration (link to JSON file)
    /// 3. Api configuration located in the app folder (apisettings.json)
    /// </summary>
    public static async Task<PayrollHttpConfiguration> GetHttpConfigurationAsync()
    {
        // priority 1: from http connection string in an environment variable
        var apiConfig = Environment.GetEnvironmentVariable(SystemSpecification.PayrollApiConnection);
        if (!string.IsNullOrWhiteSpace(apiConfig))
        {
            Log.Trace($"Payroll http configuration source: environment variable {SystemSpecification.PayrollApiConnection}.");
            return PayrollHttpConfiguration.FromConnectionString(apiConfig);
        }

        // priority 2: from http configuration file, specified in an environment variable
        var configFile = Environment.GetEnvironmentVariable(SystemSpecification.PayrollApiConfiguration);
        if (!string.IsNullOrWhiteSpace(configFile))
        {
            Log.Trace($"Payroll http configuration source: api configuration file {configFile}.");
            return await ReadHttpConfigFileAsync(configFile);
        }

        // priority 3: from http configuration file apisettings.json located in the program folder
        configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PayrollApiSpecification.ApiSettingsFileName);
        if (File.Exists(configFile))
        {
            Log.Trace($"Payroll http configuration source: api settings file {configFile}.");
            return await ReadHttpConfigFileAsync(configFile);
        }

        return null;
    }
    
    /// <summary>Get the http configuration by priority:
    /// 1. Environment variable payroll api connection
    /// 2. Environment variable payroll api configuration (link to JSON file)
    /// 3. Api configuration located in the app folder (apisettings.json)
    /// 4. Application configuration (appsettings.json)
    /// </summary>
    /// <param name="configuration">Application configuration</param>
    public static async Task<PayrollHttpConfiguration> GetHttpConfigurationAsync(this IConfiguration configuration)
    {
        // priority 1 to 3
        var payrollHttpConfiguration = await GetHttpConfigurationAsync();
        if (payrollHttpConfiguration != null)
        {
            return payrollHttpConfiguration;
        }

        // priority 4: from http configuration section of the application configuration appsettings.json
        Log.Trace("Payroll http configuration source: application configuration (section ApiSettings).");
        return configuration.GetConfiguration<PayrollHttpConfiguration>("ApiSettings");
    }

    private static async Task<PayrollHttpConfiguration> ReadHttpConfigFileAsync(string fileName)
    {
        if (!File.Exists(fileName))
        {
            return null;
        }
        var json = await File.ReadAllTextAsync(fileName);
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var config = JsonSerializer.Deserialize<PayrollHttpConfiguration>(json, options);
        return config;
    }
}