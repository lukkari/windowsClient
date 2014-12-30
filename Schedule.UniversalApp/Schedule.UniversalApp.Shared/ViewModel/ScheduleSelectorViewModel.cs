using GalaSoft.MvvmLight;
using Schedule.UniversalApp.Model;
using Schedule.UniversalApp.BaseTypes;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using Schedule.UniversalApp.Services;
using System.Threading.Tasks;
using Schedule.UniversalApp.Services.Interfaces;

namespace Schedule.UniversalApp.ViewModel
{
    public class ScheduleSelectorViewModel : ViewModelBase
    {
        ObservableCollection<Group> groups;
        ObservableCollection<Teacher> teachers;
        ObservableCollection<Room> rooms;
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
                RaisePropertyChanged("IsFailure");
            }
        }
        public bool IsLoading
        {
            get { return stateService.IsLoading; }
            set
            {
                if (value == stateService.IsLoading) return;
                stateService.IsLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        public ObservableCollection<Room> Rooms
        {
            get { return rooms; }
            set
            {
                if (value == rooms) return;
                rooms = value;
                RaisePropertyChanged("Rooms");
            }
        }
        public ObservableCollection<Group> Groups
        {
            get { return groups; }
            set
            {
                if (value == groups) return;
                groups = value;
                RaisePropertyChanged("Groups");
            }
        }
        public ObservableCollection<Teacher> Teachers
        {
            get { return teachers; }
            set
            {
                if (value == teachers) return;
                teachers = value;
                RaisePropertyChanged("Teachers");
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
                RaisePropertyChanged("SelectedCategory");
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
        async Task SetScheduleCategoriesAsync()
        {
            Rooms = await dataService.GetRoomsAsync();
            Teachers = await dataService.GetTeachersAsync();
            Groups = await dataService.GetGroupsAsync();
        }
    }
}
