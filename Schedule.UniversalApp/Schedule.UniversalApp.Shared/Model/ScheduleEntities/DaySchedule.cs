using System;
using System.Collections.ObjectModel;

namespace Schedule.UniversalApp.Model.ScheduleEntities
{
    public class DaySchedule
    {       
        DateTime date;       
        public WeekDay WeekDay { get; set; }
        public ObservableCollection<ScheduleEntity> Subjects { get; set; }    
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value.ToLocalTime();
            }
        }
        public string DayAndMonth
        {
            get { return date.GetDateTimeFormats('m')[0]; }
        }
        public override string ToString()
        {
            if (Date == DateTime.Today)
            {
                return "Today";
            }
            return Date == DateTime.Today.AddDays(1) ? "Tomorrow" : WeekDay.Name;
        }
    }
}
