using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismoApp.Models
{
    public class Client
    {
        public readonly static string INSERT = "INSERT INTO Client (Name, Phone, AddressId) VALUES(@Name, @Phone, @IdAddress);SELECT CAST(scope_identity() as INT);";
        public readonly static string SELECTALL =   "SELECT c.Id, c.Name, c.Phone, a.Id, a.Street, a.Number, a.Complement, a.Neighborhood, a.PostalCode, cl.Id, cl.Description FROM Client c LEFT JOIN Address a on c.AddressId = a.Id LEFT JOIN City cl on a.IdCity = cl.Id;";
        public readonly static string SELECTID =    "SELECT c.Id, c.Name, c.Phone, a.Id, a.Street, a.Number, a.Complement, a.Neighborhood, a.PostalCode, cl.Id, cl.Description FROM Client c LEFT JOIN Address a on c.AddressId = a.Id LEFT JOIN City cl on a.IdCity = cl.Id WHERE c.Id = @Id;";
        public readonly static string SELECTNAME =  "SELECT c.Id, c.Name, c.Phone, a.Id, a.Street, a.Number, a.Complement, a.Neighborhood, a.PostalCode, cl.Id, cl.Description FROM Client c LEFT JOIN Address a on c.AddressId = a.Id LEFT JOIN City cl on a.IdCity = cl.Id WHERE c.Name = @Name;";
        public readonly static string UPDATE = "UPDATE Client SET Name = @Name, Phone = @Phone WHERE Id = @Id;";
        public readonly static string DELETE = "DELETE FROM Client WHERE Id = @Id;";
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public DateTime Created { get; set; }

        public override string ToString()
        {
            if (Name == null && Phone == null && Address == null)
                return "Cliente não encontrado";
            
            return  $"Nome: {Name}" +
                    $"\nTelefone: {Phone}" +
                    $"\nEndereço: {Address}\n";
        }
    }
}
