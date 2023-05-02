using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models.DTO;

namespace AndreTurismoApp.Models
{
    public class Address
    {
        public readonly static string INSERT = "INSERT INTO Address (Street, Number, Neighborhood, PostalCode, Complement, IdCity) VALUES(@Street, @Number, @Neighborhood, @PostalCode, @Complement, @IdCity);SELECT CAST(scope_identity() as INT);";
        public readonly static string SELECTALL =   "SELECT a.Id, a.Street, a.Number, a.Complement, a.Neighborhood, a.PostalCode, c.Id, c.Description FROM Address a JOIN City c on a.IdCity = c.Id;";
        public readonly static string SELECTID =    "SELECT a.Id, a.Street, a.Number, a.Complement, a.Neighborhood, a.PostalCode, c.Id, c.Description FROM Address a JOIN City c on a.IdCity = c.Id WHERE a.Id = @Id;";
        public readonly static string UPDATE = "UPDATE Address SET Street = @Street, Number = @Number, Neighborhood = @Neighborhood, Complement = @Complement, PostalCode = @PostalCode, IdCity = @IdCity WHERE Id = @Id;";
        public readonly static string DELETE = "DELETE FROM Address WHERE Id = @Id;";

        public int Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Neighborhood { get; set; }
        public string PostalCode { get; set; }
        public string Complement { get; set; }
        public City City { get; set; }
        public DateTime Created { get; set; }

        public Address() { }

        public Address(AddressDTO addressDTO)
        {
            PostalCode = addressDTO.CEP;
            this.City = new();
            City.Description = addressDTO.City;
        }

        public override string ToString()
        {
            return $"{Street}, Nº {Number} {Complement}, {Neighborhood}, {City}, {PostalCode}\n";
        }
    }
}
