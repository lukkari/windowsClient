using Newtonsoft.Json;

namespace Schedule.UniversalApp.Model
{
    public class FeedbackForm
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "screen")]
        public string Screen { get; set; }

        [JsonProperty(PropertyName = "device")]
        public string Device { get; set; }

        public FeedbackForm(double width, double height, string message)
        {
            Screen = width.ToString("F0") + 'x' + height.ToString("F0");
            Message = message;
            Device = "Windows Phone 8.1";
        }
    }
}
