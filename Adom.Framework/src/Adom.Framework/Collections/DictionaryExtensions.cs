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
            Debug.Assert(!string.IsNullOrEmpty($"{keyAndValueSeparator}"));
            Debug.Assert(!string.IsNullOrEmpty($"{dataSeparator}"));
            Debug.Assert(dictionary != null);

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
            Debug.Assert(!string.IsNullOrEmpty($"{keyAndValueSeparator}"));
            Debug.Assert(dictionary != null);

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
            Debug.Assert(!string.IsNullOrEmpty($"{keyAndValueSeparator}"));
            Debug.Assert(dictionary != null);

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
    }
}
