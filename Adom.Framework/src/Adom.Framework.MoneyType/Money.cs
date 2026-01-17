using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace Adom.Framework.MoneyType
{
    public struct Money : IMoneyComparer
    {
        internal Currency _currency;
        internal string _otherCurrency;
        internal double _amount;
        internal DefaultMoneyStringFormatter _moneyStringFormatter;

        #region Exception Message

        internal const string EXCEPTION_MSG_CURRENCY_MISMATCH = "Cannot compare. Currency mismatch !";
        internal const string EXCEPTION_MSG_TYPE_MISMATCH = "The object is not a type of {0}";
        internal const string EXCEPTION_MSG_CANTHAVE_TWO_CURRENCY = "A Money struct value cannot have two currency codes";
        internal const string EXCEPTION_MSG_MONEY_MUST_HAVE_CURRENCY = "A money struct value required a currency code";
        internal const string EXCEPTION_MSG_MONEY_DIVIDED_BY_ZERO = "Cannot divide Money by zero";

        private static readonly CompositeFormat TypeMismatchFormat = CompositeFormat.Parse(EXCEPTION_MSG_TYPE_MISMATCH);

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
            _otherCurrency = string.Empty;
            _amount = value;
            _moneyStringFormatter = new DefaultMoneyStringFormatter();
        }

        public Money(Currency currency, string otherCurrency)
        {
            _currency = currency;
            _otherCurrency = otherCurrency;
            _amount = 0d;
            _moneyStringFormatter = new DefaultMoneyStringFormatter();
        }

        public Money(double value, string otherCurrency)
        {
            _currency = Currency.None;
            _otherCurrency = otherCurrency;
            _amount = value;
            _moneyStringFormatter = new DefaultMoneyStringFormatter();
        }

        public Money(string otherCurrency)
        {
            _currency = Currency.None;
            _otherCurrency = otherCurrency;
            _amount = 0d;
            _moneyStringFormatter = new DefaultMoneyStringFormatter();
        }

        public Money(Currency currency)
        {
            _currency = currency;
            _otherCurrency = string.Empty;
            _amount = 0d;
            _moneyStringFormatter = new DefaultMoneyStringFormatter();
        }

        public Money(double value)
        {
            _currency = Currency.None;
            _otherCurrency = string.Empty;
            _amount = value;
            _moneyStringFormatter = new DefaultMoneyStringFormatter();
        }

        #endregion


        #region IMoneyComparer

        public int CompareTo(Money other)
        {
            if (this._currency != other._currency)
            {
                ThrowHelper.ThrowInvalidOperationException(EXCEPTION_MSG_CURRENCY_MISMATCH);
            }

            if (this > other) return 1;
            else if (this < other) return -1;
            else return 0;
        }

        public bool Equals(Money other)
        {
            return (this == other);
        }

        public int CompareTo(object? obj)
        {
            if (obj is Money && obj != null)
            {
                return CompareTo((Money)obj);
            }

            ThrowHelper.ThrowInvalidOperationException(string.Format(CultureInfo.InvariantCulture, TypeMismatchFormat, "Money"));
            return -1;
        }

        #endregion


        #region Operations

        #region Operator +

        public static Money operator +(Money m1, Money m2)
        {
            IsCurrencyMatch(m1, m2);
            EnsureMoneyHasOneCurrency(m1);
            EnsureMoneyHasOneCurrency(m2);

            if (m1._currency != Currency.None)
            {
                return new Money(m1._amount + m2._amount, m1._currency);
            }
            else 
            {
                return new Money(m1._amount + m2._amount, m1._otherCurrency);
            }
        }

        public static Money operator +(Money m1, int value) => Add(m1, (double) value);

        public static Money operator +(Money m1, long value) => Add(m1, (double) value);

        public static Money operator +(Money m1, decimal value) => Add(m1, (double) value);

        public static Money Add(Money m1, double value) => Operate(m1, (double)value, MoneyOperator.Add);

        #endregion

        #region Operator -

        public static Money operator -(Money m1, Money m2)
        {
            IsCurrencyMatch(m1, m2);
            EnsureMoneyHasOneCurrency(m1);
            EnsureMoneyHasOneCurrency(m2);

            if (m1._currency != Currency.None)
            {
                return new Money(m1._amount - m2._amount, m1._currency);
            }
            else
            {
                return new Money(m1._amount - m2._amount, m1._otherCurrency);
            }
        }

        public static Money operator -(Money m1, int value) => Subtract(m1, (double)value);

        public static Money operator -(Money m1, long value) => Subtract(m1, (double)value);

        public static Money operator -(Money m1, decimal value) => Subtract(m1, (double)value);

        public static Money Subtract(Money m1, double value) => Operate(m1, value, MoneyOperator.Subtract);

        #endregion

        #region Operator *

        public static Money operator *(Money m1, int value) => Multiply(m1, (double)value);

        public static Money operator *(Money m1, long value) => Multiply(m1, (double)value);

        public static Money operator *(Money m1, decimal value) => Multiply(m1, (double)value);

        public static Money Multiply(Money m1, double value) => Operate(m1, value, MoneyOperator.Multiply);

        #endregion

        #region Operator /

        public static Money operator /(Money m1, int value) => Divide(m1, (double)value);

        public static Money operator /(Money m1, long value) => Divide(m1, (double)value);

        public static Money operator /(Money m1, decimal value) => Divide(m1, (double)value);

        public static Money Divide(Money m1, double value)
        {
            if (value == 0)
            {
                ThrowHelper.ThrowDivideByZeroException(EXCEPTION_MSG_MONEY_DIVIDED_BY_ZERO);
            }

            return Operate(m1, value, MoneyOperator.Divide);
        }

        #endregion

        public static bool operator ==(Money m1, Money m2)
        {
            return ((m1._currency == m2._currency) && (m1._otherCurrency == m2._otherCurrency) &&
                (m1._amount == m2._amount));
        }

        public static bool operator !=(Money m1, Money m2)
        {
            return ((m1._currency != m2._currency) || (m1._otherCurrency != m2._otherCurrency) ||
                (m1._amount != m2._amount));
        }

        public static bool operator >(Money m1, Money m2)
        {
            if ((m1._currency == m2._currency) || (m1._otherCurrency == m2._otherCurrency))
                return (m1._amount > m2._amount);
            else return false;
        }

        public static bool operator <(Money m1, Money m2)
        {
            if ((m1._currency == m2._currency) || (m1._otherCurrency == m2._otherCurrency))
                return (m1._amount < m2._amount);
            else return false;
        }

        public static bool operator <=(Money left, Money right) => left.CompareTo(right) <= 0;

        public static bool operator >=(Money left, Money right) => left.CompareTo(right) >= 0;

        #endregion

        public new string ToString() => FormatMoneyToString(this, _moneyStringFormatter, MoneyThousandSeparator.Space);

        public string ToString(MoneyThousandSeparator separator) => FormatMoneyToString(this, _moneyStringFormatter, separator);

        public string ToString([NotNull] IMoneyStringFormatter formatter) => FormatMoneyToString(this, formatter!, MoneyThousandSeparator.Space);

        public string ToString([NotNull] IMoneyStringFormatter formatter, MoneyThousandSeparator separator) => FormatMoneyToString(this, formatter!, separator);


        #region Convert

        public static explicit operator double(Money m1) => m1._amount;
        public static double ToDouble(Money m1) => m1._amount;

        public static explicit operator int(Money m1) => (int)m1._amount;
        public static int ToInt32(Money m1) => (int)m1._amount;

        public static explicit operator long(Money m1) => (long)m1._amount;
        public static long ToInt64(Money m1) => (long)m1._amount;

        public static explicit operator decimal(Money m1) => (decimal)m1._amount;
        public static decimal ToDecimal(Money m1) => (decimal)m1._amount;

        #endregion

        #region Internals

        [DoesNotReturn]
        private static void IsCurrencyMatch(Money m1, Money m2)
        {
            if ((m1._currency != m2._currency) && (m1._otherCurrency != m2._otherCurrency))
            {
                ThrowHelper.ThrowInvalidOperationException(EXCEPTION_MSG_CURRENCY_MISMATCH);
            }
#pragma warning disable CS8763 // Une méthode marquée [DoesNotReturn] ne doit pas être retournée.
        }
#pragma warning restore CS8763 // Une méthode marquée [DoesNotReturn] ne doit pas être retournée.

        [DoesNotReturn]
        private static void EnsureMoneyHasOneCurrency(Money m1)
        {
            if (m1._currency != Currency.None && !string.IsNullOrEmpty(m1._otherCurrency))
            {
                ThrowHelper.ThrowInvalidOperationException(EXCEPTION_MSG_CANTHAVE_TWO_CURRENCY);
            }
#pragma warning disable CS8763 // Une méthode marquée [DoesNotReturn] ne doit pas être retournée.
        }
#pragma warning restore CS8763 // Une méthode marquée [DoesNotReturn] ne doit pas être retournée.

        private static Money Operate(Money left, double value, MoneyOperator @operator)
        {
            Money sum = @operator switch
            {
                MoneyOperator.Add => new Money(left._amount + value),
                MoneyOperator.Subtract => new Money(left._amount - value),
                MoneyOperator.Multiply => new Money(left._amount * value),
                MoneyOperator.Divide => new Money(left._amount / value),
                _ => left
            };

            sum._currency = left._currency;
            sum._otherCurrency = left._otherCurrency;
            return sum;
        }

        private static string FormatMoneyToString(Money m, IMoneyStringFormatter formatter, MoneyThousandSeparator separator)
        {
            return formatter.Format(m, separator);
        }

        #endregion

        public override bool Equals(object? obj) => this.CompareTo(obj) <= 0;

        public override int GetHashCode() => this._amount.GetHashCode() + this._currency.GetHashCode()
            + this._otherCurrency.GetHashCode(StringComparison.InvariantCulture);
    }

    enum MoneyOperator
    {
        None,
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public enum MoneyThousandSeparator
    {
        None,
        Coma    = 0x1,
        Space   = 0x2,
        Dot     = 0x3
    }
}
