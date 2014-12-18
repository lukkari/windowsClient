using Schedule.UniversalApp.Model;

namespace Schedule.UniversalApp.Services.Interfaces
{
    public interface IScheduleStateService
    {
        ScheduleState LoadState();
        void SaveCategoryState(string category);
        void SaveWeekNumberState(int weekNumber);

    }
}
