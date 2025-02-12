using System;
using System.Net.Http;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Security.Authentication;

namespace PayrollEngine.Client;

/// <summary>Console program with http client and</summary>
/// <typeparam name="TApp"></typeparam>
public abstract class ConsoleProgram<TApp> : ConsoleToolBase, IDisposable
    where TApp : class
{
    /// <summary>The program configuration</summary>
    protected ProgramConfiguration<TApp> Configuration { get; }

    /// <summary>The console program constructor</summary>
    /// <param name="configurationOptions">The program configuration options</param>
    protected ConsoleProgram(ProgramConfigurationOptions configurationOptions = ProgramConfigurationOptions.Default)
    {
        Configuration = new(configurationOptions);
    }

    #region Log

    /// <summary>Log thr program lifecycle, default is true</summary>
    protected virtual bool LogLifecycle => true;

    /// <summary>Log the errors, default is true</summary>
    protected virtual bool LogErrors => true;

    #endregion

    #region Execution

    /// <summary>Run the program</summary>
    protected abstract Task RunAsync();

    /// <summary>Initialize the console program</summary>
    protected virtual Task SetupLogAsync()
    {
        return Task.CompletedTask;
    }

    /// <summary>Initialize the console program</summary>
    /// <returns>True for valid program start state</returns>
    protected virtual async Task<bool> InitializeAsync()
    {
        // log program start
        if (LogLifecycle)
        {
            Log.Information($"{GetProgramTitle()} {GetProgramVersion()} started");
        }

        // culture
        var curCulture = CultureInfo.DefaultThreadCurrentCulture ?? CultureInfo.InvariantCulture;
        var cultureName = await GetProgramCultureAsync();
        if (!string.IsNullOrWhiteSpace(cultureName) &&
            !string.Equals(curCulture.Name, cultureName))
        {
            var culture = new CultureInfo(cultureName);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            if (LogLifecycle)
            {
                Log.Information($"Culture changed to {cultureName}");
            }
        }

        // valid initialization
        return true;
    }

    /// <summary>Shutdown the program</summary>
    protected virtual Task ShutdownAsync()
    {
        // log program end
        if (LogLifecycle)
        {
            Log.Information($"{GetProgramTitle()} ended");
        }
        return Task.CompletedTask;
    }

    /// <summary>Show full error log (default: false)</summary>
    protected virtual bool FullErrorLog() => false;

    /// <summary>Execute the program</summary>
    public virtual async Task ExecuteAsync()
    {
        try
        {
            // program title
            if (ShowProgramTitle)
            {
                await ShowTitleAsync();
            }

            // help
            if ((UseHelpArgument && IsHelpMode()) ||
                (MandatoryArgumentCount > 0 && ConsoleArguments.ParameterCount < MandatoryArgumentCount))
            {
                await HelpAsync();
                PressAnyKey();
                return;
            }

            // log
            await SetupLogAsync();

            // http client
            if (UseHttpClient && !await SetupHttpClientAsync())
            {
                return;
            }

            // initialize
            if (!await InitializeAsync())
            {
                return;
            }

            // run program
            await RunAsync();
        }
        catch (Exception exception)
        {
            // full error log
            if (FullErrorLog())
            {
                Log.Error(exception, exception.GetBaseException().Message);
            }

            // user notification
            try
            {
                await NotifyGlobalErrorAsync(exception);
            }
            catch (Exception errorException)
            {
                var message = $"Error in program error handling: {errorException}";
                EnsureExitCode();
                Log.Critical(message);
                WriteErrorLine(message);
                PressAnyKey();
            }
        }
        finally
        {
            try
            {
                await ShutdownAsync();
            }
            catch (Exception shutdownException)
            {
                var message = $"Error in program shutdown: {shutdownException}";
                EnsureExitCode();
                Log.Critical(message);
                WriteErrorLine(message);
                PressAnyKey();
            }
        }
    }

    private static void EnsureExitCode()
    {
        if (Environment.ExitCode == 0)
        {
            Environment.ExitCode = -10;
        }
    }

    #endregion

    #region Help

    /// <summary>Show the connection status, default is true</summary>
    protected virtual bool UseHelpArgument => true;

    /// <summary>Count of mandatory arguments, default is 0</summary>
    protected virtual int MandatoryArgumentCount => 0;

    /// <summary>Test for single command line argument help</summary>
    /// <returns></returns>
    private static bool IsHelpMode()
    {
        var firstArgument = ConsoleArguments.Get(1, allowToggle: true);
        return string.Equals("/?", firstArgument) || string.Equals("-?", firstArgument) ||
               string.Equals("/help", firstArgument, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>Show the program help</summary>
    protected virtual Task HelpAsync()
    {
        return Task.CompletedTask;
    }

    #endregion

    #region Http client

    /// <summary>The payroll http client</summary>
    protected PayrollHttpClient HttpClient { get; private set; }

    /// <summary>Use the payroll http client, default is true</summary>
    protected virtual bool UseHttpClient => true;

    /// <summary>Show the connection status, default is true</summary>
    protected virtual bool ShowConnectionInfo => true;

    /// <summary>Get the http configuration</summary>
    /// <remarks>This request ignores the web host variable</remarks>
    protected virtual async Task<PayrollHttpConfiguration> GetHttpConfigurationAsync() =>
        await Configuration.Configuration.GetHttpConfigurationAsync();

    /// <summary>Get the http client handler, by default always a valid server certificate</summary>
    protected virtual async Task<HttpClientHandler> GetHttpClientHandlerAsync()
    {
        var handler = new HttpClientHandler
        {
            SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13
        };
        return await Task.FromResult(handler);
    }

    /// <summary>Http client setup</summary>
    protected virtual async Task<bool> SetupHttpClientAsync()
    {
        // http client configuration
        var httpConfiguration = await GetHttpConfigurationAsync();
        if (httpConfiguration == null)
        {
            OnMissingHttpConfiguration();
            return false;
        }
        if (!httpConfiguration.Valid())
        {
            OnInvalidHttpConfiguration(httpConfiguration);
            return false;
        }

        // connection info
        if (ShowConnectionInfo)
        {
            WriteInfo($"Connecting to {httpConfiguration}...");
        }

        // http client handler
        var httpClientHandler = await GetHttpClientHandlerAsync();
        if (httpClientHandler == null)
        {
            throw new PayrollException("Missing Payroll HTTP client handler.");
        }

        // create client
        HttpClient = new(httpClientHandler, httpConfiguration);

        // api key
        var apiKey = GetApiKey(httpConfiguration);
        if (!string.IsNullOrWhiteSpace(apiKey))
        {
            HttpClient.SetApiKey(apiKey);
        }

        // connection test
        var setup = await HttpClient.IsConnectionAvailableAsync(TenantApiEndpoints.TenantsUrl());
        if (ShowConnectionInfo)
        {
            WriteInfoLine("done.");
            WriteLine();
        }

        // setup error
        if (!setup)
        {
            await NotifyConnectionErrorAsync();
        }

        // log
        if (LogLifecycle)
        {
            Log.Information($"Connected http client to {HttpClient.Address}");
        }

        return setup;
    }

    /// <summary>
    /// Handler on missing http configuration
    /// </summary>
    protected virtual void OnMissingHttpConfiguration()
    {
        WriteErrorLine("Missing Payroll HTTP client configuration.");
        WriteLine();
        WriteLine("Please specify the Payroll HTTP client configuration in the following locations:");
        WriteLine($"  1. Environment variable `{SystemSpecification.PayrollApiConnection}` - connection string with the HTTP client configuration");
        WriteLine($"  2. Environment variable `{SystemSpecification.PayrollApiConfiguration}` - file name containing the HTTP client configuration");
        WriteLine($"  3. File `{PayrollApiSpecification.ApiSettingsFileName}` - API configuration file located in the program folder");
        WriteLine("  4. File `appsettings.json` - API configuration from the program configuration file");
        if (WaitOnError)
        {
            PressAnyKey();
        }
    }

    /// <summary>
    /// Handler on invalid http configuration
    /// </summary>
    protected virtual void OnInvalidHttpConfiguration(PayrollHttpConfiguration _)
    {
        WriteErrorLine("Invalid Payroll HTTP client configuration.");
        if (WaitOnError)
        {
            PressAnyKey();
        }
    }

    private static string GetApiKey(PayrollHttpConfiguration httpConfiguration)
    {
        // priority 1: environment variable (console app does not support web host environment variables)
        var apiKey = Environment.GetEnvironmentVariable(SystemSpecification.PayrollApiKey);
        if (!string.IsNullOrWhiteSpace(apiKey))
        {
            return apiKey;
        }

        // priority 2: http configuration
        return httpConfiguration.ApiKey;
    }

    #endregion

    #region Culture

    /// <summary>The default culture name</summary>
    protected virtual string DefaultCultureName => "en-US";

    /// <summary>Program culture setup</summary>
    protected virtual Task<string> GetProgramCultureAsync() =>
        Task.FromResult(DefaultCultureName);

    #endregion

    #region Error

    /// <summary>Use error confirmation, default is true</summary>
    protected virtual bool WaitOnError => true;

    /// <summary>Use program exit code, default is true</summary>
    protected virtual bool ShowErrorExitCode => true;

    /// <summary>Connection error handler</summary>
    protected virtual async Task NotifyConnectionErrorAsync()
    {
        await NotifyErrorAsync($"Backend connection {HttpClient.Address} is not available or cannot be accessed.");
    }

    /// <summary>Notify global error</summary>
    /// <param name="exception">The error exception</param>
    protected virtual Task NotifyGlobalErrorAsync(Exception exception)
    {
        // error notification
        NotifyErrorAsync(exception);

        // exit code
        if (ShowErrorExitCode && ExitCode != 0)
        {
            WriteLine($"Exit code: {ExitCode}");
        }
        return Task.CompletedTask;
    }

    /// <summary>Notify error</summary>
    /// <param name="exception">The error exception</param>
    protected virtual Task NotifyErrorAsync(Exception exception)
    {
        var message = $"{GetProgramTitle()} error: {exception.GetBaseMessage()}";
        if (LogErrors)
        {
            Log.Error(exception, message);
        }
        WriteErrorLine(message);
        if (WaitOnError)
        {
            PressAnyKey();
        }
        return Task.CompletedTask;
    }

    /// <summary>Show error</summary>
    /// <param name="error">The error message</param>
    protected virtual Task NotifyErrorAsync(string error)
    {
        if (!string.IsNullOrWhiteSpace(error))
        {
            if (LogErrors)
            {
                Log.Error(error);
            }
            WriteErrorLine(error);
            if (WaitOnError)
            {
                PressAnyKey();
            }
        }
        return Task.CompletedTask;
    }

    #endregion

    #region Application Info

    /// <summary>Get the program title</summary>
    protected virtual string GetProgramTitle() =>
        Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyProductAttribute>()?.Product;

    /// <summary>Get the program version</summary>
    protected virtual string GetProgramVersion()
    {
        var assembly = Assembly.GetEntryAssembly() ?? GetType().Assembly;
        return "v" + FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;
    }

    /// <summary>Get the program copyright</summary>
    protected virtual string GetProgramCopyright() =>
        "Copyright (c) Software Consulting Giannoudis. All rights reserved.";

    /// <summary>Show the program title, default is true</summary>
    protected virtual bool ShowProgramTitle => true;

    /// <summary>Show the program title</summary>
    protected virtual Task ShowTitleAsync()
    {
        // title and version
        Write(GetProgramTitle());
        var version = GetProgramVersion();
        if (!string.IsNullOrWhiteSpace(version))
        {
            Write(" ");
            Write(version);
        }
        WriteLine();

        // copyright
        var copyright = GetProgramCopyright();
        if (!string.IsNullOrWhiteSpace(copyright))
        {
            WriteLine(copyright);
        }
        WriteLine();

        return Task.CompletedTask;
    }

    #endregion

    /// <summary>Program dispose</summary>
    public void Dispose()
    {
        HttpClient?.Dispose();
    }
}
