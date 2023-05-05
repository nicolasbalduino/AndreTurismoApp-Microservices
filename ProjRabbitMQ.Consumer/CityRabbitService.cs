using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;
using Newtonsoft.Json;

namespace AndreTurismoApp.Consumer
{
    public class CityRabbitService
    {
        static readonly HttpClient cityClient = new HttpClient();
        static readonly string endpoint = "https://localhost:5002/api/cities/";

        public async Task<City> Insert(City city)
        {
            try
            {
                HttpResponseMessage response = await cityClient.PostAsJsonAsync(endpoint, city);
                response.EnsureSuccessStatusCode();
                string cityResp = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<City>(cityResp);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
