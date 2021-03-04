using System;
using System.Linq;

namespace TollFeeCalculator
{
    public static class HolidayCalculator
    {
        public static bool IsHoliday(DateTime date)
        {
            return IsFixedHoliday(date) || IsDynamicHoliday(date);
        }

        private static DateTime EasterDay(int year)
        {
            //Anonymous Gregorian algorithm, revised version
            int a = year % 19;
            int b = year / 100;
            int c = year % 100;
            int d = b / 4;
            int e = b % 4;

            int g = (8 * b + 13) / 25;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;

            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 19 * l) / 433;
            int month = (h + l - 7 * m + 90) / 25;
            int day = (h + l - 7 * m + 33 * month + 19) % 32;

            return new DateTime(year, month, day);
        }

        private static DateTime NthConsecutiveWeekDayAfter(int n, DayOfWeek certainDayOfWeek, DateTime referenceDate)
        {
            DateTime theDay = referenceDate;
            bool dayFound = false;
            int occurrencesCount = 0;

            while (!dayFound)
            {
                theDay = theDay.AddDays(1);

                if (theDay.DayOfWeek == certainDayOfWeek) 
                    occurrencesCount++;

                if (occurrencesCount == n) 
                    dayFound = true;
            }
            return theDay;
        }

        private static DateTime WeekDayBetween(DayOfWeek certainDayOfWeek, DateTime start, DateTime end)
        {
            DateTime theDay = start;

            for (int i = 0; i < (end.Date - start.Date).Days; i++)
            {
                theDay = theDay.AddDays(1);

                if (theDay.DayOfWeek == certainDayOfWeek) 
                    return theDay;
            }
            return theDay;
        }

        private static bool IsFixedHoliday(DateTime date)
        {
            int month = date.Month;
            int day = date.Day;

            int[] january = { 1, 5, 6 };
            int[] april = { 30 };
            int[] may = { 1 };
            int[] june = { 6 };
            int[] december = { 24, 25, 26, 31 };

            return
                month == 1 && january.Contains(day) ||
                month == 4 && april.Contains(day) ||
                month == 5 && may.Contains(day) ||
                month == 6 && june.Contains(day) ||
                month == 12 && december.Contains(day);
        }

        private static bool IsDynamicHoliday(DateTime date)
        {
            int year = date.Year;

            DateTime easterDay = HolidayCalculator.EasterDay(year);
            DateTime maundyThursday = easterDay.AddDays(-3);
            DateTime goodFriday = easterDay.AddDays(-2);
            DateTime easterSaturday = easterDay.AddDays(-1);
            DateTime easterMonday = easterDay.AddDays(1);
            DateTime ascensionDay = HolidayCalculator.NthConsecutiveWeekDayAfter(6, DayOfWeek.Thursday, easterDay);

            DateTime pentecostEve = NthConsecutiveWeekDayAfter(7, DayOfWeek.Saturday, easterDay);
            DateTime pentecostDay = pentecostEve.AddDays(1);

            DateTime midsummersEve = WeekDayBetween(DayOfWeek.Friday, new DateTime(year, 6, 19), new DateTime(year, 6, 25));
            DateTime midsummersDay = midsummersEve.AddDays(1);

            DateTime allSaintsEve = WeekDayBetween(DayOfWeek.Friday, new DateTime(year, 10, 30), new DateTime(year, 11, 5));
            DateTime allSaintsDay = allSaintsEve.AddDays(1);

            DateTime[] dynamicHolidays = { easterDay, maundyThursday, goodFriday, easterSaturday, easterMonday, ascensionDay, pentecostEve, pentecostDay, midsummersEve, midsummersDay, allSaintsEve, allSaintsDay };

            return dynamicHolidays.Contains(date);
        }
    }
}
