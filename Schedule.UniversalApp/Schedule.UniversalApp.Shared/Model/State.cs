namespace Schedule.UniversalApp.Model
{
    class State
    {
        bool isEmpty = true;
        public string Category { get; set; }
        public int WeekNumber { get; set; }
        public bool IsEmpty { get { return isEmpty; } }

        public State(string category, int weekNumber)
        {
            Category = category;
            WeekNumber = weekNumber;
            isEmpty = false;
        }
        public State() { }
    }
}
