using GalaSoft.MvvmLight;
using Schedule.UniversalApp.Model;
using Schedule.UniversalApp.BaseTypes;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using Schedule.UniversalApp.Services;
using System.Threading.Tasks;

namespace Schedule.UniversalApp.ViewModel
{
    public class ScheduleSelectorViewModel : ViewModelBase
    {
        ObservableCollection<Group> groups;
        ObservableCollection<Teacher> teachers;
        ObservableCollection<Room> rooms;
        DataService dataService = new DataService();        
        Status status = new Status();
        Category selectedCategory;

        public RelayCommand Update { get; set; }
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
        public bool IsLoading
        {
            get { return status.IsLoading; }
            set
            {
                if (value == status.IsLoading) return;
                status.IsLoading = value;
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
        public ScheduleSelectorViewModel()
        {
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
