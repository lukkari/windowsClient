using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Command;

namespace Schedule.UniversalApp.Model
{
    public class Commands
    {
        public RelayCommand NextWeekCommand { get; set; }
        public RelayCommand PreviousWeekCommand { get; set; }
        public RelayCommand NavigateToCurrentCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
    }
}
