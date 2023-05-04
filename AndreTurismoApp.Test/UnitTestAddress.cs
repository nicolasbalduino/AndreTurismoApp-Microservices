using AndreTurismoApp.AddressService.Controllers;
using AndreTurismoApp.AddressService.Data;
using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AndreTurismoApp.Test
{
    public class UnitTestAddress
    {
        private DbContextOptions<AndreTurismoAppAddressServiceContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AndreTurismoAppAddressServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;


            // Insert data into the database using one instance of the context
            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                context.Address.Add(new Address { Id = 1, Street = "Street 1", Number = 1, Neighborhood = "Bairro", PostalCode = "12345678", Complement = "A", City = new City() { Id = 1, Description = "City1" } });
                context.Address.Add(new Address { Id = 2, Street = "Street 2", Number = 2, Neighborhood = "Bairro", PostalCode = "87654321", Complement = "A", City = new City() { Id = 2, Description = "City2" } });
                context.Address.Add(new Address { Id = 3, Street = "Street 3", Number = 3, Neighborhood = "Bairro", PostalCode = "15964784", Complement = "A", City = new City() { Id = 3, Description = "City3" } });
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                AddressesController addressController = new AddressesController(context, null);
                IEnumerable<Address> addresses = addressController.GetAddress().Result.Value;

                Assert.Equal(3, addresses.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                int addressId = 2;
                AddressesController addressController = new AddressesController(context, null);
                Address address = addressController.GetAddress(addressId).Result.Value;
                Assert.Equal(2, address.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Address address = new Address()
            {
                Id = 4,
                Street = "Rua 10",
                Number = 4,
                Neighborhood = "Bairro4",
                PostalCode = "14804300",
                Complement = "AAAA",
                City = new() { Id = 10, Description = "City 10" }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                AddressesController addressController = new AddressesController(context, new PostOfficesService());
                Address ad = addressController.PostAddress(address).Result.Value;
                //Assert.Equal("Avenida Alberto Benassi", ad.Street);
                Assert.Equal("Rua 10", ad.Street);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Address address = new Address()
            {
                Id = 3,
                Street = "Rua 10 Alterada",
                Number = 3,
                Neighborhood = "Bairro3",
                PostalCode = "14804300",
                Complement = "AAA",
                City = new() { Id = 2, Description = "City 2" }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                AddressesController addressController = new AddressesController(context, null);
                Address ad = addressController.PutAddress(3, address).Result.Value;
                Assert.Equal("Rua 10 Alterada", ad.Street);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                AddressesController addressController = new AddressesController(context, null);
                Address address = addressController.DeleteAddress(2).Result.Value;
                Assert.Null(address);
            }
        }
    }
}