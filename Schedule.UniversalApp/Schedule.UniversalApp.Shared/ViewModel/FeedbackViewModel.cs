using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight;
using Schedule.UniversalApp.Model;
using Schedule.UniversalApp.Services;


namespace Schedule.UniversalApp.ViewModel
{
    public class FeedbackViewModel : ViewModelBase
    {
        readonly DataService dataService = new DataService();
        readonly ApplicationState status = new ApplicationState();

        public bool IsFailure
        {
            get { return status.IsFailure; }
            set
            {
                if (value == status.IsFailure) return;
                status.IsFailure = value;
                RaisePropertyChanged("IsFailure");
            }
        }
        public bool IsSendingButtonEnabled
        {
            get { return status.IsWeekNavigationEnabled; }
            set
            {
                if (status.IsWeekNavigationEnabled == value) return;
                status.IsWeekNavigationEnabled = value;
                RaisePropertyChanged("IsSendingButtonEnabled");
            }
        }
        public bool IsLoading
        {
            get { return status.IsLoading; }
            set
            {
                if (status.IsLoading == value) return;
                status.IsLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        public bool IsSuccess
        {
            get { return status.IsSuccess; }
            set
            {
                if (status.IsSuccess == value) return;
                status.IsSuccess = value;
                RaisePropertyChanged("IsSuccess");
            }
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
