using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismoApp.Models
{
    public class Hotel
    {
        public readonly static string INSERT = "INSERT INTO Hotel (Name, IdAddress, Price) VALUES(@Name, @IdAddress, @Price);SELECT CAST(scope_identity() as INT);";
        public readonly static string SELECTALL =   "SELECT h.Id, h.Name, h.Price, a.Id, a.Street, a.Number, a.Complement, a.Neighborhood, a.PostalCode, c.Id, c.Description FROM Hotel h join Address a on h.IdAddress = a.Id join City c on a.IdCity = c.Id;";
        public readonly static string SELECTID =    "SELECT h.Id, h.Name, h.Price, a.Id, a.Street, a.Number, a.Complement, a.Neighborhood, a.PostalCode, c.Id, c.Description FROM Hotel h join Address a on h.IdAddress = a.Id join City c on a.IdCity = c.Id WHERE h.Id = @Id;";
        public readonly static string SELECTNAME =  "SELECT h.Id, h.Name, h.Price, a.Id, a.Street, a.Number, a.Complement, a.Neighborhood, a.PostalCode, c.Id, c.Description FROM Hotel h join Address a on h.IdAddress = a.Id join City c on a.IdCity = c.Id WHERE h.Name = @Name;";
        public readonly static string UPDATE = "UPDATE Hotel SET Name = @Name, IdAddress = @IdAddress, Price = @Price WHERE Id = @Id;";
        public readonly static string DELETE = "DELETE FROM Hotel WHERE Id = @Id;";
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public DateTime Created { get; set; }
        public Double Price { get; set; }

        public override string ToString()
        {
            return  $"Hotel: {Name}" +
                    $"\nEndenreço: {Address}" +
                    $"Preço da Diária: {Price}\n";
        }
    }
}
