using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private string Conn { get; set; }

        public HotelRepository() 
        { 
            Conn = ConfigurationManager.ConnectionStrings["StringConnection"].ConnectionString;
        }

        public int Insert(Hotel hotel)
        {
            int result = 0;
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                IAddressRepository addressRepository = new AddressRepository();
                hotel.Address = addressRepository.Insert(hotel.Address);
                result = db.Execute(Hotel.INSERT, new
                {
                    hotel.Name,
                    hotel.Price,
                    IdAddress = hotel.Address.Id
                });
                db.Close();
            }
            return result;
        }

        public List<Hotel> FindAll()
        {
            var results = new List<Hotel>();
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                results = db.Query<Hotel, Address, City, Hotel>(Hotel.SELECTALL,
                    (hotel, address, city) => { hotel.Address = address; address.City = city; return hotel; },
                    splitOn: "Id, Id").ToList();
                db.Close();
            }
            return results;
        }

        public Hotel FindById(int id)
        {
            var results = new Hotel();
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                results = db.Query<Hotel, Address, City, Hotel>(Hotel.SELECTID,
                    (hotel, address, city) => { hotel.Address = address; address.City = city; return hotel; }, new { id },
                    splitOn: "Id, Id").FirstOrDefault();
                db.Close();
            }
            return results;
        }

        public Hotel FindByName(string name)
        {
            var results = new Hotel();
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                results = db.Query<Hotel, Address, City, Hotel>(Hotel.SELECTNAME,
                    (hotel, address, city) => { hotel.Address = address; address.City = city; return hotel; }, new { name },
                    splitOn: "Id, Id").FirstOrDefault();
                db.Close();
            }
            return results;
        }

        public int Update(int id, Hotel newData)
        {
            int result = 0;
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                newData.Id = id;
                result = db.Execute(Hotel.UPDATE, new
                {
                    newData.Id,
                    newData.Name,
                    newData.Price,
                    IdAddress = newData.Address.Id
                });
                db.Close();
            }
            return result;
        }

        public int Delete(int id)
        {
            int result = 0;
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                result = db.Execute(Hotel.DELETE, new { id });
                db.Close();
            }
            return result;
        }
    }
}
