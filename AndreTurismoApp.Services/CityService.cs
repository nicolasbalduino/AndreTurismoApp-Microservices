using System.Xml.Linq;
using AndreTurismoApp.Models;
using AndreTurismoApp.Repositories;

namespace AndreTurismoApp.Services
{
    public class CityService
    {

        private ICityRepository cityRepository;

        public CityService()
        {
            cityRepository = new CityRepository();
        }

        public City Insert(City city)
        {
            return cityRepository.Insert(city);
        }

        public List<City> FindAll()
        {
            return cityRepository.FindAll();
        }

        public City FindByName(string name)
        {
            return cityRepository.FindByName(name);
        }

        public City FindById(int id)
        {
            return cityRepository.FindById(id);
        }

        public int UpdateCity(string name, string newName)
        {
            return cityRepository.Update(name, newName);
        }

        public int Delete(int id)
        {
            return cityRepository.Delete(id);
        }
    }
}
