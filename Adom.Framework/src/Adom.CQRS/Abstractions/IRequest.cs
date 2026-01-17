namespace Adom.CQRS.Abstractions;

/// <summary>
/// Marker interface for requests that return a response.
/// </summary>
/// <typeparam name="TResponse">The type of response this request produces.</typeparam>
#pragma warning disable CA1040 // Empty interface is intentional for type constraints
public interface IRequest<out TResponse>
#pragma warning restore CA1040
{
}

/// <summary>
/// Marker interface for requests that return no value.
/// </summary>
#pragma warning disable CA1040 // Empty interface is intentional for type constraints
public interface IRequest : IRequest<Unit>
#pragma warning restore CA1040
{
}
