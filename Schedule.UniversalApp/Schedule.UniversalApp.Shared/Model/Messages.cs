using Schedule.UniversalApp.BaseTypes;
using Schedule.UniversalApp.Model.ScheduleEntities;

namespace Schedule.UniversalApp.Model
{
    class WeekNumberMessage
    {
        public int WeekNumber { get; set; }
    }

    public class CategorySelectionMessage
    {
        public Category Category { get; set; }
    }

    class ScheduleRequestMessage
    {
        public string RequestedName { get; set; }
        public int RequestedWeek { get; set; }
    }
    
    class WeekScheduleMessage
    {
        public WeekSchedule WeekSchedule { get; set; }
    }
}
