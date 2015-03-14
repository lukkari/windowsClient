using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Schedule.UniversalApp.Model;
using Schedule.UniversalApp.Services;
using System;

namespace Schedule.UniversalApp.ViewModel
{
    public class WeekSelectorViewModel : ViewModelBase
    {
        private WeekAndDate selectedWeek;
        public ObservableCollection<WeekAndDate> Weeks { get; set; }
        
        public WeekAndDate SelectedWeek
        {
            get { return selectedWeek; }
            set
            {
                if (value == selectedWeek) return;
                selectedWeek = value;
                PassWeekNumber();
                RaisePropertyChanged();
            }
        }

        public WeekSelectorViewModel()
        {
            Messenger.Default.Register<int>(this, (action) => selectedWeek.WeekNum = action);
            Weeks = new ObservableCollection<WeekAndDate>();
            for (int i = 1; i < DateTimeService.GetTotalWeeksInYear(); i++)
            {
               Weeks.Add(new WeekAndDate(i));
            }    
        }

        private void PassWeekNumber()
        {
            Messenger.Default.Send<WeekNumberMessage>(new WeekNumberMessage { WeekNumber = selectedWeek.WeekNum });
        }
    }

    public class WeekAndDate
    {
        public int WeekNum { get; set; }

        public DateTime MondayDate
        {
            get { return DateTimeService.FirstDateOfWeek(DateTime.Now.Year, WeekNum); } 
        }

        public string DayAndMonth
        {
            get { return MondayDate.GetDateTimeFormats('m')[0]; }
        }

        public WeekAndDate(int weekNum)
        {
            WeekNum = weekNum;
        }
    }
}
