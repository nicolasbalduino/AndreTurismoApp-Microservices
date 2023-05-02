using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Repositories
{
    public interface IHotelRepository
    {
        int Insert(Hotel hotel);

        List<Hotel> FindAll();

        Hotel FindById(int id);

        Hotel FindByName(string name);

        int Update(int id, Hotel newData);

        int Delete(int id);
    }
}
