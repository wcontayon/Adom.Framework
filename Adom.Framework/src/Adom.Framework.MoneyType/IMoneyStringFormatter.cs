using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Adom.Framework.MoneyType
{
    /// <summary>
    /// Interface used to format a <see cref="Money"/> value
    /// </summary>
    public interface IMoneyStringFormatter
    {
        string Format(Money money, MoneyThousandSeparator separator);

        char CreateThousandSeparatorChar(MoneyThousandSeparator separator);
    }

    internal class DefaultMoneyStringFormatter : IMoneyStringFormatter
    {
        public char CreateThousandSeparatorChar(MoneyThousandSeparator separator) => separator switch
            {
                MoneyThousandSeparator.Coma => ',',
                MoneyThousandSeparator.Space => ' ',
                MoneyThousandSeparator.None => '_',
                MoneyThousandSeparator.Dot => '.',
                _ => ' '
            };

        public string Format(Money money, MoneyThousandSeparator separator)
        {
            // Get the currency
            string currencySymbol = $" {money._currency.Symbol()}";
            ReadOnlySpan<char> amountSpan = money._amount.ToString(CultureInfo.InvariantCulture).Reverse();

            using (ValueStringBuilder vsbAmount = new ValueStringBuilder())
            {
                char separatorChar = CreateThousandSeparatorChar(separator);

                if (amountSpan.Length > 3)
                {
                    ReadOnlySpan<char> thousand = Span<char>.Empty;
                    int lastIndex = 0;
                    for (int i = 0; i < amountSpan.Length; i += 3)
                    {
                        if (amountSpan.Length > 3)
                        {
                            thousand = amountSpan.Slice(i, 3);
                            for (int j = 0; j < 3; j++)
                            {
                                vsbAmount.Append(thousand[j]);
                            }
                            vsbAmount.Append(separatorChar);
                        }
                        else
                        {
                            amountSpan = amountSpan.Slice(i);
                            lastIndex = i;
                            break;
                        }
                    }

                    if (!amountSpan.IsEmpty)
                    {
                        for (int i = 0; i < amountSpan.Length; i++)
                        {
                            vsbAmount.Append(amountSpan[i]);
                        }
                    }

                    // Before return the value, check that the first char is not the 
                    // separator. If then delete it
                    string s = vsbAmount.ToString();
                    if (s[0] == separatorChar)
                    {
                        s = s.AsSpan().Slice(1, s.Length - 1).ToString();
                    }

                    return $"{s}{currencySymbol}";
                }
            }
                
            return $"{amountSpan.ToString()}{currencySymbol}";
        }
    }
}
