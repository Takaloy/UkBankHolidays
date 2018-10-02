using System;
using Xunit;

namespace UkBankHolidayCalculator.Tests
{
    public class UkBankHolidayCalculatorTests
    {
        private readonly IUkBankHolidayCalculator _calculator;

        public UkBankHolidayCalculatorTests()
        {
            _calculator = new UkBankHolidayCalculator(new GregorianEasterCalculator());
        }

        [Theory]
        [InlineData("Jan 1 2015")]
        [InlineData("Jan 1 2016")]
        [InlineData("Jan 2 2017")]
        [InlineData("Jan 1 2018")]
        [InlineData("Jan 1 2019")]
        [InlineData("Jan 1 2020")]
        public void january_bank_holidays_marked_correctly(string date)
        {
            
            var dateTime = DateTime.Parse(date);
            
            var isBankHoliday = _calculator.IsBankHoliday(dateTime);

            
            Assert.True(isBankHoliday);
        }


        [Theory]
        [InlineData("May 4 2015")]
        [InlineData("May 25 2015")]
        [InlineData("May 2 2016")]
        [InlineData("May 30 2016")]
        [InlineData("May 1 2017")]
        [InlineData("May 29 2017")]
        [InlineData("May 7 2018")]
        [InlineData("May 28 2018")]
        public void may_bank_holidays_marked_correctly(string date)
        {
            
            var dateTime = DateTime.Parse(date);
            var isBankHoliday = _calculator.IsBankHoliday(dateTime);
            Assert.True(isBankHoliday);
        }

        [Theory]
        [InlineData("August 31 2015")]
        [InlineData("August 29 2016")]
        [InlineData("August 28 2017")]
        [InlineData("August 27 2018")]
        public void august_bank_holidays_marked_correctly(string date)
        {

            var dateTime = DateTime.Parse(date);
            var isBankHoliday = _calculator.IsBankHoliday(dateTime);
            Assert.True(isBankHoliday);
        }

        [Theory]
        [InlineData("6 April 2015")]
        [InlineData("28 March 2016")]
        [InlineData("17 April 2017")]
        [InlineData("2 April 2018")]
        [InlineData("22 April 2019")]
        [InlineData("13 April 2020")]
        public void easter_bank_holidays_marked_correctly(string date)
        {

            var dateTime = DateTime.Parse(date);
            var isBankHoliday = _calculator.IsBankHoliday(dateTime);
            Assert.True(isBankHoliday);
        }


        [Theory]
        [InlineData("26 December 2014")]
        [InlineData("28 December 2015")]
        [InlineData("27 December 2016")]
        [InlineData("26 December 2017")]
        [InlineData("26 December 2018")]
        [InlineData("26 December 2019")]
        [InlineData("28 December 2020")]
        public void december_bank_holidays_marked_correctly(string date)
        {

            var dateTime = DateTime.Parse(date);
            var isBankHoliday = _calculator.IsBankHoliday(dateTime);
            Assert.True(isBankHoliday);
        }
    }
}
