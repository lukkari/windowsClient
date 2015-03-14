using GalaSoft.MvvmLight;
using Schedule.UniversalApp.Model;
using Schedule.UniversalApp.BaseTypes;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using Schedule.UniversalApp.Model.ScheduleEntities;
using System.Threading.Tasks;
using Schedule.UniversalApp.Services.Interfaces;

namespace Schedule.UniversalApp.ViewModel
{
    public class ScheduleSelectorViewModel : ViewModelBase
    {
        ObservableCollection<Group> groups;
        ObservableCollection<Teacher> teachers;
        ObservableCollection<Room> rooms;
        ObservableCollection<Filter> filters;

        readonly IDataService dataService;
        readonly IApplicationStateService stateService;
        Category selectedCategory;

        public RelayCommand Update { get; set; }
        public bool IsFailure
        {
            get { return stateService.IsFailure; }
            set
            {
                if (value == stateService.IsFailure) return;
                stateService.IsFailure = value;
                RaisePropertyChanged();
            }
        }
        public bool IsLoading
        {
            get { return stateService.IsLoading; }
            set
            {
                if (value == stateService.IsLoading) return;
                stateService.IsLoading = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Filter> Filters
        {
            get { return filters; }
            set
            {
                if (value == filters) return;
                filters = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Room> Rooms
        {
            get { return rooms; }
            set
            {
                if (value == rooms) return;
                rooms = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Group> Groups
        {
            get { return groups; }
            set
            {
                if (value == groups) return;
                groups = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Teacher> Teachers
        {
            get { return teachers; }
            set
            {
                if (value == teachers) return;
                teachers = value;
                RaisePropertyChanged();
            }
        }
        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                if (value == selectedCategory) return;
                selectedCategory = value;
                PassSelectedCategory();
                RaisePropertyChanged();
            }
        }
        public ScheduleSelectorViewModel(IDataService dataService, IApplicationStateService stateService)
        {
            this.dataService = dataService;
            this.stateService = stateService;
            SetPropertiesAsync();
            Update = new RelayCommand(SetPropertiesAsync);
        }
        void PassSelectedCategory()
        {
            Messenger.Default.Send<CategorySelectionMessage>(new CategorySelectionMessage() { Category = SelectedCategory });
        }
        async void SetPropertiesAsync()
        {
            IsLoading = true;
            IsFailure = false;

            try
            {
                await SetScheduleCategoriesAsync();
                await SetFilters();
            }
            catch (NoConectionException)
            {
                IsFailure = true;
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task SetFilters()
        {
            Filters = await dataService.GetFiltersAsync();
        }

        async Task SetScheduleCategoriesAsync()
        {
            Rooms = await dataService.GetRoomsAsync();
            Teachers = await dataService.GetTeachersAsync();
            Groups = await dataService.GetGroupsAsync();
        }
    }
}
