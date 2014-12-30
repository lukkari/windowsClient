using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight;
using Schedule.UniversalApp.Model;
using Schedule.UniversalApp.Services.Interfaces;


namespace Schedule.UniversalApp.ViewModel
{
    public class FeedbackViewModel : ViewModelBase
    {
        private readonly IDataService dataService;
        private readonly IApplicationStateService stateService;

        public bool IsFailure
        {
            get { return stateService.IsFailure; }
            set
            {
                if (value == stateService.IsFailure) return;
                stateService.IsFailure = value;
                RaisePropertyChanged("IsFailure");
            }
        }
        public bool IsSendingButtonEnabled
        {
            get { return stateService.IsWeekNavigationEnabled; }
            set
            {
                if (stateService.IsWeekNavigationEnabled == value) return;
                stateService.IsWeekNavigationEnabled = value;
                RaisePropertyChanged("IsSendingButtonEnabled");
            }
        }
        public bool IsLoading
        {
            get { return stateService.IsLoading; }
            set
            {
                if (stateService.IsLoading == value) return;
                stateService.IsLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        public bool IsSuccess
        {
            get { return stateService.IsSuccess; }
            set
            {
                if (stateService.IsSuccess == value) return;
                stateService.IsSuccess = value;
                RaisePropertyChanged("IsSuccess");
            }
        }

        public FeedbackViewModel(IDataService dataService, IApplicationStateService stateService)
        {
            this.dataService = dataService;
            this.stateService = stateService;
        }

        public async Task<bool> SendFeedbackAsync(string message)
        {
            IsLoading = true;

            var height = Window.Current.Bounds.Height * DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            var width = Window.Current.Bounds.Width * DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            var response = await dataService.SendFeedback(new FeedbackForm(width, height, message));

            IsLoading = false;
            return response != "Error";
        }
    }
}
