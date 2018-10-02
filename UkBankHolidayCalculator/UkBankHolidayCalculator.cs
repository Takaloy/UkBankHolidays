using System;
using System.Collections.Generic;
using System.Linq;

namespace UkBankHolidayCalculator
{
    public interface IUkBankHolidayCalculator
    {
        bool IsWorkingDay(DateTime date);
        bool IsBankHoliday(DateTime date);
        IEnumerable<DateTime> GetBankHolidaysForYear(int year);
    }

    /// <summary>
    /// should really call this England and Wales holiday calculator
    /// </summary>
    public class UkBankHolidayCalculator : IUkBankHolidayCalculator
    {
        private readonly IEasterCalculator _easterCalculator;

        public UkBankHolidayCalculator(IEasterCalculator easterCalculator)
        {
            _easterCalculator = easterCalculator ?? throw new ArgumentNullException(nameof(easterCalculator));
        }

        public bool IsWorkingDay(DateTime date)
        {
            return date.IsWeekday() && !IsBankHoliday(date);
        }

        public bool IsBankHoliday(DateTime date)
        {
            var holidays = GetBankHolidaysForYear(date.Year);
            return holidays.Contains(date);
        }

        public IEnumerable<DateTime> GetBankHolidaysForYear(int year)
        {
            var holidays = new List<DateTime>();

            var janHolidays = GetJanuaryBankHolidayForYear(year);   //january bank holiday
            var easterHolidays = GetEasterBankHolidays(year);       //good friday and easter
            var mayHolidays = GetMayBankHolidays(year);             //may holidays
            var augustHolidays = GetAugustBankHoliday(year);        //august holidays
            var decemberHolidays = GetDecemberHolidays(year);       //december holidays

            holidays.Add(janHolidays);
            holidays.AddRange(easterHolidays);
            holidays.AddRange(mayHolidays);
            holidays.Add(augustHolidays);
            holidays.AddRange(decemberHolidays);

            return holidays;
        }


        private IEnumerable<DateTime> GetDecemberHolidays(int year)
        {
            var christmas = new DateTime(year, 12, 25);
            var boxingDay = new DateTime(year, 12, 26);

            yield return christmas;
            yield return boxingDay;

            if (!boxingDay.IsWeekday()) // if boxing day falls on a weekend then the nearest Monday is definitely a bank holiday
                yield return boxingDay.GetNearestMonday();

            if (!christmas.IsWeekday()) // if Christmas falls on a weekend then nearest Tuesday is also a bank holiday
                yield return christmas.GetNextWeekDay(DayOfWeek.Tuesday);
        }

        /// <summary>
        /// last day of August is a bank holiday
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private DateTime GetAugustBankHoliday(int year)
        {
            //the earliest spring holiday day that can happen is the 25th of August, If 31st falls on a Sunday
            var august25th = new DateTime(year, 8, 25);
            return august25th.GetNearestMonday();
        }

        /// <summary>
        /// returns first of May bank holiday and spring holiday
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private IEnumerable<DateTime> GetMayBankHolidays(int year)
        {
            var firstMay = new DateTime(year, 5, 1);
            yield return firstMay.GetNearestMonday();

            //the earliest spring holiday day that can happen is the 25th of may, If 31st falls on a Sunday
            var may25th = new DateTime(year, 5, 25);
            yield return may25th.GetNearestMonday();
        }

        /// <summary>
        /// 1st January is always a public holiday.
        /// If Jan 1st falls on a weekend, then the next monday is the bank holiday
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private DateTime GetJanuaryBankHolidayForYear(int year)
        {
            var newyear = new DateTime(year, 1, 1);

            if (newyear.IsWeekday())
                return newyear;

            return newyear.GetNearestMonday();
        }

        private IEnumerable<DateTime> GetEasterBankHolidays(int year)
        {
            var easter = _easterCalculator.GetEasterForYear(year);
            yield return easter.AddDays(1);                         //day after easter sunday is bank holiday monday
            yield return easter.AddDays(-2);
        }

    }
}
