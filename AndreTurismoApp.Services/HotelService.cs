using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AndreTurismoApp.Models;
using AndreTurismoApp.Repositories;

namespace AndreTurismoApp.Services
{
    public class HotelService
    {
        IHotelRepository hotelRepository;

        public HotelService()
        {
            hotelRepository = new HotelRepository();
        }

        public int Insert(Hotel hotel)
        {
            return hotelRepository.Insert(hotel);
        }

        public List<Hotel> FindAll()
        {
            return hotelRepository.FindAll();
        }

        public Hotel FindById(int id)
        {
            return hotelRepository.FindById(id);
        }

        public Hotel FindByName(string name)
        {
            return hotelRepository.FindByName(name);
        }

        public int Update(int id, Hotel newData)
        {
            return hotelRepository.Update(id, newData);
        }

        public int Delete(int id)
        {
            return hotelRepository.Delete(id);
        }
    }
}
