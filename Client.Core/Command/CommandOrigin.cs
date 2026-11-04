using System;

namespace PayrollEngine.Client.Command;

/// <summary>
/// Origin of a registered command: the core application or an extension.
/// </summary>
public sealed class CommandOrigin
{
    /// <summary>
    /// Core application origin.
    /// </summary>
    public static CommandOrigin Core { get; } = new();

    private CommandOrigin()
    {
    }

    /// <summary>
    /// Extension origin.
    /// </summary>
    /// <param name="name">Extension name.</param>
    /// <param name="version">Extension version.</param>
    /// <param name="path">Extension path.</param>
    public CommandOrigin(string name, string version, string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name;
        Version = version;
        Path = path;
    }

    /// <summary>
    /// Extension name, null for the core application.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Extension version.
    /// </summary>
    public string Version { get; }

    /// <summary>
    /// Extension path.
    /// </summary>
    public string Path { get; }

    /// <summary>
    /// Test for extension origin.
    /// </summary>
    public bool IsExtension => Name != null;

    /// <inheritdoc />
    public override string ToString()
    {
        if (!IsExtension)
        {
            return "core";
        }
        var text = string.IsNullOrWhiteSpace(Version) ? $"extension {Name}" : $"extension {Name} {Version}";
        return string.IsNullOrWhiteSpace(Path) ? text : $"{text} ({Path})";
    }
}
