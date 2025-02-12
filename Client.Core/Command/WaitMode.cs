namespace PayrollEngine.Client.Command;

/// <summary>
/// Command wait mode.
/// </summary>
public enum WaitMode
{
    /// <summary>
    /// Wait on error.
    /// </summary>
    WaitError,

    /// <summary>
    /// Wait always.
    /// </summary>
    Wait,

    /// <summary>
    /// Do not wait.
    /// </summary>
    NoWait
}