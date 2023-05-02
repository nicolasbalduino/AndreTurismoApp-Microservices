using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private string Conn { get; set; }

        public AddressRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["StringConnection"].ConnectionString;
        }

        public Address Insert(Address address)
        {
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                ICityRepository cityRepository = new CityRepository();
                address.City = cityRepository.Insert(address.City);
                address.Id = (int)db.ExecuteScalar(Address.INSERT, new
                {
                    address.Street,
                    address.Number,
                    address.Neighborhood,
                    address.PostalCode,
                    address.Complement,
                    IdCity = address.City.Id
                });
                db.Close();
            }
            return address;
        }

        public List<Address> FindAll()
        {
            var results = new List<Address>();
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                results = db.Query<Address, City, Address>(Address.SELECTALL, 
                    (a, c) => { a.City = c; return a; }, 
                    splitOn: "Id").ToList();
                db.Close();
            }
            return results;
        }

        public Address FindById(int Id)
        {
            var results = new Address();
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                results = db.Query<Address, City, Address>( // mapeamento de address e city, retorna address
                    Address.SELECTID, // minha query
                    (address, city) => 
                    { 
                        address.City = city; 
                        return address; 
                    }, // vincula address com city
                    new { Id }, // parametros da query
                    splitOn: "Id").FirstOrDefault();
                db.Close();
            }
            return results;
        }

        public int Update(int id, Address newAddress)
        {
            var results = 0;
            using (var db = new SqlConnection(Conn))
            {
                newAddress.Id = id;
                db.Open();
                results = db.Execute(Address.UPDATE, new 
                {
                    newAddress.Id,
                    newAddress.Street,
                    newAddress.Number,
                    newAddress.Complement,
                    newAddress.PostalCode,
                    newAddress.Neighborhood,
                    IdCity = newAddress.City.Id,
                });
                db.Close();
            }
            return results;
        }

        public int Delete(int id)
        {
            var results = 0;
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                results = db.Execute(Address.DELETE, new { id });
                db.Close();
            }
            return results;
        }
    }
}
