using System;

namespace Adom.Framework.Validation
{
    public class CheckException : Exception
    {
        public CheckException(string message) : base(message)
        {
        }

        public CheckException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CheckException()
        {
        }

        internal CheckException(CheckLevel level, CheckType @type, string message)
            : base(message)
        {
            Data.Add("Level", level.ToString());
            Data.Add("Type", type.ToString());
        }

        internal CheckException(CheckType @type, string message)
           : base(message)
        {
            Data.Add("Type", type.ToString());
        }

        internal CheckException(InvalidOperationException innerException) : base(innerException.Message)
        {
            Data.Add("Level", CheckLevel.Operation.ToString());
        }

        internal CheckException(ArgumentNullException innerException) : base(innerException.Message)
        {
            Data.Add("Level", CheckLevel.Argument.ToString());
        }

        internal CheckException(NullReferenceException innerException) : base(innerException.Message)
        {
            Data.Add("Level", CheckLevel.Data.ToString());
        }

        internal CheckException(ArgumentException innerException) : base(innerException.Message)
        {
            Data.Add("Level", CheckLevel.Argument.ToString());
        }
    }
}
