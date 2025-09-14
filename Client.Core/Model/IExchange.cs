using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The exchange object</summary>
// ReSharper disable UnusedMemberInSuper.Global
public interface IExchange : IEquatable<IExchange>
{
    /// <summary>The tenants</summary>
    List<ExchangeTenant> Tenants { get; set; }

    /// <summary>The regulation shares</summary>
    List<RegulationShare> RegulationShares { get; set; }

    /// <summary>The default created date for new objects</summary>
    DateTime? CreatedObjectDate { get; set; }

    /// <summary>The import exchange</summary>
    void Import(IExchange source);
}