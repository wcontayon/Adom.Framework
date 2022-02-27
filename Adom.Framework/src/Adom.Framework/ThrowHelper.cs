using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Adom.Framework
{
#if NET7_0
    [StackTraceHidden]
#endif
    internal static class ThrowHelper
    {
        [DoesNotReturn]
#pragma warning disable CA2201 // Ne pas lever de types d'exception réservés
        internal static void ThrowIndexOutOfRangeException(string message) => throw new IndexOutOfRangeException(message);
#pragma warning restore CA2201 // Ne pas lever de types d'exception réservés

        [DoesNotReturn]
        internal static void ThrowArgumentOutOfRangeException(string message) => throw new ArgumentOutOfRangeException(message);

        [DoesNotReturn]
        internal static void ThrowArgumentOutOfRangeException()
            => throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection");

        [DoesNotReturn]
        internal static void ThrowArgumentNullException(string argumentName)
            => throw new ArgumentNullException(argumentName);

        [DoesNotReturn]
        internal static void ThrowCapacityReachedException(int capacity)
            => throw new InvalidOperationException($"Capacity of {capacity} has been reached");

        [DoesNotReturn]
        internal static void ThrowArgumentException(string message)
            => throw new ArgumentException(message);

        [DoesNotReturn]
        internal static void ThrowInvalidOperation_EnumeratorHasFailed(string message)
            => throw new InvalidOperationException(message);

        [DoesNotReturn]
        internal static void ThrowInvalidOperationException(string message) => throw new InvalidOperationException(message);

        [DoesNotReturn]
        internal static void ThrowDivideByZeroException(string message) => throw new DivideByZeroException(message);
    }
}
