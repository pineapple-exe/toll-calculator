using System;
using NUnit.Framework;

namespace TollFeeCalculator.UnitTest
{
    [TestFixture]
    public class HolidayCalculatorTests
    {
        static readonly object[] cases =
        {
            //Every holiday 2021
            new DateTime(2021, 1, 1),
            new DateTime(2021, 1, 5),
            new DateTime(2021, 1, 6),
            new DateTime(2021, 4, 1),
            new DateTime(2021, 4, 2),
            new DateTime(2021, 4, 3),
            new DateTime(2021, 4, 4),
            new DateTime(2021, 4, 5),
            new DateTime(2021, 4, 30),
            new DateTime(2021, 5, 1),
            new DateTime(2021, 5, 13),
            new DateTime(2021, 5, 22),
            new DateTime(2021, 5, 23),
            new DateTime(2021, 6, 6),
            new DateTime(2021, 6, 25),
            new DateTime(2021, 6, 26),
            new DateTime(2021, 11, 5),
            new DateTime(2021, 11, 6),
            new DateTime(2021, 12, 24),
            new DateTime(2021, 12, 25),
            new DateTime(2021, 12, 26),
            new DateTime(2021, 12, 31)
        };

        [TestCaseSource(nameof(cases))]
        [Test]
        public void IsHolidayTesting(DateTime date)
        {
            Assert.IsTrue(HolidayCalculator.IsHoliday(date));
        }
    }
}
