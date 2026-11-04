using System;

namespace PayrollEngine.Client.Command;

/// <summary>The payroll API error</summary>
public class CommandException : Exception
{
    /// <summary>Initializes a new instance of the <see cref="T:CommandException"></see> class.</summary>
    public CommandException()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:CommandException"></see> class with a specified error message.</summary>
    /// <param name="message">The message that describes the error.</param>
    public CommandException(string message) :
        base(message)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:CommandException"></see> class with a specified error message and inner exception.</summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that caused the error.</param>
    public CommandException(string message, Exception innerException) :
        base(message, innerException)
    {
    }
}
