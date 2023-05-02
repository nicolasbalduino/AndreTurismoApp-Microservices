using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using AndreTurismoApp.Models;

namespace   AndreTurismoApp.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private string Conn { get; set; }
        public TicketRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["StringConnection"].ConnectionString;
        }

        public int Insert(Ticket ticket)
        {
            int result = 0;
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                
                IAddressRepository originAddressRepository = new AddressRepository();
                ticket.Origin = originAddressRepository.Insert(ticket.Origin);

                IAddressRepository destinationAddressRepository = new AddressRepository();
                ticket.Destination = destinationAddressRepository.Insert(ticket.Destination);

                IClientRepository clientRepository = new ClientRepository();
                ticket.Client.Id = clientRepository.Insert(ticket.Client);

                result = (int)db.ExecuteScalar(Ticket.INSERT, new
                {
                    ticket.Checkin,
                    ticket.Price,
                    Origin = ticket.Origin.Id,
                    Destination = ticket.Destination.Id,
                    ClientId = ticket.Client.Id
                });
                db.Close();
            }
            return result;
        }

        public List<Ticket> FindAll()
        {
            var results = new List<Ticket>();
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                results = db.Query<Ticket, Address, City, Address, City, Client, Ticket>(Ticket.SELECTALL,
                    (ticket, origin, cityOrigin, destination, cityDestination,client) => 
                    {
                        
                        ticket.Origin = origin;
                        ticket.Origin.City = cityOrigin;
                        ticket.Destination = destination;
                        ticket.Destination.City = cityDestination;
                        ticket.Client = client; 
                        return ticket; 
                    },
                    splitOn: "Id, Id, Id, Id, Id").ToList();
                db.Close();
            }
            return results;
        }

        public Ticket FindById(int id)
        {
            var results = new Ticket();
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                results = db.Query<Ticket, Address, City, Address, City, Client, Ticket>(Ticket.SELECTID,
                    (ticket, origin, cityOrigin, destination, cityDestination, client) =>
                    {

                        ticket.Origin = origin;
                        ticket.Origin.City = cityOrigin;
                        ticket.Destination = destination;
                        ticket.Destination.City = cityDestination;
                        ticket.Client = client;
                        return ticket;
                    },
                    new {id},
                    splitOn: "Id, Id, Id, Id, Id").FirstOrDefault();
                db.Close();
            }
            return results;
        }

        public int Update(Ticket newData)
        {
            int result = 0;
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                result = db.Execute(Ticket.UPDATE, newData);
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
                result = db.Execute(Ticket.DELETE, new { id });
                db.Close();
            }
            return result;
        }
    }
}
