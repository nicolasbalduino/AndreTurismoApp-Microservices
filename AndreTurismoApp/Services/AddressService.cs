using AndreTurismoApp.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AndreTurismoApp.Services
{
    public class AddressService
    {
        static readonly HttpClient addressClient = new HttpClient();
        static readonly string endpoint = "https://localhost:5001/api/Addresses/";
        static readonly PostOfficesService _postOfficeService = new PostOfficesService();
        static readonly CityService _cityService = new CityService();

        public async Task<Address> Insert(Address address)
        {
            if (address.PostalCode != "")
            {
                var infoPc = _postOfficeService.GetAddress(address.PostalCode).Result;
                address.Street = infoPc.Logradouro != "" ? infoPc.Logradouro : address.Street;
                address.Neighborhood = infoPc.Bairro != "" ? infoPc.Bairro : address.Neighborhood;
                address.City.Description = infoPc.City;
            }
            City searchCity = await _cityService.FindByName(address.City.Description);
            if (searchCity != null) address.City.Id = searchCity.Id;

            try
            {
                HttpResponseMessage response = await addressClient.PostAsJsonAsync(endpoint, address);
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
