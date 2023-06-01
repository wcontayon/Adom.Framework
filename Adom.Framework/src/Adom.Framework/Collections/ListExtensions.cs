using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Adom.Framework.Collections
{
    public static class ListExtensions
    {
        /// <summary>
        /// Add items into the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list</param>
        /// <param name="items">Items to be added</param>
        public static void Add<T>(this IList<T> list, params T[] items)
        {
            ArgumentNullException.ThrowIfNull(list, nameof(list));
            for (int i = 0; i < items.Length; i++) list.Add(items[i]);
        }

        /// <summary>
        /// Add items into the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list</param>
        /// <param name="items">Items to be added</param>
        public static void Add<T>(this IList<T> list, IEnumerable<T> items)
        {
            ArgumentNullException.ThrowIfNull(list);
            foreach (var item in items) list.Add(item);
        }

        /// <summary>
        /// Add items into the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list</param>
        /// <param name="items">Items to be added</param>
        public static void Add<T>(this IList<T> list, ReadOnlySpan<T> items)
        {
            ArgumentNullException.ThrowIfNull(list);
            for (int i = 0; i < items.Length; i++) list.Add(items[i]);
        }

        /// <summary>
        /// Add items into the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list</param>
        /// <param name="items">Items to be added</param>
        public static void Add<T>(this IList<T> list, Span<T> items)
        {
            ArgumentNullException.ThrowIfNull(list);
            for (int i = 0; i < items.Length; i++) list.Add(items[i]);
        }
    }
}
