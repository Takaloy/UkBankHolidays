using System;

namespace UkBankHolidayCalculator
{
    public interface IEasterCalculator
    {
        DateTime GetEasterForYear(int year);
    }

    public class GregorianEasterCalculator : IEasterCalculator
    {
        /*
         * 1. Divide x by 19 to get remainder A.
         *      This is the year’s position in the 19-year lunar cycle. (A + 1 is the year’s Golden Number.)
         * 2. Divide x by 100 to get a quotient B and a remainder C.
         * 3. Divide B by 4 to get a quotient D and a remainder E.
         * 4. Divide 8B + 13 by 25 to get a quotient G.
         * 5. Divide 19A + B – D – G + 15 by 30 to get remainder H.
         *      (The year’s Epact is 23 – H when H is less than 24 and 53 – H otherwise.)
         * 6. Divide A + 11H by 319 to get a quotient M.
         * 7. Divide C by 4 to get a quotient J and a remainder K.
         * 8. Divide 2E + 2J – K – H + M + 32 by 7 to get a remainder L.
         * 9. Divide H – M + L + 90 by 25 to get a quotient N.
         * 10. Divide H – M + L + N + 19 by 32 to get a remainder P.
         *      Easter Sunday is the Pth day of the Nth month (N can be either 3 for March or 4 for April).
         *      The year’s dominical letter can be found by dividing 2E + 2J – K by 7 and taking the remainder
         *      (a remainder of 0 is equivalent to the letter A, 1 is equivalent to B, and so on).
         */
        public DateTime GetEasterForYear(int year)
        {
            var a = year % 19;      //position of the year

            var b = year / 100;     //quotient
            var c = year % 100;     //remainder

            var d = b / 4;          //quotient of B
            var e = b % 4;          //remainder of B

            var g = (8 * b + 13) / 25;

            var h = ((19 * a) + b - d - g + 15) % 30;

            var m = (a + 11 * h) / 319;

            var j = c / 4;
            var k = c % 4;

            var l = (2 * e + 2 * j - k - h + m + 32) % 7;

            var n = (h - m + l + 90) / 25;

            var p = (h - m + l + n + 19) % 32;

            return new DateTime(year, n, p);
        }
    }
}
