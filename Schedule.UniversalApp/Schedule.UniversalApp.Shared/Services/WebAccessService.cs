using System.Net.Http.Headers;
using System.Text;
using Windows.UI.Xaml.Media;
using Schedule.UniversalApp.Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Schedule.UniversalApp.Services
{
    public class WebAccessService
    {
        public async Task<string> GetAsync(Uri uri)
        {
            using (var client = new HttpClient())
            {
                //Temprorary discard cache.
                client.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");
                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    if (!response.IsSuccessStatusCode) throw new NoConectionException();
                    using (HttpContent content = response.Content)
                    {
                        return await content.ReadAsStringAsync();
                    }
                }
            }           
        }

        public async Task<string> PostAsync( Uri uri, string content)
        {
            using (var client = new HttpClient())
            using (HttpResponseMessage response = await client.PostAsync(uri, new StringContent(content, Encoding.UTF8, "application/json")))
            {
                return response.IsSuccessStatusCode ? "Success" : "Error";
            }
        }
    }
}
