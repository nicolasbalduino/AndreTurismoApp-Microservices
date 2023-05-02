using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private ClientService _clientService;

        public ClientController()
        {
            _clientService = new ClientService();
        }

        [HttpGet]
        public Task<List<Client>> Get() => _clientService.FindAll();

        [HttpGet("{id}", Name = "FindClientById")]
        public async Task<Client> ConsultarPorId(int id) => await _clientService.FindById(id);

        [HttpPost]
        public async Task<Client> Inserir(Client client) => await _clientService.Insert(client);

        [HttpPut("{id}")]
        public async Task<Client> Atualizar(int id, Client client) => await _clientService.Update(id, client);

        [HttpDelete("{id}")]
        public async Task Deletar(int id) => await _clientService.Delete(id);
    }
}
