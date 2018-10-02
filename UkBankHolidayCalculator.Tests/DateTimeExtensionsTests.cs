using System;
using Xunit;

namespace UkBankHolidayCalculator.Tests
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void IfTheIdealDateFallsOnSpecificDateThenReturnSpecifcDate()
        {
            var may28th = new DateTime(2018,5,28);
            Assert.Equal(may28th.GetNextWeekDay(DayOfWeek.Monday), new DateTime(2018, 5, 28));
        }

        [Fact]
        public void IfIdealDayDoesNotFallOnRequestedDayThenFindNearestCorrectDay()
        {
            var may1st = new DateTime(2018, 5, 1);
            Assert.Equal(may1st.GetNextWeekDay(DayOfWeek.Monday), new DateTime(2018, 5, 7));
        }
    }
}
