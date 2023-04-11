using System;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll backend service context</summary>
public interface IServiceContext
{
    /// <summary>
    /// The service type (satisfy NDepend)
    /// </summary>
    Type ServiceType { get; }
}