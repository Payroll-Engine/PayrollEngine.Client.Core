using System.Threading.Tasks;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll backend service with Create and Read operations</summary>
public interface ICreateService<in TModel, in TContext, in TQuery> : IReadService<TModel, TContext, TQuery>
    where TContext : IServiceContext
    where TQuery : Query
{
    /// <summary>Create new object</summary>
    /// <param name="context">The service context</param>
    /// <param name="obj">The object</param>
    /// <returns>New created object</returns>
    Task<T> CreateAsync<T>(TContext context, T obj) where T : class, TModel;

    /// <summary>Delete object</summary>
    /// <param name="context">The service context</param>
    /// <param name="objectId">The object id</param>
    Task DeleteAsync(TContext context, int objectId);
}