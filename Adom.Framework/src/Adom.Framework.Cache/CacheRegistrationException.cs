using System;

namespace Adom.Framework.Cache
{
    public sealed class CacheRegistrationException : Exception
    {
        public CacheRegistrationException(string message): base(message)
        {

        }

        public CacheRegistrationException()
        {
        }

        public CacheRegistrationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
