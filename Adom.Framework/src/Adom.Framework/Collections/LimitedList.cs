using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adom.Framework.Collections
{
    /// <summary>
    /// Limited list of <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [DebuggerDisplay("Count = {count}")]
    public sealed class LimitedList<T> : ICollection<T>, IReadOnlyList<T>
    {
        private T[] _data;
        private readonly int _capacity;
        private int _size;

        public LimitedList(int maxCapacity)
        {
            _capacity = maxCapacity;
            _size = 0;
            _data = new T[maxCapacity];
        }

        public LimitedList(IEnumerable<T>? source, int maxCapacity)
        {
            _capacity = maxCapacity;
            _size = 0;
            _data = new T[maxCapacity];

            if (source != null)
            {
                int count = 0;
                using (IEnumerator<T> enumerator = source.GetEnumerator())
                while (enumerator.MoveNext() && count < _capacity)
                    {
                        Add(enumerator.Current);
                        count++;
                    }
            }
        }

        public LimitedList(T[]? source, int maxCapacity)
        {
            _capacity = maxCapacity;
            _size = 0;
            _data = new T[maxCapacity];
            
            if (source != null)
            {
                Array.Copy(source, _data, _capacity);
            }
        }

        public LimitedList(Span<T> source, int maxCapacity)
        {
            _capacity = maxCapacity;
            _size = 0;
            _data = new T[maxCapacity];

            if (source != null)
            {
                _data = source.Slice(0, maxCapacity).ToArray();
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _capacity)
                {
                    ThrowHelper.ThrowArgumentOutOfRangeException();
                }
                return _data[index];
            }
            set
            {
                if (index < 0 || index >= _capacity)
                {
                    ThrowHelper.ThrowArgumentOutOfRangeException();
                }

                EnsureCapacityNotReach();
                _data[index] = value;
            }
        }

        public int Count => _size;

        public int Capacity => _capacity;

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(T item)
        {
            EnsureCapacityNotReached();
            _data[_size++] = item;
        }

        public void Clear()
        {
            if (_size > 0)
            {
                Array.Clear(_data, 0, _size);
                _size = 0;
            }
        }

        public void ReSize(int newCapacity){
            
        }

        public bool Contains(T item)
        {
            if (item == null)
            {
                for (int i = 0; i < _size; i++)
                {
                    if (_data[i] as object == null)
                        return true;
                }

                return false;
            }
            else
            {
                EqualityComparer<T> comparer = EqualityComparer<T>.Default;
                for (int index = 0; index < _size; index++)
                {
                    if (comparer.Equals(_data[index], item))
                        return true;
                }

                return false;
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                ThrowHelper.ThrowArgumentNullException(nameof(array));

            try
            {
                _data.CopyTo(array, arrayIndex);
            }
            catch (ArrayTypeMismatchException)
            {
                ThrowHelper.ThrowArgumentException("Array type mismatch");
            }
        }

        public Span<T> AsSpan() => _data.AsSpan();

        public Span<T> AsSpan(Index index) => _data.AsSpan(index);

        public Span<T> AsSpan(int startIndex, int length) => _data.AsSpan(startIndex, length);

        public ReadOnlySpan<T> AsReadOnlySpan() => _data.AsSpan();

        public ReadOnlySpan<T> AsReadOnlySpan(Index index) => _data.AsSpan(index);

        public ReadOnlySpan<T> AsReadOnlySpan(int startIndex, int length) => _data.AsSpan(startIndex, length);

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        [DoesNotReturn]
        private void EnsureCapacityNotReached()
        {
            if (_capacity == _size)
            {
                ThrowHelper.ThrowCapacityReachedException(_capacity);
            }
        }
    }
}
