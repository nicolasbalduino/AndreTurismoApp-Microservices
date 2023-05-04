using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.CityService.Controllers;
using AndreTurismoApp.CityService.Data;
using AndreTurismoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AndreTurismoApp.Test
{
    public class UnitTestCity
    {
        private DbContextOptions<AndreTurismoAppCityServiceContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AndreTurismoAppCityServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;


            // Insert data into the database using one instance of the context
            using (var context = new AndreTurismoAppCityServiceContext(options))
            {
                context.City.Add(new City { Id = 1, Description = "City1" } );
                context.City.Add(new City { Id = 2, Description = "City2" } );
                context.City.Add(new City { Id = 3, Description = "City3" } );
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCityServiceContext(options))
            {
                CitiesController citiesController = new CitiesController(context);
                IEnumerable<City> city = citiesController.GetCity().Result.Value;

                Assert.Equal(3, city.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCityServiceContext(options))
            {
                int cityId = 2;
                CitiesController citiesController = new CitiesController(context);
                City city = citiesController.GetCity(cityId).Result.Value;
                Assert.Equal(2, city.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            City city = new City()
            {
                Id = 4,
                Description = "city 10"
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCityServiceContext(options))
            {
                CitiesController citiesController = new CitiesController(context);
                City ct = citiesController.PostCity(city).Result.Value;
                //Assert.Equal("Avenida Alberto Benassi", ad.Street);
                Assert.Equal("city 10", ct.Description);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            City city = new City()
            {
                Id = 3,
                Description = "city 10 Alterada"
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCityServiceContext(options))
            {
                CitiesController citiesController = new CitiesController(context);
                City ct = citiesController.PutCity(3, city).Result.Value;
                Assert.Equal("city 10 Alterada", ct.Description);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCityServiceContext(options))
            {
                CitiesController citiesController = new CitiesController(context);
                City city = citiesController.DeleteCity(2).Result.Value;
                Assert.Null(city);
            }
        }
    }
}
