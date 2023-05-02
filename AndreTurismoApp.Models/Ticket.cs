using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismoApp.Models
{
    public class Ticket
    {
        public readonly static string INSERT = "INSERT INTO Ticket (Origin, Destination, ClientId, Checkin, Price) VALUES(@Origin, @Destination, @ClientId, @Checkin, @Price);SELECT CAST(scope_identity() as INT);";
        public readonly static string SELECTALL =   "SELECT t.Id, t.Checkin, t.Price, ao.Id, ao.Street, ao.Number, ao.Complement, ao.Neighborhood, ao.PostalCode, co.Id, co.Description, ad.Id, ad.Street, ad.Number, ad.Complement, ad.Neighborhood, ad.PostalCode, cd.Id, cd.Description, cl.Id, cl.Name FROM Ticket t JOIN Client cl ON t.ClientId = cl.Id JOIN Address ao ON t.Origin = ao.Id JOIN City co ON ao.IdCity = co.Id JOIN Address ad ON t.Destination = ad.Id JOIN City cd ON ad.IdCity = cd.Id;";
        public readonly static string SELECTID =    "SELECT t.Id, t.Checkin, t.Price, ao.Id, ao.Street, ao.Number, ao.Complement, ao.Neighborhood, ao.PostalCode, co.Id, co.Description, ad.Id, ad.Street, ad.Number, ad.Complement, ad.Neighborhood, ad.PostalCode, cd.Id, cd.Description, cl.Id, cl.Name FROM Ticket t JOIN Client cl ON t.ClientId = cl.Id JOIN Address ao ON t.Origin = ao.Id JOIN City co ON ao.IdCity = co.Id JOIN Address ad ON t.Destination = ad.Id JOIN City cd ON ad.IdCity = cd.Id WHERE t.Id = @Id;";
        public readonly static string UPDATE = "UPDATE Ticket SET Price = @Price, Checkin = @Checkin WHERE Id = @Id;";
        public readonly static string DELETE = "DELETE FROM Ticket WHERE Id = @Id;";
        public int Id { get; set; }
        public Address Origin { get; set; }
        public Address Destination { get; set; }
        public Client Client { get; set; }
        public DateTime Checkin { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return  $"ID: {Id}" +
                    $"\nData de Check-In: {Checkin}" +
                    $"\nSaida: {Origin.City.Description}" +
                    $"\nChegada: {Destination.City.Description}" +
                    $"\nPassageiro: {Client.Name}" +
                    $"\nPreço da Passagem: {Price}\n";
        }
    }
}
