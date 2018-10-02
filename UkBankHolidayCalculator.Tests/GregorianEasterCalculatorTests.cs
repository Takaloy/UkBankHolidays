using System;
using Xunit;

namespace UkBankHolidayCalculator.Tests
{
    public class GregorianEasterCalculatorTests
    {
        [Theory]
        [InlineData(2016, 3, 27)]
        [InlineData(2017, 4, 16)]
        [InlineData(2018, 4, 1)]
        [InlineData(2019, 4, 21)]
        [InlineData(2020, 4, 12)]
        [InlineData(2021, 4, 4)]
        [InlineData(2022, 4, 17)]
        [InlineData(2023, 4, 9)]
        public void EasterCalculatesCorrectly(int year, int month, int day)
        {
            var calculator = new GregorianEasterCalculator();
            var easter = calculator.GetEasterForYear(year);

            Assert.Equal(easter, new DateTime(year, month, day));
        }
    }
}
