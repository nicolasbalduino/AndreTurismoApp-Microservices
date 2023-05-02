using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;
using AndreTurismoApp.PackageService.Data;
using System.Net.Sockets;

namespace AndreTurismoApp.PackageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly AndreTurismoAppPackageServiceContext _context;

        public PackagesController(AndreTurismoAppPackageServiceContext context)
        {
            _context = context;
        }

        // GET: api/Packages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Package>>> GetPackage()
        {
          if (_context.Package == null)
          {
              return NotFound();
          }
            return await _context.Package.ToListAsync();
        }

        // GET: api/Packages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackage(int id)
        {
          if (_context.Package == null)
          {
              return NotFound();
          }
            var package = await _context.Package.FindAsync(id);

            if (package == null)
            {
                return NotFound();
            }

            return package;
        }

        // PUT: api/Packages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackage(int id, Package package)
        {
            if (id != package.Id)
            {
                return BadRequest();
            }

            _context.Entry(package).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Packages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Package>> PostPackage(Package package)
        {
            if (_context.Package == null)
            {
                return Problem("Entity set 'AndreTurismoAppPackageServiceContext.Package'  is null.");
            }

            var packageExists = await _context.Package.Where(p => p.Id == package.Id).FirstOrDefaultAsync();
            if (packageExists != null) return packageExists;

            var hotel = await _context.Hotel.Where(h => h.Id == package.Hotel.Id).FirstOrDefaultAsync();
            if (hotel == null)
            {
                hotel = new Hotel();
                hotel = package.Hotel;
            }
            package.Hotel = hotel;

            var client = await _context.Client.Where(c => c.Id == package.Client.Id).FirstOrDefaultAsync();
            if (client == null)
            {
                client = new Client();
                client = package.Client;
            }
            package.Client = client;

            var ticket = await _context.Ticket.Where(t => t.Id == package.Ticket.Id).FirstOrDefaultAsync();
            if (ticket == null)
            {
                ticket = new Ticket();
                ticket = package.Ticket;
            }
            package.Ticket = ticket;

            _context.Package.Add(package);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPackage", new { id = package.Id }, package);
        }

        // DELETE: api/Packages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            if (_context.Package == null)
            {
                return NotFound();
            }
            var package = await _context.Package.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }

            _context.Package.Remove(package);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackageExists(int id)
        {
            return (_context.Package?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
