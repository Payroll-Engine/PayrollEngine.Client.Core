using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>Custom collector result client object</summary>
public interface ICollectorCustomResult : IModel, IAttributeObject, IEquatable<ICollectorCustomResult>
{
    /// <summary>The wage type result id (immutable)</summary>
    int CollectorResultId { get; set; }

    /// <summary>The collector name (immutable)</summary>
    string CollectorName { get; set; }

    /// <summary>The localized collector names (immutable)</summary>
    Dictionary<string, string> CollectorNameLocalizations { get; set; }

    /// <summary>The value source (immutable)</summary>
    string Source { get; set; }

    /// <summary>The value type (immutable)</summary>
    ValueType ValueType { get; set; }

    /// <summary>The collector custom result value (immutable)</summary>
    decimal Value { get; set; }
    
    /// <summary>The collector custom result culture name based on RFC 4646+</summary>
    string Culture { get; set; }

    /// <summary>The period starting date for the value</summary>
    DateTime Start { get; set; }

    /// <summary>The period ending date for the value</summary>
    DateTime End { get; set; }

    /// <summary>The result tags</summary>
    List<string> Tags { get; set; }
}