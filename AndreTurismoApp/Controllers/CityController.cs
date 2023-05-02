using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private CityService _cityService;

        public CityController()
        {
            _cityService = new CityService();
        }

        [HttpGet]
        public Task<List<City>> Get() => _cityService.FindAll();

        [HttpGet("{id}", Name = "FindById")]
        public async Task<City> ConsultarPorId(int id) => await _cityService.FindById(id);

        [HttpPost]
        public async Task<City> Inserir(City city) => await _cityService.Insert(city);

        [HttpPut("{id}")]
        public async Task<City> Atualizar(int id, City city) => await _cityService.Update(id, city);

        [HttpDelete("{id}")]
        public async Task Deletar(int id) => await _cityService.Delete(id);
    }
}
