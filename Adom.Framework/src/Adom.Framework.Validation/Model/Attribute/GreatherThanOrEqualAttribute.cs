﻿using System;

namespace Adom.Framework.Validation.Model.Attribute
{
    /// <summary>
    /// GreatherThanOrEqual attribute, to specify that the property should have a value
    /// greather than or equals to a specific value.
    /// </summary>
    /// <remarks>Property should be in type short, int,long, decimal, float, ushort, uint, ulong/.</remarks>
    public sealed class GreatherThanOrEqualAttribute : BaseRuleAttribute
    {
        /// <summary>
        /// Gets or Sets the type of value to compare.
        /// </summary>
        public Type? Type { get; set; }

        /// <summary>
        /// Gets or Sets the value to be compared.
        /// </summary>
        public object? Value { get; set; }

        /// <inheritdoc />
        public override bool IsValid(object? item)
        {
            bool isValid = Data.Assume.That(!(item is null), false, "SHould not be null");

            Data.Require.NotNull(this.Value, nameof(item));

            if (isValid)
            {
                isValid = CheckerExtension.Compare(item!, this.Value!, Operator.GreatherOrEqualThan);
            }

            return isValid;
        }
    }
}
