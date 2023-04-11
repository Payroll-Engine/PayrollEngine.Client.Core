using System;
using System.Collections;
using System.Text;

namespace PayrollEngine.Client;

/// <summary>Extension methods for <see cref="Console"/></summary>
public static class ConsoleExtensions
{
    /// <summary>Display the function results on the screen</summary>
    /// <param name="source">The object</param>
    /// <param name="details">Show details</param>
    public static void WriteProperties(this object source, bool details = true)
    {
        if (source == null)
        {
            return;
        }

        // type properties
        var properties = source.GetType().GetInstanceProperties();
        Console.WriteLine($"{source.GetType().FullName}");
        // display properties
        foreach (var property in properties)
        {
            var value = property.GetValue(source);
            var displayValue = value?.ToString();
            if (details && value is ICollection collection)
            {
                // concat collection values
                var buffer = new StringBuilder();
                foreach (var item in collection)
                {
                    if (buffer.Length > 0)
                    {
                        buffer.Append(", ");
                    }
                    buffer.Append(item);
                }
                displayValue = $"[{buffer}]";
            }
            Console.WriteLine($"    {property.Name,-16}: {displayValue}");
        }
        Console.WriteLine();
    }
}