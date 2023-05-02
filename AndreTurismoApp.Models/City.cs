using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismoApp.Models
{
    public class City
    {
        public readonly static string INSERT = "INSERT INTO City (Description) VALUES(@Description);SELECT CAST(scope_identity() as INT);";
        public readonly static string SELECTALL = "SELECT c.Id, c.Description FROM City c";
        public readonly static string SELECTNAME = "SELECT c.Id, c.Description FROM City c WHERE c.Description = @Name;";
        public readonly static string SELECTID = "SELECT c.Id, c.Description FROM City c WHERE c.Id = @Id;";
        public readonly static string UPDATE = "UPDATE City SET Description = @NewName WHERE Description = @Name;";
        public readonly static string DELETE = "DELETE FROM City WHERE Id = @Id;";

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DtCreated { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}
