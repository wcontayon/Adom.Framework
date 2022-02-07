using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adom.Framework
{
    [StackTraceHidden]
    internal static class ThrowHelper
    {
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
    }
}
