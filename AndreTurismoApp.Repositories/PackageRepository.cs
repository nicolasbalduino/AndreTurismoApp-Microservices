using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Repositories
{
    public class PackageRepository : IPackageRepository
    {
        private string Conn { get; set; }
        
        public PackageRepository()
        {
            Conn = Conn = ConfigurationManager.ConnectionStrings["StringConnection"].ConnectionString;
        }

        public int Insert(Package package)
        {
            int result = 0;
            using (var db = new SqlConnection(Conn))
            {
                db.Open();

                IHotelRepository hotelRepository = new HotelRepository();
                package.Hotel.Id = hotelRepository.Insert(package.Hotel);

                ITicketRepository ticketRepository = new TicketRepository();
                package.Ticket.Id = ticketRepository.Insert(package.Ticket);

                IClientRepository clientRepository = new ClientRepository();
                package.Client.Id = clientRepository.Insert(package.Client);

                result = (int)db.ExecuteScalar(Package.INSERT, new
                {
                    package.Price,
                    HotelId = package.Hotel.Id,
                    TicketId = package.Ticket.Id,
                    ClientId = package.Client.Id
                });
                db.Close();
            }
            return result;
        }

        public List<Package> FindAll()
        {
            var results = new List<Package>();
            using (var db = new SqlConnection(Conn))
            {
                
                db.Open();
                results = db.Query<Package>(Package.SELECTALL,
                    new[] 
                    {
                        typeof(Package),
                        typeof(Client),
                        typeof(Address),
                        typeof(City),
                        typeof(Hotel),
                        typeof(Address),
                        typeof(City),
                        typeof(Ticket),
                        typeof(Address),
                        typeof(City),
                        typeof(Address),
                        typeof(City),
                        typeof(Client),
                        typeof(Address),
                        typeof(City),
                    },
                    (obj => {
                        Package package = obj[0] as Package;
                        Client client = obj[1] as Client;
                        Address clientAddress = obj[2] as Address;
                        City ClientAddressCity = obj[3] as City;
                        Hotel hotel = obj[4] as Hotel;
                        Address hotelAddress = obj[5] as Address; 
                        City hotelAddressCity = obj[6] as City;
                        Ticket ticket = obj[7] as Ticket; 
                        Address addressOrigin = obj[8] as Address; 
                        City CityOrigin = obj[9] as City; 
                        Address addressDestination = obj[10] as Address; 
                        City destinationCity = obj[11] as City;
                        Client ticketClient = obj[12] as Client; 
                        Address ticketClientAddress = obj[13] as Address;
                        City ticketClientAddressCity = obj[14] as City;
                        
                        package.Client = client;
                        package.Client.Address = clientAddress;
                        package.Client.Address.City = ClientAddressCity;
                        package.Hotel = hotel;
                        package.Hotel.Address = hotelAddress;
                        package.Hotel.Address.City = hotelAddressCity;
                        package.Ticket = ticket;
                        package.Ticket.Origin = addressOrigin;
                        package.Ticket.Origin.City = CityOrigin;
                        package.Ticket.Destination = addressDestination;
                        package.Ticket.Destination.City = destinationCity;
                        package.Ticket.Client = ticketClient;
                        package.Ticket.Client.Address = ticketClientAddress;
                        package.Ticket.Client.Address.City = ticketClientAddressCity;
                        return package;
                    }),
                    splitOn: "Id, Id, Id, Id, Id, Id, Id, Id, Id, Id, Id, Id, Id, Id").ToList();
                db.Close();
            }
            return results;
        }

        public Package FindById(int id)
        {
            var results = new Package();
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                results = db.Query<Package, Client, Hotel, Ticket, Address, City, Package>(Package.SELECTALL,
                    (package, client, hotel, ticket, address, destinationCity) =>
                    {
                        package.Client = client;
                        package.Hotel = hotel;
                        package.Ticket = ticket;
                        package.Ticket.Destination = address;
                        package.Ticket.Destination.City = destinationCity;
                        return package;
                    },
                    new {id},
                    splitOn: "Id, Id, Id, Id, Id").FirstOrDefault();
                db.Close();
            }
            return results;
        }

        public int Update(Package newData)
        {
            int result = 0;
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                result = db.Execute(Package.UPDATE, newData);
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
                result = db.Execute(Package.DELETE, new { id });
                db.Close();
            }
            return result;
        }
    }
}
