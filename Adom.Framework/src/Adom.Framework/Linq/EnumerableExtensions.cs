using System;
using System.Collections.Generic;
using System.Linq;

namespace Adom.Framework.Linq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Replace a item in the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"><see cref="IList{T}"/></param>
        /// <param name="oldItem">Item to replace</param>
        /// <param name="newItem">Item to insert</param>
        public static void Replace<T>(this IList<T> list, T oldItem, T newItem)
        {
            ArgumentNullException.ThrowIfNull(list, nameof(list));

            var index = list.IndexOf(oldItem);
            if (index < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException();
            }

            list[index] = newItem;
        }

        /// <summary>
        /// Add the <paramref name="newItem"/> into the list, or
        /// replace the <paramref name="oldItem"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"><see cref="IList{T}"/></param>
        /// <param name="oldItem">Item to replace if not null</param>
        /// <param name="newItem">Item to insert</param>
        public static void AddOrReplace<T>(this IList<T> list, T? oldItem, T newItem)
        {
            ArgumentNullException.ThrowIfNull(list, nameof(list));

            if (oldItem == null)
            {
                list.Add(newItem);
            }
            else
            {
                var index = list.IndexOf(oldItem);
                if (index < 0)
                    ThrowHelper.ThrowArgumentOutOfRangeException();

                list[index] = newItem;
            }
        }

        /// <summary>
        /// Get a <see cref="IEnumerable{T}"/> from the <paramref name="source"/>
        /// starting at <paramref name="index"/> and with capacity of <paramref name="size"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"><see cref="IEnumerable{T}"/></param>
        /// <param name="index">Index to start from</param>
        /// <param name="size">Size of range to get</param>
        /// <returns></returns>
        public static IEnumerable<T> Range<T>(this IEnumerable<T>? source, int index, int size)
        {
            if (source == null)
                ThrowHelper.ThrowArgumentNullException(nameof(source));

            if (size < 0 || source.Count() < size)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException();
            }

            Span<T> range = source.ToArray().AsSpan(index, size);
            return Enumerable.OfType<T>(range.ToArray());
        }

        /// <summary>
        /// Filters a sequence to return not null items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Sequence to filter</param>
        /// <returns></returns>
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : class, new()
        {
            if (source == null)
                ThrowHelper.ThrowArgumentNullException(nameof(source));

            return source.Where(it => it != null)!;
        }

        /// <summary>
        /// Filters a sequence to return not null items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Sequence to filter</param>
        /// <returns></returns>
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : struct
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));

            foreach (var item in source)
            {
                if (item.HasValue)
                    yield return item.GetValueOrDefault();
            }
        }

        /// <summary>
        /// Return an empty <see cref="IEnumerable{T}"/> if it's null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"><see cref="IEnumerable{T}"/></param>
        /// <returns></returns>
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? source) where T : class
        {
            source ??= Enumerable.Empty<T>();
            return source;
        }

        /// <summary>
        /// Check if there is any null item in the <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"><see cref="IEnumerable{T}"/></param>
        /// <returns></returns>
        public static bool AnyNull<T>(this IEnumerable<T>? source) where T : class
        {
            if (source == null)
                ThrowHelper.ThrowArgumentNullException(nameof (source));

            return source.Any(it => it == null);
        }

        /// <summary>
        /// Check if there is any not null item in the <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"><see cref="IEnumerable{T}"/></param>
        /// <returns></returns>
        public static bool AnyNotNull<T>(this IEnumerable<T>? source) where T : class
        {
            if (source == null)
                ThrowHelper.ThrowArgumentNullException(nameof(source));

            return source.Any(it => it != null);
        }

        /// <summary>
        /// Count null item in the <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"><see cref="IEnumerable{T}"/></param>
        /// <returns></returns>
        public static int CountNull<T>(this IEnumerable<T>? source) where T : class
        {
            if (source == null)
                ThrowHelper.ThrowArgumentNullException(nameof(source));

            return CountPredicate(source, it => it == null);
        }

        /// <summary>
        /// Count non null item in the <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"><see cref="IEnumerable{T}"/></param>
        /// <returns></returns>
        public static int CountNotNull<T>(this IEnumerable<T>? source) where T : class
        {
            if (source == null)
                ThrowHelper.ThrowArgumentNullException(nameof(source));

            return CountPredicate(source, it => it != null);
        }

        /// <summary>
        /// Count number of item in the <see cref="IEnumerable{T}"/> based on the predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"><see cref="IEnumerable{T}"/></param>
        /// <param name="predicateCount">Predicate used to count</param>
        /// <returns></returns>
        private static int CountPredicate<T>(this IEnumerable<T> source, Func<T, bool> predicateCount)
        {
            int count = 0;

            for (int i = 0; i < source.Count(); i++)
            {
                if (predicateCount(source.ElementAt(i)))
                {
                    count++;
                }
            }

            return count;
        }
    }
}
