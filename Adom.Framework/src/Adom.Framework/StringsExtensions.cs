using System;

namespace Adom.Framework
{
    public static class StringsExtensions
    {
        // Inspired from https://github.com/meziantou/Meziantou.Framework/blob/main/src/Meziantou.Framework/StringExtensions.SplitLines.cs

        /// <summary>
        /// Split the specified <see cref="string"/> with the <see cref="char"/>
        /// and return an <see cref="SplitedLineEnumerator"/> to iterate through the
        /// Line (<see cref="SplitedLine"/>).
        /// </summary>
        /// <param name="str">The <see cref="string"/> to split</param>
        /// <param name="separator">st</param>
        /// <returns></returns>
        public static SplitedLineEnumerator SplitLine(this string str, char separator)
        {
            return new SplitedLineEnumerator(str.AsSpan(), separator);
        }
    }

    // Inspired from https://github.com/meziantou/Meziantou.Framework/blob/main/src/Meziantou.Framework/StringExtensions.SplitLines.cs

    /// <summary>
    /// Splited line enumerator returned by <see cref="StringsExtensions.SplitLine(string, char)"/> extension method.
    /// </summary>
    public ref struct SplitedLineEnumerator
    {
        private ReadOnlySpan<char> _splitedLine;
        private readonly ReadOnlySpan<char> _seperator;

        public SplitedLineEnumerator(ReadOnlySpan<char> line, char separator)
        {
            _splitedLine = line;
            _seperator = new[] { separator }.AsSpan();
            Current = default;
        }

        public SplitedLineEnumerator(ReadOnlySpan<char> line, ReadOnlySpan<char> separators)
        {
            _splitedLine = line;
            _seperator = separators;
            Current = default;
        }

        public SplitedLineEnumerator GetEnumerator() => this;

        public SplitedLine Current { get; private set; }

        public bool MoveNext()
        {
            if (_splitedLine.IsEmpty) return false;

            var span = _splitedLine;
            if (span .Length == 0) return false;

            int index = span.IndexOfAny(_seperator);
            // we have one line
            if (index == -1)
            {
                _splitedLine = ReadOnlySpan<char>.Empty; // the next line will be empty
                Current = new SplitedLine(span.Slice(0, index), _seperator);
                return true;
            }

            // set the current
            Current = new SplitedLine(span.Slice(0, index), _seperator);

            // Move _splitedLine to the next span after the separator
            _splitedLine = span.Slice(index + 1);
            return true;
        }
    }

    /// <summary>
    /// Represents a <see cref="string"/> splited line 
    /// </summary>
    public readonly ref struct SplitedLine
    {
        private readonly ReadOnlySpan<char> _line;
        private readonly ReadOnlySpan<char> _separators;

        public SplitedLine(ReadOnlySpan<char> line, ReadOnlySpan<char> separator)
        {
            _line = line;
            _separators = separator;
        }

        public ReadOnlySpan<char> Line { get { return _line; } }
        public ReadOnlySpan<char> Separators { get { return _separators; } }

        public static implicit operator ReadOnlySpan<char>(SplitedLine splitedLine) => splitedLine.Line;

        public ReadOnlySpan<char> ToReadOnlySpan() => Line;
    }
}
