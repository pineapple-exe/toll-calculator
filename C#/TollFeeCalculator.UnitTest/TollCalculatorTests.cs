using System;
using NUnit.Framework;

namespace TollFeeCalculator.UnitTest
{
    [TestFixture]
    public class TollCalculatorTests
    {
        static readonly object[] cases =
        {
            //Timestamps with varying toll fees
            new object[] { 9, new DateTime[] { new DateTime(2021, 3, 10, 6, 0, 5) } },
            new object[] { 16, new DateTime[] { new DateTime(2021, 3, 10, 6, 40, 0) } },
            new object[] { 22, new DateTime[] { new DateTime(2021, 3, 10, 7, 0, 5) } },
            new object[] { 16, new DateTime[] { new DateTime(2021, 3, 10, 8, 20, 0) } },
            new object[] { 9, new DateTime[] { new DateTime(2021, 3, 10, 8, 46, 0) } },
            new object[] { 16, new DateTime[] { new DateTime(2021, 3, 10, 15, 15, 0) } },
            new object[] { 22, new DateTime[] { new DateTime(2021, 3, 10, 16, 10, 2) } },
            new object[] { 16, new DateTime[] { new DateTime(2021, 3, 10, 17, 22, 11) } },
            new object[] { 9, new DateTime[] { new DateTime(2021, 3, 10, 18, 10, 5) } },
            new object[] { 0, new DateTime[] { new DateTime(2021, 3, 10, 19, 40, 0) } },

            //Multiple tolls same hour
            new object[] { 31, new DateTime[] { new DateTime(2021, 3, 3, 7, 0, 0), new DateTime(2021, 3, 3, 7, 30, 0), new DateTime(2021, 3, 3, 7, 35, 0), new DateTime(2021, 3, 3, 6, 0, 0) } },

            //Multiple hour clusters
            new object[] { 54, new DateTime[] { new DateTime(2021, 3, 3, 6, 0, 0), new DateTime(2021, 3, 3, 6, 5, 0), new DateTime(2021, 3, 3, 6, 10, 0), new DateTime(2021, 3, 3, 6, 30, 0), new DateTime(2021, 3, 3, 6, 35, 0), new DateTime(2021, 3, 3, 6, 40, 0), new DateTime(2021, 3, 3, 7, 0, 0), new DateTime(2021, 3, 3, 7, 10, 0), new DateTime(2021, 3, 3, 7, 20, 0), new DateTime(2021, 3, 3, 8, 0, 0), new DateTime(2021, 3, 3, 8, 10, 0), new DateTime(2021, 3, 3, 8, 40, 0) } },

            // >= 60 SEK
            new object[] { 60, new DateTime[] { new DateTime(2021, 3, 4, 6, 35, 0), new DateTime(2021, 3, 4, 7, 36, 0), new DateTime(2021, 3, 4, 15, 40, 0), new DateTime(2021, 3, 4, 16, 40, 0), new DateTime(2021, 3, 4, 18, 0, 0) } },
            new object[] { 60, new DateTime[] { new DateTime(2021, 3, 4, 6, 0, 0), new DateTime(2021, 3, 4, 7, 0, 0), new DateTime(2021, 3, 4, 8, 0, 0), new DateTime(2021, 3, 4, 15, 0, 0), new DateTime(2021, 3, 4, 17, 0, 0) } }
        };

        [Test]
        [TestCaseSource(nameof(cases))]
        public void GetTotalFeeTesting(int fee, DateTime[] dates)
        {
            Assert.AreEqual(fee, TollCalculator.GetTotalFee(new Car(), dates) );
        }
    }
}
