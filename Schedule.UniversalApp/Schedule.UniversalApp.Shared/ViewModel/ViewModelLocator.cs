using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Schedule.UniversalApp.Services;
using Schedule.UniversalApp.Services.Interfaces;

namespace Schedule.UniversalApp.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            if (!SimpleIoc.Default.IsRegistered<IDataService>())
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ScheduleSelectorViewModel>();
            SimpleIoc.Default.Register<WeekSelectorViewModel>();
            SimpleIoc.Default.Register<FeedbackViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public ScheduleSelectorViewModel ScheduleSelector
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScheduleSelectorViewModel>();
            }
        }
        public WeekSelectorViewModel WeekSelector
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WeekSelectorViewModel>();
            }
        }

        public FeedbackViewModel Feedback
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FeedbackViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}