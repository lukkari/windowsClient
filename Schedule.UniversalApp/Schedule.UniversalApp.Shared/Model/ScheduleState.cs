namespace Schedule.UniversalApp.Model
{
   public class ScheduleState
    {
        bool isEmpty = true;
        public string Category { get; set; }
        public int WeekNumber { get; set; }
        public bool IsEmpty { get { return isEmpty; } }

        public ScheduleState(string category, int weekNumber)
        {
            Category = category;
            WeekNumber = weekNumber;
            isEmpty = false;
        }
        public ScheduleState() { }
    }
}
