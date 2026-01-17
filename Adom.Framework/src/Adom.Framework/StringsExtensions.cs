using System;
using System.Text;

namespace Adom.Framework
{
    public static class StringsExtensions
    {
        /// <summary>
        /// Reverse the <see cref="string"/>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Reverse(this string str)
        {
            var span = str.AsSpan();
            if (span.IsEmpty)
            {
                return string.Empty;
            }

            ValueStringBuilder reversedString = new ValueStringBuilder(stackalloc char[span.Length]);
            for (int i = span.Length - 1; i >= 0; i--)
            {
                reversedString.Append(span[i]);
            }

            string s = reversedString.ToString();
            reversedString.Dispose();
            return s;
        }

        /// <summary>
        /// Reverse the <see cref="string"/>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static ReadOnlySpan<char> Reverse(this ReadOnlySpan<char> span)
        {
            if (span.IsEmpty)
            {
                return Span<char>.Empty;
            }

            ValueStringBuilder reversedString = new ValueStringBuilder(stackalloc char[span.Length]);
            for (int i = span.Length - 1; i >= 0; i--)
            {
                reversedString.Append(span[i]);
            }

            string s = reversedString.ToString();
            reversedString.Dispose();
            return s.AsSpan();
        }

        /// <summary>
        /// Check if the <see cref="string"/> is a <see cref="Guid"/> value.
        /// </summary>
        /// <param name="str"><see cref="string"/></param>
        /// <returns>true or false</returns>
        public static bool IsGuid(this string str)
        {
            var span = str.AsSpan();
            if (span.IsEmpty)
                return false;

            // use SplitLine method to split by '-'
            int step = 0;
            bool check = true;
            foreach (SplitedLine line in str.SplitLine('-'))
            {
                // first digit should be on 8 length
                if (step == 0 && line.Line.Length != 8)
                {
                    check = false;
                    break;
                }

                // second, third and fourth digits should be on 4 length
                if ((step == 1 || step == 2 || step == 3) && line.Line.Length != 4)
                {
                    check = false;
                    break;
                }

                // fifth digit should be on 12 length
                if (step == 4 && line.Line.Length != 12)
                {
                    check = false;
                    break;
                }

                // Stop iteratation
                if (step == 5) break;

                // the current line should only contains allowed chars (0-9 || A-Z || a-z)
                check = _matchAllowedAscii(line.Line);
                if (!check) break;

                step++;
            }

            bool _matchAllowedAscii(ReadOnlySpan<char> str)
            {
                bool match = true;
                for (int i = 0; i < str.Length; i++)
                {
                    // https://theasciicode.com.ar/
                    match = (str[i] >= 48 && str[i] <= 57) // 0-9
                            || (str[i] >= 65 && str[i] <= 90) // A-Z
                            || (str[i] >= 97 && str[i] <= 122); // a-z

                    if (!match) break;
                }

                return match;
            }

            return check;
        }

        /// <summary>
        /// Check if the <see cref="string"/> is a <see cref="Guid.Empty"/> value
        /// wihtout converting to <see cref="Guid"/>.
        /// </summary>
        /// <param name="str"><see cref="string"/></param>
        /// <returns>true of false</returns>
        public static bool IsGuidEmpty(this string str)
        {
            var span = str.AsSpan();
            if (span.IsEmpty)
                return false;

            // use SplitLine method to split by '-'
            int step = 0;
            bool check = true;
            foreach (SplitedLine line in str.SplitLine('-'))
            {
                // the digit should contains only '0' char
                check = _isZeroChar(line.Line);
                if (!check) break;

                // first digit should be on 8 length
                if (step == 0 && line.Line.Length != 8)
                {
                    check = false;
                    break;
                }

                // second, third and fourth digits should be on 4 length
                if ((step == 1 || step == 2 || step == 3) && line.Line.Length != 4)
                {
                    check = false;
                    break;
                }

                // fifth digit should be on 12 length
                if (step == 4 && line.Line.Length != 12)
                {
                    check = false;
                    break;
                }

                // Stop iteratation
                if (step == 5) break;

                if (!check) break;

                step++;
            }

            bool _isZeroChar(ReadOnlySpan<char> str)
            {
                bool match = true;
                for (int i = 0; i < str.Length; i++)
                {
                    // https://theasciicode.com.ar/
                    match = (str[i] == 48);

                    if (!match) break;
                }

                return match;
            }

            return check;
        }

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

        /// <summary>
        /// Split the specified <see cref="ReadOnlySpan{char}"/> with the <see cref="char"/>
        /// and return an <see cref="SplitedLineEnumerator"/> to iterate through the
        /// Line (<see cref="SplitedLine"/>).
        /// </summary>
        /// <param name="str">The <see cref="ReadOnlySpan{char}"/> to split</param>
        /// <param name="separator">st</param>
        /// <returns></returns>
        public static SplitedLineEnumerator SplitLine(this ReadOnlySpan<char> strAsSpan, char separator)
        {
            return new SplitedLineEnumerator(strAsSpan, separator);
        }

        /// <summary>
        /// Split the specified <see cref="string"/> with the <see cref="char"/>
        /// and return an <see cref="SplitedLineEnumerator"/> to iterate through the
        /// Line (<see cref="SplitedLine"/>).
        /// </summary>
        /// <param name="str">The <see cref="string"/> to split</param>
        /// <param name="separator">st</param>
        /// <returns></returns>
        public static SplitedLineEnumerator SplitLine(this string str, ReadOnlySpan<char> separator)
        {
            return new SplitedLineEnumerator(str.AsSpan(), separator);
        }

        /// <summary>
        /// Split the specified <see cref="ReadOnlySpan{char}"/> with the <see cref="char"/>
        /// and return an <see cref="SplitedLineEnumerator"/> to iterate through the
        /// Line (<see cref="SplitedLine"/>).
        /// </summary>
        /// <param name="str">The <see cref="ReadOnlySpan{char}"/> to split</param>
        /// <param name="separator">st</param>
        /// <returns></returns>
        public static SplitedLineEnumerator SplitLine(this ReadOnlySpan<char> strAsSpan, ReadOnlySpan<char> separator) => new SplitedLineEnumerator(strAsSpan, separator);
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
            if (span.Length == 0) return false;

            int index = span.IndexOfAny(_seperator);
            // we have one line
            if (index == -1)
            {
                _splitedLine = ReadOnlySpan<char>.Empty; // the next line will be empty
                Current = new SplitedLine(span, _seperator);
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
