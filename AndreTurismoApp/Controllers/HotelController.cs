using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private HotelService _hotelService;

        public HotelController()
        {
            _hotelService = new HotelService();
        }

        [HttpGet]
        public Task<List<Hotel>> Get() => _hotelService.FindAll();

        [HttpGet("{id}", Name = "FindHotelById")]
        public async Task<Hotel> ConsultarPorId(int id) => await _hotelService.FindById(id);

        [HttpPost]
        public async Task<Hotel> Inserir(Hotel hotel) => await _hotelService.Insert(hotel);

        [HttpPut("{id}")]
        public async Task<Hotel> Atualizar(int id, Hotel hotel) => await _hotelService.Update(id, hotel);

        [HttpDelete("{id}")]
        public async Task Deletar(int id) => await _hotelService.Delete(id);
    }
}
