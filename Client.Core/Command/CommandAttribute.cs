using System;

namespace PayrollEngine.Client.Command;

/// <summary>
/// Payroll engine client command attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class CommandAttribute : Attribute
{
    /// <summary>
    /// Command name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">Command name</param>
    public CommandAttribute(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(nameof(name));
        }
        Name = name;
    }
}