using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Adom.Framework.Validation.Model
{
    internal static partial class CheckerExtension
    {
        public static bool NotNull<T>([NotNull] T? value, string? paramName, CheckType checkType, CheckLevel level)
            where T : class
        {
            try
            {
                return Checker.NotNull<T>(value, paramName, checkType, level) != null;
            }
            catch (CheckException)
            {
#pragma warning disable CS8777 // Le paramètre doit avoir une valeur non null au moment de la sortie.
                return false;
#pragma warning restore CS8777 // Le paramètre doit avoir une valeur non null au moment de la sortie.
            }
        }

        public static bool NotNull<T>([NotNull] T? value, string? paramName, CheckType checkType, CheckLevel level)
            where T : struct
        {
            try
            {
                Checker.NotNull<T>(value, paramName, checkType, level);
                return true;
            }
            catch (CheckException)
            {
#pragma warning disable CS8777 // Le paramètre doit avoir une valeur non null au moment de la sortie.
                return false;
#pragma warning restore CS8777 // Le paramètre doit avoir une valeur non null au moment de la sortie.
            }
        }

        public static bool NotNullOrEmpty(string? value, string? paramName, CheckType checkType, CheckLevel checkLevel)
            => NotNull(value, paramName, checkType, checkLevel) &&
               Checker.True(value.Length > 0, "Should not be empty", false, checkType, checkLevel) &&
               Checker.True(value[0] != '\0', "First item should not be equals to \0", false, checkType, checkLevel);

        public static bool NotNullOrEmpty<T>(ICollection<T>? values, CheckType checkType, CheckLevel checkLevel)
            => NotNull(values, "collection", checkType, checkLevel) &&
               Checker.True(values!.Count != 0, "Should not be empty", false, checkType, checkLevel);

        public static bool NotNullOrEmpty<T>(IEnumerable<T>? values, CheckType checkType, CheckLevel checkLevel)
            => NotNull(values, "collection", checkType, checkLevel) &&
               Checker.True(values!.Any(), "Should not be empty", false, checkType, checkLevel);

        public static bool NotNullOrEmpty(ICollection? values, CheckType checkType, CheckLevel checkLevel)
            => NotNull(values, "collection", checkType, checkLevel) &&
               Checker.True(values.Count > 0, "Should not be empty", false, checkType, checkLevel);

        public static bool NotNullOrEmpty(Array? values, CheckType checkType, CheckLevel checkLevel)
            => NotNull(values, "collection", checkType, checkLevel) &&
               Checker.True(values.Length == 0, "Should not be empty", false, checkType, checkLevel);

        public static bool Compare(object? item, object? value, Operator @operator)
            => @operator switch
            {
                Operator.Equals => item!.Equals(value!),
                Operator.NotEquals => !item!.Equals(value!),
                Operator.LessThan => item switch
                {
                    int i => i < (int)value!,
                    decimal d => d < (decimal)value!,
                    float f => f < (float)value!,
                    long l => l < (long)value!,
                    ushort us => us < (ushort)value!,
                    uint ui => ui < (uint)value!,
                    ulong ul => ul < (ulong)value!,
                    _ => false,
                },
                Operator.LessOrEqualThan => item switch
                {
                    int i => i <= (int)value!,
                    decimal d => d <= (decimal)value!,
                    float f => f <= (float)value!,
                    long l => l <= (long)value!,
                    ushort us => us <= (ushort)value!,
                    uint ui => ui <= (uint)value!,
                    ulong ul => ul <= (ulong)value!,
                    _ => false,
                },
                Operator.GreatherThan => item switch
                {
                    int i => i > (int)value!,
                    decimal d => d > (decimal)value!,
                    float f => f > (float)value!,
                    long l => l > (long)value!,
                    ushort us => us > (ushort)value!,
                    uint ui => ui > (uint)value!,
                    ulong ul => ul > (ulong)value!,
                    _ => false,
                },
                Operator.GreatherOrEqualThan => item switch
                {
                    int i => i >= (int)value!,
                    decimal d => d >= (decimal)value!,
                    float f => f >= (float)value!,
                    long l => l >= (long)value!,
                    ushort us => us >= (ushort)value!,
                    uint ui => ui >= (uint)value!,
                    ulong ul => ul >= (ulong)value!,
                    _ => false,
                },
                _ => false,
            };
    }
}
