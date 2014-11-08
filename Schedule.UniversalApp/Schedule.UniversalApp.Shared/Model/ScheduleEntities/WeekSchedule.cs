using System.Collections.ObjectModel;

namespace Schedule.UniversalApp.Model.ScheduleEntities
{
    public class WeekSchedule
    {
        public int Week { get; set; }
        public string Title { get; set; }    
        public ObservableCollection<DaySchedule> WeekDays { get; set; }
        public override string ToString()
        {
            return "Week " + Week;
        }
    }
}
