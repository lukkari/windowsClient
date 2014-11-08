using System;

namespace Schedule.UniversalApp.Services
{
    public class UriService
    {
        const string ScheduleUrl = "http://lukkari.dc.turkuamk.fi/api/schedule/";
        const string GroupCategoryUrl = "http://lukkari.dc.turkuamk.fi/api/groups";
        const string RoomCategoryUrl = "http://lukkari.dc.turkuamk.fi/api/rooms";
        const string TeacherCategoryUrl = "http://lukkari.dc.turkuamk.fi/api/teachers";

        public Uri GetScheduleUri(string categoryName, int weekNumber)
        {
            return new Uri(ScheduleUrl + categoryName.ToLower() + "?w=" + weekNumber.ToString());
        }

        public Uri RoomsUri
        {
            get { return new Uri(RoomCategoryUrl); }
        }
        public Uri GroupsUri
        {
            get { return new Uri(GroupCategoryUrl); }
        }
        public Uri TeachersUri
        {
            get { return new Uri(TeacherCategoryUrl); }
        }
    }
}
