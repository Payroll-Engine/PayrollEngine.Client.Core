using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using PayrollEngine.Serialization;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service.Api;

/// <summary>Payroll user service</summary>
public class UserService : Service, IUserService
{
    /// <summary>Initializes a new instance of the <see cref="UserService"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    public UserService(PayrollHttpClient httpClient) :
        base(httpClient)
    {
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> QueryAsync<T>(TenantServiceContext context, Query query = null) where T : class, IUser
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Items;
        var url = query.AppendQueryString(TenantApiEndpoints.UsersUrl(context.TenantId));
        return await HttpClient.GetCollectionAsync<T>(url);
    }

    /// <inheritdoc />
    public virtual async Task<long> QueryCountAsync(TenantServiceContext context, Query query = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.Count;
        var url = query.AppendQueryString(TenantApiEndpoints.UsersUrl(context.TenantId));
        return await HttpClient.GetAsync<long>(url);
    }

    /// <inheritdoc />
    public virtual async Task<QueryResult<T>> QueryResultAsync<T>(TenantServiceContext context, Query query = null) where T : class, IUser
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        query ??= new();
        query.Result = QueryResultType.ItemsWithCount;
        var url = query.AppendQueryString(TenantApiEndpoints.UsersUrl(context.TenantId));
        return await HttpClient.GetAsync<QueryResult<T>>(url);
    }

    /// <inheritdoc />
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, int userId) where T : class, IUser
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (userId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(userId));
        }

        return await HttpClient.GetAsync<T>(TenantApiEndpoints.UserUrl(context.TenantId, userId));
    }

    /// <inheritdoc />
    public virtual async Task<T> GetAsync<T>(TenantServiceContext context, string identifier) where T : class, IUser
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (string.IsNullOrWhiteSpace(identifier))
        {
            throw new ArgumentException(nameof(identifier));
        }

        // query single item
        var query = QueryFactory.NewIdentifierQuery(identifier);
        var uri = query.AppendQueryString(TenantApiEndpoints.UsersUrl(context.TenantId));
        return await HttpClient.GetSingleAsync<T>(uri);
    }

    /// <inheritdoc />
    public virtual async Task<T> CreateAsync<T>(TenantServiceContext context, T user) where T : class, IUser
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        return await HttpClient.PostAsync(TenantApiEndpoints.UsersUrl(context.TenantId), user);
    }

    /// <inheritdoc />
    public virtual async Task UpdateAsync<T>(TenantServiceContext context, T user) where T : class, IUser
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        await HttpClient.PutAsync(TenantApiEndpoints.UsersUrl(context.TenantId), user);
    }

    /// <inheritdoc />
    public virtual async Task DeleteAsync(TenantServiceContext context, int userId)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (userId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(userId));
        }

        await HttpClient.DeleteAsync(TenantApiEndpoints.UsersUrl(context.TenantId), userId);
    }

    /// <inheritdoc />
    public virtual async Task<bool> TestPasswordAsync(TenantServiceContext context, int userId, string password)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // test user password: 200/Ok or 400/BadRequest
        try
        {
            await HttpClient.GetAsync(TenantApiEndpoints.UserPasswordUrl(context.TenantId, userId),
                DefaultJsonSerializer.SerializeJson(DefaultJsonSerializer.Serialize(password)));
        }
        catch (HttpRequestException exception)
        {
            if (exception.StatusCode == HttpStatusCode.BadRequest)
            {
                return false;
            }
            throw;
        }
        return true;
    }

    /// <inheritdoc />
    public virtual async Task UpdatePasswordAsync(TenantServiceContext context, int userId, string password)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException(nameof(password));
        }

        // update user password: 200/Ok or 403/Forbidden
        await HttpClient.PostAsync(TenantApiEndpoints.UserPasswordUrl(context.TenantId, userId),
            DefaultJsonSerializer.SerializeJson(DefaultJsonSerializer.Serialize(password)));
    }

    /// <inheritdoc />
    public virtual async Task<string> GetAttributeAsync(TenantServiceContext context, int userId, string attributeName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (userId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(userId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        return await HttpClient.GetAttributeAsync(TenantApiEndpoints.UserAttributeUrl(context.TenantId, userId,
            attributeName));
    }

    /// <inheritdoc />
    public virtual async Task SetAttributeAsync(TenantServiceContext context, int userId, string attributeName, string attributeValue)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (userId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(userId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        await HttpClient.PostAttributeAsync(TenantApiEndpoints.UserAttributeUrl(context.TenantId, userId,
            attributeName), attributeValue);
    }

    /// <inheritdoc />
    public virtual async Task DeleteAttributeAsync(TenantServiceContext context, int userId, string attributeName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (userId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(userId));
        }
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException(nameof(attributeName));
        }

        await HttpClient.DeleteAttributeAsync(TenantApiEndpoints.UserAttributeUrl(context.TenantId, userId,
            attributeName));
    }
}