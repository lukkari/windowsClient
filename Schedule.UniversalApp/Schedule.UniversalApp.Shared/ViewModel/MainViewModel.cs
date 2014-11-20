using System.ServiceModel;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Schedule.UniversalApp.BaseTypes;
using Schedule.UniversalApp.Model;
using System;
using Schedule.UniversalApp.Model.ScheduleEntities;
using Schedule.UniversalApp.Services;
using System.Threading.Tasks;
using Schedule.UniversalApp.Services.Interfaces;

namespace Schedule.UniversalApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        WeekSchedule currentWeekSchedule;
        Category selectedCategory;
        int currentSelectedWeek;
        readonly Status status = new Status();
        readonly IDataService dataService;
        ApplicationStateService stateService;

        public Commands Commands { get; set; }
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
        public bool IsWeekNavigationEnabled
        {
            get { return status.IsWeekNavigationEnabled; }
            set
            {
                if (status.IsWeekNavigationEnabled == value) return;
                status.IsWeekNavigationEnabled = value;
                RaisePropertyChanged("IsWeekNavigationEnabled");
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
        public Category SelectedCategory
        {
            get { return selectedCategory ?? new Category() { Name = "Select schedule" }; }
            set
            {
                if (selectedCategory == value) return;
                IsWeekNavigationEnabled = true;
                selectedCategory = value;
                RaisePropertyChanged("SelectedCategory");
            }
        }
        public int CurrentSelectedWeek
        {
            get { return currentSelectedWeek; }
            set
            {
                if (value == currentSelectedWeek) return;
                currentSelectedWeek = value;
                RaisePropertyChanged("CurrentSelectedWeek");
                //Update selected week on WeekSelector page.    
                Messenger.Default.Send<int>(value);
            }
        }
        public WeekSchedule CurrentWeekSchedule
        {
            get { return currentWeekSchedule; }
            set
            {
                if (currentWeekSchedule == value) return;
                currentWeekSchedule = value;
                RaisePropertyChanged("CurrentWeekSchedule");
            }
        }
        public MainViewModel(IDataService dataService)
        {
            this.dataService = dataService;
            Commands = new Commands
            {
                NextWeekCommand = new RelayCommand(GetNextWeekAsync),
                PreviousWeekCommand = new RelayCommand(GetPreviousWeekAsync),
                NavigateToCurrentCommand = new RelayCommand(async () => await GetScheduleByWeekNumberAsync(DateTimeService.GetCurrentCalendarWeek)),
                UpdateCommand = new RelayCommand(async () => await GetScheduleByWeekNumberAsync(CurrentSelectedWeek)),
            };

            Messenger.Default.Register<CategorySelectionMessage>(this, async (action) => await ReceiveCategoryMessageAsync(action));
            Messenger.Default.Register<WeekNumberMessage>(this, async (action) => await GetScheduleByWeekNumberAsync(action.WeekNumber));
            Messenger.Default.Register<WeekSchedule>(this, (action) => CurrentWeekSchedule = action);

            LoadStateAsync();
        }
        private async void LoadStateAsync()
        {
            stateService = new ApplicationStateService();
            State state = stateService.LoadState();
            if (!state.IsEmpty)
            {
                await ReceiveCategoryMessageAsync(new CategorySelectionMessage() { Category = new Category() { Name = state.Category } });
            }
        }
        async void GetNextWeekAsync()
        {
            CurrentSelectedWeek = DateTimeService.GetNextWeek(CurrentSelectedWeek);
            await GetScheduleByWeekNumberAsync(CurrentSelectedWeek);
        }
        async void GetPreviousWeekAsync()
        {
            CurrentSelectedWeek = DateTimeService.GetPreviousWeek(CurrentSelectedWeek);
            await GetScheduleByWeekNumberAsync(CurrentSelectedWeek);
        }
        async Task ReceiveCategoryMessageAsync(CategorySelectionMessage action)
        {
            SelectedCategory = action.Category;
            await GetScheduleByWeekNumberAsync(DateTimeService.GetCurrentCalendarWeek);
        }
        async Task GetScheduleByWeekNumberAsync(int weekRequested)
        {
            CurrentSelectedWeek = weekRequested;
            IsFailure = false;
            IsLoading = true;

            CurrentWeekSchedule = new WeekSchedule();
            try
            {
                CurrentWeekSchedule = await dataService.GetWeekScheduleAsync(SelectedCategory, weekRequested);
            }
            catch (Exception)
            {
                IsFailure = true;
            }
            finally
            {
                IsLoading = false;
                stateService.SaveWeekNumberState(CurrentSelectedWeek);
                stateService.SaveCategoryState(SelectedCategory.Name);
            }
        }
    }
}