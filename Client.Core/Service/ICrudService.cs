using System.Threading.Tasks;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll backend service with CRUD operations</summary>
public interface ICrudService<in TModel, in TContext, in TQuery> : ICreateService<TModel, TContext, TQuery>
    where TContext : IServiceContext
    where TQuery : Query
{
    /// <summary>Update object</summary>
    /// <param name="context">The service context</param>
    /// <param name="obj">The object</param>
    Task UpdateAsync<T>(TContext context, T obj) where T : class, TModel;
}