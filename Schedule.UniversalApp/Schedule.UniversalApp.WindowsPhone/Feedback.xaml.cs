using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Schedule.UniversalApp.ViewModel;

namespace Schedule.UniversalApp
{ 
    public sealed partial class Feedback : Page
    {
        public Feedback()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            var frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                return;
            }

            if (frame.CanGoBack)
            {
                frame.GoBack();
                e.Handled = true;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private void FeedbackBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Focus(FocusState.Programmatic);
        }

        private async void Send_OnClick(object sender, RoutedEventArgs e)
        {
            //Hack to close the keyboard.
            FeedbackBox.IsEnabled = false;
            FeedbackBox.IsEnabled = true;

            var text = FeedbackBox.Text;
            await ((FeedbackViewModel)this.DataContext).SendFeedbackAsync(text);
        }
    }
}
