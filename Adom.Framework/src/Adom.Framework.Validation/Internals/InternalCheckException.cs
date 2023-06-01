using System;
using System.Diagnostics;

namespace Adom.Framework.Validation
{
    /// <summary>
    /// Internal exception used by <see cref="Argument"/>, <see cref="Operation"/> and <see cref="Data"/> checker.
    /// </summary>
    [Serializable]
#pragma warning disable CA1064 // Les exceptions doivent être publiques
    internal sealed class InternalCheckException : Exception
#pragma warning restore CA1064 // Les exceptions doivent être publiques
    {
        [DebuggerStepThrough]
        public InternalCheckException()
        {
        }

        [DebuggerStepThrough]
        public InternalCheckException(string message)
            : base(message)
        {
        }

        [DebuggerStepThrough]
        public InternalCheckException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
