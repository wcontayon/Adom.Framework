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
            Debug.Assert(list != null);

            for (int i = 0; i < items.Length; i++) list.Add(items[i]);
        }
    }
}
