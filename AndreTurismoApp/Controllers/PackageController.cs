using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private PackageService _packageService;

        public PackageController()
        {
            _packageService = new PackageService();
        }

        [HttpGet]
        public Task<List<Package>> Get() => _packageService.FindAll();

        [HttpGet("{id}", Name = "FindPackageById")]
        public async Task<Package> ConsultarPorId(int id) => await _packageService.FindById(id);

        [HttpPost]
        public async Task<Package> Inserir(Package package) => await _packageService.Insert(package);

        [HttpPut("{id}")]
        public async Task<Package> Atualizar(int id, Package package) => await _packageService.Update(id, package);

        [HttpDelete("{id}")]
        public async Task Deletar(int id) => await _packageService.Delete(id);
    }
}
