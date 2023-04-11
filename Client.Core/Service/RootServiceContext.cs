using System;

namespace PayrollEngine.Client.Service;

/// <summary>Tenant service context</summary>
public class RootServiceContext : IServiceContext
{
    /// <inheritdoc />
    public Type ServiceType => GetType();
}