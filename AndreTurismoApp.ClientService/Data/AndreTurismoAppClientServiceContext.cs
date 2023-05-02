using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.ClientService.Data
{
    public class AndreTurismoAppClientServiceContext : DbContext
    {
        public AndreTurismoAppClientServiceContext (DbContextOptions<AndreTurismoAppClientServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Client> Client { get; set; } = default!;
        public DbSet<AndreTurismoApp.Models.Address> Address { get; set; } = default!;
        public DbSet<AndreTurismoApp.Models.City> City { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasOne(c => c.Address).WithOne().HasForeignKey<Client>("AddressId").OnDelete(DeleteBehavior.NoAction);
        }
    }
}
