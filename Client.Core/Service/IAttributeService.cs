using System.Threading.Tasks;

namespace PayrollEngine.Client.Service;

/// <summary>Service with attribute support</summary>
public interface IAttributeService<in TContext>
    where TContext : IServiceContext
{
    /// <summary>Get object attribute</summary>
    /// <param name="context">The service context</param>
    /// <param name="objectId">The object id</param>
    /// <param name="attributeName">The attribute name</param>
    /// <returns>The attribute value</returns>
    Task<string> GetAttributeAsync(TContext context, int objectId, string attributeName);

    /// <summary>Set object attribute</summary>
    /// <param name="context">The service context</param>
    /// <param name="objectId">The object id</param>
    /// <param name="attributeName">The attribute name</param>
    /// <param name="attributeValue">The attribute value</param>
    Task SetAttributeAsync(TContext context, int objectId, string attributeName, string attributeValue);

    /// <summary>Delete object attribute</summary>
    /// <param name="context">The service context</param>
    /// <param name="objectId">The object id</param>
    /// <param name="attributeName">The attribute name</param>
    Task DeleteAttributeAsync(TContext context, int objectId, string attributeName);
}