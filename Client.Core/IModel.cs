using System;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client;

/// <summary>Base for all Payroll models</summary>
public interface IModel
{
    /// <summary>The unique object id (immutable)</summary>
    int Id { get; set; }

    /// <summary>Test for existing object (opposite of <see cref="IsNewObject"/>)</summary>
    [JsonIgnore]
    bool IsExistingObject { get; }

    /// <summary>Test for new object (opposite of <see cref="IsExistingObject"/>)</summary>
    [JsonIgnore]
    bool IsNewObject { get; }

    /// <summary>The status of the object</summary>
    ObjectStatus Status { get; set; }

    /// <summary>The date which the client object was created (immutable)</summary>
    DateTime Created { get; set; }

    /// <summary>The date which the client object was last updated (immutable)</summary>
    DateTime Updated { get; set; }

    /// <summary>The object update mode</summary>
    UpdateMode UpdateMode { get; set; }

    /// <summary>The object UI string</summary>
    string GetUiString();
}