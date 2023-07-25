using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PayrollEngine.Client;

/// <summary>Represents the environment command line arguments</summary>
public static class ConsoleArguments
{
    private static string[] CommandLineArgs { get; } = Environment.GetCommandLineArgs();

    /// <summary>Get the command line arguments count, excluding the implicit parameter at index 0</summary>
    public static int ParameterCount =>
        Count - 1;

    /// <summary>Get the command line arguments count</summary>
    public static int Count =>
        CommandLineArgs.Length;

    /// <summary>Determines whether the specified argument exists</summary>
    /// <param name="name">The argument</param>
    /// <returns>True if the specified argument exists</returns>
    public static bool Contains(string name) =>
        CommandLineArgs.FirstOrDefault(x => string.Equals(name, x, StringComparison.InvariantCultureIgnoreCase)) != null;

    /// <summary>Determines whether the specified argument exists</summary>
    /// <returns>True if the specified toggle exists</returns>
    public static bool IsValidOrder()
    {
        var arguments = CommandLineArgs;
        int? firstToggleIndex = null;
        for (var i = 0; i < arguments.Length; i++)
        {
            var argument = arguments[i];
            if (IsToggleArgument(argument))
            {
                firstToggleIndex ??= i;
            }
            else if (i > firstToggleIndex)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>Determines whether the specified argument exists</summary>
    /// <param name="toggleValue">The toggle value</param>
    /// <returns>True if the specified toggle exists</returns>
    public static bool ContainsToggle(string toggleValue)
    {
        if (IsToggleArgument(toggleValue))
        {
            return Contains(toggleValue);
        }
        return Contains($"/{toggleValue}") || Contains($"-{toggleValue}");
    }

    /// <summary>Determines whether the specified argument is a toggle</summary>
    /// <param name="argument">The argument</param>
    /// <returns>True if the specified argument exists</returns>
    public static bool IsToggleArgument(string argument)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            return false;
        }
        return argument.StartsWith('/') || argument.StartsWith('-');
    }

    /// <summary>Gets the specified index, starting at 1</summary>
    /// <param name="type">The source type</param>
    /// <param name="index">The argument index</param>
    /// <param name="memberName">The caller name</param>
    /// <param name="allowToggle">Argument can be a toggle</param>
    /// <returns>The argument value</returns>
    public static string GetMember(Type type, int index, [CallerMemberName] string memberName = "", bool allowToggle = false) =>
        Get(index, memberName, allowToggle);

