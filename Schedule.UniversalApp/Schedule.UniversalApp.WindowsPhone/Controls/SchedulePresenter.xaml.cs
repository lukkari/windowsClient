using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Schedule.UniversalApp.Model.ScheduleEntities;

namespace Schedule.UniversalApp
{
    public sealed partial class SchedulePresenter : UserControl
    {
        public SchedulePresenter()
        {
            this.InitializeComponent();
            this.DataContextChanged += SchedulePresenter_DataContextChanged;
        }

        private void SchedulePresenter_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            var timetable = args.NewValue as DaySchedule;
            RenderTimetable(timetable);
        }

        private void RenderTimetable(DaySchedule timetable)
        {
            if (timetable == null) return;
            for (var i = 7; i < 20; )
            {
                foreach (var subject in timetable.Subjects)
                {
                    if (subject.Date.Start.Hour != i) continue;
                    DayPanel.Children.Add(new SubjectPanel(subject));
                    i = i + subject.Date.Duration;
                }
                DayPanel.Children.Add(new SubjectPanel(i));
                i++;
            }
        }
    }
}
