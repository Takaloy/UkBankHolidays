using System;

namespace UkBankHolidayCalculator
{
    public static class DateTimeExtensions
    {
        public static DateTime GetNextWeekDay(this DateTime axis, DayOfWeek day)
        {
            var difference = ((int)day - (int)axis.DayOfWeek + 7) % 7;
            return axis.AddDays(difference);
        }

        public static DateTime GetNearestMonday(this DateTime axis)
        {
            return axis.GetNextWeekDay(DayOfWeek.Monday);
        }

        public static bool IsWeekday(this DateTime axis)
        {
            return axis.DayOfWeek != DayOfWeek.Saturday && axis.DayOfWeek != DayOfWeek.Sunday;
        }
    }
}