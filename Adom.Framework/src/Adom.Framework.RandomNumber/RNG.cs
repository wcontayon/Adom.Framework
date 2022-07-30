using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adom.Framework.RandomNumber
{
    /// <summary>
    /// Random Number Generator class. RNG helps developer
    /// to generate random number using various algorithm
    /// </summary>
    public class AdomRandomNumber
    {
        [ThreadStatic] XorShift? _xorShiftRNG;

        private readonly object _rngLock = new object();

        #region Random Number Generator

        public uint XorShift32(uint seed = 0)
        {
            var _xorShift = Singleton<XorShift>(_xorShiftRNG);
            return _xorShift.RandomInt32(seed);
        }

        #endregion

        #region Init Random generator classes

        private T Singleton<T>(T? rngInstance) where T : RngAlgo, new()
        {
            if (rngInstance != null)
            {
                return rngInstance;
            }
            else
            {
                lock (_rngLock)
                {
#pragma warning disable CA1508 // Éviter le code conditionnel mort
                    if (rngInstance != null)
                    {
                        return rngInstance;
                    }
                    else
                    {
                        rngInstance = new T();
                    }
#pragma warning restore CA1508 // Éviter le code conditionnel mort
                }

                return rngInstance;
            }
        }

        #endregion
    }
}
