using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Adom.Framework.Collections
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Return a string that represent the key:value from the dictionary
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary">The dictionary source</param>
        /// <param name="keyAndValueSeparator">Separator used in (key/value) pair</param>
        /// <param name="dataSeparator">Separator used to distinct (key/value) pair</param>
        /// <returns></returns>
        public static string ToString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, char keyAndValueSeparator, char dataSeparator)
        {
            ArgumentException.ThrowIfNullOrEmpty(keyAndValueSeparator.ToString(), nameof(keyAndValueSeparator));
            ArgumentException.ThrowIfNullOrEmpty(dataSeparator.ToString(), nameof(dataSeparator));
            ArgumentNullException.ThrowIfNull(dictionary);

            if (dictionary.Count > 0)
            {
                ValueStringBuilder vsb = new ValueStringBuilder();
                foreach (var kvp in dictionary)
                {
                    vsb.Append($"{kvp.Key!.ToString()}{keyAndValueSeparator}{kvp.Value!.ToString()}{dataSeparator}");
                }

                var str = vsb.ToString();
                vsb.Dispose();

                return str;
            }

            return string.Empty;
        }

        /// <summary>
        /// Return a string array that represent the key:value from the dictionary
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary">The dictionary source</param>
        /// <param name="keyAndValueSeparator">Separator used in (key/value) pair</param>
        /// <returns></returns>
        public static string[] ToStringArray<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, char keyAndValueSeparator)
        {
            ArgumentNullException.ThrowIfNull(dictionary);
            ArgumentException.ThrowIfNullOrEmpty(keyAndValueSeparator.ToString(), nameof(keyAndValueSeparator));

            if (dictionary.Count > 0)
            {
                List<string> values = new List<string>();
                foreach (var kvp in dictionary)
                {
                    values.Add($"{kvp.Key!.ToString()}{keyAndValueSeparator}{kvp.Value!.ToString()}");
                }

                return values.ToArray();
            }

            return Array.Empty<string>();
        }

        /// <summary>
        /// Return a string array that represent the key:value from the dictionary
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary">The dictionary source</param>
        /// <param name="keyAndValueSeparator">Separator used in (key/value) pair</param>
        /// <returns></returns>
        public static Span<string> ToStringArrayAsSpan<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, char keyAndValueSeparator)
        {
            ArgumentNullException.ThrowIfNull(dictionary);
            ArgumentNullException.ThrowIfNullOrEmpty(keyAndValueSeparator.ToString());

            if (dictionary.Count > 0)
            {
                List<string> values = new List<string>();
                foreach (var kvp in dictionary)
                {
                    values.Add($"{kvp.Key!.ToString()}{keyAndValueSeparator}{kvp.Value!.ToString()}");
                }

                return values.ToArray().AsSpan();
            }

            return Span<string>.Empty;
        }

        /// <summary>
        /// Add items from another dictionary
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary">The current dictionary</param>
        /// <param name="items">Items to add</param>
        public static void Add<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IDictionary<TKey, TValue> items)
        {
            ArgumentNullException.ThrowIfNull(dictionary);

            if (items != null)
            {
                foreach (var item in items)
                {
                    dictionary[item.Key] = item.Value;
                }
            }
        }
    }
}
