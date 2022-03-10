using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Adom.Framework.Validation
{
    internal static class ThrowHelper
    {
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#pragma warning disable CA2201 // Ne pas lever de types d'exception réservés
        public static Exception ThrowException(string? message) => throw new Exception(message);
#pragma warning restore CA2201 // Ne pas lever de types d'exception réservés

        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#pragma warning disable CA2201 // Ne pas lever de types d'exception réservés
        public static Exception ThrowException(string? message, Exception innerException) => throw new Exception(message, innerException);
#pragma warning restore CA2201 // Ne pas lever de types d'exception réservés

        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CheckException ThrowNullArgumentException(string? message) => throw new ArgumentNullException(message);

        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CheckException ThrowNullArgumentException(string? message, string? nameofArgument)
            => throw new CheckException(new ArgumentNullException(nameofArgument, message));

        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CheckException ThrowNullArgumentException(string? message, Exception innerException) => throw new CheckException(new ArgumentNullException(message, innerException));

        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CheckException ThrowArgumentException(string? message) => throw new CheckException(new ArgumentException(message));

        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CheckException ThrowArgumentException(string? message, string? nameofArgument) => throw new CheckException(new ArgumentException(nameofArgument, message));

        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CheckException ThrowArgumentException(string? message, Exception innerException) => throw new CheckException(new ArgumentNullException(message, innerException));

        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CheckException ThrowInvalidOperationException(string? message) => throw new CheckException(new InvalidOperationException(message));

        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception ThrowInvalidCastException(string? message) => throw new InvalidCastException(message);

        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#pragma warning disable CA2201 // Ne pas lever de types d'exception réservés
        public static CheckException ThrowNullReferenceException(string? message) => throw new CheckException(new NullReferenceException(message));
#pragma warning restore CA2201 // Ne pas lever de types d'exception réservés

        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception ThrowInternalException(string? message) => throw new InternalCheckException(message!);

        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CheckException ThrowCheckException(CheckLevel level, CheckType type, string? message) => throw new CheckException(level, type, message!);

        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CheckException ThrowCheckException(CheckType type, string? message) => throw new CheckException(type, message!);

        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CheckException ThrowCheckException(string? message) => throw new CheckException(message!);
    }
}
