using System.Text;
using AndreTurismoApp.Models;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace AndreTurismoApp.Services
{
    public class CityService
    {
        static readonly HttpClient cityClient = new HttpClient();
        static readonly string endpoint = "https://localhost:5002/api/cities/";

        public async Task<City> Insert(City city)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(city), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await cityClient.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();
                string cityResp = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<City>(cityResp);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<List<City>> FindAll()
        {
            try
            {
                HttpResponseMessage response = await cityClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                string city = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<City>>(city);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<City> FindById(int id)
        {
            try
            {
                HttpResponseMessage response = await cityClient.GetAsync(endpoint + id);
                response.EnsureSuccessStatusCode();
                string city = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<City>(city);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<City> Update(int id, City newCity)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(newCity), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await cityClient.PutAsync(endpoint + id, content);
                response.EnsureSuccessStatusCode();
                string cityResp = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<City>(cityResp);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                HttpResponseMessage response = await cityClient.DeleteAsync(endpoint + id);
                response.EnsureSuccessStatusCode();
                string city = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
