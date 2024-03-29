﻿using System;

namespace Adom.Framework.Validation.Model.Attribute
{
    public sealed class GreatherThanAttribute : BaseRuleAttribute
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
                isValid = CheckerExtension.Compare(item!, this.Value!, Operator.GreatherThan);
            }

            return isValid;
        }
    }
}
