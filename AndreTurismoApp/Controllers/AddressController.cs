using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private AddressService _addressService;
        private CityService _cityService;
        private PostOfficesService _postOfficeService;

        public AddressController()
        {
            _addressService = new AddressService();
            _cityService = new CityService();
            _postOfficeService = new PostOfficesService();
        }

        [HttpGet]
        public async Task<List<Address>> Listar() => await _addressService.FindAll();

        [HttpGet("{id}", Name = "ConsultarProdutoPorId")]
        public async Task<Address> ConsultarPorId(int id) => await _addressService.FindById(id);

        [HttpPost]
        public async Task<ActionResult<Address>> Inserir(Address address) => await _addressService.Insert(address);

        [HttpPut("{id}")]
        public async Task<Address> Atualizar(int id, Address address) => await _addressService.Update(id, address);

        [HttpDelete("{id}")]
        public async Task Deletar(int id) => await _addressService.Delete(id);
    }
}
