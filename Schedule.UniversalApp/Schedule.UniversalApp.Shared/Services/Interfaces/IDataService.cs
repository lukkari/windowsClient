using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Schedule.UniversalApp.BaseTypes;
using Schedule.UniversalApp.Model;
using Schedule.UniversalApp.Model.ScheduleEntities;

namespace Schedule.UniversalApp.Services.Interfaces
{
    public interface IDataService
    {
        Task<ObservableCollection<Room>> GetRoomsAsync();
        Task<ObservableCollection<Teacher>> GetTeachersAsync();
        Task<ObservableCollection<Group>> GetGroupsAsync();
        Task<WeekSchedule> GetWeekScheduleAsync(Category category, int weekNumber = 0);
        Task<string> SendFeedback(FeedbackForm feedback);
    }
}
