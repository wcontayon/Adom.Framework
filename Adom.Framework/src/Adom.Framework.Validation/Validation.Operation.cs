using System;
using System.Diagnostics.CodeAnalysis;

namespace Adom.Framework.Validation
{
    public sealed partial class Operation
    {
        [ThreadStatic]
#pragma warning disable CA2019 // Improper 'ThreadStatic' field initialization
        private const CheckLevel LEVEL = CheckLevel.Operation;
#pragma warning restore CA2019 // Improper 'ThreadStatic' field initialization

        /// <summary>
        /// Throw an <see cref="InvalidOperationException"/> if the condition is true.
        /// </summary>
        /// <param name="condition">Condition to check.</param>
        /// <param name="message">The message to show if the exception is throwned.</param>
        /// <exception cref="InvalidOperationException">.</exception>
        public static void ThrowIf([DoesNotReturnIf(true)] bool condition, string? message) => Checker.False(condition, message, true, CheckType.Required, LEVEL);

        /// <summary>
        /// Throw an <see cref="InvalidOperationException"/> if the condition is false.
        /// </summary>
        /// <param name="condition">Condition to evaluate.</param>
        /// <param name="throwException">Exception if true..</param>
        /// <param name="message">Message to show on the exception.</param>
        /// <exception cref="InvalidOperationException">.</exception>
        public static bool That(bool condition, [DoesNotReturnIf(true)] bool throwException, string? message) => Checker.True(condition, message, throwException, CheckType.Required, LEVEL);

        /// <summary>
        /// Check if the condition is true.
        /// </summary>
        /// <param name="condition">Condition to check.</param>
        /// <param name="message">The message to show if the exception is throwned.</param>
        /// <param name="throwException">Throw an <see cref="InvalidOperationException"/> if true.</param>
        /// <returns>True or False.</returns>
        public static bool True(bool condition, string? message, [DoesNotReturnIf(true)] bool throwException = true) => Checker.True(condition, message, throwException, CheckType.Required, LEVEL);

        /// <summary>
        /// Check if the condition is false.
        /// </summary>
        /// <param name="condition">Condition to check.</param>
        /// <param name="message">The message to show if the exception is throwned.</param>
        /// <param name="throwException">Throw an <see cref="InvalidOperationException"/> if true.</param>
        /// <returns>True or False.</returns>
        public static bool False(bool condition, string? message, [DoesNotReturnIf(true)] bool throwException = true) => Checker.False(condition, message, throwException, CheckType.Required, LEVEL);

        /// <summary>
        /// Throw an <see cref="InvalidOperationException"/> if the condition is false.
        /// </summary>
        /// <param name="function">Function to evaluate.</param>
        /// <param name="throwException">Exception if true..</param>
        /// <param name="message">Message to show on the exception.</param>
        /// <exception cref="InvalidOperationException">.</exception>
        public static bool That([NotNull]Func<bool> function, [DoesNotReturnIf(true)] bool throwException, string? message) => Checker.True(function(), message, throwException, CheckType.Required, LEVEL);

        /// <summary>
        /// Check if the condition is true.
        /// </summary>
        /// <param name="function">Function to evaluate.</param>
        /// <param name="message">The message to show if the exception is throwned.</param>
        /// <param name="throwException">Throw an <see cref="InvalidOperationException"/> if true.</param>
        /// <returns>True or False.</returns>
        public static bool True([NotNull]Func<bool> function, string? message, [DoesNotReturnIf(true)] bool throwException = true) => Checker.True(function(), message, throwException, CheckType.Required, LEVEL);

        /// <summary>
        /// Check if the condition is false.
        /// </summary>
        /// <param name="function">Function to evaluate.</param>
        /// <param name="message">The message to show if the exception is throwned.</param>
        /// <param name="throwException">Throw an <see cref="InvalidOperationException"/> if true.</param>
        /// <returns>True or False.</returns>
        public static bool False([NotNull]Func<bool> function, string? message, [DoesNotReturnIf(true)] bool throwException = true) => Checker.False(function(), message, throwException, CheckType.Required, LEVEL);

    }
}
