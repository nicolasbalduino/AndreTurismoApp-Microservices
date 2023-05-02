using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.PackageService.Data
{
    public class AndreTurismoAppPackageServiceContext : DbContext
    {
        public AndreTurismoAppPackageServiceContext (DbContextOptions<AndreTurismoAppPackageServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Package> Package { get; set; } = default!;
        public DbSet<AndreTurismoApp.Models.Hotel> Hotel { get; set; } = default!;
        public DbSet<AndreTurismoApp.Models.Ticket> Ticket { get; set; } = default!;
        public DbSet<AndreTurismoApp.Models.Client> Client { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>().HasOne(t => t.Origin).WithOne().HasForeignKey<Ticket>("OriginId").OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Ticket>().HasOne(t => t.Destination).WithOne().HasForeignKey<Ticket>("DestinationId").OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Ticket>().HasOne(t => t.Client).WithOne().HasForeignKey<Ticket>("ClientId").OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Package>().HasOne(p => p.Hotel).WithOne().HasForeignKey<Package>("HotelId").OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Package>().HasOne(p => p.Ticket).WithOne().HasForeignKey<Package>("TicketId").OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Package>().HasOne(p => p.Client).WithOne().HasForeignKey<Package>("ClientId").OnDelete(DeleteBehavior.NoAction);
        }
    }
}
