namespace PayrollEngine.Client.Model;

/// <summary>The tenant client object</summary>
public interface ITenant : IModel, IAttributeObject, IKeyEquatable<ITenant>
{
    /// <summary>The unique identifier of the tenant (immutable)</summary>
    string Identifier { get; set; }

    /// <summary>The tenant culture name based on RFC 4646 (fallback: system culture)</summary>
    string Culture { get; set; }

    /// <summary>The tenant calendar (fallback: default calendar)</summary>
    string Calendar { get; set; }
}