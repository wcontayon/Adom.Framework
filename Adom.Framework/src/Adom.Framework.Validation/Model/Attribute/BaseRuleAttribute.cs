using System;

namespace Adom.Framework.Validation.Model.Attribute
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public abstract class BaseRuleAttribute : System.Attribute
    {
        /// <summary>
        /// Gets or Sets the message of the rule.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Validate the rule.
        /// </summary>
        /// <param name="item">Item to validate.</param>
        /// <returns>True or false.</returns>
        public abstract bool IsValid(object? item);
    }
}
