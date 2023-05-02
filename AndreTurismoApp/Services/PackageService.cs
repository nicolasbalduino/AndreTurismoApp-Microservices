using AndreTurismoApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace AndreTurismoApp.Services
{
    public class PackageService
    {
        static readonly HttpClient packageClient = new HttpClient();
        static readonly string endpoint = "https://localhost:5006/api/packages/";

        public async Task<Package> Insert(Package package)
        {
            //HttpContent contentHotel = new StringContent(JsonConvert.SerializeObject(package.Hotel), Encoding.UTF8, "application/json");
            //try
            //{
            //    HttpResponseMessage response = await packageClient.PostAsync("https://localhost:5003/api/hotels", contentHotel);
            //    response.EnsureSuccessStatusCode();
            //    string hotelResp = await response.Content.ReadAsStringAsync();
            //    package.Hotel = JsonConvert.DeserializeObject<Hotel>(hotelResp);
            //}
            //catch (HttpRequestException e)
            //{
            //    throw;
            //}

            //HttpContent contentClient = new StringContent(JsonConvert.SerializeObject(package.Client), Encoding.UTF8, "application/json");
            //try
            //{
            //    HttpResponseMessage response = await packageClient.PostAsync("https://localhost:5004/api/clients", contentClient);
            //    response.EnsureSuccessStatusCode();
            //    string addressResp = await response.Content.ReadAsStringAsync();
            //    package.Client = JsonConvert.DeserializeObject<Client>(addressResp);
            //}
            //catch (HttpRequestException e)
            //{
            //    throw;
            //}

            //HttpContent contentTicket = new StringContent(JsonConvert.SerializeObject(package.Ticket), Encoding.UTF8, "application/json");
            //try
            //{
            //    HttpResponseMessage response = await packageClient.PostAsync("https://localhost:5005/api/tickets", contentTicket);
            //    response.EnsureSuccessStatusCode();
            //    string ticketResp = await response.Content.ReadAsStringAsync();
            //    package.Ticket = JsonConvert.DeserializeObject<Ticket>(ticketResp);
            //}
            //catch (HttpRequestException e)
            //{
            //    throw;
            //}

            //HttpContent content = new StringContent(JsonConvert.SerializeObject(package), Encoding.UTF8, "application/json");
            try
            {
                //HttpResponseMessage response = await packageClient.PostAsync("https://localhost:5006/api/packages/", content);
                HttpResponseMessage response = new HttpResponseMessage();
                 response = await packageClient.PostAsJsonAsync(endpoint, package);
                response.EnsureSuccessStatusCode();
                string packageResp = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Package>(packageResp);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<List<Package>> FindAll()
        {
            try
            {
                HttpResponseMessage response = await packageClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                string package = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Package>>(package);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Package> FindById(int id)
        {
            try
            {
                HttpResponseMessage response = await packageClient.GetAsync(endpoint + id);
                response.EnsureSuccessStatusCode();
                string package = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Package>(package);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Package> Update(int id, Package newPackage)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(newPackage), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await packageClient.PutAsync(endpoint + id, content);
                response.EnsureSuccessStatusCode();
                string packageResp = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Package>(packageResp);
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
                HttpResponseMessage response = await packageClient.DeleteAsync(endpoint + id);
                response.EnsureSuccessStatusCode();
                string package = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
