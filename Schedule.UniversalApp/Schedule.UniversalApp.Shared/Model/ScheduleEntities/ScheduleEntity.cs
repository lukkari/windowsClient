using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Schedule.UniversalApp.Model.ScheduleEntities
{
    public class ScheduleEntity
    {

        [JsonProperty(PropertyName = "__v")]
        public int Version { get; set; }

        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; }
        
        [JsonProperty(PropertyName = "date")]
        public Date Date { get; set; }
        public Subject Subject { get; set; }
        public ObservableCollection<Teacher> Teachers { get; set; }
        public ObservableCollection<Group> Groups { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }

    }
}