    /// <summary>Gets the specified index, starting at 1</summary>
    /// <param name="index">The argument index</param>
    /// <param name="name">The argument name (optional)</param>
    /// <param name="allowToggle">Argument can be a toggle (optional)</param>
    /// <returns>The argument value</returns>
    public static string Get(int index, string name = null, bool allowToggle = false)
    {
        if (index <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if (CommandLineArgs.Length <= index)
        {
            return null;
        }

        // named argument
        if (!string.IsNullOrWhiteSpace(name))
        {
            var marker = $"{name}:";
            foreach (var cmdLineArg in CommandLineArgs)
            {
                if (cmdLineArg.StartsWith(marker, StringComparison.InvariantCultureIgnoreCase))
                {
                    return cmdLineArg.Substring(marker.Length);
                }
            }
        }

        var arg = CommandLineArgs[index];
        if (!allowToggle && IsToggleArgument(arg))
        {
            arg = null;
        }
        return arg;
    }

    /// <summary>Gets an integer argument</summary>
    /// <param name="index">The argument index</param>
    /// <param name="name">The argument name (optional)</param>
    /// <returns>The argument value</returns>
    public static int? GetInt(int index, string name = null)
    {
        if (index <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        var arg = Get(index, name);
        if (!string.IsNullOrWhiteSpace(arg) &&
            int.TryParse(arg, out var value))
        {
            return value;
        }
        return default;
    }

    /// <summary>Gets an integer argument</summary>
    /// <param name="index">The argument index</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="name">The argument name (optional)</param>
    /// <returns>The argument value</returns>
    public static int GetInt(int index, int defaultValue, string name = null)
    {
        var arg = Get(index, name);
        if (!string.IsNullOrWhiteSpace(arg) &&
            int.TryParse(arg, out var value))
        {
            return value;
        }
        return defaultValue;
    }

    /// <summary>Gets a mandatory enum argument</summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="index">The argument index</param>
    /// <param name="name">The argument name (optional)</param>
    /// <returns>The argument value</returns>
    public static T GetEnum<T>(int index, string name = null)
    {
        var enumType = typeof(T);
        if (!enumType.IsEnum)
        {
            throw new ArgumentException("Argument must an enum");
        }
        var arg = Get(index, name);
        if (string.IsNullOrWhiteSpace(arg))
        {
            throw new ArgumentException($"Missing argument at position {index}", nameof(index));

        }
        if (string.IsNullOrWhiteSpace(arg))
        {
            return default;
        }
        if (!Enum.TryParse(typeof(T), arg, true, out var value))
        {
            throw new ArgumentOutOfRangeException($"Unknown argument {arg} at position {index}", nameof(index));
        }
        return (T)value;
    }

    /// <summary>Gets an enum argument</summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="index">The argument index</param>
    /// <param name="name">The argument name (optional)</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The argument value</returns>
    public static T GetEnum<T>(int index, T defaultValue, string name = null)
    {
        var enumType = typeof(T);
        if (!enumType.IsEnum)
        {
            throw new ArgumentException("Argument must an enum");
        }
        var arg = Get(index, name);
        if (!string.IsNullOrWhiteSpace(arg) &&
            Enum.TryParse(typeof(T), arg, true, out var value))
        {
            return (T)value;
        }
        return defaultValue;
    }

    /// <summary>Gets an enum toggle is present: /toggle or -toggle</summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <returns>The argument value</returns>
    public static T? GetEnumToggle<T>() where T : struct, Enum
    {
        var enumType = typeof(T);
        if (!enumType.IsEnum)
        {
            throw new ArgumentException("Argument must an enum");
        }
        foreach (var enumValue in Enum.GetValues<T>().ToList())
        {
            if (ContainsToggle(Enum.GetName(enumValue)))
            {
                return enumValue;
            }
        }
        return null;
    }

    /// <summary>Gets all toggle with fallback value</summary>
    /// <returns>The argument value</returns>
    public static IEnumerable<string> GetToggles() =>
        CommandLineArgs.Where(IsToggleArgument);

    /// <summary>Gets an toggle with fallback value</summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The argument value</returns>
    public static T GetEnumToggle<T>(T defaultValue) where T : struct, Enum =>
        GetEnumToggle<T>() ?? defaultValue;

    /// <summary>Test for unknown toggle arguments</summary>
    /// <param name="enumTypes">The supported enum types</param>
    /// <returns>The unknown argument</returns>
    public static string TestUnknownToggles(IEnumerable<Type> enumTypes)
    {
        var enumTypesArray = enumTypes.ToArray();
        // test for unknown toggles
        for (var i = 1; i < CommandLineArgs.Length; i++)
        {
            // command line argument
            var arg = CommandLineArgs[i];
            if (!IsToggleArgument(arg))
            {
                continue;
            }

            var knownToggle = false;
            var toggleName = arg.Substring(1);
            foreach (var enumType in enumTypesArray)
            {
                var enumNames = Enum.GetNames(enumType).ToList();
                if (!enumNames.Any(x => string.Equals(x, toggleName, StringComparison.InvariantCultureIgnoreCase)))
                {
                    // unknown toggle
                    continue;
                }
                knownToggle = true;
                break;
            }
            if (!knownToggle)
            {
                return arg;
            }
        }
        return null;
    }

    /// <summary>Test for multiple toggles</summary>
    /// <param name="enumTypes">The supported enum types</param>
    /// <returns>The failed type</returns>
    public static Type TestMultipleToggles(IEnumerable<Type> enumTypes)
    {
        foreach (var enumType in enumTypes)
        {
            if (Enum.GetNames(enumType).Count(ContainsToggle) > 1)
            {
                return enumType;
            }
        }
        return null;
    }
}