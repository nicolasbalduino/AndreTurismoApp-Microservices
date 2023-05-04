using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.HotelService.Controllers;
using AndreTurismoApp.HotelService.Data;
using AndreTurismoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AndreTurismoApp.Test
{
    public class UnitTestHotel
    {
        private DbContextOptions<AndreTurismoAppHotelServiceContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AndreTurismoAppHotelServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;


            // Insert data into the database using one instance of the context
            using (var context = new AndreTurismoAppHotelServiceContext(options))
            {
                context.Hotel.Add(new Hotel { Id = 1, Name = "Hotel 1", Price = 100, Address = new Address { Id = 1, Street = "Street 1", Number = 1, Neighborhood = "Bairro", PostalCode = "12345678", Complement = "A", City = new City() { Id = 1, Description = "City1" } } });
                context.Hotel.Add(new Hotel { Id = 2, Name = "Hotel 2", Price = 200, Address = new Address { Id = 2, Street = "Street 2", Number = 2, Neighborhood = "Bairro", PostalCode = "87654321", Complement = "A", City = new City() { Id = 2, Description = "City2" } } });
                context.Hotel.Add(new Hotel { Id = 3, Name = "Hotel 3", Price = 300, Address = new Address { Id = 3, Street = "Street 3", Number = 3, Neighborhood = "Bairro", PostalCode = "15964784", Complement = "A", City = new City() { Id = 3, Description = "City3" } } });
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppHotelServiceContext(options))
            {
                HotelsController hotelController = new HotelsController(context);
                IEnumerable<Hotel> addresses = hotelController.GetHotel().Result.Value;

                Assert.Equal(3, addresses.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppHotelServiceContext(options))
            {
                int hotelId = 2;
                HotelsController hotelController = new HotelsController(context);
                Hotel hotel = hotelController.GetHotel(hotelId).Result.Value;
                Assert.Equal(2, hotel.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Hotel hotel = new Hotel()
            {
                Id = 4,
                Name = "Hotel 4",
                Price = 400,
                Address = new Address { 
                    Id = 4, 
                    Street = "Street 4", 
                    Number = 4, 
                    Neighborhood = "Bairro4", 
                    PostalCode = "12345678", 
                    Complement = "AAAA", 
                    City = new City() { Id = 4, Description = "City4" } 
                }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppHotelServiceContext(options))
            {
                HotelsController hotelController = new HotelsController(context);
                Hotel ht = hotelController.PostHotel(hotel).Result.Value;
                Assert.Equal("Hotel 4", ht.Name);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();
            Hotel hotel = new Hotel()
            {
                Id = 3,
                Name = "Hotel 4 Alterado",
                Price = 400,
                Address = new Address
                {
                    Id = 4,
                    Street = "Street 4",
                    Number = 4,
                    Neighborhood = "Bairro4",
                    PostalCode = "12345678",
                    Complement = "AAAA",
                    City = new City() { Id = 1, Description = "City4" }
                }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppHotelServiceContext(options))
            {
                HotelsController hotelController = new HotelsController(context);
                Hotel ht = hotelController.PutHotel(3, hotel).Result.Value;
                Assert.Equal("Hotel 4 Alterado", ht.Name);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppHotelServiceContext(options))
            {
                HotelsController hotelController = new HotelsController(context);
                Hotel hotel = hotelController.DeleteHotel(2).Result.Value;
                Assert.Null(hotel);
            }
        }
    }
}
