using System;
using System.Reflection;

namespace Adom.Framework.MoneyType
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
    internal sealed class CurrencySymbolAttribute : Attribute
    {
        public CurrencySymbolAttribute(string symbol) => this.Symbol = symbol;

        public string Symbol { get; private set; }
    }

    internal static class CurrencySymboleAttributeExtension
    {
        /// <summary>
        /// Return the symbol defined on the currency
        /// </summary>
        /// <param name="currency"><see cref="Currency"/></param>
        /// <returns></returns>
        internal static string Symbol(this Currency currency)
        {
            // Get fieldinfo for this type
            FieldInfo fieldInfo = typeof(Currency).GetField(currency.ToString())!;

            if (fieldInfo != null)
            {
                // Get the stringvalue attributes
                CurrencySymbolAttribute? symboleAttribute = fieldInfo.GetCustomAttribute<CurrencySymbolAttribute>();

                if (symboleAttribute != null && currency != Currency.None)
                {
                    return symboleAttribute.Symbol;
                }
            }

            return $"{currency}";
        }
    }
}
