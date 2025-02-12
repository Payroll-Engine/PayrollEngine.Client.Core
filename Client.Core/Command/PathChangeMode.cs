namespace PayrollEngine.Client.Command;

/// <summary>
/// Command path change mode.
/// </summary>
public enum PathChangeMode
{
    /// <summary>
    /// Change current path to the command file path.
    /// </summary>
    ChangePath,

    /// <summary>
    /// Remain on the current path.
    /// </summary>
    KeepPath
}