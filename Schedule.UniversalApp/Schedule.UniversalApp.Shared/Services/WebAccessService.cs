using Schedule.UniversalApp.Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Schedule.UniversalApp.Services
{
    public class WebAccessService
    {
        public async Task<string> HttpRequestAsync(Uri uri)
        {
            using (var client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(uri))
            {
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        return await content.ReadAsStringAsync();
                    }
                }
                throw new NoConectionException();
            }
        }
    }
}
