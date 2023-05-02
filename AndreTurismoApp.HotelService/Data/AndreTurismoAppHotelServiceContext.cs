﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.HotelService.Data
{
    public class AndreTurismoAppHotelServiceContext : DbContext
    {
        public AndreTurismoAppHotelServiceContext (DbContextOptions<AndreTurismoAppHotelServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Hotel> Hotel { get; set; } = default!;
        public DbSet<AndreTurismoApp.Models.Address> Address { get; set; } = default!;
        public DbSet<AndreTurismoApp.Models.City> City { get; set; } = default!;
    }
}
