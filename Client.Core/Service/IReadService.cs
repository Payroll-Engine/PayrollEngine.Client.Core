using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll backend service with CRUD operations</summary>
public interface IReadService<in TModel, in TContext, in TQuery>
    where TContext : IServiceContext
    where TQuery : Query
{
    /// <summary>Query objects</summary>
    /// <param name="context">The service context</param>
    /// <param name="query">The query</param>
    /// <returns>List of objects</returns>
    Task<List<T>> QueryAsync<T>(TContext context, TQuery query = null) where T : class, TModel;

    /// <summary>Query count of objects</summary>
    /// <param name="context">The service context</param>
    /// <param name="query">The query</param>
    /// <returns>Count of objects</returns>
    Task<long> QueryCountAsync(TContext context, TQuery query = null);

    /// <summary>Query items and count of objects</summary>
    /// <param name="context">The service context</param>
    /// <param name="query">The query</param>
    /// <returns>List and count of objects</returns>
    Task<QueryResult<T>> QueryResultAsync<T>(TContext context, TQuery query = null) where T : class, TModel;

    /// <summary>Get object</summary>
    /// <param name="context">The service context</param>
    /// <param name="objectId">The object id</param>
    /// <returns>The object, null if missing</returns>
    Task<T> GetAsync<T>(TContext context, int objectId) where T : class, TModel;
}