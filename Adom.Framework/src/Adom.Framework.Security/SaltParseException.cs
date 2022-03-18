using System;

namespace Adom.Framework.Security
{
    /// <summary>Exception for signalling parse errors. </summary>
    public class SaltParseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaltParseException"/> class.Default constructor. </summary>
        public SaltParseException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaltParseException"/> class.Initializes a new instance of <see cref="SaltParseException"/>.</summary>
        /// <param name="message">The message.</param>
        public SaltParseException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaltParseException"/> class.Initializes a new instance of <see cref="SaltParseException"/>.</summary>
        /// <param name="message">       The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public SaltParseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
