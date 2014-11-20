using Schedule.UniversalApp.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Schedule.UniversalApp.BaseTypes;
using GalaSoft.MvvmLight.Messaging;
using Schedule.UniversalApp.Model.ScheduleEntities;
using Schedule.UniversalApp.Services.Interfaces;

namespace Schedule.UniversalApp.Services
{
    public class DataService : IDataService
    {
        readonly WebDataService webData = new WebDataService();

        public async Task<ObservableCollection<Room>> GetRoomsAsync()
        {
            ObservableCollection<Room> rooms = await webData.GetRoomsFromUriAsync();
            return rooms;
        }
        public async Task<ObservableCollection<Teacher>> GetTeachersAsync()
        {
            ObservableCollection<Teacher> teachers = await webData.GetTeachersFromUriAsync();         
            return teachers;
        }
        public async Task<ObservableCollection<Group>> GetGroupsAsync()
        {
            ObservableCollection<Group> groups = await webData.GetGroupsFromUriAsync();
            return groups;
        }
        public async Task<WeekSchedule> GetWeekScheduleAsync(Category category, int weekNumber = 0)
        {
            WeekSchedule schedule = await webData.GetWeekScheduleFromUriAsync(category, weekNumber);
            return schedule;
        }

        public async Task<string> SendFeedback(FeedbackForm feedback)
        {
            return await webData.SendAsync(feedback);
        }
        void UpdateWeekSchedule(WeekSchedule validSchedule)
        {
            Messenger.Default.Send(validSchedule);
        }
    }
}
