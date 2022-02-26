using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adom.Framework.MoneyType
{
    public struct Money : IMoneyComparer
    {
        internal Currency _currency;
        internal string _otherCurrency;
        internal double _amount;

        #region Exception Message

        #endregion


        #region Constructor

        public Money(decimal value, string otherCurrency) : this((double)value, otherCurrency) { }

        public Money(decimal value, Currency currency) : this((double)value, currency) { }

        public Money(decimal value) : this((double)value) { }

        public Money(long value, string otherCurrency) : this((double)value, otherCurrency) { }

        public Money(long value, Currency currency) : this((double)value, currency) { }

        public Money(long value) : this((double)value) { }

        public Money(int value, Currency currency) : this((double)value, currency) { }

        public Money(int value, string otherCurrency) : this((double)value, otherCurrency) { }

        public Money(int value) : this((double)value) { }

        public Money(double value, Currency currency)
        {
            _currency = currency;
            _OtherCurrency = string.Empty;
            _amount = value;
        }

        public Money(Currency currency, string otherCurrency)
        {
            _currency = currency;
            _OtherCurrency = otherCurrency;
            _amount = 0d;
        }

        public Money(double value, string otherCurrency)
        {
            _currency = Currency.None;
            _OtherCurrency = otherCurrency;
            _amount = value;
        }

        public Money(string otherCurrency)
        {
            _currency = Currency.None;
            _OtherCurrency = otherCurrency;
            _amount = 0d;
        }

        public Money(Currency currency)
        {
            _currency = currency;
            _OtherCurrency = string.Empty;
            _amount = 0d;
        }

        public Money(double value)
        {
            _currency = Currency.None;
            _OtherCurrency = string.Empty;
            _amount = value;
        }

        #endregion


        #region IMoneyComparer

        public int CompareTo(Money other)
        {
            if (this._currency == other._currency && this._OtherCurrency == other._OtherCurrency)
            {
                if (this > other) return 1;
                else if (this < other) return -1;
                else return 0;
            }
            throw new Exception("Impossible de comparer de objet de type Money, avec des codes monétaires différents");
        }

        public bool Equals(Money other)
        {
            return (this == other);
        }

        public int CompareTo(object obj)
        {
            if (obj is Money)
            {
                return CompareTo((Money)obj);
            }
            throw new Exception("L'objet doit être du type Money");
        }

        #endregion


        #region Opérations

        #region Operator +

        public static Money operator +(Money m1, Money m2)
        {
            if ((m1._currency == m2._currency) || (m1._OtherCurrency == m2._OtherCurrency))
            {
                Money m_somm = new Money();
                if (m1._OtherCurrency == string.Empty && m1._currency != Currency.NONE)
                {
                    m_somm = new Money(m1._amount + m2._amount, m1._currency);
                }
                else if (m1._OtherCurrency != string.Empty && m1._currency == Currency.NONE)
                {
                    m_somm = new Money(m1._amount + m2._amount, m1._OtherCurrency);
                }
                else throw new Exception("Un objet de type Money ne peut avoir deux codes monétaires (currency)");
                return m_somm;
            }
            else throw new Exception("Impossible de définir un objet de type Money, sans un definir un code monétaire (currency)");
        }

        public static Money operator +(Money m1, int value)
        {
            Money m_somm = new Money(m1._amount + (double)value);
            m_somm._currency = m1._currency;
            m_somm._OtherCurrency = m1._OtherCurrency;
            return m_somm;
        }

        public static Money operator +(Money m1, long value)
        {
            Money m_somm = new Money(m1._amount + (double)value);
            m_somm._currency = m1._currency;
            m_somm._OtherCurrency = m1._OtherCurrency;
            return m_somm;
        }

        public static Money operator +(Money m1, decimal value)
        {
            Money m_somm = new Money(m1._amount + (double)value);
            m_somm._currency = m1._currency;
            m_somm._OtherCurrency = m1._OtherCurrency;
            return m_somm;
        }

        #endregion

        #region Operator -

        public static Money operator -(Money m1, Money m2)
        {
            if ((m1._currency == m2._currency) || (m1._OtherCurrency == m2._OtherCurrency))
            {
                Money m_somm = new Money();
                if (m1._OtherCurrency == string.Empty && m1._currency != Currency.NONE)
                {
                    m_somm = new Money(m1._amount - m2._amount, m1._currency);
                }
                else if (m1._OtherCurrency != string.Empty && m1._currency == Currency.NONE)
                {
                    m_somm = new Money(m1._amount - m2._amount, m1._OtherCurrency);
                }
                else throw new Exception("Un objet de type Money ne peut avoir deux codes monétaires (currency)");
                return m_somm;
            }
            else throw new Exception("Impossible de définir un objet de type Money, sans un definir un code monétaire (currency)");
        }

        public static Money operator -(Money m1, int value)
        {
            Money m_somm = new Money(m1._amount - (double)value);
            m_somm._currency = m1._currency;
            m_somm._OtherCurrency = m1._OtherCurrency;
            return m_somm;
        }

        public static Money operator -(Money m1, long value)
        {
            Money m_somm = new Money(m1._amount - (double)value);
            m_somm._currency = m1._currency;
            m_somm._OtherCurrency = m1._OtherCurrency;
            return m_somm;
        }

        public static Money operator -(Money m1, decimal value)
        {
            Money m_somm = new Money(m1._amount - (double)value);
            m_somm._currency = m1._currency;
            m_somm._OtherCurrency = m1._OtherCurrency;
            return m_somm;
        }

        #endregion

        #region Operator *

        public static Money operator *(Money m1, int value)
        {
            Money m_somm = new Money(m1._amount * (double)value);
            m_somm._currency = m1._currency;
            m_somm._OtherCurrency = m1._OtherCurrency;
            return m_somm;
        }

        public static Money operator *(Money m1, long value)
        {
            Money m_somm = new Money(m1._amount * (double)value);
            m_somm._currency = m1._currency;
            m_somm._OtherCurrency = m1._OtherCurrency;
            return m_somm;
        }

        public static Money operator *(Money m1, decimal value)
        {
            Money m_somm = new Money(m1._amount * (double)value);
            m_somm._currency = m1._currency;
            m_somm._OtherCurrency = m1._OtherCurrency;
            return m_somm;
        }

        #endregion

        #region Operator /

        public static Money operator /(Money m1, int value)
        {
            if ((double)value != 0)
            {
                Money m_somm = new Money(m1._amount / (double)value);
                m_somm._currency = m1._currency;
                m_somm._OtherCurrency = m1._OtherCurrency;
                return m_somm;
            }
            else throw new Exception("Division impossible par zero");
        }

        public static Money operator /(Money m1, long value)
        {
            if ((double)value != 0)
            {
                Money m_somm = new Money(m1._amount / (double)value);
                m_somm._currency = m1._currency;
                m_somm._OtherCurrency = m1._OtherCurrency;
                return m_somm;
            }
            else throw new Exception("Division impossible par zero");
        }

        public static Money operator /(Money m1, decimal value)
        {
            if ((double)value != 0)
            {
                Money m_somm = new Money(m1._amount / (double)value);
                m_somm._currency = m1._currency;
                m_somm._OtherCurrency = m1._OtherCurrency;
                return m_somm;
            }
            else throw new Exception("Division impossible par zero");
        }

        #endregion

        public static bool operator ==(Money m1, Money m2)
        {
            return ((m1._currency == m2._currency) && (m1._OtherCurrency == m2._OtherCurrency) &&
                (m1._amount == m2._amount));
        }

        public static bool operator !=(Money m1, Money m2)
        {
            return ((m1._currency != m2._currency) || (m1._OtherCurrency != m2._OtherCurrency) ||
                (m1._amount != m2._amount));
        }

        public static bool operator >(Money m1, Money m2)
        {
            if ((m1._currency == m2._currency) || (m1._OtherCurrency == m2._OtherCurrency))
                return (m1._amount > m2._amount);
            else return false;
        }

        public static bool operator <(Money m1, Money m2)
        {
            if ((m1._currency == m2._currency) || (m1._OtherCurrency == m2._OtherCurrency))
                return (m1._amount < m2._amount);
            else return false;
        }


        #endregion


        //#region Extensions

        //public static double ToDouble(this Convert c, Money m)
        //{
        //    return m._amount;
        //}

        //public static decimal ToDecimal(this Convert c, Money m)
        //{
        //    return (decimal)m._amount;
        //}

        //public static long ToLong(this Convert c, Money m)
        //{
        //    return (long)m._amount;
        //}

        //public static int ToInt32(this Convert c, Money m)
        //{
        //    return (int)m._amount;
        //}

        //public static int Parse(this int i, Money m)
        //{
        //    return (int)m._amount;
        //}

        //public static long Parse(this long l, Money m)
        //{
        //    return (long)m._amount;
        //}

        //public static decimal Parse(this decimal d, Money m)
        //{
        //    return (decimal)m._amount;
        //}

        //#endregion


        #region Fonctions

        public new string ToString()
        {
            if (_currency != Currency.NONE) return _amount.ToString() + " " + _String_Code_CurrencyMoney.CurrencyMoneyCode[_currency];
            else if (_OtherCurrency != string.Empty)
                return _amount.ToString() + " " + _OtherCurrency;
            else throw new Exception("Veuillez definir un code monétaire pour l'affichage");
        }

        public new string ToString(MoneyThousandSeparator separator)
        {
            //Avant toutes chose, on récupère le code monétaire
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

        #endregion
    }

    public enum MoneyThousandSeparator
    {
        None,
        Coma    = 0x1,
        Space   = 0x2,
        Dot     = 0x3
    }
}
