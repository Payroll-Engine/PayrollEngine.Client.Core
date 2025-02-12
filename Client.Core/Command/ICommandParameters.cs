using System;

namespace PayrollEngine.Client.Command;

/// <summary>
/// Command parameters contract.
/// </summary>
public interface ICommandParameters
{
    /// <summary>
    /// Command toggles.
    /// </summary>
    Type[] Toggles { get; }

    /// <summary>
    /// Test for valid command parameters.
    /// </summary>
    /// <returns></returns>
    string Test();
}