
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Adom.Framework.Tasks
{
    public static class TaskAwaiterExtensions
    {
        public static TaskAwaiter<T> GetAwaiter<T>(this T value) => Task.FromResult<T>(value).GetAwaiter();

        public static TaskAwaiter<T[]> GetAwaiter<T>(this IEnumerable<Task<T>> tasks) => Task.WhenAll(tasks).GetAwaiter();

        //public static ValueTaskAwaiter<T> GetAwaiter<T>(this T value) where T : struct => ValueTask.FromResult<T>(value).GetAwaiter();
    }
}
