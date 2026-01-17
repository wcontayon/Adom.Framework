using System.Diagnostics.CodeAnalysis;
using Adom.CQRS.Exceptions;

namespace Adom.CQRS.Internal;

/// <summary>
/// Centralized exception throwing helper to keep hot paths small for JIT inlining.
/// </summary>
internal static class ThrowHelper
{
    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the specified argument is null.
    /// </summary>
    [DoesNotReturn]
    public static void ThrowArgumentNullException(string paramName)
    {
        throw new ArgumentNullException(paramName);
    }

    /// <summary>
    /// Throws a <see cref="HandlerNotFoundException"/> for the specified request type.
    /// </summary>
    [DoesNotReturn]
    public static void ThrowHandlerNotFoundException(Type requestType)
    {
        throw new HandlerNotFoundException(requestType);
    }

    /// <summary>
    /// Throws a <see cref="DispatcherException"/> for the specified request type and inner exception.
    /// </summary>
    [DoesNotReturn]
    public static void ThrowDispatcherException(Type requestType, Exception innerException)
    {
        throw new DispatcherException(requestType, innerException);
    }
}
