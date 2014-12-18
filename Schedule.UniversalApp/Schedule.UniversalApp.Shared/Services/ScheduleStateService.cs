using System;
using Windows.Storage;
using Schedule.UniversalApp.Model;
using Schedule.UniversalApp.Services.Interfaces;

namespace Schedule.UniversalApp.Services
{
    class ScheduleStateService : IScheduleStateService
    {
        private readonly ApplicationDataContainer settings = ApplicationData.Current.LocalSettings;

        public ScheduleState LoadState()
        {
            if (settings.Values.ContainsKey("Category") && settings.Values.ContainsKey("WeekNumber"))
            {
                return new ScheduleState((String)settings.Values["Category"], (Int32)settings.Values["WeekNumber"]);
            }
            return new ScheduleState();
        }
        public void SaveCategoryState(string category)
        {
            settings.Values["Category"] = category;
        }
        public void SaveWeekNumberState(int weekNumber)
        {
            settings.Values["WeekNumber"] = weekNumber;
        }
    }
}
