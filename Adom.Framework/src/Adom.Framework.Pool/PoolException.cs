
namespace Adom.Framework.Pools;

public class PoolException : Exception
{
    public PoolException()
    {
    }

    public PoolException(string message) : base(message)
    {
    }

    public PoolException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public const string MSGPOOLSIZEREACHED = "Maximum size of pool reached";
}

