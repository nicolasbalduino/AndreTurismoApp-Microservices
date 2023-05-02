using System.Configuration;
using System.Data.SqlClient;
using System.Xml.Linq;
using Dapper;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Repositories
{
    public class CityRepository : ICityRepository
    {
        private string Conn { get; set; }

        public CityRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["StringConnection"].ConnectionString;
        }
        public City Insert(City city)
        {
            using(var db = new SqlConnection(Conn))
            {
                db.Open();
                city.Id = (int)db.ExecuteScalar(City.INSERT, city);
                db.Close();
            }
            return city;
        }

        public List<City> FindAll()
        {
            var result = new List<City>();
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                result = db.Query<City>(City.SELECTALL).ToList();
                db.Close();
            }
            return result;
        }

        public City FindById(int Id)
        {
            var result = new City();
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                result = db.QueryFirstOrDefault<City>(City.SELECTID, new { Id });
                db.Close();
            }
            return result;
        }

        public City FindByName(string Name)
        {
            var result = new City();
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                result = db.QueryFirstOrDefault<City>(City.SELECTNAME, new { Name });
                db.Close();
            }
            return result;
        }

        public int Update(string Name, string NewName)
        {
            var result = 0;
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                result = db.Execute(City.UPDATE, new { Name, NewName });
                db.Close();
            }
            return result;
        }

        public int Delete(int Id)
        {
            var result = 0;
            using (var db = new SqlConnection(Conn))
            {
                db.Open();
                result = db.Execute(City.DELETE, new { Id });
                db.Close();
            }
            return result;
        }
    }
}
