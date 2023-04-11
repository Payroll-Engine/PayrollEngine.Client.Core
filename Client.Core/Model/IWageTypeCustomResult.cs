using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The wage type custom result client object</summary>
public interface IWageTypeCustomResult : IModel, IAttributeObject
{
    /// <summary>The wage type result id (immutable)</summary>
    int WageTypeResultId { get; set; }

    /// <summary>The wage type number (immutable)</summary>
    decimal WageTypeNumber { get; set; }
        
    /// <summary>The wage type name (immutable)</summary>
    string WageTypeName { get; set; }

    /// <summary>The localized wage type names (immutable)</summary>
    Dictionary<string, string> WageTypeNameLocalizations { get; set; }

    /// <summary>The value source (immutable)</summary>
    string Source { get; set; }

    /// <summary>The value type (immutable)</summary>
    ValueType ValueType { get; set; }

    /// <summary>The wage type custom result value (immutable)</summary>
    decimal Value { get; set; }

    /// <summary>The period starting date for the value</summary>
    DateTime Start { get; set; }

    /// <summary>The period ending date for the value</summary>
    DateTime End { get; set; }

    /// <summary>The result tags</summary>
    List<string> Tags { get; set; }
}