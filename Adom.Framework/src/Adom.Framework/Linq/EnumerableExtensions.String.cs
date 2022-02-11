using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adom.Framework.Linq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Check if <see cref="IEnumerable{string}"/> contains any empty string
        /// </summary>
        /// <param name="source"><see cref="IEnumerable{string}" /></param>
        /// <returns>True or false</returns>
        public static bool AnyEmpty(this IEnumerable<string>? source)
        {
            if (source == null){
                ThrowHelper.ThrowArgumentNullException(nameof(source));
            }

            return CountPredicate(source, str => string.IsNullOrWhiteSpace(str)) > 0;
        }

        /// <summary>
        /// Check if <see cref="IEnumerable{string}"/> contains any non empty string
        /// </summary>
        /// <param name="source"><see cref="IEnumerable{string}" /></param>
        /// <returns>True or false</returns>
        public static bool AnyNotEmpty(this IEnumerable<string>? source)
        {
            if (source == null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(source));
            }

            return CountPredicate(source, str => !string.IsNullOrWhiteSpace(str)) > 0;
        }

        /// <summary>
        /// Return the <see cref="IEnumerable{string}"/> with only non empty string.
        /// </summary>
        /// <param name="source"><see cref="IEnumerable{string}"/></param>
        /// <returns><see cref="IEnumerable{string}"/> with non empty string</returns>
        public static IEnumerable<string> RemoveEmpty(this IEnumerable<string?>? source)
        {
#if NET6_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(source, nameof(source));
#else
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
#endif

            foreach (var item in source)
            {
                if (!string.IsNullOrWhiteSpace(item))
                    yield return item;
            }
        }
    }
}
