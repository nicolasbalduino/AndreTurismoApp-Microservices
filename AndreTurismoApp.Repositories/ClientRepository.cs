using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dapper;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private string Conn { get; set; }
        public ClientRepository() 
        {
            Conn = ConfigurationManager.ConnectionStrings["StringConnection"].ConnectionString;
        }

        public int Insert(Client client)
        {
            int result = 0;
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                IAddressRepository clientAddressRepository = new AddressRepository();
                client.Address = clientAddressRepository.Insert(client.Address);
                result = (int)db.ExecuteScalar(Client.INSERT, new
                {
                    client.Name,
                    client.Phone,
                    IdAddress = client.Address.Id
                });
                db.Close();
            }
            return result;
        }

        public List<Client> FindAll()
        {
            var results = new List<Client>();
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                results = db.Query<Client, Address, City, Client>(Client.SELECTALL,
                    (client, address, city) => { client.Address = address; address.City = city; return client; },
                    splitOn: "Id, Id").ToList();
                db.Close();
            }
            return results;
        }

        public Client FindById(int id)
        {
            var results = new Client();
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                results = db.Query<Client, Address, City, Client>(Client.SELECTID,
                    (client, address, city) => { client.Address = address; address.City = city; return client; },
                    new { id },
                    splitOn: "Id, Id").FirstOrDefault();
                db.Close();
            }
            return results;
        }

        public List<Client> FindByName(string name)
        {
            var results = new List<Client>();
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                results = db.Query<Client, Address, City, Client>(Client.SELECTNAME,
                    (client, address, city) => { client.Address = address; address.City = city; return client; },
                    new {name},
                    splitOn: "Id, Id").ToList();
                db.Close();
            }
            return results;
        }

        public int Update(int id, Client newData)
        {
            int result = 0;
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                newData.Id = id;
                result = db.Execute(Client.UPDATE, new
                {
                    newData.Id,
                    newData.Name,
                    newData.Phone,
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
                result = db.Execute(Client.DELETE, new { id });
                db.Close();
            }
            return result;
        }
    }
}
