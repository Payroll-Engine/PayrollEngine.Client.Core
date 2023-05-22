namespace PayrollEngine.Client.Model;

/// <summary>The tenant client object</summary>
public interface ITenant : IModel, IAttributeObject, IKeyEquatable<ITenant>
{
    /// <summary>The unique identifier of the tenant (immutable)</summary>
    string Identifier { get; set; }

    /// <summary>The culture including the calendar</summary>
    string Culture { get; set; }

    /// <summary>The default/fallback calendar</summary>
    CalendarConfiguration Calendar { get; set; }
}