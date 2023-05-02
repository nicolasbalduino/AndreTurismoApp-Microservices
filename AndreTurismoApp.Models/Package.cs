using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismoApp.Models
{
    public class Package
    {
        public readonly static string INSERT = "INSERT INTO Package (HotelId, TicketId, Price, ClientId) VALUES(@HotelId, @TicketId, @Price, @ClientId);SELECT CAST(scope_identity() as INT);";
        
        public readonly static string SELECTALL = @"SELECT p.Id, p.Price, 
	                                                c.Id, c.Name, pca.Id, pca.Street, pcac.Id, pcac.Description, 
	                                                h.Id, h.Name, ha.Id, ha.Street, hc.Id, hc.Description, 
	                                                t.Id, ao.Id, ao.Street, co.Id, co.Description, 
	                                                ad.Id, ad.Street, cd.Id, cd.Description, 
	                                                tc.Id, tc.Name, tca.Id, tca.Street, tac.Id, tac.Description  
                                                FROM Package p, 
	                                                Hotel h, Address ha, City hc, 
	                                                Ticket t, Address ao, City co, Address ad, City cd, 
	                                                Client tc, Address tca, City tac, 
	                                                Client c, Address pca, City pcac 
                                                WHERE (p.HotelId = h.Id) and 
	                                                (h.IdAddress = ha.Id) and (ha.IdCity = hc.Id) and 
	                                                (p.TicketId = t.Id) and (t.Origin = ao.Id) and (ao.IdCity = co.Id) and 
	                                                (t.Destination = ad.Id) and (ad.IdCity = cd.Id) and 
	                                                (t.ClientId = tc.Id) and (tc.AddressId = tca.Id) and (tca.IdCity = tac.Id) and
	                                                (p.ClientId = c.Id) and (c.AddressId = pca.Id) and (tca.IdCity = pcac.Id);";
        
        public readonly static string SELECTID =  "SELECT p.Id, p.Price, c.Id, c.Name, h.Id, h.Name, t.Id, d.Id, cd.Id, cd.Description FROM Package p, Hotel h, Ticket t, Client c, Address d, City cd WHERE (p.HotelId = h.Id) and (p.TicketId = t.Id) and (p.ClientId = c.Id) and (t.Destination = d.Id) and (d.IdCity = cd.Id) AND p.Id = @Id;";
        public readonly static string UPDATE = "UPDATE Package SET Price = @Price WHERE Id = @Id;";
        public readonly static string DELETE = "DELETE FROM Package WHERE Id = @Id;";
        public int Id { get; set; }
        public Hotel Hotel { get; set; }
        public Ticket Ticket { get; set; }
        public double Price { get; set; }
        public Client Client { get; set; }
        public DateTime Created { get; set; }

        public override string ToString()
        {
            return  $"Pacote: {Id}" +
                    $"\nHotel: {Hotel.Name}" +
                    $"\nDestino: {Ticket.Destination.City}" +
                    $"\nPreço: {Price}" +
                    $"\nCliente: {Client.Name}\n";
        }
    }
}
