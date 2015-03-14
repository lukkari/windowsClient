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
        readonly IApplicationStateService applicationState;
        readonly IDataService dataService;
        readonly IScheduleStateService scheduleStateService;

        public Commands Commands { get; set; }
        public bool IsFailure
        {
            get { return applicationState.IsFailure; }
            set
            {
                if (value == applicationState.IsFailure) return;
                applicationState.IsFailure = value;
                RaisePropertyChanged();
            }
        }
        public bool IsWeekNavigationEnabled
        {
            get { return applicationState.IsWeekNavigationEnabled; }
            set
            {
                if (applicationState.IsWeekNavigationEnabled == value) return;
                applicationState.IsWeekNavigationEnabled = value;
                RaisePropertyChanged();
            }
        }
        public bool IsLoading
        {
            get { return applicationState.IsLoading; }
            set
            {
                if (applicationState.IsLoading == value) return;
                applicationState.IsLoading = value;
                RaisePropertyChanged();
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
                RaisePropertyChanged();
            }
        }
        public int CurrentSelectedWeek
        {
            get { return currentSelectedWeek; }
            set
            {
                if (value == currentSelectedWeek) return;
                currentSelectedWeek = value;
                RaisePropertyChanged();
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
                RaisePropertyChanged();
            }
        }
        public MainViewModel(IDataService dataService, IScheduleStateService scheduleState, IApplicationStateService applicationState)
        {
            this.dataService = dataService;
            this.scheduleStateService = scheduleState;
            this.applicationState = applicationState;

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
            ScheduleState state = scheduleStateService.LoadState();
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
                scheduleStateService.SaveWeekNumberState(CurrentSelectedWeek);
                scheduleStateService.SaveCategoryState(SelectedCategory.Name);
            }
        }
    }
}