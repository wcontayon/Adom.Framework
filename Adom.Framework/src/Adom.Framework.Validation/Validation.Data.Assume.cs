using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Adom.Framework.Validation
{
    [SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
    public sealed partial class Data
    {
#pragma warning disable CA1034 // Les types imbriqués ne doivent pas être visibles
        public sealed class Assume
#pragma warning restore CA1034 // Les types imbriqués ne doivent pas être visibles
        {
            [ThreadStatic]
#pragma warning disable CA2019 // Improper 'ThreadStatic' field initialization
            private const CheckType TYPE = CheckType.Assume;
#pragma warning restore CA2019 // Improper 'ThreadStatic' field initialization

            /// <summary>
            /// Throw an <see cref="InternalCheckException"/> if the value is null.
            /// </summary>
            /// <typeparam name="T"><typeparamref name="T"/>.</typeparam>
            /// <param name="value">Value to be tested.</param>
            /// <param name="paramName">Name of the parameter.</param>
            /// <returns><typeparamref name="T"/> value.</returns>
            /// <exception cref="InternalCheckException">.</exception>
            public static T NotNull<T>(T? value, string? paramName)
                where T : class
                => Checker.NotNull<T>(value, paramName, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="InternalCheckException"/> if the value is null.
            /// </summary>
            /// <typeparam name="T"><typeparamref name="T"/>.</typeparam>
            /// <param name="value">Value to be tested.</param>
            /// <param name="paramName">Name of the parameter.</param>
            /// <returns><typeparamref name="T"/> value.</returns>
            /// <exception cref="InternalCheckException">.</exception>
            public static T NotNull<T>(T? value, string? paramName)
                where T : struct
                => Checker.NotNull<T>(value, paramName, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="InternalCheckException"/> if the value (<see cref="string"/>) is null or empty.
            /// </summary>
            /// <param name="value">Value to be tested.</param>
            /// <param name="paramName">Name of the parameter.</param>
            /// <exception cref="InternalCheckException">.</exception>
            public static void NotNullOrEmpty(string? value, string? paramName) => Checker.NotNullOrEmpty(value, paramName, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="InternalCheckException"/> if the values (<see cref="ICollection{T}"/>) is null.
            /// </summary>
            /// <typeparam name="T"><typeparamref name="T"/>.</typeparam>
            /// <param name="values">Value to be tested.</param>
            /// <exception cref="InternalCheckException">.</exception>
            public static void NotNullOrEmpty<T>(ICollection<T> values) => Checker.NotNullOrEmpty<T>(values, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="InternalCheckException"/> if the values (<see cref="IEnumerable{T}"/>) is null.
            /// </summary>
            /// <typeparam name="T"><typeparamref name="T"/>.</typeparam>
            /// <param name="values">Value to be tested.</param>
            /// <exception cref="InternalCheckException">.</exception>
            public static void NotNullOrEmpty<T>(IEnumerable<T> values) => Checker.NotNullOrEmpty<T>(values, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="Exception"/> if the value is not type of <typeparamref name="T"/>.
            /// </summary>
            /// <typeparam name="T"><typeparamref name="T"/>.</typeparam>
            /// <param name="value">Value to be tested.</param>
            /// <param name="paramName">Name of the parameter.</param>
            /// <returns><typeparamref name="T"/> value.</returns>
            /// <exception cref="Exception">.</exception>
            public static T Is<T>(object? value, string? paramName)
                where T : class => Checker.Is<T>(value, paramName, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="Exception"/> if the value is not null.
            /// </summary>
            /// <typeparam name="T"><typeparamref name="T"/>.</typeparam>
            /// <param name="value">Value to be tested.</param>
            /// <param name="paramName">Name of the parameter.</param>
            /// <exception cref="Exception">.</exception>
            public static void Null<T>(T? value, string? paramName)
                where T : class => Checker.Null<T>(value, paramName, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="Exception"/> if the value is not null.
            /// </summary>
            /// <typeparam name="T"><typeparamref name="T"/>.</typeparam>
            /// <param name="value">Value to be tested.</param>
            /// <param name="paramName">Name of the parameter.</param>
            /// <exception cref="Exception">.</exception>
            public static void Null<T>(T? value, string? paramName)
                where T : struct => Checker.Null<T>(value, paramName, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="Exception"/> if the value is not null.
            /// </summary>
            /// <param name="value">Value to be tested.</param>
            /// <param name="paramName">Name of the parameter.</param>
            /// <exception cref="Exception">.</exception>
            public static void NullOrEmpty(string? value, string? paramName) => Checker.NullOrEmpty(value, paramName, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="Exception"/> if the values (<see cref="ICollection{T}"/>) is not null or empty.
            /// </summary>
            /// <typeparam name="T"><typeparamref name="T"/>.</typeparam>
            /// <param name="values">Value to be tested.</param>
            /// <exception cref="Exception">.</exception>
            public static void NullOrEmpty<T>(ICollection<T> values) => Checker.NullOrEmpty<T>(values, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="Exception"/> if the values (<see cref="IEnumerable{T}"/>) is not null or empty.
            /// </summary>
            /// <typeparam name="T"><typeparamref name="T"/>.</typeparam>
            /// <param name="values">Value to be tested.</param>
            /// <exception cref="Exception">.</exception>
            public static void NullOrEmpty<T>(IEnumerable<T> values) => Checker.NullOrEmpty<T>(values, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="Exception"/> if the condition is false.
            /// </summary>
            /// <param name="condition">Condition to evaluate.</param>
            /// <param name="throwException">Exception if true..</param>
            /// <param name="message">Message to show on the exception.</param>
            /// <exception cref="Exception">.</exception>
            public static bool That(bool condition, [DoesNotReturnIf(true)] bool throwException, string? message) => Checker.True(condition, message, throwException, TYPE, LEVEL);

            /// <summary>
            /// Check if the condition is true.
            /// </summary>
            /// <param name="condition">Condition to check.</param>
            /// <param name="message">The message to show if the exception is throwned.</param>
            /// <param name="throwException">Throw an <see cref="Exception"/> if true.</param>
            /// <returns>True or False.</returns>
            public static bool True(bool condition, string? message, [DoesNotReturnIf(true)] bool throwException = true) => Checker.True(condition, message, throwException, TYPE, LEVEL);

            /// <summary>
            /// Check if the condition is false.
            /// </summary>
            /// <param name="condition">Condition to check.</param>
            /// <param name="message">The message to show if the exception is throwned.</param>
            /// <param name="throwException">Throw an <see cref="Exception"/> if true.</param>
            /// <returns>True or False.</returns>
            public static bool False(bool condition, string? message, [DoesNotReturnIf(true)] bool throwException = true) => Checker.False(condition, message, throwException, TYPE, LEVEL);
        }
    }
}
