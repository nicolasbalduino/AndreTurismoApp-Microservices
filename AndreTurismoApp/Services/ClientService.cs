using AndreTurismoApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace AndreTurismoApp.Services
{
    public class ClientService
    {
        static readonly HttpClient clientClient = new HttpClient();
        static readonly string endpoint = "https://localhost:5004/api/clients/";

        public async Task<Client> Insert(Client client)
        {
            //HttpContent contentAddress = new StringContent(JsonConvert.SerializeObject(client.Address), Encoding.UTF8, "application/json");
            //try
            //{
            //    HttpResponseMessage response = await clientClient.PostAsync("https://localhost:5001/api/Addresses", contentAddress);
            //    response.EnsureSuccessStatusCode();
            //    string hotelResp = await response.Content.ReadAsStringAsync();
            //    client.Address = JsonConvert.DeserializeObject<Address>(hotelResp);
            //}
            //catch (HttpRequestException e)
            //{
            //    throw;
            //}

            try
            {
                HttpResponseMessage response = await clientClient.PostAsJsonAsync(endpoint, client);
                response.EnsureSuccessStatusCode();
                string clientResp = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Client>(clientResp);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<List<Client>> FindAll()
        {
            try
            {
                HttpResponseMessage response = await clientClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                string client = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Client>>(client);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Client> FindById(int id)
        {
            try
            {
                HttpResponseMessage response = await clientClient.GetAsync(endpoint + id);
                response.EnsureSuccessStatusCode();
                string client = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Client>(client);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Client> Update(int id, Client newClient)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(newClient), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await clientClient.PutAsync(endpoint + id, content);
                response.EnsureSuccessStatusCode();
                string clientResp = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Client>(clientResp);
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
                HttpResponseMessage response = await clientClient.DeleteAsync(endpoint + id);
                response.EnsureSuccessStatusCode();
                string client = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
