using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Schedule.UniversalApp.Model;
using Schedule.UniversalApp.Services;
using Schedule.UniversalApp.Services.Interfaces;

namespace Schedule.UniversalApp.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (!SimpleIoc.Default.IsRegistered<IDataService>())
            {
                SimpleIoc.Default.Register<IDataService, WebDataService>();
            }

            if (!SimpleIoc.Default.IsRegistered<IScheduleStateService>())
            {
                SimpleIoc.Default.Register<IScheduleStateService, ScheduleStateService>();
            }

            if (!SimpleIoc.Default.IsRegistered<IApplicationStateService>())
            {
                SimpleIoc.Default.Register<IApplicationStateService, ApplicationState>();
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
            
        }
    }
}