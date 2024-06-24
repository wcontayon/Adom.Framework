using Adom.Framework.Collections;
using System;
using System.Numerics;

namespace Adom.Framework.RandomNumber
{
    /// <summary>
    /// Implement of XorShift random number generator algorihtm<br />
    /// <see href="https://en.wikipedia.org/wiki/Xorshift">https://en.wikipedia.org/wiki/Xorshift</see>
    /// </summary>
    internal class XorShift : RngAlgo
    {
        [ThreadStatic] uint _xorShift32_state = 0;

        [ThreadStatic] ulong _xorShift64_state = 0;

        [ThreadStatic] uint[] _xorShift128_state = new uint[4];

        public XorShift(): base() { }

        /// <summary>
        /// Generate a seed number by increment the <see cref="_xorShift32_state"/>
        /// and add <see cref="Environment.TickCount"/>
        /// </summary>
        internal void Seed32()
        {
            _xorShift32_state++;
            _xorShift32_state += (uint)Environment.TickCount;
        }
        internal void Seed64()
        {
            _xorShift64_state++;
            _xorShift64_state += (ulong)Environment.TickCount64;
        }

        internal void Seed128()
        {
            _xorShift128_state[0] = (_xorShift32_state++) + (uint)Environment.TickCount;
            _xorShift128_state[1] = (_xorShift32_state++) + (uint)Environment.TickCount;
            _xorShift128_state[2] = (_xorShift32_state++) + (uint)Environment.TickCount;
            _xorShift128_state[3] = (_xorShift32_state++) + (uint)Environment.TickCount;
        }

        /// <summary>
        /// Generate a random <see cref="uint"/> number
        /// by using the specific XorShift algorithm.<br />
        /// Algorithm "xor" from p.4 of Marsaglia, "XorShift" RNGs
        /// </summary>
        /// <param name="seed">The seed used to generate</param>
        /// <returns>Random <see cref="uint"/> number</returns>
        internal override uint RandomInt32(uint seed = 0)
        {
            Seed32();
            uint x = seed == 0 ? _xorShift32_state : seed;
            x ^= x << 13;
            x ^= x >> 17;
            x ^= x << 5;
            return x;
        }

        /// <summary>
        /// Generate a random <see cref="ulong"/> number
        /// by using the specific XorShift algorithm.<br />
        /// Algorithm "xor" from p.4 of Marsaglia, "XorShift" RNGs
        /// </summary>
        /// <param name="seed">The seed used to generate</param>
        /// <returns>Random <see cref="ulong"/> number</returns>
        internal override ulong RandomInt64(ulong seed = 0)
        {
            Seed64();
            ulong x = seed == 0 ? _xorShift64_state : seed;
            x ^= x << 13;
            x ^= x >> 7;
            x ^= x << 17;
            return x;
        }

        /// <summary>
        /// Generate a random <see cref="BigInteger"/> (128 bytes) number
        /// by using the specific XorShift algorithm.<br />
        /// Algorithm "xor" from p.5 of Marsaglia, "XorShift" RNGs
        /// </summary>
        /// <param name="seedArray">The seed used to generate</param>
        /// <returns>Random <see cref="BigInteger"/> (128 bytes) number</returns>
        internal override BigInteger RandomBigInteger(uint[] seedArray)
        {
            // Algorithm "xor128" from p. 5 of Marsaglia
            if (seedArray.IsEmpty() && seedArray.IsUniqueValue<uint>(0))
            {
                Seed128();
                seedArray = _xorShift128_state; 
            }

            return RandomXorShift128(seedArray);
        }

        private static BigInteger RandomXorShift128(Span<uint> seed)
        {
            uint t = seed[3];
            uint s = seed[0];
            seed[3] = seed[2];
            seed[1] = s;

            t ^= t << 11;
            t ^= t >> 8;

            return t ^ s ^ (s >> 19);
        }
    }
}
