using System;
using System.Globalization;

namespace Schedule.UniversalApp.Services
{
    public static class DateTimeService
    {
        public static int GetCurrentCalendarWeek
        {
            get { return GetCalendarWeek(DateTime.Now); }
        }
        public static int GetCalendarWeek(DateTime date)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                date = date.AddDays(3);
            }
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        public static int GetNextWeek(int week)
        {
            DateTime nextWeek = FirstDateOfWeek(DateTime.Now.Year, week).AddDays(7);
            return GetCalendarWeek(nextWeek);
        }
        public static int GetPreviousWeek(int week)
        {
            DateTime previousWeek = FirstDateOfWeek(DateTime.Now.Year, week).Subtract(new TimeSpan(7,0,0,0));
            return GetCalendarWeek(previousWeek);
        }
        public static DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            var jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            
            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }

        public static int GetTotalWeeksInYear()
        {
            var dfi = DateTimeFormatInfo.CurrentInfo;
            var cal = CultureInfo.CurrentCulture.Calendar;
            var today = DateTime.Today;
            return cal.GetWeekOfYear(new DateTime(today.Year, 12, DateTime.DaysInMonth(today.Year, 12)), dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }
    }
}
