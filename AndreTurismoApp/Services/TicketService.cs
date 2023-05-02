using AndreTurismoApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace AndreTurismoApp.Services
{
    public class TicketService
    {
        static readonly HttpClient ticketClient = new HttpClient();
        static readonly string endpoint = "https://localhost:5005/api/tickets/";

        public async Task<Ticket> Insert(Ticket ticket)
        {
            HttpContent contentOrigin = new StringContent(JsonConvert.SerializeObject(ticket.Origin), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ticketClient.PostAsync("https://localhost:5001/api/Addresses", contentOrigin);
                response.EnsureSuccessStatusCode();
                string addressResp = await response.Content.ReadAsStringAsync();
                ticket.Origin = JsonConvert.DeserializeObject<Address>(addressResp);
            }
            catch (HttpRequestException e)
            {
                throw;
            }

            HttpContent contentDestination = new StringContent(JsonConvert.SerializeObject(ticket.Destination), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ticketClient.PostAsync("https://localhost:5001/api/Addresses", contentDestination);
                response.EnsureSuccessStatusCode();
                string addressResp = await response.Content.ReadAsStringAsync();
                ticket.Destination = JsonConvert.DeserializeObject<Address>(addressResp);
            }
            catch (HttpRequestException e)
            {
                throw;
            }

            HttpContent contentClient = new StringContent(JsonConvert.SerializeObject(ticket.Client), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ticketClient.PostAsync("https://localhost:5004/api/clients", contentClient);
                response.EnsureSuccessStatusCode();
                string addressResp = await response.Content.ReadAsStringAsync();
                ticket.Client = JsonConvert.DeserializeObject<Client>(addressResp);
            }
            catch (HttpRequestException e)
            {
                throw;
            }

            HttpContent content = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ticketClient.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();
                string ticketResp = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Ticket>(ticketResp);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<List<Ticket>> FindAll()
        {
            try
            {
                HttpResponseMessage response = await ticketClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                string ticket = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Ticket>>(ticket);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Ticket> FindById(int id)
        {
            try
            {
                HttpResponseMessage response = await ticketClient.GetAsync(endpoint + id);
                response.EnsureSuccessStatusCode();
                string ticket = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Ticket>(ticket);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Ticket> Update(int id, Ticket newTicket)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(newTicket), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ticketClient.PutAsync(endpoint + id, content);
                response.EnsureSuccessStatusCode();
                string ticketResp = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Ticket>(ticketResp);
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
                HttpResponseMessage response = await ticketClient.DeleteAsync(endpoint + id);
                response.EnsureSuccessStatusCode();
                string ticket = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
