using Newtonsoft.Json;
using Schedule.UniversalApp.BaseTypes;
using Schedule.UniversalApp.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Schedule.UniversalApp.Model.ScheduleEntities;

namespace Schedule.UniversalApp.Services
{
    public class WebDataService
    {
        readonly WebAccessService httpClient = new WebAccessService();
        readonly UriService uriProvider = new UriService();

        public async Task<ObservableCollection<Room>> GetRoomsFromUriAsync()
        {
            string httpResponse = await httpClient.HttpRequestAsync(uriProvider.RoomsUri);
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<ObservableCollection<Room>>(httpResponse));
        }

        public async Task<ObservableCollection<Teacher>> GetTeachersFromUriAsync()
        {
            string httpResponse = await httpClient.HttpRequestAsync(uriProvider.TeachersUri);
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<ObservableCollection<Teacher>>(httpResponse));
        }

        public async Task<ObservableCollection<Group>> GetGroupsFromUriAsync()
        {
            string httpResponse = await httpClient.HttpRequestAsync(uriProvider.GroupsUri);
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<ObservableCollection<Group>>(httpResponse));
        }

        public async Task<WeekSchedule> GetWeekScheduleFromUriAsync(Category category, int weekNumber = 0)
        {
            string httpResponse = await httpClient.HttpRequestAsync(uriProvider.GetScheduleUri(category.Name, weekNumber));
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<WeekSchedule>(httpResponse));
        }        
    }
}
