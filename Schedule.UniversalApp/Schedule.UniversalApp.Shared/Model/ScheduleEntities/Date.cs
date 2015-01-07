using System;
using Newtonsoft.Json;

namespace Schedule.UniversalApp.Model.ScheduleEntities
{
    public class Date
    {
        private DateTime start;
        private DateTime end;

        [JsonProperty(PropertyName = "start")]
        public DateTime Start
        {
            get { return start; }
            set { start = value.ToLocalTime(); }
        }

        [JsonProperty(PropertyName = "end")]
        public DateTime End
        {
            get { return end; }
            set { end = value.ToLocalTime(); }
        }

        public int Duration
        {
            get { return ((End - Start).Add(new TimeSpan(0,0,15,0))).Hours; }
        }
    }
}
