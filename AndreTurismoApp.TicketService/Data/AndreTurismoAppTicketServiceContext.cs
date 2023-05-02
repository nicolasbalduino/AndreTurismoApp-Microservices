using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.TicketService.Data
{
    public class AndreTurismoAppTicketServiceContext : DbContext
    {
        public AndreTurismoAppTicketServiceContext (DbContextOptions<AndreTurismoAppTicketServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Ticket> Ticket { get; set; } = default!;
        public DbSet<AndreTurismoApp.Models.City> City { get; set; } = default!;
        public DbSet<AndreTurismoApp.Models.Address> Address { get; set; } = default!;
        public DbSet<AndreTurismoApp.Models.Client> Client { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>().HasOne(t => t.Origin).WithOne().HasForeignKey<Ticket>("OriginId").OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Ticket>().HasOne(t => t.Destination).WithOne().HasForeignKey<Ticket>("DestinationId").OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Ticket>().HasOne(t => t.Client).WithOne().HasForeignKey<Ticket>("ClientId").OnDelete(DeleteBehavior.NoAction);
        }
    }
}
