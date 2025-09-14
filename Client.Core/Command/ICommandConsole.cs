namespace PayrollEngine.Client.Command;

/// <summary>
/// Command console contract.
/// </summary>
// ReSharper disable UnusedMemberInSuper.Global
public interface ICommandConsole
{
    /// <summary>
    /// Display level.
    /// </summary>
    DisplayLevel DisplayLevel { get; set; }

    /// <summary>
    /// Error mode.
    /// </summary>
    ErrorMode ErrorMode { get; set; }

    /// <summary>
    /// Wait mode.
    /// </summary>
    WaitMode WaitMode { get; set; }

    /// <summary>
    /// Display success line.
    /// </summary>
    /// <param name="text">Text to display.</param>
    void DisplaySuccessLine(string text);

    /// <summary>
    /// Display title line.
    /// </summary>
    /// <param name="text">Text to display.</param>
    void DisplayTitleLine(string text);

    /// <summary>
    /// Display info.
    /// </summary>
    /// <param name="text">Text to display.</param>
    void DisplayInfo(string text);

    /// <summary>
    /// Display info line.
    /// </summary>
    /// <param name="text">Text to display.</param>
    void DisplayInfoLine(string text);

    /// <summary>
    /// Display error line.
    /// </summary>
    /// <param name="text">Text to display.</param>
    void DisplayErrorLine(string text = null);

    /// <summary>
    /// Display text.
    /// </summary>
    /// <param name="text">Text to display.</param>
    void DisplayText(string text);

    /// <summary>
    /// Display new line.
    /// </summary>
    void DisplayNewLine();

    /// <summary>
    /// Display text line.
    /// </summary>
    /// <param name="text">Text to display.</param>
    void DisplayTextLine(string text);
 
    /// <summary>
    /// Write error line.
    /// </summary>
    /// <param name="text">Text to display.</param>
    void WriteErrorLine(string text);
}