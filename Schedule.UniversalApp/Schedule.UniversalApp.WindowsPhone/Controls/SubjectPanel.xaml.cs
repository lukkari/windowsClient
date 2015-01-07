using System;
using Windows.UI.Xaml.Controls;

using Schedule.UniversalApp.Model.ScheduleEntities;

namespace Schedule.UniversalApp
{
    public sealed partial class SubjectPanel : UserControl
    {
        public SubjectPanel(ScheduleEntity scheduleEntity)
        {
            this.InitializeComponent();
            StartTime.Text = scheduleEntity.Date.Start.ToString("HH:mm");
            Duration.Text = scheduleEntity.Date.Duration + "h.";
            EndTime.Text = scheduleEntity.Date.End.ToString("HH:mm");
            SetSize(scheduleEntity.Date.Duration);
            DescriptionPanel.Text = scheduleEntity.Subject.Name;
            Room.Text = string.Join(",", scheduleEntity.Rooms);
            TeacherName.Text = string.Join(",", scheduleEntity.Teachers);
        }

        //TODO: Needs better implementation.
        public SubjectPanel(int startingTime)
        {
            this.InitializeComponent();
            StartTime.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, startingTime, 15, 0).ToString("HH:mm");
            EndTime.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, startingTime + 1, 0, 0).ToString("HH:mm");
            SetSize();
        }
      
        private void SetSize(int duration = 1)
        {
            RootGrid.Height = 60 * duration;
        }
    }
}
