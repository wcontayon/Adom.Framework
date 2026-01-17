using System;
using System.Reflection;

namespace Adom.Framework.MoneyType
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
    public sealed class CurrencyLabelAttribute : Attribute
    {
        public CurrencyLabelAttribute(string label) => this.Label = label;

        public string Label { get; private set; }
    }

    internal static class CurrencyLabelAttributeExtension
    {
        /// <summary>
        /// Return the label defined on the currency
        /// </summary>
        /// <param name="currency"><see cref="Currency"/></param>
        /// <returns></returns>
        internal static string GetLabel(this Currency currency)
        {
            // Get fieldinfo for this type
            FieldInfo fieldInfo = typeof(Currency).GetField(currency.ToString())!;

            if (fieldInfo != null)
            {
                // Get the stringvalue attributes
                CurrencyLabelAttribute? symbolAttribute = fieldInfo.GetCustomAttribute<CurrencyLabelAttribute>();

                if (symbolAttribute != null)
                {
                    return symbolAttribute.Label;
                }
            }

            return $"{currency}";
        }
    }
}
