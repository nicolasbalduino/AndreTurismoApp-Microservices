using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.AddressService.Data;
using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using AndreTurismoApp.Models.DTO;
using Newtonsoft.Json;
using System.Runtime.ConstrainedExecution;
using System.Net;

namespace AndreTurismoApp.AddressService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly AndreTurismoAppAddressServiceContext _context;
        private readonly PostOfficesService _postOfficesService;

        public AddressesController(AndreTurismoAppAddressServiceContext context, PostOfficesService postOfficesService)
        {
            _context = context;
            _postOfficesService = postOfficesService;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
            if (_context.Address == null)
            {
                return NotFound();
            }
            return await _context.Address.Include(a => a.City).ToListAsync();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            if (_context.Address == null)
            {
                return NotFound();
            }
            var address = await _context.Address.Include(a => a.City).Where(a => a.Id == id).FirstOrDefaultAsync();

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // GET: api/cep/15910000
        //[HttpGet("{cep}")]
        //public async Task<ActionResult<AddressDTO>> GetAddress(string cep)
        //{
        //    return await _postOfficesService.GetAddress(cep);
        //}

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Address>> PutAddress(int id, Address address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }

            var newAddress = await _context.Address.Include(a => a.City).Where(a => a.Id == id).FirstOrDefaultAsync();
            newAddress.Street = address.Street;
            newAddress.City = address.City;
            newAddress.PostalCode = address.PostalCode;
            newAddress.Number = address.Number;
            newAddress.Neighborhood = address.Neighborhood;
            newAddress.Complement = address.Complement;

            _context.Entry(newAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return await _context.Address.Include(a => a.City).Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            if (_context.Address == null)
            {
                return Problem("Entity set 'AndreTurismoAppAddressServiceContext.Address'  is null.");
            }

            if (address.Id != 0)
            {
                var addressExists = await _context.Address.Where(a => a.Id == address.Id).FirstOrDefaultAsync();
                if (addressExists != null) return addressExists;
            }

            //AddressDTO addressDTO = await _postOfficesService.GetAddress(address.PostalCode);
            //address.Neighborhood = addressDTO.Bairro != "" ? addressDTO.Bairro : address.Neighborhood;
            //address.Street = addressDTO.Logradouro != "" ? addressDTO.Logradouro : address.Street;

            var city = await _context.City.Where(c => c.Description == address.City.Description).FirstOrDefaultAsync();
            if (city == null) city = new City() { Description = address.City.Description };
            address.City = city;

            _context.Address.Add(address);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetAddress", new { id = address.Id }, address);
            //address = await _context.Address.Include(a => a.City).Where(a => a.Id == address.Id).FirstOrDefaultAsync();
            return address;
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            if (_context.Address == null)
            {
                return NotFound();
            }
            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Address.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return (_context.Address?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
