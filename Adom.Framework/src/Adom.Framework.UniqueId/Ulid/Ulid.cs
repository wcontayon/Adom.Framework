using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Adom.Framework.UniqueId.Ulid
{
    /// <summary>
    /// Represents a Universally Unique Lexicographically Sortable Identifier (ULID).
    /// Spec: https://github.com/ulid/spec
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public ref partial struct Ulid
    {
        const int TIMESTAMP_LEN = 8;
        const int RANDOMNESS_LEN = 10;
        const int MAX_LENGTH = 16;
        static readonly char[] Base32Text = "0123456789ABCDEFGHJKMNPQRSTVWXYZ".ToCharArray();
        static readonly byte[] Base32Bytes = Encoding.UTF8.GetBytes(Base32Text);
        static readonly byte[] CharToBase32 = new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 255, 255, 255, 255, 255, 255, 255, 10, 11, 12, 13, 14, 15, 16, 17, 255, 18, 19, 255, 20, 21, 255, 22, 23, 24, 25, 26, 255, 27, 28, 29, 30, 31, 255, 255, 255, 255, 255, 255, 10, 11, 12, 13, 14, 15, 16, 17, 255, 18, 19, 255, 20, 21, 255, 22, 23, 24, 25, 26, 255, 27, 28, 29, 30, 31 };
        static readonly DateTimeOffset EpochUnix = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        const int ENCODING_LEN = 36; // ENCODING.Length

        #region Exceptions messages

        private const string RANDOMNESS_WRONG_LENGTH_MSG = "Randomness length sould be {0}";
        private const string TIMESTAMP_WRONG_LENGTH_MSG = "Timestamp length sould be {0}";

        #endregion

        #region Ulid byte field

        // Ulid spec:
        // Timestamp on 48 bytes
        [FieldOffset(0)] private byte _timestamp0;
        [FieldOffset(1)] private byte _timestamp1;
        [FieldOffset(2)] private byte _timestamp2;
        [FieldOffset(3)] private byte _timestamp3;
        [FieldOffset(4)] private byte _timestamp4;
        [FieldOffset(5)] private byte _timestamp5;

        // Randomness on 80 bytes
        [FieldOffset(6)] private byte _randomness0;
        [FieldOffset(7)] private byte _randomness1;
        [FieldOffset(8)] private byte _randomness2;
        [FieldOffset(9)] private byte _randomness3;
        [FieldOffset(10)] private byte _randomness4;
        [FieldOffset(11)] private byte _randomness5;
        [FieldOffset(12)] private byte _randomness6;
        [FieldOffset(13)] private byte _randomness7;
        [FieldOffset(14)] private byte _randomness8;
        [FieldOffset(15)] private byte _randomness9;

        #endregion

        /// <summary>
        /// Gets the underlying timestamp and the randomness as <see cref="ReadOnlySpan{byte}"/>
        /// </summary>
        public readonly ReadOnlySpan<byte> Datas
        {
            get
            {
                return new byte[]
                {
                    _timestamp0,
                    _timestamp1,
                    _timestamp2,
                    _timestamp4,
                    _timestamp5,
                    _randomness0,
                    _randomness1,
                    _randomness2,
                    _randomness3,
                    _randomness4,
                    _randomness5,
                    _randomness6,
                    _randomness7,
                    _randomness8,
                    _randomness9
                };
            }
        }

        /// <summary>
        /// Gets the timestamp generated for the <see cref="Ulid"/> instance
        /// </summary>
        public readonly long TimeStamp { get => ConvertTimestampToLong(); }

        public DateTimeOffset DateTimeOffset
        {
            get
            {
                return DateTimeOffset.FromUnixTimeMilliseconds(ConvertTimestampToLong());
            }
        }

        #region Constructors



        #endregion

    }
}
