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
            string currencySymbol = money._currency.Symbol();
            ReadOnlySpan<char> amountSpan = money._amount.ToString(CultureInfo.InvariantCulture).Reverse();

            ValueStringBuilder vsbAmount = new ValueStringBuilder();
            char separatorChar = CreateThousandSeparatorChar(separator);

            if (amountSpan.Length > 3)
            {
                ReadOnlySpan<char> thousand = Span<char>.Empty;
                int lastIndex = 0;
                for (int i = 0; i < amountSpan.Length; i+= 3)
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
                    for (int i = 0; i< amountSpan.Length; i++)
                    {
                        vsbAmount.Append(amountSpan[i]);
                    }
                }

                // Before return the value, check that the first char is not the 
                // separator. If then delete it
                string s = vsbAmount.ToString();
                if (s[0] == separatorChar)
                {
                    return s.AsSpan().Slice(1, s.Length - 1);
                }

                return s;
            }
            string codeMonnaie = ((_currency != Currency.NONE) ? _String_Code_CurrencyMoney.CurrencyMoneyCode[_currency] :
                ((_OtherCurrency != string.Empty) ? _OtherCurrency : string.Empty));

            //On procède maintenant à la conversion
            string montant = _amount.ToString().Reverse();
            string new_montant = string.Empty;
            char c_separator = Money.CaractereSeparator(separator);
            string str_separator = (c_separator == '0') ? "" : c_separator.ToString();
            char[] __chiffres = montant.ToCharArray();
            int nombre_chiffres = montant.Length;
            if (nombre_chiffres > 3)
            {
                int nombre_restant = 0;
                int dernier_index = 0;
                for (int i = 0; i < nombre_chiffres; i += 3)
                {
                    try
                    {
                        string milier = montant.Substring(i, 3);
                        foreach (var c in milier)
                        {
                            new_montant = c.ToString() + new_montant;
                        }
                        new_montant = str_separator + new_montant;
                    }
                    catch
                    {
                        nombre_restant = montant.Length - i;
                        dernier_index = i;
                        break;
                    }
                }
                if (0 != nombre_restant)
                {
                    foreach (char c in montant.Substring(dernier_index, nombre_restant))
                    {
                        new_montant = c.ToString() + new_montant;
                    }
                }

                //Avant de retourner la valeur, on vérifie le 1er char, si c'est le separator
                //on le supprime
                var _1erChar = new_montant[0];
                if (_1erChar.ToString() == c_separator.ToString())
                {
                    new_montant = new_montant.Substring(1, new_montant.Length - 1);
                }
            }
            else return _amount.ToString() + " " + codeMonnaie;
            return new_montant.ToString() + " " + codeMonnaie;
        }
    }
}
