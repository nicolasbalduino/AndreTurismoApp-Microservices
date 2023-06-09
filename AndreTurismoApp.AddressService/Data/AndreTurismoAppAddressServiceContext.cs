﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.AddressService.Data
{
    public class AndreTurismoAppAddressServiceContext : DbContext
    {
        public AndreTurismoAppAddressServiceContext (DbContextOptions<AndreTurismoAppAddressServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Address> Address { get; set; } = default!;

        public DbSet<AndreTurismoApp.Models.City>? City { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasOne(a => a.City).WithOne().HasForeignKey<Address>("CityId").OnDelete(DeleteBehavior.NoAction);
        }
    }
}
