namespace Schedule.UniversalApp.Model.ScheduleEntities
{
    public class Filter
    {
        public string _id { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
