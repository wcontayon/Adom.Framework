using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adom.Framework.Collections
{
    public static class ArrayExtension
    {
        public static bool IsEmpty<T>(this T[] array)
            where T : struct, IComparable
        {
            return array.Length == 0;
        }

        public static bool IsUniqueValue<T>(this T[] array, T valueToCheck)
            where T : struct, IComparable
        {
            if (array.IsEmpty())
            {
                return false;
            }

            bool uniqueValue = true;
            T value = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (!value.Equals(array[i]) && value.Equals(valueToCheck))
                {
                    uniqueValue = false;
                    break;
                }
            }

            return uniqueValue;
        }
    }
}
