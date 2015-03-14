using System;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
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

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            await statusBar.HideAsync();
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

        private void About_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(About));
        }
    }
}
