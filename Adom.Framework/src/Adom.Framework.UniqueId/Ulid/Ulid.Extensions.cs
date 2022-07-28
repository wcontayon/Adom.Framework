using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Adom.Framework.UniqueId.Ulid
{
    public ref partial struct Ulid
    {
        internal readonly long ConvertTimestampToLong()
        {
            Span<byte> buffer = stackalloc byte[TIMESTAMP_LEN];
            buffer[0] = _timestamp5;
            buffer[1] = _timestamp4;
            buffer[2] = _timestamp3;
            buffer[3] = _timestamp2;
            buffer[4] = _timestamp1;
            buffer[5] = _timestamp0;

            return Unsafe.As<byte, long>(ref MemoryMarshal.GetReference(buffer));
        }

        /// <summary>
        /// Set the timestamp field from a long value
        /// </summary>
        /// <param name="timestamp"><see cref="long"/></param>
        internal void SetTimestampFromLong(long timestamp)
        {
            _timestamp0 = (byte)((timestamp & 280376465082880) >> 40);
            _timestamp1 = (byte)((timestamp & 1095216660480) >> 32);
            _timestamp2 = (byte)((timestamp & 4278190080) >> 24);
            _timestamp3 = (byte)((timestamp & 16711680) >> 16);
            _timestamp4 = (byte)((timestamp & 65280) >> 8);
            _timestamp5 = (byte)((timestamp & 255) >> 0);
        }

        internal void SetTimeFromSpan(ReadOnlySpan<byte> timestamp)
        {
            if (timestamp.Length != TIMESTAMP_LEN)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, TIMESTAMP_WRONG_LENGTH_MSG, TIMESTAMP_LEN));
            }

            _timestamp0 = timestamp[0];
            _timestamp1 = timestamp[1];
            _timestamp2 = timestamp[2];
            _timestamp3 = timestamp[3];
            _timestamp4 = timestamp[4];
            _timestamp5 = timestamp[5];
        }
    }
}
