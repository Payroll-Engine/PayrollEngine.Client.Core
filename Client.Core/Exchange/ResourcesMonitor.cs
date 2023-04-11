using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PayrollEngine.Client.Service;

namespace PayrollEngine.Client.Exchange;

/// <summary>
/// Class to monitor a resource collection by GET queries
/// </summary>
public class ResourcesMonitor<TModel, TContext, TQuery>
    where TModel : class
    where TContext : IServiceContext
    where TQuery : Query, new()
{
    private DateTime lastRequest = DateTime.MinValue;

    /// <summary>The minimum query interval</summary>
    public static TimeSpan MinInterval => TimeSpan.FromSeconds(1);

    /// <summary>The query service</summary>
    public IReadService<TModel, TContext, TQuery> Service { get; }

    /// <summary>The execution context</summary>
    public TContext Context { get; set; }

    /// <summary>The monitoring handler</summary>
    public Action<ICollection<TModel>> ChangeHandler { get; }

    /// <summary>The query interval</summary>
    public TimeSpan Interval { get; set; }

    /// <summary>Monitoring running state</summary>
    public bool IsRunning { get; private set; }

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="service">The query service</param>
    /// <param name="context">The execution context</param>
    /// <param name="changeHandler">The monitoring handler</param>
    public ResourcesMonitor(IReadService<TModel, TContext, TQuery> service,
        TContext context, Action<ICollection<TModel>> changeHandler)
    {
        Service = service ?? throw new ArgumentNullException(nameof(service));
        Context = context ?? throw new ArgumentNullException(nameof(context));
        ChangeHandler = changeHandler ?? throw new ArgumentNullException(nameof(changeHandler));
    }

    /// <summary>
    /// Start the monitoring
    /// see https://nitinmanju.medium.com/a-simple-scheduled-task-using-c-and-net-c9d3230769ea
    /// </summary>
    public Task Start(CancellationToken token)
    {
        var interval = Interval;
        if (interval < MinInterval)
        {
            interval = MinInterval;
        }
        return Task.Run(async () =>
        {
            try
            {
                IsRunning = true;
                while (!token.IsCancellationRequested)
                {
                    // skip initial query
                    if (lastRequest == DateTime.MinValue)
                    {
                        // initialize for the next iteration
                        lastRequest = Date.Now;
                    }
                    else
                    {
                        // query items created since the last request
                        var query = new TQuery
                        {
                            Filter = $"{nameof(Model.Model.Created)} gt '{lastRequest.ToUtcString(CultureInfo.CurrentCulture)}'"
                        };
                        var items = (await Service.QueryAsync<TModel>(Context, query)).ToList();

                        // update loop trigger
                        lastRequest = Date.Now;

                        // external handler
                        if (items.Any())
                        {
                            ChangeHandler(items);
                        }
                    }

                    // delay between request
                    await Task.Delay(interval, token);
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Resource monitoring error");
            }
            finally
            {
                IsRunning = false;
            }
        }, token);
    }
}