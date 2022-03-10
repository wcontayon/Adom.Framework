using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Adom.Framework.Validation
{
    /// <summary>
    /// Internal checker class used to provide check methods.
    /// </summary>
    internal partial class Checker
    {
        public const string ARGUMENT_NULL_MSG_PATTERN = "{0} is null";
        public const string ARGUMENT_NOTNULL_MSG_PATTERN = "{0} is not null";
        public const string ARGUMENT_TYPE_MSG_PATTERN = "{0} is not of type {1}";

        #region NotNull Check

        public static T NotNull<T>([NotNull] T? value, string? paramName, CheckType checkType, CheckLevel level)
            where T : class
        {
            Fail(
                !(value is null),
                string.Format(CultureInfo.InvariantCulture, ARGUMENT_NULL_MSG_PATTERN, paramName),
                checkType,
                level,
                true);

#pragma warning disable CS8777 // Le paramètre doit avoir une valeur non null au moment de la sortie.
            return value!;
#pragma warning restore CS8777 // Le paramètre doit avoir une valeur non null au moment de la sortie.
        }

        public static T NotNull<T>([NotNull] T? value, string? paramName, CheckType checkType, CheckLevel level)
            where T : struct
        {
            Fail(
                value.HasValue,
                string.Format(CultureInfo.InvariantCulture, ARGUMENT_NULL_MSG_PATTERN, paramName),
                checkType,
                level,
                true);

            return value!.Value;
        }

        public static void NotNullOrEmpty(string? value, string? paramName, CheckType checkType, CheckLevel checkLevel)
        {
            Fail(!string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value), string.Format(CultureInfo.InvariantCulture, ARGUMENT_NULL_MSG_PATTERN, paramName), checkType, checkLevel, true);
            Fail(value!.Length > 0, string.Format(CultureInfo.InvariantCulture, ARGUMENT_NULL_MSG_PATTERN, paramName), checkType, checkLevel, true);
            Fail(value[0] != '\0', string.Format(CultureInfo.InvariantCulture, ARGUMENT_NULL_MSG_PATTERN, paramName), checkType, checkLevel, true);
        }

        public static void NotNullOrEmpty<T>(ICollection<T>? values, CheckType checkType, CheckLevel checkLevel)
        {
            NotNull(values, "collection", checkType, checkLevel);
            Fail(values!.Any(), string.Format(CultureInfo.InvariantCulture, ARGUMENT_NULL_MSG_PATTERN, "collection"), checkType, checkLevel, true);
        }

        public static void NotNullOrEmpty<T>(IEnumerable<T>? values, CheckType checkType, CheckLevel checkLevel)
        {
            NotNull(values, "collection", checkType, checkLevel);
            Fail(values!.Any(), string.Format(CultureInfo.InvariantCulture, ARGUMENT_NULL_MSG_PATTERN, "collection"), checkType, checkLevel, true);
        }

        #endregion

        #region Null Check

        public static T Null<T>([NotNull] T? value, string? paramName, CheckType checkType, CheckLevel level)
            where T : class
        {
            // nullCheck param should be false, either we want the value to be null.
            Fail(
                value == null,
                string.Format(CultureInfo.InvariantCulture, ARGUMENT_NOTNULL_MSG_PATTERN, paramName),
                checkType,
                level,
                false);

#pragma warning disable CS8777 // Le paramètre doit avoir une valeur non null au moment de la sortie.
            return value!;
#pragma warning restore CS8777 // Le paramètre doit avoir une valeur non null au moment de la sortie.
        }

        public static T Null<T>([NotNull] T? value, string? paramName, CheckType checkType, CheckLevel level)
            where T : struct
        {
            // nullCheck param should be false, either we want the value to be null.
            Fail(
                !value.HasValue,
                string.Format(CultureInfo.InvariantCulture, ARGUMENT_NOTNULL_MSG_PATTERN, paramName),
                checkType,
                level,
                false);

            return value!.Value;
        }

        public static void NullOrEmpty(string? value, string? paramName, CheckType checkType, CheckLevel checkLevel)
        {
            Fail(string.IsNullOrEmpty(value), string.Format(CultureInfo.InvariantCulture, ARGUMENT_NOTNULL_MSG_PATTERN, paramName), checkType, checkLevel, false);
            Fail(string.IsNullOrWhiteSpace(value), string.Format(CultureInfo.InvariantCulture, ARGUMENT_NOTNULL_MSG_PATTERN, paramName), checkType, checkLevel, false);
        }

        public static void NullOrEmpty<T>(ICollection<T>? values, CheckType checkType, CheckLevel checkLevel)
        {
            Fail(values is null || !values!.Any(), string.Format(CultureInfo.InvariantCulture, ARGUMENT_NOTNULL_MSG_PATTERN, "collection"), checkType, checkLevel, false);
        }

        public static void NullOrEmpty<T>(IEnumerable<T>? values, CheckType checkType, CheckLevel checkLevel)
        {
            Fail(values is null || !values!.Any(), string.Format(CultureInfo.InvariantCulture, ARGUMENT_NOTNULL_MSG_PATTERN, "collection"), checkType, checkLevel, false);
        }

        #endregion

        #region Type check

        public static T Is<T>([NotNull] object? value, string? paramName, CheckType checkType, CheckLevel level)
            where T : class
        {
            Fail(
                !(value is T),
                string.Format(CultureInfo.InvariantCulture, ARGUMENT_TYPE_MSG_PATTERN, paramName, typeof(T).Name),
                checkType,
                level,
                true);

#pragma warning disable CS8777 // Le paramètre doit avoir une valeur non null au moment de la sortie.
            return (value as T)!;
#pragma warning restore CS8777 // Le paramètre doit avoir une valeur non null au moment de la sortie.
        }

        #endregion

        #region Condition Check

        public static bool False([NotNull] bool condition, string? message, bool throwException, CheckType checkType, CheckLevel level)
        {
            if (throwException)
            {
                Fail(!condition, message, checkType, level, false);
            }

            return !condition;
        }

        public static bool True([NotNull] bool condition, string? message, bool throwException, CheckType checkType, CheckLevel level)
        {
            if (throwException)
            {
                Fail(condition, message, checkType, level, false);
            }

            return condition;
        }

        #endregion

        #region private check method

        #endregion

        #region Private methods

        /// <summary>
        /// Throw an exception with the message specified in parameter.
        /// </summary>
        /// <param name="condition">Condition to evaluate.</param>
        /// <param name="message">The message of exception.</param>
        /// <param name="checkType">The <see cref="CheckType"/> executed.</param>
        /// <param name="checkLevel">The <see cref="CheckLevel"/> executed.</param>
        /// <param name="nullCheck">Define if we do a null (data/input) check.</param>
        //[DebuggerStepThrough]
        private static CheckException Fail([DoesNotReturnIf(true)] bool condition, string? message, CheckType checkType, CheckLevel checkLevel, bool nullCheck) =>
            !condition ? checkType switch
            {
                CheckType.Assume => ThrowHelper.ThrowCheckException(CheckType.Assume, message),
                CheckType.Required =>
                    checkLevel switch
                    {
                        CheckLevel.Argument => nullCheck ? ThrowHelper.ThrowNullArgumentException(message) : ThrowHelper.ThrowArgumentException(message),
                        CheckLevel.Data => nullCheck ? ThrowHelper.ThrowNullReferenceException(message) : ThrowHelper.ThrowCheckException(message),
                        CheckLevel.Operation => ThrowHelper.ThrowInvalidOperationException(message),
                        _ => ThrowHelper.ThrowCheckException(message),
                    },
                CheckType.NotRequired => null!,
                CheckType.Validation => ThrowHelper.ThrowCheckException(CheckType.Validation, message),
                _ => ThrowHelper.ThrowCheckException(message),
            } : null!;

        #endregion
    }
}
