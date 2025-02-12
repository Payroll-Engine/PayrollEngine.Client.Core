namespace PayrollEngine.Client.Command;

/// <summary>
/// Console display level.
/// </summary>
public enum DisplayLevel
{
    /// <summary>
    /// Display all messages.
    /// </summary>
    Full,

    /// <summary>
    /// Display key messages.
    /// </summary>
    Compact,

    /// <summary>
    /// No messages.
    /// </summary>
    Silent
}