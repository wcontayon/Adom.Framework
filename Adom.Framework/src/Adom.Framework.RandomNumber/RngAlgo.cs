using System;
using System.Linq;
using System.Numerics;

namespace Adom.Framework.RandomNumber
{
    internal abstract class RngAlgo
    {
        public RngAlgo() { }

        /// <summary>
        /// Generate a random <see cref="uint"/> number
        /// by using the specific RNGs algorithm
        /// </summary>
        /// <param name="seed">The seed used to generate</param>
        /// <returns>Random <see cref="uint"/> number</returns>
        internal virtual uint RandomInt32(uint seed = 0) => (uint)Environment.TickCount + (seed++);

        /// <summary>
        /// Generate a random <see cref="uint"/> number
        /// by using the specific RNGs algorithm
        /// </summary>
        /// <param name="seed">The seed used to generate</param>
        /// <returns>Random <see cref="uint"/> number</returns>
        internal virtual ulong RandomInt64(ulong seed = 0) => (ulong)Environment.TickCount64 + (seed++);

        /// <summary>
        /// Generate a random <see cref="BigInteger"/> number
        /// by using the specific RNGs algorithm
        /// </summary>
        /// <param name="seed">The seed used to generate</param>
        /// <returns>Random <see cref="BigInteger"/> number</returns>
        internal virtual BigInteger RandomBigInteger(uint[] seedArray) => seedArray.Sum(s => Environment.TickCount64 + (s++));
    }
}
