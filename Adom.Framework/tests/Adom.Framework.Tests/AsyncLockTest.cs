using Adom.Framework.AsyncLock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Adom.Framework.Tests
{
    public class AsyncLockTest
    {
        [Fact]
        public async ValueTask LockShouldNotThrowDisposedObject()
        {
            AsyncLock.AsyncLock asyncLock = new AsyncLock.AsyncLock();
            int increment = 0;
            await using (await asyncLock.LockAsync().ConfigureAwait(false))
            {
                increment++;
                increment++;
            }

            await using (await asyncLock.LockAsync().ConfigureAwait(false))
            {
                increment++;
                increment++;
            }

            await using (await asyncLock.LockAsync().ConfigureAwait(false))
            {
                increment++;
                increment++;
            }

            Assert.Equal(6, increment);
        }
    }
}
