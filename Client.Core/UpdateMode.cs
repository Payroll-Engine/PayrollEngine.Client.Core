using System.Text.Json.Serialization;

namespace PayrollEngine.Client;

/// <summary>The exchange update mode</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UpdateMode
{
    /// <summary>Update always</summary>
    Update,

    /// <summary>Update only changes</summary>
    NoUpdate
}