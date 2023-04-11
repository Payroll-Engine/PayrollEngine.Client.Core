using System;

namespace PayrollEngine.Client;

/// <summary>Tool with console operations</summary>
public abstract class ConsoleToolBase
{
    /// <summary>Obtains the next character or function key pressed by the user</summary>
    /// <param name="intercept">Determines whether to display the pressed key in the console window. true to not display the pressed key; otherwise, false</param>
    public static ConsoleKeyInfo ReadKey(bool intercept) =>
        Console.ReadKey(intercept);

    /// <summary>Wait for key input</summary>
    public static void PressAnyKey()
    {
        WriteLine();
        Write("Press any key...");
        ReadKey(true);
    }

    /// <summary>The console success foreground color</summary>
    public static ConsoleColor SuccessColor { get; set; } = ConsoleColor.Green;
    /// <summary>The console title foreground color</summary>
    public static ConsoleColor TitleColor { get; set; } = ConsoleColor.Cyan;
    /// <summary>The console info foreground color</summary>
    public static ConsoleColor InfoColor { get; set; } = ConsoleColor.DarkGray;
    /// <summary>The console error foreground color</summary>
    public static ConsoleColor ErrorColor { get; set; } = ConsoleColor.Red;

    /// <summary>Gets or sets the foreground color of the console</summary>
    public static ConsoleColor ForegroundColor
    {
        get => Console.ForegroundColor;
        set => Console.ForegroundColor = value;
    }

    /// <summary>Gets or sets the exit code of the process</summary>
    public static int ExitCode
    {
        get => Environment.ExitCode;
        set => Environment.ExitCode = value;
    }

    /// <summary>Write success</summary>
    public static void WriteSuccess(string text) =>
        WriteColor(text, SuccessColor);

    /// <summary>Write success line</summary>
    public static void WriteSuccessLine(string text) =>
        WriteColorLine(text, SuccessColor);

    /// <summary>Write title</summary>
    public static void WriteTitle(string text) =>
        WriteColor(text, TitleColor);

    /// <summary>Write title line</summary>
    public static void WriteTitleLine(string text) =>
        WriteColorLine(text, TitleColor);

    /// <summary>Write info</summary>
    public static void WriteInfo(string text) =>
        WriteColor(text, InfoColor);

    /// <summary>Write info line</summary>
    public static void WriteInfoLine(string text) =>
        WriteColorLine(text, InfoColor);

    /// <summary>Write error</summary>
    public static void WriteError(string text) =>
        WriteColor(text, ErrorColor);

    /// <summary>Write error line</summary>
    public static void WriteErrorLine(string text) =>
        WriteColorLine(text, ErrorColor);

    /// <summary>Writes the text representation of the specified value or values to the standard output stream</summary>
    public static void Write(string text) =>
        Console.Write(text);

    /// <summary>Writes the specified data, followed by the current line terminator, to the standard output stream</summary>
    public static void WriteLine(string text = null) =>
        Console.WriteLine(text);

    /// <summary>Write colored line</summary>
    public static void WriteColor(string text, ConsoleColor color)
    {
        var previousColor = Console.ForegroundColor;
        ForegroundColor = color;
        Write(text);
        ForegroundColor = previousColor;
    }

    /// <summary>Write colored line</summary>
    public static void WriteColorLine(string text, ConsoleColor color)
    {
        var previousColor = Console.ForegroundColor;
        ForegroundColor = color;
        WriteLine(text);
        ForegroundColor = previousColor;
    }
}