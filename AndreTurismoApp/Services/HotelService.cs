using AndreTurismoApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace AndreTurismoApp.Services
{
    public class HotelService
    {
        static readonly HttpClient hotelClient = new HttpClient();
        static readonly string endpoint = "https://localhost:5003/api/hotels/";
        static readonly AddressService _addressService = new AddressService();

        public async Task<Hotel> Insert(Hotel hotel)
        {
            Address hoteladdress = await _addressService.Insert(hotel.Address);
            if (hoteladdress == null) return null;
            hotel.Address = hoteladdress;

            try
            {
                HttpResponseMessage response = await hotelClient.PostAsJsonAsync(endpoint, hotel);
                response.EnsureSuccessStatusCode();
                string hotelResp = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Hotel>(hotelResp);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<List<Hotel>> FindAll()
        {
            try
            {
                HttpResponseMessage response = await hotelClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                string hotel = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Hotel>>(hotel);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Hotel> FindById(int id)
        {
            try
            {
                HttpResponseMessage response = await hotelClient.GetAsync(endpoint + id);
                response.EnsureSuccessStatusCode();
                string hotel = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Hotel>(hotel);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Hotel> Update(int id, Hotel newHotel)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(newHotel), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await hotelClient.PutAsync(endpoint + id, content);
                response.EnsureSuccessStatusCode();
                string hotelResp = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Hotel>(hotelResp);
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
                HttpResponseMessage response = await hotelClient.DeleteAsync(endpoint + id);
                response.EnsureSuccessStatusCode();
                string hotel = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
