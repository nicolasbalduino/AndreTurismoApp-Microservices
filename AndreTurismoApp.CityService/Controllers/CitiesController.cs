using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.CityService.Data;
using AndreTurismoApp.Models;
using Newtonsoft.Json;
using System.Net;
using System.Reflection.Metadata;

namespace AndreTurismoApp.CityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly AndreTurismoAppCityServiceContext _context;

        public CitiesController(AndreTurismoAppCityServiceContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCity()
        {
          if (_context.City == null)
          {
              return NotFound();
          }
            return await _context.City.ToListAsync();
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            if (_context.City == null)
            {
                return NotFound();
            }
            var city = await _context.City.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // GET: api/Cities/5
        [HttpGet("name/{name}", Name = "GetCityByName")]
        public async Task<ActionResult<City>> GetCityByName(string name)
        {
            if (_context.City == null)
            {
                return NotFound();
            }
            var city = await _context.City.Where(c => c.Description == name).FirstOrDefaultAsync();

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // PUT: api/Cities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<City>> PutCity(int id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return city;
        }

        // POST: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/api​/Cities/rabbit/", Name = "InsertRabbit")]
        public async Task<ActionResult<City>> PostCityRabbit(City city)
        {
            try
            {
                string endpoint = "https://localhost:7195/api/Cities";
                HttpClient cityClient = new HttpClient();
                HttpResponseMessage response = await cityClient.PostAsJsonAsync(endpoint, city);
                response.EnsureSuccessStatusCode();
                string cityResp = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<City>(cityResp);
            }
            catch (Exception e)
            {

                throw;
            }

            return NotFound();

        }

        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
            if (_context.City == null)
            {
                return Problem("Entity set 'AndreTurismoAppCityServiceContext.City'  is null.");
            }
            _context.City.Add(city);
            await _context.SaveChangesAsync();

            return city;
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<City>> DeleteCity(int id)
        {
            if (_context.City == null)
            {
                return NotFound();
            }
            var city = await _context.City.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.City.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(int id)
        {
            return (_context.City?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
