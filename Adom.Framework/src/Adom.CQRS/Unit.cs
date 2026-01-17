namespace Adom.CQRS;

/// <summary>
/// Represents a void return type in a type-safe way.
/// </summary>
public readonly struct Unit : IEquatable<Unit>
{
    /// <summary>
    /// The singleton Unit value.
    /// </summary>
    public static readonly Unit Value;

    /// <summary>
    /// Determines whether the specified <see cref="Unit"/> is equal to the current <see cref="Unit"/>.
    /// </summary>
    public bool Equals(Unit other) => true;

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="Unit"/>.
    /// </summary>
    public override bool Equals(object? obj) => obj is Unit;

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    public override int GetHashCode() => 0;

    /// <summary>
    /// Returns a string representation of the <see cref="Unit"/> value.
    /// </summary>
    public override string ToString() => "()";

    /// <summary>
    /// Determines whether two specified <see cref="Unit"/> values are equal.
    /// </summary>
    public static bool operator ==(Unit left, Unit right) => true;

    /// <summary>
    /// Determines whether two specified <see cref="Unit"/> values are not equal.
    /// </summary>
    public static bool operator !=(Unit left, Unit right) => false;
}
