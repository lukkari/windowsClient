using Newtonsoft.Json;

namespace Schedule.UniversalApp.Model.ScheduleEntities
{
    public class Subject
    {
        [JsonProperty(PropertyName = "_Id")]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
