﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.HotelService.Data;
using AndreTurismoApp.Models;
using AndreTurismoApp.Models.DTO;

namespace AndreTurismoApp.HotelService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly AndreTurismoAppHotelServiceContext _context;

        public HotelsController(AndreTurismoAppHotelServiceContext context)
        {
            _context = context;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotel()
        {
          if (_context.Hotel == null)
          {
              return NotFound();
          }
            return await _context.Hotel.ToListAsync();
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
          if (_context.Hotel == null)
          {
              return NotFound();
          }
            var hotel = await _context.Hotel.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Hotel>> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return hotel;
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            if (_context.Hotel == null)
            {
                return Problem("Entity set 'AndreTurismoAppHotelServiceContext.Hotel'  is null.");
            }

            var hotelExists = await _context.Hotel.Where(h => h.Id == hotel.Id).FirstOrDefaultAsync();
            if (hotelExists != null) return hotelExists;

            var address = await _context.Address.Where(a => a.Id == hotel.Address.Id).FirstOrDefaultAsync();
            if (address == null)
            {
                address = new Address();
                address = hotel.Address;
            }
            hotel.Address = address;

            _context.Hotel.Add(hotel);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
            return hotel;
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hotel>> DeleteHotel(int id)
        {
            if (_context.Hotel == null)
            {
                return NotFound();
            }
            var hotel = await _context.Hotel.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelExists(int id)
        {
            return (_context.Hotel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
