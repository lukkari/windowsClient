using System;

namespace Schedule.UniversalApp.Services
{
    public class UriService
    {
        private const string ApiUrl = "http://lukkari.dc.turkuamk.fi/api/";

        public Uri GetScheduleUri(string categoryName, int weekNumber)
        {
            return new Uri(ApiUrl + "schedule/" + categoryName.ToLower() + "?w=" + weekNumber.ToString());
        }

        public Uri RoomsUri
        {
            get { return new Uri(ApiUrl + "rooms"); }
        }
        public Uri GroupsUri
        {
            get { return new Uri(ApiUrl + "groups"); }
        }
        public Uri TeachersUri
        {
            get { return new Uri(ApiUrl + "teachers"); }
        }

        public Uri GetMessagingUri
        {
            get { return new Uri(ApiUrl + "message?"); }
        }

        public Uri GetFiltersUri
        {
            get { return  new Uri(ApiUrl + "filters");}
        }
    }
}
