using System;
using System.Collections.Generic;
using System.Linq;

namespace TollFeeCalculator
{
    public class TollCalculator
    {
        public static int GetTotalFee(Vehicle vehicle, IEnumerable<DateTime> dates)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));
            if (dates == null)
                throw new ArgumentNullException(nameof(dates));
            if (dates.Select(d => d.Date).Distinct().Count() > 1)
                throw new ArgumentException("All input dates must be of same day.");

            if (vehicle.IsTollFree() || IsTollFreeDate(dates.First()))
                return 0;

            if (dates.Count() == 1)
                return GetTollFee(dates.First());

            DateTime[] orderedDates = dates.OrderBy(d => d).ToArray();
            DateTime intervalStart = orderedDates[0];
            List<int> oneHourCluster = new List<int>();
            int totalFee = 0;

            for (int i = 0; i < orderedDates.Length; i++)
            {
                if (Math.Abs(intervalStart.Hour - orderedDates[i].Hour) > 0)
                {
                    totalFee += oneHourCluster.Max();
                    oneHourCluster.Clear();
                    intervalStart = orderedDates[i];
                }

                if (totalFee >= 60)
                    return totalFee;

                oneHourCluster.Add(GetTollFee(orderedDates[i]));
            }
            return totalFee += oneHourCluster.Max();
        }

        private static bool IsTollFreeDate(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                return true;

            return HolidayCalculator.IsHoliday(date);
        }

        private static int GetTollFee(DateTime date)
        {
            int hour = date.Hour;
            int minute = date.Minute;

            //Transportstyrelsens angivelser om trängselskatt Gbg, 2020
            if (hour == 6 && minute <= 29) 
                return 9;
            else if (hour == 6) 
                return 16;
            else if (hour == 7) 
                return 22;
            else if (hour == 8 && minute <= 29) 
                return 16;
            else if (hour == 8 && minute >= 30 || hour <= 14)
                return 9;
            else if (hour == 15 && minute <= 29) 
                return 16;
            else if (hour == 15 && minute >= 30 || hour == 16)
                return 22;
            else if (hour == 17) 
                return 16;
            else if (hour == 18 && minute <= 29) 
                return 9;
            else 
                return 0;
        }
    }
}
