﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Adom.Framework.Validation
{
    public sealed partial class Argument
    {
#pragma warning disable CA1034 // Les types imbriqués ne doivent pas être visibles
        public sealed class Require
#pragma warning restore CA1034 // Les types imbriqués ne doivent pas être visibles
        {
            [ThreadStatic]
#pragma warning disable CA2019 // Improper 'ThreadStatic' field initialization
            private const CheckType TYPE = CheckType.Required;
#pragma warning restore CA2019 // Improper 'ThreadStatic' field initialization

            /// <summary>
            /// Throw an <see cref="ArgumentNullException"/> if the value is null.
            /// </summary>
            /// <typeparam name="T"><typeparamref name="T"/>.</typeparam>
            /// <param name="value">Value to be tested.</param>
            /// <param name="paramName">Name of the parameter.</param>
            /// <returns><typeparamref name="T"/> value.</returns>
            /// <exception cref="ArgumentNullException">.</exception>
            public static T NotNull<T>(T? value, string? paramName)
                where T : class
                => Checker.NotNull<T>(value, paramName, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="ArgumentNullException"/> if the value is null.
            /// </summary>
            /// <typeparam name="T"><typeparamref name="T"/>.</typeparam>
            /// <param name="value">Value to be tested.</param>
            /// <param name="paramName">Name of the parameter.</param>
            /// <returns><typeparamref name="T"/> value.</returns>
            /// <exception cref="ArgumentNullException">.</exception>
            public static T NotNull<T>(T? value, string? paramName)
                where T : struct
                => Checker.NotNull<T>(value, paramName, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="ArgumentNullException"/> if the value (<see cref="string"/>) is null or empty.
            /// </summary>
            /// <param name="value">Value to be tested.</param>
            /// <param name="paramName">Name of the parameter.</param>
            /// <exception cref="ArgumentNullException">.</exception>
            public static void NotNullOrEmpty(string? value, string? paramName) => Checker.NotNullOrEmpty(value, paramName, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="ArgumentNullException"/> if the values (<see cref="ICollection{T}"/>) is null.
            /// </summary>
            /// <typeparam name="T"><typeparamref name="T"/>.</typeparam>
            /// <param name="values">Value to be tested.</param>
            /// <exception cref="ArgumentNullException">.</exception>
            public static void NotNullOrEmpty<T>(ICollection<T> values) => Checker.NotNullOrEmpty<T>(values, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="ArgumentNullException"/> if the values (<see cref="IEnumerable{T}"/>) is null.
            /// </summary>
            /// <typeparam name="T"><typeparamref name="T"/>.</typeparam>
            /// <param name="values">Value to be tested.</param>
            /// <exception cref="ArgumentNullException">.</exception>
            public static void NotNullOrEmpty<T>(IEnumerable<T> values) => Checker.NotNullOrEmpty<T>(values, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="ArgumentException"/> if the value is not type of <typeparamref name="T"/>.
            /// </summary>
            /// <typeparam name="T"><typeparamref name="T"/>.</typeparam>
            /// <param name="value">Value to be tested.</param>
            /// <param name="paramName">Name of the parameter.</param>
            /// <returns><typeparamref name="T"/> value.</returns>
            /// <exception cref="ArgumentException">.</exception>
            public static T Is<T>(object? value, string? paramName)
                where T : class => Checker.Is<T>(value, paramName, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="ArgumentException"/> if the value is not null.
            /// </summary>
            /// <typeparam name="T"><typeparamref name="T"/>.</typeparam>
            /// <param name="value">Value to be tested.</param>
            /// <param name="paramName">Name of the parameter.</param>
            /// <exception cref="ArgumentException">.</exception>
            public static void Null<T>(T? value, string? paramName)
                where T : class => Checker.Null<T>(value, paramName, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="ArgumentException"/> if the value is not null.
            /// </summary>
            /// <typeparam name="T"><typeparamref name="T"/>.</typeparam>
            /// <param name="value">Value to be tested.</param>
            /// <param name="paramName">Name of the parameter.</param>
            /// <exception cref="ArgumentException">.</exception>
            public static void Null<T>(T? value, string? paramName)
                where T : struct => Checker.Null<T>(value, paramName, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="ArgumentException"/> if the value is not null.
            /// </summary>
            /// <param name="value">Value to be tested.</param>
            /// <param name="paramName">Name of the parameter.</param>
            /// <exception cref="ArgumentException">.</exception>
            public static void NullOrEmpty(string? value, string? paramName) => Checker.NullOrEmpty(value, paramName, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="ArgumentException"/> if the values (<see cref="ICollection{T}"/>) is not null or empty.
            /// </summary>
            /// <typeparam name="T"><typeparamref name="T"/>.</typeparam>
            /// <param name="values">Value to be tested.</param>
            /// <exception cref="ArgumentException">.</exception>
            public static void NullOrEmpty<T>(ICollection<T> values) => Checker.NullOrEmpty<T>(values, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="ArgumentException"/> if the values (<see cref="IEnumerable{T}"/>) is not null or empty.
            /// </summary>
            /// <typeparam name="T"><typeparamref name="T"/>.</typeparam>
            /// <param name="values">Value to be tested.</param>
            /// <exception cref="ArgumentException">.</exception>
            public static void NullOrEmpty<T>(IEnumerable<T> values) => Checker.NullOrEmpty<T>(values, TYPE, LEVEL);

            /// <summary>
            /// Throw an <see cref="ArgumentException"/> if the condition is false.
            /// </summary>
            /// <param name="condition">Condition to evaluate.</param>
            /// <param name="throwException">Exception if true..</param>
            /// <param name="message">Message to show on the exception.</param>
            /// <exception cref="ArgumentException">.</exception>
            public static bool That(bool condition, [DoesNotReturnIf(true)] bool throwException, string? message) => Checker.True(condition, message, throwException, TYPE, LEVEL);
        }
    }
}
