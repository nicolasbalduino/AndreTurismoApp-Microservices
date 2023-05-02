using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private TicketService _ticketService;

        public TicketController()
        {
            _ticketService = new TicketService();
        }

        [HttpGet]
        public Task<List<Ticket>> Get() => _ticketService.FindAll();

        [HttpGet("{id}", Name = "FindTicketById")]
        public async Task<Ticket> ConsultarPorId(int id) => await _ticketService.FindById(id);

        [HttpPost]
        public async Task<Ticket> Inserir(Ticket ticket) => await _ticketService.Insert(ticket);

        [HttpPut("{id}")]
        public async Task<Ticket> Atualizar(int id, Ticket ticket) => await _ticketService.Update(id, ticket);

        [HttpDelete("{id}")]
        public async Task Deletar(int id) => await _ticketService.Delete(id);
    }
}
