using System;

namespace PayrollEngine.Client.Model;

/// <summary>Base for all Payroll models</summary>
public interface IModel : IEquatable<IModel>
{
    /// <summary>The unique object id (immutable)</summary>
    int Id { get; set; }

    /// <summary>The status of the object</summary>
    ObjectStatus Status { get; set; }

    /// <summary>The date which the client object was created (immutable)</summary>
    DateTime Created { get; set; }

    /// <summary>The date which the client object was last updated (immutable)</summary>
    DateTime Updated { get; set; }

    /// <summary>The object update mode</summary>
    UpdateMode UpdateMode { get; set; }
}