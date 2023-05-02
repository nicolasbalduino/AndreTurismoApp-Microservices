using AndreTurismoApp.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AndreTurismoApp.Services
{
    public class AddressService
    {
        static readonly HttpClient addressClient = new HttpClient();

        public async Task<Address> Insert(Address address)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(address), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await addressClient.PostAsync("https://localhost:5001/api/Addresses", content);
                response.EnsureSuccessStatusCode();
                string addressResp = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Address>(addressResp);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<List<Address>> FindAll()
        {
            try
            {
                HttpResponseMessage response = await addressClient.GetAsync("https://localhost:5001/api/Addresses");
                response.EnsureSuccessStatusCode();
                string address = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Address>>(address);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Address> FindById(int id)
        {
            try
            {
                HttpResponseMessage response = await addressClient.GetAsync("https://localhost:5001/api/Addresses/" + id);
                response.EnsureSuccessStatusCode();
                string address = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Address>(address);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Address> Update(int id, Address newAddress)
        {
            try
            {
                HttpResponseMessage response = await addressClient.PutAsJsonAsync("https://localhost:5001/api/Addresses/" + id, newAddress);
                response.EnsureSuccessStatusCode();
                string addressResp = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Address>(addressResp);
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
                HttpResponseMessage response = await addressClient.DeleteAsync("https://localhost:5001/api/Addresses/" + id);
                response.EnsureSuccessStatusCode();
                string address = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
