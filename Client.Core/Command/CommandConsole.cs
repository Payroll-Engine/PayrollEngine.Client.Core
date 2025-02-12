using System;

namespace PayrollEngine.Client.Command;

/// <inheritdoc />
public class CommandConsole : ICommandConsole
{
    /// <inheritdoc />
    public DisplayLevel DisplayLevel { get; set; }

    /// <inheritdoc />
    public ErrorMode ErrorMode { get; set; }

    /// <inheritdoc />
    public WaitMode WaitMode { get; set; }

    /// <inheritdoc />
    public void DisplaySuccessLine(string text)
    {
        if (DisplayLevel != DisplayLevel.Silent)
        {
            ConsoleToolBase.WriteSuccessLine(text);
        }
    }

    /// <inheritdoc />
    public void DisplayTitleLine(string text)
    {
        if (DisplayLevel != DisplayLevel.Silent)
        {
            ConsoleToolBase.WriteTitleLine(text);
        }
    }

    /// <inheritdoc />
    public void DisplayInfo(string text)
    {
        if (DisplayLevel != DisplayLevel.Silent)
        {
            ConsoleToolBase.WriteInfo(text);
        }
    }

    /// <inheritdoc />
    public void DisplayInfoLine(string text)
    {
        if (DisplayLevel != DisplayLevel.Silent)
        {
            ConsoleToolBase.WriteInfoLine(text);
        }
    }

    /// <inheritdoc />
    public void DisplayErrorLine(string text = null)
    {
        if (ErrorMode == ErrorMode.Errors)
        {
            // ensure error is displayed in separate line
            if (Console.CursorLeft > 0)
            {
                ConsoleToolBase.WriteLine();
            }
            WriteErrorLine(text);
        }
    }

    /// <inheritdoc />
    public void DisplayText(string text)
    {
        if (DisplayLevel != DisplayLevel.Silent)
        {
            ConsoleToolBase.Write(text);
        }
    }

    /// <inheritdoc />
    public void DisplayNewLine()
    {
        if (DisplayLevel != DisplayLevel.Silent)
        {
            ConsoleToolBase.WriteLine();
        }
    }

    /// <inheritdoc />
    public void DisplayTextLine(string text)
    {
        if (DisplayLevel != DisplayLevel.Silent)
        {
            ConsoleToolBase.WriteLine(text);
        }
    }

    /// <inheritdoc />
    public void WriteErrorLine(string text) =>
        ConsoleToolBase.WriteErrorLine(text);
}