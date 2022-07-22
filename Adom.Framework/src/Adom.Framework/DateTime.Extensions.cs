using System;
using System.Runtime.CompilerServices;

namespace Adom.Framework
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Sets the time of the current <see cref="DateTime"/> with millisecond precision.
        /// </summary>
        /// <param name="current">The current date.</param>
        /// <param name="hour">The hour.</param>
        /// <param name="minute">The minute.</param>
        /// <param name="second">The second.</param>
        /// <param name="millisecond">The millisecond.</param>
        /// <returns><see cref="DateTime"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime SetTime(this DateTime date, int hour, int minutes = 0, int seconds = 0, int milliseconds = 0)
            => ChangeTime(date, hour, minutes, seconds, milliseconds);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static DateTime ChangeTime(DateTime current, int hour, int minute = 0, int second = 0, int millisecond = 0)
            => new DateTime(current.Year, current.Month, current.Day, hour, minute, second, millisecond, DateTimeKind.Unspecified);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TimeOnly ChangeTime(TimeOnly currentTime, int hour, int minute = 0, int second = 0, int millisecond = 0)
            => new TimeOnly(hour, minute, second, millisecond);
    }
}
