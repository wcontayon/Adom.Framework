using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public sealed class LimitedList<T> : IList<T>, IReadOnlyList<T>
    {
        private T[] _data;
        private int _capacity;
        private int _size;
        // Help check if items has been added during enumeration
        private int _version;

        private const string MSG_FORMAT_NEGATIVE_CAPACITY = "Capacity must be greater than 0";
        private const string MSG_FORMAT_ENUM_FAILED = "Enumeration has failed, items has been added during enumeration";
        private const string MSG_FORMAT_ENUM_CANNOT_HAPPEN = "Enumeration cannot be done";

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

                EnsureCapacityNotReached();
                _data[index] = value;
            }
        }

        public int Count => _size;

        public int Capacity => _capacity;

        public bool IsReadOnly => false;

        public object CollectionMarshal { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T item)
        {
            EnsureCapacityNotReached();
            _version++;
            _data[_size++] = item;
        }

        public void Clear()
        {
            if (_size > 0)
            {
                Array.Clear(_data, 0, _size);
                _size = 0;
                _version++;
            }
        }

        /// <summary>
        /// Resize the max capacity of the <see cref="LimitedList{T}" />
        /// </summary>
        /// <param name="newCapacity"></param>
        public void ReSize(int newCapacity)
        {
            if (newCapacity < 0)
            {
                ThrowHelper.ThrowArgumentException(MSG_FORMAT_NEGATIVE_CAPACITY);
            }

            if ((uint)newCapacity > Array.MaxLength)
            {
                _capacity = Array.MaxLength;
            } 
            else 
            {
                _capacity += newCapacity;
            }
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

        public IEnumerator<T> GetEnumerator() => new LimitedListEnumerator(this);

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => new LimitedListEnumerator(this);

        private void EnsureCapacityNotReached()
        {
            if (_capacity == _size)
            {
                ThrowHelper.ThrowCapacityReachedException(_capacity);
            }
        }

        public int IndexOf(T item) => Array.IndexOf(_data, item, 0, _size);

        public void Insert(int index, T item)
        {
            if (index < 0 || ((uint)index > (uint)_size))
            {
                ThrowHelper.ThrowArgumentOutOfRangeException();
            }

            EnsureCapacityNotReached();
            if (index < _size)
            {
                Array.Copy(_data, index, _data, index + 1, _size - index);
            }
            _version++;
            _data[index] = item;
            _size++;
        }

        public void RemoveAt(int index)
        {
            if ((uint)index >= (uint)_size)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException();
            }

            _size--;
            if (index < _size)
            {
                Array.Copy(_data, index+1, _data, index, _size - index);
            }

            _data[_size] = default;
            _version++;
        }

        #region Search

        public ReadOnlySpan<T> FindAllAsSpan(Predicate<T> match)
        {
            if (match == null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(match));
            }

            T[] array = Array.Empty<T>();
            for (int i = 0; i < _size; i++)
            {
                if (match(_data[i]))
                {
                    array[i] = _data[i];
                }
            }

            return array.AsSpan();
        }

        public List<T> FindAll(Predicate<T> match)
        {
            if (match == null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(match));
            }

            List<T> list = new();
            for (int i = 0; i < _size; i++)
            {
                if (match(_data[i]))
                {
                    list.Add(_data[i]);
                }
            }

            return list;
        }

        public int FindIndex(Predicate<T> match) => FindIndex(0, _size, match);

        public int FindIndex(int startIndex, Predicate<T> match) => FindIndex(startIndex, _size - startIndex, match);

        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            if ((uint)startIndex > (uint)_size)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException();
            }

            if (count < 0 || startIndex > _size - count)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException();
            }

            if (match == null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(match));
            }

            // iterate through datas
            for (int i = startIndex; i < startIndex + count; i++)
            {
                if (match(_data[i])) return i;
            }

            return -1;
        }

        private void AddItemToArray(T[] array, T item)
        {
            if (array == null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(array));
            }
        }

        #endregion

        public struct LimitedListEnumerator : IEnumerator, IEnumerator<T>
        {
            private readonly LimitedList<T> _list;
            private int _index;
            private readonly int _version;
            private T? _current;

            public LimitedListEnumerator(LimitedList<T> list)
            {
                _list = list;
                _index = 0;
                _version = list._version;
                _current = default(T);
            }

            public T Current => _current!;

            object? IEnumerator.Current
            {
                get
                {
                    if (_index == 0 || _index == _list._size + 1)
                    {
                        ThrowHelper.ThrowInvalidOperationException(MSG_FORMAT_ENUM_CANNOT_HAPPEN);
                    }
                    return _current;
                }
            }

            public void Dispose() { }

            public bool MoveNext()
            {
                var localList = _list;
                if (_version == localList._version && ((uint)_index) < (uint)localList._size)
                {
                    _current = localList._data[_index];
                    _index++;

                    return true;
                }

                return MoveNextRare();
            }

            private bool MoveNextRare()
            {
                if (_version != _list._version)
                {
                    ThrowHelper.ThrowInvalidOperation_EnumeratorHasFailed(MSG_FORMAT_ENUM_FAILED);
                }

                _index = _list._size + 1;
                _current = default;
                return false;
            }

            public void Reset()
            {
                if (_version != _list._version)
                {
                    ThrowHelper.ThrowInvalidOperation_EnumeratorHasFailed(MSG_FORMAT_ENUM_FAILED);
                }
                _current = default;
                _index = 0;
            }
        }
    }
}
