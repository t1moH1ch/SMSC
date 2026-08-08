using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace SMSC;

internal static class ThrowHelper
{
    public static void ThrowIfNull([NotNull] object? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null)
            throw new ArgumentNullException(paramName);
    }

    public static void ThrowIfNullOrEmpty([NotNull] string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null || argument.Length == 0)
            throw new ArgumentException("The value cannot be an empty string.", paramName);
    }

    public static void ThrowIfLessThan<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        where T : IComparable<T>
    {
        if (value.CompareTo(other) < 0)
            throw new ArgumentOutOfRangeException(paramName, value, $"Value must be greater than or equal to {other}.");
    }

    public static void ThrowIfGreaterThan<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        where T : IComparable<T>
    {
        if (value.CompareTo(other) > 0)
            throw new ArgumentOutOfRangeException(paramName, value, $"Value must be less than or equal to {other}.");
    }
}
