using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The collector result client object</summary>
public interface ICollectorResult : IModel, IAttributeObject,
    IEquatable<ICollectorResult>, INumericValueResult

{
    /// <summary>The payroll result id (immutable)</summary>
    int PayrollResultId { get; set; }

    /// <summary>The tenant id (denormalized, immutable)</summary>
    int TenantId { get; set; }

    /// <summary>The employee id (denormalized, immutable)</summary>
    int EmployeeId { get; set; }

    /// <summary>The division id (denormalized, immutable)</summary>
    int DivisionId { get; set; }

    /// <summary>The payrun job id (denormalized, immutable)</summary>
    int PayrunJobId { get; set; }

    /// <summary>The forecast name (denormalized, immutable)</summary>
    string Forecast { get; set; }

    /// <summary>The parent payrun job id (denormalized, immutable)</summary>
    int? ParentJobId { get; set; }

    /// <summary>The collector id (immutable)</summary>
    int CollectorId { get; set; }

    /// <summary>The collector name (immutable)</summary>
    string CollectorName { get; set; }

    /// <summary>The localized collector names (immutable)</summary>
    Dictionary<string, string> CollectorNameLocalizations { get; set; }

    /// <summary>The collect mode (immutable)</summary>
    CollectMode CollectMode { get; set; }

    /// <summary>Negated collector result (immutable)</summary>
    bool Negated { get; set; }

    /// <summary>The value type (immutable)</summary>
    ValueType ValueType { get; set; }
    
    /// <summary>The collector result culture name based on RFC 4646</summary>
    string Culture { get; set; }

    /// <summary>The starting date for the value (immutable)</summary>
    DateTime Start { get; set; }

    /// <summary>The ending date for the value (immutable)</summary>
    DateTime End { get; set; }

    /// <summary>The result tags</summary>
    List<string> Tags { get; set; }
}