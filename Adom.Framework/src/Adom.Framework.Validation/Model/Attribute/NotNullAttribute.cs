﻿namespace Adom.Framework.Validation.Model.Attribute
{
    /// <summary>
    /// Attribute used to validate that the property should not be null.
    /// </summary>
    public sealed class NotNullAttribute : BaseRuleAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotNullAttribute"/> class.
        /// </summary>
        /// <param name="message">Message of the attribute.</param>
        public NotNullAttribute(string message) => this.Message = message;

        /// <inheritdoc/>
        public override bool IsValid(object? item) => Data.Require.That(!(item is null), false, "Should be null");
    }
}
