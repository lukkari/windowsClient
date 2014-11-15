using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Schedule.UniversalApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void CategorySelection_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ScheduleSelector));
        }

        private void WeekSelector_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(WeekSelector));
        }

        private void Feedback_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Feedback));
        }
    }
}
