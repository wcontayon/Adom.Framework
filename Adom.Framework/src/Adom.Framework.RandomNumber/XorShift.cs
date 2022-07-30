using Adom.Framework.Collections;
using System;
using System.Numerics;

namespace Adom.Framework.RandomNumber
{
    internal struct XorShift32_State
    {
        uint state;
    }

    /// <summary>
    /// Implement of XorShift random number generator algorihtm<br />
    /// <see href="https://en.wikipedia.org/wiki/Xorshift">https://en.wikipedia.org/wiki/Xorshift</see>
    /// </summary>
    internal class XorShift
    {
        [ThreadStatic] uint _xorShift32_state = 0;

        [ThreadStatic] ulong _xorShift64_state = 0;

        [ThreadStatic] uint[] _xorShift128_state = new uint[4];

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

        // Algorithm "xor" from p.4 of Marsaglia, "XorShift" RNGs
        internal uint XorShift32(uint seed = 0)
        {
            Seed32();
            uint x = seed == 0 ? _xorShift32_state : seed;
            x ^= x << 13;
            x ^= x >> 17;
            x ^= x << 5;
            return x;
        }

        internal ulong XorShift64(ulong seed = 0)
        {
            Seed64();
            ulong x = seed == 0 ? _xorShift64_state : seed;
            x ^= x << 13;
            x ^= x >> 7;
            x ^= x << 17;
            return x;
        }

        internal BigInteger XorShift128(uint[] seedArray)
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
