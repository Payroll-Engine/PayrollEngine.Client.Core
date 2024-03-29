﻿using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Tenant service context</summary>
public class TenantServiceContext : RootServiceContext
{
    /// <summary>Initializes a new instance of the <see cref="TenantServiceContext"/> class</summary>
    /// <param name="tenantId">The tenant id</param>
    public TenantServiceContext(int tenantId)
    {
        TenantId = tenantId;
    }

    /// <summary>Initializes a new instance of the <see cref="TenantServiceContext"/> class</summary>
    /// <param name="tenant">The tenant</param>
    public TenantServiceContext(ITenant tenant) :
        this(tenant.Id)
    {
    }

    /// <summary>The tenant id</summary>
    public int TenantId { get; }
}