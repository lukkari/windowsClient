using Schedule.UniversalApp.Services.Interfaces;

namespace Schedule.UniversalApp.Model
{
    public class ApplicationState : IApplicationStateService
    {
        public bool IsLoading { get; set; }
        public bool IsWeekNavigationEnabled { get; set; }
        public bool IsFailure { get; set; }
        public bool IsSuccess { get; set; }
    }
}
