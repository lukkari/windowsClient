using System;
using System.Collections.ObjectModel;

namespace Schedule.UniversalApp.Model.ScheduleEntities
{
    public class ScheduleEntity
    {
        DateTime date;
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }                     
        public Subject Subject { get; set; }
        public int __V { get; set; }
        public string _Id { get; set; }
        public ObservableCollection<Teacher> Teachers { get; set; }
        public ObservableCollection<Group> Groups { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }

        public string GetStringDate
        {
            get { return date.ToString("HH:mm"); }
        }
        public string GetStringEndTime
        {
            get { return EndTime.ToString("HH:mm"); }
        }
        public DateTime Date
        {
            get { return date; }
            set
            {
                EndTime = value.ToLocalTime().AddHours(Duration).Subtract(new TimeSpan(0, 15, 0));
                date = value.ToLocalTime();
            }
        }
    }
}
